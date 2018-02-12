using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;

namespace lifeline.DAL
{
    public class membersDb: DALbase
    {
        
       
        public IEnumerable<Members> getAll()
        {
            return db.members.ToList();
        }

        public Members getById(int id)
        {
            return db.members.Find(id);
        }

        public Members getByEmail(string email)
        {
           return db.members.Where(x => x.email == email).FirstOrDefault();
        }

        public void insert(Members member)
        {
            db.members.Add(member);
            save();
        }

        public void delete(string email)
        {
            Members member = db.members.Where(x => x.email == email).FirstOrDefault();
            db.members.Remove(member);
            save();
        }
        

        public void update(Members member)
        {
            
            db.Entry(member).State = System.Data.Entity.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}
