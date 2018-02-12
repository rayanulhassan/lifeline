using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using lifeline.BOL;
using lifeline.DAL;

namespace lifeline.API
{
    public class JsonFormattorsObject
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class getFootwareSuggestionsObj
    {
        public string dressTone { set; get; }
        public string dressType { set; get; }
        public string gender { set; get; }
    }

    public class JsonFormatterList
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public int DataCount { get; set; }
        public object Data { get; set; }
    }
       
    public class getMemberStyleSuggestions
    {
        public int id { set; get; }
        public string type { set; get; }
    }

    public class getStyleItemsByTypeAndCategoryObj
    {
        public string type
        {
            set;
            get;

        }

        public string category
        {
            set;
            get;
        }

    }

    public class getSunglassesSuggestionsObj
    {
        public string faceShape { set; get; }
        public string gender { set; get; }
    }

    public class getHairStyleSuggestionsObj
    {
        public string faceShape { set; get; }
        public string gender { set; get; }
        public string hairType { set; get; }
    }

    public class postMemberStyleSuggestionObj
    {
        public int memberId { set; get; }
        public List<int> styleAccessoriesIds { set; get; }
    }

    public class responseFormattors
    {
        public static Dictionary<string,object> memberStyleSuggestionformattor(List<Styles> list)
        {
            Dictionary<string, object> result1 = new Dictionary<string, object>();
            List<Dictionary<string, object>> resList = new List<Dictionary<string, object>>();
            result1.Add("memberId", list[0].memberId);


            foreach (var item in list)
            {
                Dictionary<string, object> styleAccessorieDictionary = new Dictionary<string, object>();
                Dictionary<string, object> result = new Dictionary<string, object>();

                result.Add("styleId", item.styleId);          
                               
                styleAccessorieDictionary.Add("styleAccessorieId", item.styleAccessorie.styleAccessoriesId);
                
                styleAccessorieDictionary.Add("styleId", item.styleAccessorie.name);

                styleAccessorieDictionary.Add("picture", item.styleAccessorie.picture);
                
                styleAccessorieDictionary.Add("type", item.styleAccessorie.type);
                
                styleAccessorieDictionary.Add("category", item.styleAccessorie.category);
                
                styleAccessorieDictionary.Add("subCategory", item.styleAccessorie.subCategory);
                
                result.Add("styleAccessories", styleAccessorieDictionary);
                
                resList.Add(result);
               
            }

            result1.Add("styleAccessories", resList);
            return result1;
        }

        //public sttic
    }

    public class getSkinCareTipsObj
    {
        public string type { set; get; }
        public int id { set; get; }
    }

    public class getFoodItemByCategoryObj
    {
        public string name { set; get; }
    }

    public class BMICalculatorObj
    {
        //public int age { set; get; }
        public double height { set; get; }
        public double weight { set; get; }

    }

    public class getClothingSuggesionsObj
    {
        public string gender { set; get; }
        public string height { set; get; }
        public string weight { set; get; }
        public string category { set; get; }
        public string skinColor { set; get; }
        public string age { set; get; }
    }

    public class BMIrespnse
    {
        public float index { set; get; }
        public string result { set; get; }
    }

    public class dietPlanCreatorObj
    {
        public double weight { set; get; }
        public double height { set; get; }
        public int age { set; get; }
        public double activityFactor { set; get; }
        public string gender { set; get; }
    }


}