using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;
using lifeline.DAL;

namespace lifeline.BLL
{
    public class foodItemsBs
    {
        private foodItemsDb db;

        public foodItemsBs()
        {
            db = new foodItemsDb();
        }

        public IEnumerable<Food_Items> getAll()
        {
            return db.getAll();
        }

        public IEnumerable<Food_Items> getByCategory(string category)
        {
            return db.getByCategory(category);
        }

        public Food_Items getItemByName(string name)
        {
            return db.getItemByName(name);
        }

       
            
    }
}
