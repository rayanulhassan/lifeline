using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lifeline.BLL
{
    public class dietPlansBs
    {
//        The Mifflin St Jeor Equation
//Men BMR = (10 x weight in kg) + (6.25 x height in cm) - (5 x age in years) + 5 (measured in Kcal/day)
//Women BMR = (10 x weight in kg) + (6.25 x height in cm) - (5 x age in years) - 161 (measured in Kcal/day)

        public double calorieCounter(double weight, double height , double age, double activityFactor ,string gender)
        {
            //metric units -- cm and kg
            //Sedentary factor 1.2
            //Mild activity level 1.375
            //Moderate activity level factor 1.55
            //Heavy or(Labor - intensive) activity level factor 1.7
            //Extreme level factor 1.9

            if (gender == "male")
                return ((10 * weight) + (6.25 * height) - (5 * age) + 5)*activityFactor;

            else 
                return ((10 * weight) + (6.25 * height) - (5 * age) - 161)*activityFactor;
        }

        public Dictionary<string,object> BMICalculator(double weight , double height)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            height = height / 100;
            double bmi = (weight / (height*height));
            string response = "";
            int responseStatus = 0;

            if (bmi <= 18.599)
            {
                response = "underweight";
                responseStatus = 1;

            }
                
            if (bmi >= 18.6 && bmi <= 24.999)
            {
                response = "normal weight";
                responseStatus = 2;
            }
                
            if (bmi >= 25 && bmi <= 29.999)
            {
                response = "overweight";
                responseStatus = 3;
            }
                
            if (bmi >= 30 && bmi <= 34.999)
            {
                response = "class I obesity";
                responseStatus = 4;
            }
                
            if (bmi >= 35 && bmi <= 39.999)
            {
                response = "class II obesity";
                responseStatus = 5;
            }
                
            if (bmi >= 40)
            {
                response = "class III obesity";
                responseStatus = 6;
            }
                

            double maximumWeight = 25 * (height*height);
            double idealWeight = 22.5 * (height*height);
            double minimumWeight = 18.6 * (height * height);

            result.Add("BMI", Math.Round(bmi,2));
            result.Add("weight status", response);
            result.Add("maximum healthy weight", Math.Round(maximumWeight,2));
            result.Add("ideal weight", Math.Round(idealWeight,2));
            result.Add("response status", responseStatus);
            result.Add("minimum healthy weight", minimumWeight);

            return (result);
                
        }

        public Dictionary<string, object> planCreator(double weight, double height, double age, double activityFactor, string gender)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Dictionary<string, object> minPlan = new Dictionary<string, object>();
            Dictionary<string, object> maxPlan = new Dictionary<string, object>();

            Dictionary<string, object> BMIResult = BMICalculator(weight, height);

            double BMI = (double)BMIResult["BMI"];
            string weightStatus = (string)BMIResult["weight status"];
            double idealWeight = (double)BMIResult["ideal weight"];
            double minimumHealthyWeight = (double)BMIResult["minimum healthy weight"];
            double maximumHealthyWeight = (double)BMIResult["maximum healthy weight"];
            double weightToGain = -1;
            double weightToLoose = -1;
            double minWeeks = -1;
            double maxWeeks = -1;
            double minCalorieIntake=-1;
            double maxCalorieIntake=-1;

            switch (weightStatus)
            {
                case "underweight":
                    {
                        result.Add("action", "You should gain weight to reach minimum healthy weight");
                        result.Add("action status", 1);
                        minCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) + 500;
                        maxCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) + 1000;
                        weightToGain = minimumHealthyWeight - weight;
                        maxWeeks = weightToGain * 1.08;
                        minWeeks = weightToGain * 2.2;
                        break;
                    }
                case "normal weight":
                    {
                        if(weight > idealWeight)
                        {
                            result.Add("action", "You are currently in helthy state but you can loose some weight to reach ideal weight");
                            result.Add("action status", 2);
                            minCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) -1000;
                            maxCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 500;
                            weightToLoose = weight - idealWeight;
                            minWeeks = weightToLoose * 1.08;
                            maxWeeks = weightToLoose * 2.2;
                        }
                        else if(weight < idealWeight)
                        {
                            result.Add("action", "You are currently in helthy state but you can gain some weight to reach ideal weight");
                            result.Add("action status", 3);
                            minCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) + 500;
                            maxCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) +1000;
                            weightToGain = idealWeight - weight;
                            maxWeeks = weightToGain * 1.08;
                            minWeeks = weightToGain * 2.2;
                        }
                        break;
                    }

                case "overweight":
                    {
                        result.Add("action", "You should loose weight to reach maximum healthy weight");
                        result.Add("action status", 4);
                        minCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 1000;
                        maxCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 500;
                        weightToLoose = weight - maximumHealthyWeight;
                        minWeeks = weightToLoose * 1.08;
                        maxWeeks = weightToLoose * 2.2;
                        break;                       
                     }
                case "class I obesity":
                    {
                        result.Add("action", "You should loose weight to reach \'overweight\' category then let lifeline to create a better plan for you");
                        result.Add("action status", 5);
                        minCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 1000;
                        maxCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 500;
                        height = height / 100;
                       // weight = 
                        weightToLoose = weight - ((height * height) * 29.9); 
                        minWeeks = weightToLoose * 1.08;
                        maxWeeks = weightToLoose * 2.2;
                        break;
                    }

                case "class II obesity":
                    {
                        result.Add("action", "You should loose weight to reach \'class I obesity\' category then let lifeline to create a better plan for you");
                        result.Add("action status", 6);
                        minCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 1000;
                        maxCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 500;
                        height = height / 100;
                        // weight = 
                        weightToLoose = weight - ((height * height) * 34.9);
                        minWeeks = weightToLoose * 1.08;
                        maxWeeks = weightToLoose * 2.2;
                        break;
                    }
                case "class III obesity":
                    {
                        result.Add("action", "You should loose weight to reach \'class II obesity\' category then let lifeline to create a better plan for you");
                        result.Add("action status", 6);
                        minCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 1000;
                        maxCalorieIntake = calorieCounter(weight, height, age, activityFactor, gender) - 500;
                        height = height / 100;
                        // weight = 
                        weightToLoose = weight - ((height * height) * 39.9);
                        minWeeks = weightToLoose * 1.08;
                        maxWeeks = weightToLoose * 2.2;
                        break;
                    }

            }
            minPlan.Add("BMI", BMI);
            minPlan.Add("weight status", weightStatus);
            minPlan.Add("Calorie intake per day", Math.Round(minCalorieIntake));
            minPlan.Add("ideal weight", Math.Round( idealWeight,1));
            minPlan.Add("minimum healthy weight", Math.Round(minimumHealthyWeight,1));
            minPlan.Add("maximum healthy weight", Math.Round(maximumHealthyWeight, 1));
            minPlan.Add("weight to gain",Math.Round(weightToGain,2));
            minPlan.Add("weight to loose", Math.Round(weightToLoose, 2));
            minPlan.Add("weeks required",Math.Round(minWeeks,1));

            maxPlan.Add("BMI", BMI);
            maxPlan.Add("weight status", weightStatus);
            maxPlan.Add("Calorie intake per day", Math.Round(maxCalorieIntake));
            maxPlan.Add("ideal weight", Math.Round(idealWeight, 1));
            maxPlan.Add("minimum healthy weight", Math.Round(minimumHealthyWeight, 1));
            maxPlan.Add("maximum healthy weight", Math.Round(maximumHealthyWeight, 1));
            maxPlan.Add("weight to gain", Math.Round(weightToGain, 2));
            maxPlan.Add("weight to loose", Math.Round(weightToLoose, 2));
            maxPlan.Add("weeks required", Math.Round(maxWeeks, 1));

            result.Add("Plan A", minPlan);
            result.Add("Plan B", maxPlan);
            return result;
        }


    }
}
