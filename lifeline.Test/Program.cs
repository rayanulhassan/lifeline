using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;
using lifeline.DAL;
using lifeline.BLL;
using System.IO;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;

namespace lifeline.Test
{
    class Program
    {
        static void Main(string[] args)

        {

            dietPlansBs b = new dietPlansBs();

                Console.WriteLine(b.BMICalculator(54,170)["BMI"]);
                Console.ReadKey();
            }
        }
    }

