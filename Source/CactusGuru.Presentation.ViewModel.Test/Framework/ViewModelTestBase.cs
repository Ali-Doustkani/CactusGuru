using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.NavigationService;
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
        protected readonly Mock<TProvider> dataProvider;
        protected readonly Mock<INavigationService> navigationService;
        protected readonly Mock<IDialogService> dialogService;

        [TestInitialize]
        public void SetUp()
        {
            viewModel = Make();
        }

        protected abstract TEditor Make();

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
