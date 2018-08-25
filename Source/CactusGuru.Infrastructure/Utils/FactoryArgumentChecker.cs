using CactusGuru.Infrastructure.ObjectCreation;
using System;

namespace CactusGuru.Infrastructure.Utils
{
    public class FactoryArgumentChecker
    {
        public FactoryArgumentChecker(FactoryArg argument)
        {
            _argument = argument;
        }

        private readonly FactoryArg _argument;

        public T Is<T>()
        {
            if (_argument.GetType() == typeof(T))
                return (T)(object)_argument;
            throw new ArgumentException("FactoryArg object is not valid.");
        }
    }
}
