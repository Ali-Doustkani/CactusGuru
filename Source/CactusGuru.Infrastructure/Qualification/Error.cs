using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Infrastructure.Qualification
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }

        public static void ThrowExceptionIf(IEnumerable<Error> errors)
        {
            if (errors.Any())
                throw new ErrorHappenedException(errors);
        }

        public static string CreateErrorMessage(IEnumerable<Error> errors)
        {
            return errors.Select(x => x.Message).Aggregate((i, j) => i + Environment.NewLine + j);
        }

        private static Error _empty;
        public static Error Empty
        {
            get
            {
                if (_empty == null)
                    _empty = new Error(string.Empty);
                return _empty;
            }
        }
    }
}
