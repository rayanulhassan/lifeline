using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;
using lifeline.DAL;

namespace lifeline.BLL
{
    public class stylesBs
    {
        private stylesDb db;

        public stylesBs()
        {
            db = new stylesDb();
        }        

        public void insertRange(int memberId, int[] styleAccessoriesIds)
        {
            foreach (var item in styleAccessoriesIds)
            {
                db.insert(new Styles() { memberId = memberId, styleAccessoriesId = item });
            }
        }

        public IEnumerable<Styles> getByMemberId(int? id,bool member, bool styleAccessorie)
        {
            return db.getByMemberId(id,member, styleAccessorie);
        }

        public void insert(Styles style)
        {
            db.insert(style);
        }

        public Styles update(int? id, Styles style,bool member,bool styleAccessorie)
        {
            Styles oldStyle = db.getById(id,member,styleAccessorie);
            


            if (style.memberId != oldStyle.memberId && style.memberId != null)
            {
                oldStyle.memberId = style.memberId;
               
            }
                

            if (style.styleAccessoriesId != oldStyle.styleAccessoriesId && style.styleAccessoriesId != null)
            {
                
                oldStyle.styleAccessoriesId = style.styleAccessoriesId;
            }
                

            db.update(oldStyle);
            
            return oldStyle;
        }

        public void updateMemberIdRange(int memberId, int[] styleAccessoriesId,bool member,bool styleAccessorie)
        {
            List<Styles> list = getByMemberId(memberId,member,styleAccessorie).ToList();
            
            int count = 0;
            if (styleAccessoriesId.Length == list.Count)
            {
                foreach (Styles item in list)
                {
                    update(item.styleId, new Styles { memberId = memberId, styleAccessoriesId = styleAccessoriesId[count]},member,styleAccessorie);
                    count++;
                    
                }
            }
            else if(styleAccessoriesId.Length > list.Count)
            {
                foreach (Styles item in list)
                {
                    update(item.styleId, new Styles { memberId = memberId, styleAccessoriesId = styleAccessoriesId[count] },member,styleAccessorie);
                    count++;                   
                }

                do
                {
                    insert(new Styles() { memberId = memberId, styleAccessoriesId = styleAccessoriesId[count]});
                    count++;
                } while (count != styleAccessoriesId.Length);
            }

            else
            {
                foreach (Styles item in list)
                {
                    try
                    {
                        update(item.styleId, new Styles { memberId = memberId, styleAccessoriesId = styleAccessoriesId[count] },member,styleAccessorie);
                        count++;                        
                    }
                    catch
                    {
                        db.deleteById(item.styleId);                        
                    }
                    
                }
            }
            count = 0;


        }

        public IEnumerable<Styles> getByMemberIdAndType(int memberId, string type,bool member, bool styleAccessorie)
        {
            return db.getByMemberIdAndType(memberId, type,member,styleAccessorie);
        }

        
    }
}
