namespace CactusGuru.Presentation.ViewModel.NavigationService
{
    public class DialogResult<T>
    {
        public DialogResult(bool result, T value)
        {
            Result = result;
            Value = value;
        }

        public bool Result { get; private set; }

        public T Value { get; private set; }
    }
}
