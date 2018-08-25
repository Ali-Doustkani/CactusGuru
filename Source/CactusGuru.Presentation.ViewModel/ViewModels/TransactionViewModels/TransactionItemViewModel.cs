using DevExpress.XtraEditors.DXErrorProvider;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TransactionViewModels
{
    public class TransactionItemViewModel : IDXDataErrorInfo
    {
        public int RowNumber { get; set; }

        public CollectionItemViewModel CollectionItem { get; set; }

        public PartTypeViewModel PartType { get; set; }

        public int Amount { get; set; }


        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            switch (propertyName)
            {
                case nameof(Amount):
                    CheckAmount(info);
                    break;
                case nameof(CollectionItem):
                    CheckCollectionItem(info);
                    break;
            }
        }

        public void GetError(ErrorInfo info)
        {
            CheckAmount(info);
            CheckCollectionItem(info);
        }

        private void CheckAmount(ErrorInfo info)
        {
            if (Amount > 0) return;
            info.ErrorText = "تعداد باید بیشتر از صفر باشد";
            info.ErrorType = ErrorType.Critical;
        }

        private void CheckCollectionItem(ErrorInfo info)
        {
            if (CollectionItem != null) return;
            info.ErrorText = "انتخاب گونه اجباری است";
            info.ErrorType = ErrorType.Critical;
        }
    }
}
