using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels
{
    public class IncomeTypeRowItem : BaseViewModel
    {
        public IncomeTypeRowItem(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public const int PLANT = 2;
        public const int SEED = 1;

        public string Title
        {
            get
            {
                switch (Value)
                {
                    case PLANT:
                        return "Plant";
                    case SEED:
                        return "Seed";
                }
                return string.Empty;
            }
        }

        #region EQUALITY

        public override bool Equals(object obj)
        {
            var other = obj as IncomeTypeRowItem;
            if (other == null) return false;
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        #endregion

        public override string ToString()
        {
            return Title;
        }
    }
}
