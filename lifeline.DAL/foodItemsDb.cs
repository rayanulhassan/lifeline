using lifeline.BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lifeline.DAL
{
    public class foodItemsDb: DALbase
    {
        public void insert(Food_Items foodItem)
        {
            db.foodItems.Add(foodItem);
            save();
        }

        public IEnumerable<Food_Items> getAll()
        {
            return db.foodItems.ToList();
        }

        public Food_Items getItemByName(string name)
        {
            return db.foodItems.Where(x => x.name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public Food_Items getById(int id)
        {
            return db.foodItems.Find(id);
        }

        public IEnumerable<Food_Items> getByCategory(string categoryName)
        {
            return db.foodItems.Where(x => x.category.ToLower() == categoryName.ToLower());
        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}
