namespace CactusGuru.Application.Common
{
    public class TaxonDto : TransferObjectBase
    {
        public GenusDto Genus { get; set; }
        public string Species { get; set; }
        public string Variety { get; set; }
        public string SubSpecies { get; set; }
        public string Forma { get; set; }
        public string Cultivar { get; set; }

        /// <summary>
        /// Complete Formatted Name
        /// </summary>
        public string Name { get; set; }
    }
}
