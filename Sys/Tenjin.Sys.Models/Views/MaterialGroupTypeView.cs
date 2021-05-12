using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    public class MaterialGroupTypeView : MaterialGroupType
    {
        public string FullCode
        {
            get
            {
                return $"{MaterialGroup?.DefCode}-{MaterialSubgroup?.DefCode}-{DefCode}";
            }
        }

        public MaterialGroup MaterialGroup { get; set; }
        public MaterialSubgroup MaterialSubgroup { get; set; }
    }
}
