using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;
using lifeline.DAL;
using System.Threading;

namespace lifeline.BLL
{
    public class styleAccessoriesBs
    {
        private styleAccessoriesDb db;

        public styleAccessoriesBs()
        {
            db = new styleAccessoriesDb();
        }

        public IEnumerable<Style_Accessories> getAll()
        {
            return db.getAll();
        }

        public Style_Accessories getById(int id)
        {
            return db.getById(id);
        }

        public IEnumerable<Style_Accessories> getByCategory(string category)
        {
            return db.getByCategory(category);
        }

        public IEnumerable<Style_Accessories> getByType(string type)
        {
            return db.getByType(type);
        }
        
        public IEnumerable<Style_Accessories> getHairStyleSuggesstions(string faceShape, string gender)
        {            
            List<Style_Accessories> list1;
            switch (faceShape)
            {                
                case "oval":
                    list1 = db.getByTypeAndCategory("hairStyle", "oval").ToList();
                    break;
                case "round":
                    list1 = db.getByTypeAndCategory("hairStyle", "round").ToList();
                    break;
                case "square":
                    list1 = db.getByTypeAndCategory("hairStyle", "square").ToList();
                    break;
                case "diamond":
                    list1 = db.getByTypeAndCategory("hairStyle", "diamond").ToList();
                    break;
                case "oblong":
                    list1 = db.getByTypeAndCategory("hairStyle", "oblong").ToList();
                    break;
                case "heart":
                    list1 = db.getByTypeAndCategory("hairStyle", "heart").ToList();
                    break;
                default:
                    throw new Exception("Invalid faceshape provided");
                }

            if (gender == "male")
            {
                list1.Shuffle();
                return list1.Where(x => x.subCategory == "male");
            }

            else if (gender == "female")
            {
                list1.Shuffle();
                return list1.Where(x => x.subCategory == "female");
            }
            else
            {
                throw new Exception("gender not defined properly");
                return list1.Where(x => x.subCategory == "no category");
            }


        }

        public IEnumerable<Style_Accessories> getSunglassessSuggestions(string faceShape, string gender)
        {
            List<Style_Accessories> list = new List<Style_Accessories>() { };
            switch (faceShape)
            {
                case "diamond":
                    List<Style_Accessories> list1 = db.getByTypeAndCategory("sunglasses", "oval").ToList();
                    List<Style_Accessories> list2 = db.getByTypeAndCategory("sunglasses", "rimless").ToList();
                    List<Style_Accessories> list3 = db.getByTypeAndCategory("sunglasses", "cateyes").ToList();

                    list.AddRange(list1);
                    list.AddRange(list2);
                    list.AddRange(list3);
                    break;

                case "heart":
                    List<Style_Accessories> list5 = db.getByTypeAndCategory("sunglasses", "aviators").ToList();
                    List<Style_Accessories> list4 = db.getByTypeAndCategory("sunglasses", "rimless").ToList();

                    list.AddRange(list5);
                    list.AddRange(list4);
                    break;

                case "oblong":
                    List<Style_Accessories> list6 = db.getByTypeAndCategory("sunglasses", "clubmasters").ToList();
                    List<Style_Accessories> list7 = db.getByTypeAndCategory("sunglasses", "wayfares").ToList();

                    list.AddRange(list6);
                    list.AddRange(list7);
                    break;

                case "oval":
                    List<Style_Accessories> list8 = db.getByTypeAndCategory("sunglasses", "aviators").ToList();
                    List<Style_Accessories> list9 = db.getByTypeAndCategory("sunglasses", "clubmasters").ToList();
                    List<Style_Accessories> list10 = db.getByTypeAndCategory("sunglasses", "wayfares").ToList();
                    List<Style_Accessories> list11 = db.getByTypeAndCategory("sunglasses", "butterfly").ToList();
                    List<Style_Accessories> list12 = db.getByTypeAndCategory("sunglasses", "sports").ToList();

                    list.AddRange(list8);
                    list.AddRange(list9);
                    list.AddRange(list10);
                    list.AddRange(list11);
                    list.AddRange(list12);
                    break;

                case "square":

                    List<Style_Accessories> list13 = db.getByTypeAndCategory("sunglasses", "oval").ToList();
                    List<Style_Accessories> list14 = db.getByTypeAndCategory("sunglasses", "round").ToList();
                    List<Style_Accessories> list15 = db.getByTypeAndCategory("sunglasses", "aviators").ToList();
                    List<Style_Accessories> list16 = db.getByTypeAndCategory("sunglasses", "butterfly").ToList();

                    list.AddRange(list13);
                    list.AddRange(list14);
                    list.AddRange(list15);
                    list.AddRange(list16);

                    //Console.WriteLine(list13.Count + " r" + " d" + list.Count);

                    break;

                case "round":
                    List<Style_Accessories> list17 = db.getByTypeAndCategory("sunglasses", "wayfares").ToList();
                    List<Style_Accessories> list18 = db.getByTypeAndCategory("sunglasses", "clubmasters").ToList();
                    List<Style_Accessories> list19 = db.getByTypeAndCategory("sunglasses", "shield").ToList();

                    list.AddRange(list17);
                    list.AddRange(list18);
                    list.AddRange(list19);

                    break;
                default:
                    throw new Exception("faceshape is undefined");

            }

            if (gender == "male")
            {
                list.Shuffle();
                return list.Where(x => x.subCategory == "male");
            }

            else if (gender == "female")
            {
                list.Shuffle();
                return list.Where(x => x.subCategory == "female");
            }
            else
            {
                throw new Exception("gender not defined properly");
                return list.Where(x => x.subCategory == "no category");
            }

        }

        public IEnumerable<Style_Accessories> getFootwareSuggestions(string dressTone, string type , string gender)
        {
            List<Style_Accessories> result;
            try
            {
                result = getByTypeAndCategory("footware", gender + " " + type).ToList();
            }
            catch
            {
                throw new Exception("gender or dressType is not defined properly");
            }

            try
            {
                return result.Where(x => x.subCategory == dressTone);
            }
            catch
            {
                throw new Exception("dressTone is not defined properly");
            }
            
            
        }

        public string getWeightRange(string weight)
        {
            int weightInt = int.Parse(weight);
            //45 - 54 | 55 - 70 | 71 - 85 | 86 - 100 | 101 - 125 | 126 onwards
            if (weightInt >= 35 && weightInt <= 44)
                return "35-44";
            else if (weightInt >= 45 && weightInt <= 54)
                return "45-54";
            else if (weightInt >= 55 && weightInt <= 70)
                return "55-70";
            else if (weightInt >= 71 && weightInt <= 85)
                return "71-85";
            else if (weightInt >= 86 && weightInt <= 100)
                return "86-100";
            else if (weightInt >= 101 && weightInt <= 125)
                return "101-125";
            else if (weightInt >= 126)
                return "126 onwards";
            else
                throw new Exception("weight not defined properly");
        }

        public string getAgeRange(string age)
        {
           // 5 - 20 , 21 - 25, 26 - 45, 45 - 55 ,56 onwards

            int ageInt = int.Parse(age);
            if (ageInt >= 15 && ageInt <= 18)
                return "15-18";
            else if (ageInt >= 19 && ageInt <= 23)
                return "19-23";
            else if (ageInt >= 24 && ageInt <= 30)
                return "24-30";
            else if (ageInt >= 31 && ageInt <= 45)
                return "31-45";
            else if (ageInt >= 46 && ageInt <= 60)
                return "46-60";
            else if (ageInt >= 61)
                return "61 onwards";
            else
                throw new Exception("age not defined properly");

        }

        public IEnumerable<Style_Accessories> getClothsSuggestions(string gender, string weight, string category, string age)
        {
            if (weight.Length <= 3)
            {
                weight = getWeightRange(weight);
            }

            if (age.Length <= 3)
                age = getAgeRange(age);

            if (age == "15-18" || age == "19-23")
                age = "small";
            else if (age == "24-30" || age == "31-45")
                age = "medium";
            else if (age == "46-60" || age == "61 onwards")
                age = "big";

            if (weight == "35-44" || weight == "45-54")
                weight = "slim";
            else if (weight == "55-70" || weight == "71-85")
                weight = "normal";
            else if (weight == "86-100" || weight == "101-125" || weight == "126 onwards")
                weight = "fat";

            string subCategory = age + "," + weight;
            Console.WriteLine("weight : {0} \n age : {1} \n subCategory : {2} \n category : {3} \n", weight, age, subCategory,category);
            IEnumerable<Style_Accessories> res = db.getByTypeAndCategory("cloths", gender + " " + category).Where(x => x.subCategory == subCategory);
            return res;


        }

        public IEnumerable<Style_Accessories> getByTypeAndCategory(string type, string category)
        {
            return db.getByTypeAndCategory(type, category);
        }
    }


    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }


    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
