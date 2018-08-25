using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Test.Framework
{
    [TestClass]
    public abstract class ViewModelTestBase<TSut, TProvider>
        where TProvider : class
    {
        public ViewModelTestBase()
        {
            dataProvider = new Mock<TProvider>();
            navigationService = new Mock<INavigationService>();
            windowController = new Mock<IWindowController>();
            dialogService = new Mock<IDialogService>();
        }

        protected TSut viewModel;
        protected readonly Mock<TProvider> dataProvider;
        protected readonly Mock<INavigationService> navigationService;
        protected readonly Mock<IWindowController> windowController;
        protected readonly Mock<IDialogService> dialogService;

        [TestInitialize]
        public void SetUp()
        {
            var ctor = typeof(TSut).GetConstructors()[0];
            var parameters = new List<object>();
            foreach (var p in ctor.GetParameters())
            {
                if (p.ParameterType == typeof(TProvider) || p.ParameterType == typeof(IDataEntryViewProvider))
                    parameters.Add(dataProvider.Object);
                else if (p.ParameterType == typeof(IDialogService))
                    parameters.Add(dialogService.Object);
                else if (p.ParameterType == typeof(INavigationService))
                    parameters.Add(navigationService.Object);
                else if (p.ParameterType == typeof(IWindowController))
                    parameters.Add(windowController.Object);
                else if (p.ParameterType == typeof(EventAggregator))
                    parameters.Add(new EventAggregator());
            }

            viewModel = (TSut)Activator.CreateInstance(typeof(TSut), parameters.ToArray());
        }

        protected Result<TResult> The<TResult>(Expression<Func<TProvider, IEnumerable<TResult>>> exp)
               where TResult : new()
        {
            return new Result<TResult>(dataProvider.Setup(exp));
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

        private void RunCommand(string command)
        {
            ((ICommand)viewModel.GetType().GetProperty(command).GetValue(viewModel)).Execute(null);

        }

        public class Result<T>
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

            private ISetup<TProvider, IEnumerable<T>> _generalSetup;
            private ISetup<TProvider, IEnumerable<TransferObjectBase>> _dtoSetup;

            public void ReturnsCollection()
            {
                _generalSetup.Returns(Enumerable.Repeat(new T(), 2));

            }

            public void ReturnsCollection<TDto>()
                where TDto : TransferObjectBase, new()
            {
                _dtoSetup.Returns(Enumerable.Repeat(new TDto(), 2));
            }

            public void ReturnsEmptyCollection()
            {
                if (_generalSetup != null)
                    _generalSetup.Returns(Enumerable.Empty<T>());
                if (_dtoSetup != null)
                    _dtoSetup.Returns(Enumerable.Empty<TestDto>());
            }
        }
    }

    public enum User
    {
        Accepted,
        NotAccepted
    }
}
