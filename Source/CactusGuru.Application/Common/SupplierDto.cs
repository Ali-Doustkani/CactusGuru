namespace CactusGuru.Application.Common
{
    public class SupplierDto : TransferObjectBase
    {
        /// <summary>
        /// Natalia Shelkonova
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// NS
        /// </summary>
        public string Acronym { get; set; }

        /// <summary>
        /// Natalia Shelkonova (NS)
        /// </summary>
        public string FormattedName { get; set; }

        /// <summary>
        /// kaktus.ru
        /// </summary>
        public string Website { get; set; }
    }
}
