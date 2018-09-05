using System;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public interface IRuleSet
    {
        IRuleSet IsNotEmpty();
        IRuleSet ValidatesForWhole(Func<string> action);
    }
}
