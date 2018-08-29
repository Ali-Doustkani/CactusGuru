namespace CactusGuru.Presentation.ViewModel.Framework
{
    public class LoaderState : BaseViewModel
    {
        private enum State
        {
            Busy = 1,
            Idle = 2
        }

        public LoaderState()
        {
            Set(State.Busy);
        }

        private State _state;

        public bool IsBusy => _state == State.Busy;
        public bool IsIdle => _state == State.Idle;

        public void ToIdle() => Set(State.Idle);

        private void Set(State state)
        {
            if (state == _state) return;
            _state = state;
            OnPropertyChanged(nameof(IsBusy));
            OnPropertyChanged(nameof(IsIdle));
        }
    }
}
