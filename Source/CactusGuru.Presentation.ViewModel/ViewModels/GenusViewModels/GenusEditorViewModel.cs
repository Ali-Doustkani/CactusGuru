﻿using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;

namespace CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels
{
    public class GenusEditorViewModel : SimpleEditorViewModel<GenusViewModel>
    {
        public GenusEditorViewModel(IGenusViewProvider dataProvider)
            : base(dataProvider)
        {
            _dataProvider = dataProvider;
        }

        private readonly IGenusViewProvider _dataProvider;

        protected override void OnLoad()
        {
            base.OnLoad();
            Rules.MakeSure(nameof(FormattedName)).IsNotEmpty().ValidatesForWhole(Similarity);
        }

        protected override bool Filter(GenusViewModel vm, string text) => vm.FormattedName.Has(text);

        public string FormattedName
        {
            get { return WorkingItem?.FormattedName; }
            set
            {
                WorkingItem.FormattedName = value;
                Rules.Check(nameof(FormattedName), value);
            }
        }

        private string Similarity()
        {
            if (_dataProvider.HasSimilar((GenusDto)WorkingItem.InnerObject))
                return "This genus already exists";
            return null;
        }
    }
}
