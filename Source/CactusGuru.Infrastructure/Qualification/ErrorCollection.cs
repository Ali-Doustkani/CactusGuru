using System.Collections;
using System.Collections.Generic;

namespace CactusGuru.Infrastructure.Qualification
{
    public class ErrorCollection : IEnumerable<Error>
    {
        public ErrorCollection()
        {
            _errors = new List<Error>();
        }

        private readonly List<Error> _errors;

        public IEnumerator<Error> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Error error)
        {
            if (error != Error.Empty)
                _errors.Add(error);
        }

        public void Add(string errorMessage)
        {
            _errors.Add(new Error(errorMessage));
        }
    }
}
