using System;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public interface IRuleSet
    {
        IRuleSet IsNotEmpty();
        IRuleSet ValidatesForItself(Func<string> action);
        IRuleSet ValidatesForWhole(Func<string> action);
        IRuleSet ValidatesFor(string theOtherProperty, Func<string> action);
    }
}
