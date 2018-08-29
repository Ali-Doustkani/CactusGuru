namespace CactusGuru.Presentation.ViewModel.Framework
{
    public class EditorState : BaseViewModel
    {
        private enum EditorViewModelState
        {
            View = 1,
            Add = 2,
            Edit = 3
        }

        public EditorState()
        {
            Set(EditorViewModelState.View);
        }

        private EditorViewModelState _state;

        public bool FirstControlFocused { get; set; }

        public bool DefaultControlFocused { get; set; }

        public bool IsView => _state == EditorViewModelState.View;

        public bool IsAdd => _state == EditorViewModelState.Add;

        public bool IsEdit => _state == EditorViewModelState.Edit;

        public bool IsNotView => _state != EditorViewModelState.View;

        public void ToAdd() => Set(EditorViewModelState.Add);

        public void ToEdit() => Set(EditorViewModelState.Edit);

        public void ToView() => Set(EditorViewModelState.View);

        private void Set(EditorViewModelState value)
        {
            if (_state == value) return;
            _state = value;
            OnPropertyChanged(nameof(IsNotView));
            OnPropertyChanged(nameof(IsView));
            if (IsView)
            {
                DefaultControlFocused = true;
                OnPropertyChanged(nameof(DefaultControlFocused));
            }
            else if (IsAdd || IsEdit)
            {
                FirstControlFocused = true;
                OnPropertyChanged(nameof(FirstControlFocused));
            }
        }
    }
}
