
using lifeline.BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lifeline.DAL
{
    public class stylesDb : DALbase
    {
        public IEnumerable<Styles> getAll()
        {
            //var s = db.styles.Include(x => x.member).ToList();
            //Console.WriteLine(s[0].member.lastName);
            return db.styles.Include(x => x.member).ToList();
            
        }

        public Styles getById(int? id, bool memberCheck, bool styleAccesorieCheck)
        {
            var res = db.styles.Where(iid => iid.styleAccessoriesId == id).ToList();

            if (memberCheck == true && styleAccesorieCheck == true)
            {
                 res = db.styles
                .Include(x => x.member)
                .Include(x => x.styleAccessorie).ToList();
            }
            else if (memberCheck == true && styleAccesorieCheck == false)
            {
                res = db.styles.Include(x => x.member)
                            .ToList();
            }

            else if (memberCheck == false && styleAccesorieCheck == true)
            {
                res = db.styles.Include(x => x.styleAccessorie)
                            .ToList();
            }
            else
            {
                res = db.styles.ToList();
            }

            
            return res.First(x => x.styleId == id);
        }
        
        public IEnumerable<Styles> getByMemberId(int? id, bool memberCheck, bool styleAccesorieCheck)
        {
            if(memberCheck == true && styleAccesorieCheck == true)
            {
                return db.styles.Include(x => x.member)
                            .Include(x => x.styleAccessorie)
                            .Where(x => x.memberId == id).ToList();
            }
            else if (memberCheck == true && styleAccesorieCheck == false)
            {
                return db.styles.Include(x => x.member)
                            .Where(x => x.memberId == id).ToList();
            }

            else if (memberCheck == false && styleAccesorieCheck == true)
            {
                return db.styles.Include(x => x.styleAccessorie)
                            .Where(x => x.memberId == id).ToList();
            }
            else
            {
                return db.styles.Where(x => x.memberId == id).ToList();
            }

        }

        public void insert(Styles style)
        {
            db.styles.Add(style);
            save();
        }

        public void deleteById(int id)
        {
            db.styles.Remove(getById(id,false,false));
            save();
        }

        public void deleteByMemberId(int memberId)
        {
            db.styles.RemoveRange(getByMemberId(memberId,false,false));
            save();
        }

        public IEnumerable<Styles> getByMemberIdAndType(int memberId, string type, bool member,bool styleAccessorie)
        {
            if(member == true && styleAccessorie == true)
            {
                return db.styles.Include(x => x.member)
                            .Include(x => x.styleAccessorie)
                            .Where(x => x.memberId == memberId && x.styleAccessorie.type == type);
            }
            else if(member == true && styleAccessorie == false)
            {
                return db.styles.Include(x => x.member)                            
                            .Where(x => x.memberId == memberId && x.styleAccessorie.type == type);
            }
            else if(member == false && styleAccessorie == true)
            {
                return db.styles
                            .Include(x => x.styleAccessorie)
                            .Where(x => x.memberId == memberId && x.styleAccessorie.type == type);
            }
            else
            {
                return db.styles
                            .Where(x => x.memberId == memberId && x.styleAccessorie.type == type);
            }
            
        }

        public void update(Styles style)
        {           
            db.Entry(style).State = EntityState.Modified;          
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
