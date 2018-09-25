using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SolutionEquations.App_Code
{
    public class SolutionEquation
    {
        public static string Resolve(double a, double b, double c)
        {
            double d1 = b * b - 4 * a * c;
            double r1, r2;
            string result = "";
            if (a == 0)
            {
                result = "Not a Quadratic equation, Linear equation";
            }
            else if (d1 > 0)
            {
                r1 = (-b + Math.Sqrt(d1)) / (2 * a);
                r2 = (-b - Math.Sqrt(d1)) / (2 * a);

                result = String.Format("First root is {0}. Second root is {1}", r1, r2);
            }
            else if (d1 == 0)
            {
                r1 = r2 = (-b) / (2 * a);

                result = String.Format("First root is {0}. Second root is {1}", r1, r2);
            }
            else
            {
                r1 = (-b) / (2 * a);
                r2 = Math.Sqrt(-d1) / (2 * a);

                result = String.Format("First root is {0} + i*{1}. Second root is {0} - i*{1}", r1, r2);
            }
            
            return result;
        }

        public static bool CheckIsDoubleParameters(string[] parameters)
        {
            string checkRegDouble = @"^[0-9]*[.,]?[0-9]+$";

            bool checkParameters = true;

            foreach (string parameter in parameters)
            {
                if (!Regex.IsMatch(parameter, checkRegDouble))
                {
                    checkParameters = false;
                }
            }

            return checkParameters;
        }
    }
}