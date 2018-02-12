using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;

namespace lifeline.DAL
{
    public class DALbase
    {
        protected lifelineDbContext db;

        public DALbase()
        {
            db = new lifelineDbContext();
           
        }

        
    }
}
