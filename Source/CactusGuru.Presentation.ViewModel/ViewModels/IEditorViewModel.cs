using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels
{
    /// <summary>
    /// رفتار تمام ویومدل هایی که دارای نوار ابزار هستند
    /// دارای کنترل های ورودی برای ویرایش یا ثبت موجودیت هستند
    /// و دارای یک لیست جهت درج در آن یا انتخاب یک آیتم برای ویرایش هستند
    /// </summary>
    internal interface IEditorViewModel
    {
        /// <summary>
        /// زمانی رخ می دهد که کاربر روی گزینه ی ثبت در نوار ابزار کلیک کند
        /// </summary>
        event EventHandler BeginAdding;

        /// <summary>
        /// زمانی رخ می دهد که کاربر روی گزینه ی ویرایش در نوار ابزار کلیک کند
        /// </summary>
        event EventHandler BeginEditing;

        /// <summary>
        /// تعیین کننده ی فعال/غیر فعال بودن کنترل های روی صفحه
        /// </summary>
        bool IsEditorOn { get; }

        bool ItemIsSelected();

        void OnPropertyChanged(string propertyName);

        void NotifyAllPropertiesChanged();

        void PrepareForAdd();

        void PrepareForEdit();

        void Add();

        void Edit();

        void CancelEdit();

        void Delete();
    }
}
