using System;
using System.Reflection;

namespace CactusGuru.Infrastructure.Qualification
{
    public class EmptySpecification : IEmptySpecification
    {
        public EmptySpecification(IDomainDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        private string _propertyName;
        private readonly IDomainDictionary _dictionary;

        public ISpecification SetProperty(string propertyName)
        {
            _propertyName = propertyName;
            return this;
        }

        public Error Satisfy(DomainEntity domainEntity)
        {
            var property = domainEntity.GetType().GetProperty(_propertyName);
            if (property.PropertyType == typeof(string))
            {
                if (StringCheck(domainEntity, property))
                    return CreateError();
                else
                    return Error.Empty;
            }
            if (DomainEntityCheck(domainEntity, property))
                return CreateError();
            return Error.Empty;
        }

        private bool StringCheck(DomainEntity domainEntity, PropertyInfo property)
        {
            var value = (string)property.GetValue(domainEntity);
            return string.IsNullOrWhiteSpace(value);
        }

        private bool DomainEntityCheck(DomainEntity domainEntity, PropertyInfo property)
        {
            var value = (DomainEntity)property.GetValue(domainEntity);
            return value.Id == Guid.Empty;
        }

        private Error CreateError()
        {
            var message = $"وارد کردن {_dictionary.Translate(_propertyName)} اجباری است.";
            return new Error(message);
        }
    }
}
