﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private Func<Task<IEnumerable<T>>> _asyncSourceFunc;
        private IEnumerable<T> _source;
        private List<Type> _acceptedTypes;
        private Func<object, T> _convertorFunc;
        private Func<T, string, bool> _filterFunc;

        public Builder<T> WithId<TId>(Func<T, TId> selectId)
        {
            _selectId = x => selectId(x);
            return this;
        }

        public Builder<T> Loads(Func<IEnumerable<T>> sourceFunc)
        {
            _sourceFunc = sourceFunc;
            return this;
        }

        public Builder<T> LoadFrom<TOtherType>(Func<IEnumerable<TOtherType>> sourceFunc)
        {
            _sourceFunc = () =>
            {
                var list = new List<T>();
                foreach (var item in sourceFunc())
                    list.Add(_convertorFunc(item));
                return list;
            };
            return this;
        }

        public Builder<T> LoadFromAsync<TOtherType>(Func<Task<IEnumerable<TOtherType>>> sourceFunc)
        {
            _asyncSourceFunc = async () =>
            {
                var list = new List<T>();
                var sources = await sourceFunc();
                foreach (var item in sources)
                    list.Add(_convertorFunc(item));
                return list;
            };
            return this;
        }

        public Builder<T> WithSource(IEnumerable<T> source)
        {
            _source = source;
            return this;
        }

        public Builder<T> WithConvertor<TOtherType>(Func<TOtherType, T> convertorFunc)
        {
            _acceptedTypes.Add(typeof(TOtherType));
            _convertorFunc = (other) => convertorFunc((TOtherType)other);
            return this;
        }

        public Builder<T> FilterBy(Func<T, string, bool> func)
        {
            _filterFunc = func;
            return this;
        }

        public ObservableBag<T> Build()
        {
            if (_sourceFunc != null)
                return CreateCollection(_sourceFunc());
            return CreateCollection(_source);
        }

        public async Task<ObservableBag<T>> BuildAsync()
        {
            return CreateCollection(await _asyncSourceFunc());
        }

        private ObservableBag<T> CreateCollection(IEnumerable<T> source)
        {
            if (source == null)
                source = Enumerable.Empty<T>();
            if (!_acceptedTypes.Any())
                _acceptedTypes.Add(typeof(T));

            var convertor = _convertorFunc;
            if (_convertorFunc == null)
                convertor = DefaultConvertor;

            return new ObservableBag<T>(source, _selectId, Acceptor, convertor, _filterFunc);
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