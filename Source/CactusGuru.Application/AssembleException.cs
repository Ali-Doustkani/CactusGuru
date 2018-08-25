using System;

namespace CactusGuru.Application
{
    public class AssembleException : Exception
    {
        public AssembleException(string dtoMember)
            : base($"{dtoMember} is required.")
        { }
    }
}
