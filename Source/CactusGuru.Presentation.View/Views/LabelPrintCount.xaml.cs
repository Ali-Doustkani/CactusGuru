namespace CactusGuru.Presentation.View.Views
{
    public partial class LabelPrintCount  
    {
        public LabelPrintCount()
        {
            InitializeComponent();
            txtCount.Focus();
        }

        public int GetCount()
        {
            int result;
            var canParse = int.TryParse(txtCount.Text, out result);
            if (canParse)
                return result;
            return 1;
        }
    }
}
