namespace CactusGuru.Presentation.View.Views
{
    public partial class BaseEditorWindow
    {
        public BaseEditorWindow()
        {
            InitializeComponent();
        }

        public void SetUserControl(IUserControlView view)
        {
            ContentControl.Content = view;
            view.Save += view_Save;
        }

        private void view_Save(object sender, System.EventArgs e)
        {
            btnSave.Command.Execute(null);
        }
    }
}
