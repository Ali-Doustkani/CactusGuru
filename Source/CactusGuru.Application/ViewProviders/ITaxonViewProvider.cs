﻿using System.Collections.Generic;
using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders
{
    public interface ITaxonViewProvider : IDataEntryViewProvider
    {
        IEnumerable<GenusDto> GetGenera();
    }
}
