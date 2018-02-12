using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lifeline.BOL
{
    
    public partial class Members
    {
        [Key]
        public int memberId { set; get; }
               
        public string firstName { set; get; }
        public string lastName { set; get; }

        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Required]
        public string email { set; get; }
        public string password { set; get; }
        public string profilePicture { set; get; }
        public string gender { set; get; }
        public string weight { set; get; }
        public string height { set; get; }
        public string age { set; get; }
        public string username { set; get; }
        public string facebookId { set; get; }
        public string instagramId { set; get; }
        public string joiningDate { set; get; }
        public string faceShape { set; get; }
        public string hairType { set; get; }
        public string skinColor { set; get; }

        public virtual IEnumerable<Styles> styles { set; get; }
    }
}
