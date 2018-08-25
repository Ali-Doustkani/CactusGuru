namespace CactusGuru.Presentation.ViewModel.Utils
{
    public static class TypeConvertor
    {
        public static int ToInt(int? value)
        {
            if (value.HasValue) return value.Value;
            return 0;
        }
    }
}
