using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lifeline.BOL
{
    public partial class Food_Items
    {
        [Key]
        public int foodItemId { set; get; }
        public string name { set; get; }
        public string amount { set; get; }
        public string category { set; get; }
        public string subCategory { set; get; }
        public string fat { set; get; }
        public string protein { set; get; }
        public string carbohydrates { set; get; }
        public int calorie { set; get; }

    }
}
