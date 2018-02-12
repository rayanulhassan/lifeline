using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lifeline.BOL
{
    public partial class Style_Accessories
    {
        [Key]
        public int styleAccessoriesId { set; get; }
        public string name { set; get; }
        public string picture { set; get; }       
        public string type { set; get; }
        public string category { set; get; }
        public string subCategory { set; get; }

        public virtual IEnumerable<Styles> style { set; get; }



    }
}