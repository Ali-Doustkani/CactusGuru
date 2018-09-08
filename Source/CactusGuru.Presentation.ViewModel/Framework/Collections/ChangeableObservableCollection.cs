using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework.Collections
{
    public class Builder<T>
    {
        public Builder()
        {
            _acceptedTypes = new List<Type>();
        }

        private Func<T, object> _selectId;
        private Func<IEnumerable<T>> _sourceFunc;
        private List<Type> _acceptedTypes;
        private Func<object, T> _convertorFunc;

        public Builder<T> WithId<TId>(Func<T, TId> selectId)
        {
            _selectId = x => selectId(x);
            return this;
        }

        public Builder<T> WithSource(Func<IEnumerable<T>> source)
        {
            _sourceFunc = source;
            return this;
        }

        public Builder<T> WithConvertor<TOtherType>(Func<TOtherType, T> convertorFunc)
        {
            _acceptedTypes.Add(typeof(TOtherType));
            _convertorFunc = (other) => convertorFunc((TOtherType)other);
            return this;
        }

        public ObservableBag<T> Build()
        {
            var source = Enumerable.Empty<T>();
            if (_sourceFunc != null)
                source = _sourceFunc();

            if (!_acceptedTypes.Any())
                _acceptedTypes.Add(typeof(T));

            var convertor = _convertorFunc;
            if (_convertorFunc == null)
                convertor = DefaultConvertor;

            return new ObservableBag<T>(source, _selectId, Acceptor, convertor);
        }

        private bool Acceptor(object obj)
        {
            return _acceptedTypes.Contains(obj.GetType());
        }

        private T DefaultConvertor(object obj)
        {
            return (T)obj;
        }
    }
}