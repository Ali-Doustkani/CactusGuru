using CactusGuru.Infrastructure.ObjectCreation;
using System;

namespace CactusGuru.Infrastructure.Utils
{
    public static class ArgumentChecker
    {
        public static T CheckUp<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
            return obj;
        }

        public static void CheckNull(object argumentCanNotBeNull)
        {
            if (argumentCanNotBeNull == null)
                throw new ArgumentNullException("argumentCanNotBeNull");
        }

        public static void CheckEmpty(DomainEntity domainEntity)
        {
            CheckNull(domainEntity);
            if (domainEntity.Id == Guid.Empty)
                throw new ArgumentException("poco can not be empty.");
        }

        public static void CheckEmpty(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("id can not be empty.");
        }

        public static void CheckEmpty(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentException("string argument can not be empty or null.");
        }

        public static void CheckIs(object arg, Type type)
        {
            if (arg.GetType() != type)
                throw new ArgumentException(string.Format("Expected arg is {0}", type.Name));
        }

        public static FactoryArgumentChecker CheckFactoryArgument(FactoryArg argument)
        {
            return new FactoryArgumentChecker(argument);
        }
    }
}
