using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;
using lifeline.DAL;

namespace lifeline.BLL
{
    public class membersBs
    {
        private membersDb db;

        public membersBs()
        {
            db = new membersDb();
        }

        public IEnumerable<Members> getAll()
        {
            return db.getAll().ToList();
        }

        public Members getByEmail(string email)
        {
            return db.getByEmail(email);
        }

        public Members getById(int id)
        {
            return db.getById(id);
        }

        public void insert(Members member)
        {
            db.insert(member);
        }

        public void delete(string email)
        {
            db.delete(email);
        }

        public Members update(int id, Members member)
        {
            Members oldMember = getById(id);
           

            if (member.email != oldMember.email && member.email != null)
                oldMember.email = member.email;

            if (member.firstName != oldMember.firstName && member.firstName != null)
                oldMember.firstName = member.firstName;

            if (member.lastName != oldMember.lastName && member.lastName != null)
                oldMember.lastName = member.lastName;

            if (member.password != oldMember.password && member.password != null)
                oldMember.password = member.password;

            if (member.profilePicture != oldMember.profilePicture && member.profilePicture != null)
                oldMember.profilePicture = member.profilePicture;

            if (member.gender != oldMember.gender && member.gender != null)
                oldMember.gender = member.gender;

            if (member.weight != oldMember.weight && member.weight != null)
                oldMember.weight = member.weight;

            if (member.height != oldMember.height && member.height != null)
                oldMember.height = member.height;

            if (member.age != oldMember.age && member.age != null)
                oldMember.age = member.age;

            if (member.username != oldMember.username && member.username != null)
                oldMember.username = member.username;

            if (member.facebookId != oldMember.facebookId && member.facebookId != null)
                oldMember.facebookId = member.facebookId;


            if (member.instagramId != oldMember.instagramId && member.instagramId != null)
                oldMember.instagramId = member.instagramId;

            if (member.joiningDate != oldMember.joiningDate && member.joiningDate != null)
                oldMember.joiningDate = member.joiningDate;

            if (member.faceShape != oldMember.faceShape && member.faceShape != null)
                oldMember.faceShape = member.faceShape;


            if (member.hairType != oldMember.hairType && member.hairType != null)
                oldMember.hairType = member.hairType;


            if (member.skinColor != oldMember.skinColor && member.skinColor != null)
                oldMember.skinColor = member.skinColor;
            db.update(oldMember);

            return oldMember;

        }

    }
}
