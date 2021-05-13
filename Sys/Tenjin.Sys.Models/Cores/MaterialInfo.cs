namespace Tenjin.Sys.Models.Cores
{
    public class MaterialInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string DefCode { get; set; }

        public long Quantity { get; set; }

        public string SortName { get; set; }

        public string MaterialGroupCode { get; set; }
        public string MaterialGroupName { get; set; }

        public string MaterialSubgroupCode { get; set; }
        public string MaterialSubgroupName { get; set; }

        public string MaterialGroupTypeCode { get; set; }
        public string MaterialGroupTypeName { get; set; }

        public string ShortDescription { get; set; }

        public string Producer { get; set; }

        public string UnitCode { get; set; }


        public string SerialNumber { get; set; }

        public string Specification { get; set; }

        public long Price { get; set; }

        public long SalePrice { get; set; }
    }
}
