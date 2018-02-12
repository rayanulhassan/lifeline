using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lifeline.BOL
{
    public partial class Styles
    {
        [Key]
        public int styleId { set; get; }
        public int? memberId { set; get; }
        public int? styleAccessoriesId { set; get; }

        [ForeignKey("styleAccessoriesId")]
        public virtual Style_Accessories styleAccessorie { set; get; }

        [ForeignKey("memberId")]
        public virtual Members member { set; get; }

    }
}
