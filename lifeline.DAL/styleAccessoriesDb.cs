using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;

namespace lifeline.DAL
{
    public class styleAccessoriesDb : DALbase
    {
        public IEnumerable<Style_Accessories> getAll()
        {
            return db.styleAccessories.ToList();
        }

        public Style_Accessories getById(int id)
        {
            return db.styleAccessories.Find(id);
        }

        public IEnumerable<Style_Accessories> getByType(string type)
        {
            return db.styleAccessories.Where(x => x.type == type);
        }

        public IEnumerable<Style_Accessories> getByCategory(string category)
        {
            return db.styleAccessories.Where(x => x.category == category);
        }

        public IEnumerable<Style_Accessories> getByTypeAndCategory(string type, string category)
        {

            return db.styleAccessories.Where(x => x.type == type && x.category == category);
        }

        public void insert(Style_Accessories accessorie)
        {
            db.styleAccessories.Add(accessorie);
            db.SaveChanges();
        }

    }
}

