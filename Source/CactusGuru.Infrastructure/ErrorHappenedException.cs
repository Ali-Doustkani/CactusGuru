using CactusGuru.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Infrastructure
{
    public class ErrorHappenedException : Exception
    {
        public ErrorHappenedException(string message)
            : base(message) { }

        public ErrorHappenedException(IEnumerable<Error> errors)
            : base(Error.CreateErrorMessage(errors))
        {
            Errors = new List<Error>(errors);
        }

        public IReadOnlyCollection<Error> Errors { get; }
    }
}
