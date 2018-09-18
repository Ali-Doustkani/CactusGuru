using CactusGuru.Application.Common;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Services.Navigations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Test.Framework
{
    [TestClass]
    public abstract class ViewModelTestBase<TEditor, TProvider>
        where TProvider : class
    {
        public ViewModelTestBase()
        {
            dataProvider = new Mock<TProvider>();
            navigationService = new Mock<INavigationService>();
            dialogService = new Mock<IDialogService>();
        }

        protected TEditor viewModel;
        private ResultBase _result;
        protected readonly Mock<TProvider> dataProvider;
        protected readonly Mock<INavigationService> navigationService;
        protected readonly Mock<IDialogService> dialogService;

        [TestInitialize]
        public void SetUp()
        {
            _result = null;
            viewModel = Make();
            viewModel.GetType().GetProperty(nameof(FormViewModel.Dialog)).SetValue(viewModel, dialogService.Object);
            viewModel.GetType().GetProperty(nameof(FormViewModel.Navigations)).SetValue(viewModel, navigationService.Object);
            viewModel.GetType().GetProperty(nameof(FormViewModel.EventAggregator)).SetValue(viewModel, new EventAggregator());
        }

        protected abstract TEditor Make();

        protected Result<TResult> The<TResult>(Expression<Func<TProvider, IEnumerable<TResult>>> exp)
               where TResult : new()
        {
            return new Result<TResult>(dataProvider.Setup(exp));
        }

        protected Result<TResult> The<TResult>(Expression<Func<TProvider, Task<IEnumerable<TResult>>>> exp)
          where TResult : new()
        {
            var result = new Result<TResult>(dataProvider.Setup(exp));
            _result = result;
            return result;
        }

        protected Result<TResult> The<TResult>(Expression<Func<TProvider, Task<TResult>>> exp)
         where TResult : new()
        {
            var result = new Result<TResult>(dataProvider.Setup(exp));
            _result = result;
            return result;
        }

        protected Result<TestDto> The(Expression<Func<TProvider, IEnumerable<TransferObjectBase>>> exp)
        {
            return new Result<TestDto>(dataProvider.Setup(exp));
        }

        protected void MustNavigatedTo(Expression<Action<INavigationService>> expression)
        {
            navigationService.Verify(expression, Times.Once());
        }

        protected void Load()
        {
            RunCommand("LoadCommand");
        }

        protected void PrepareForAdd()
        {
            RunCommand("PrepareForAddCommand");
        }

        protected void PrepareForEdit()
        {
            RunCommand("PrepareForEditCommand");
        }

        protected void Cancel(User userAnswer)
        {
            dialogService.Setup(x => x.Ask(It.IsAny<string>())).Returns(userAnswer == User.Accepted);
            RunCommand("CancelCommand");
        }

        protected void Delete(User userAnswer)
        {
            dialogService.Setup(x => x.Ask(It.IsAny<string>())).Returns(userAnswer == User.Accepted);
            RunCommand("DeleteCommand");
        }

        protected Asserter<T> MakeSure<T>(Expression<Func<TEditor, T>> exp)
        {
            Func<T> valueRetreiverFunc = () => exp.Compile()(viewModel);
            return new Asserter<T>(valueRetreiverFunc, _result);
        }

        private void RunCommand(string command)
        {
            ((ICommand)viewModel.GetType().GetProperty(command).GetValue(viewModel)).Execute(null);

        }

        public class Asserter<T>
        {
            public Asserter(Func<T> actualFunc, ResultBase result)
            {
                _actualFunc = actualFunc;
                _result = result;
            }

            private readonly Func<T> _actualFunc;
            private readonly ResultBase _result;

            public void Is<TVal>(TVal value)
            {
                _result?.Task?.Wait();
                Assert.AreEqual(value, _actualFunc());
            }
        }

        public abstract class ResultBase
        {
            public Task Task { get; protected set; }
        }

        public class Result<T> : ResultBase
          where T : new()
        {
            public Result(ISetup<TProvider, IEnumerable<T>> setup)
            {
                _generalSetup = setup;
            }

            public Result(ISetup<TProvider, IEnumerable<TransferObjectBase>> setup)
            {
                _dtoSetup = setup;
            }

            public Result(ISetup<TProvider, Task<IEnumerable<T>>> setup)
            {
                _collectionTaskSetup = setup;
            }

            public Result(ISetup<TProvider, Task<T>> setup)
            {
                _taskSetup = setup;
            }

            private ISetup<TProvider, IEnumerable<T>> _generalSetup;
            private ISetup<TProvider, Task<IEnumerable<T>>> _collectionTaskSetup;
            private ISetup<TProvider, Task<T>> _taskSetup;
            private ISetup<TProvider, IEnumerable<TransferObjectBase>> _dtoSetup;

            public void ReturnsCollection()
            {
                if (_generalSetup != null)
                    _generalSetup.Returns(Enumerable.Repeat(new T(), 2));
                else if (_collectionTaskSetup != null)
                {
                    Task = Task.Factory.StartNew(() =>
                    {
                        return Enumerable.Repeat(new T(), 2);
                    });
                    _collectionTaskSetup.Returns((Task<IEnumerable<T>>)Task);
                }
            }

            public void ReturnsEmptyCollection()
            {
                if (_generalSetup != null)
                    _generalSetup.Returns(Enumerable.Empty<T>());
                if (_dtoSetup != null)
                    _dtoSetup.Returns(Enumerable.Empty<TestDto>());
            }

            public void Returns(T result)
            {
                Task = Task.Factory.StartNew(() =>
                {
                    return result;
                });
                _taskSetup.ReturnsAsync(result);
            }
        }
    }

    public enum User
    {
        Accepted,
        NotAccepted
    }
}
