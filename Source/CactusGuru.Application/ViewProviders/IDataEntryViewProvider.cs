using System.Collections.Generic;
using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders
{
    /// <summary>
    /// Standard view provider for forms that give data in top of them and
    /// add the data to the list below.
    /// </summary>
    public interface IDataEntryViewProvider
    {
        /// <summary>
        /// gets all the entered data before
        /// </summary>
        IEnumerable<TransferObjectBase> GetList();

        /// <summary>
        /// create a new object with the default data
        /// </summary>
        TransferObjectBase Build();

        /// <summary>
        /// clones dto
        /// </summary>
        TransferObjectBase Copy(TransferObjectBase dto);

        /// <summary>
        /// copies source data to destination
        /// </summary>
        void CopyTo(TransferObjectBase source, TransferObjectBase destination);

        /// <summary>
        /// adds dto to storage, and returns it again in case if anything has been changed after adding.
        /// </summary>
        /// <exception cref="ErrorHappenedException"/>
        TransferObjectBase Add(TransferObjectBase dto);

        /// <summary>
        /// updates dto to storage, and returns it again in case if anything has been changed after updating.
        /// </summary>
        /// <exception cref="ErrorHappenedException"/>
        TransferObjectBase Update(TransferObjectBase dto);

        /// <summary>
        /// deletes the dto from storage.
        /// </summary>
        /// <exception cref="ErrorHappenedException"/>
        void Delete(TransferObjectBase dto);
    }
}
