using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Text;

namespace SolutionEquations.App_Code
{
    /// <summary>
    /// Class for working 
    /// with equation 
    ///</summary>
    sealed public class SolutionEquation
    {
        /// <summary>
        /// Method for solving quadratic equations
        /// </summary>
        /// <param name="a">first parameter equation</param>
        /// <param name="b">second parameter equation</param>
        /// <param name="c">third parameter equation</param>
        /// <returns>solution of the equation</returns>
        public static object[] Resolve(double a, double b, double c)
        {
            double d = b * b - 4 * a * c;
            
            object[] result = new object[2];

            if (a != 0)
            {
                if (d > 0)
                {
                    result[0] = (-b + Math.Sqrt(d)) / (2 * a);
                    result[1] = (-b - Math.Sqrt(d)) / (2 * a);
                }
                else if (d == 0)
                {
                    result[0] = result[1] = (-b) / (2 * a);
                }
                else
                {
                    double real = (-b) / (2 * a);
                    double imaginary = Math.Sqrt(-d) / (2 * a);

                    Complex complex1 = new Complex(real, imaginary);
                    Complex complex2 = new Complex(real, -imaginary);

                    result[0] = complex1;
                    result[1] = complex2;
                }
            }

            return result;
        }

        /// <summary>
        /// Method for checking array string can be converted to double
        /// </summary>
        /// <param name="parameters">array string parameters</param>
        /// <returns>true or false</returns>
        public static bool StrIsDouble(string[] parameters)
        {
            double retDouble;
            bool isDouble = true;

            foreach (string parameter in parameters)
            {
                if (!Double.TryParse(Convert.ToString(parameter), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retDouble))
                {
                    isDouble = false;
                }
            }
            
            return isDouble;
        }
        /// <summary>
        /// Method for creating equation string
        /// </summary>
        /// <param name="a">first parameter equation</param>
        /// <param name="b">second parameter equation</param>
        /// <param name="c">third parameter equation</param>
        /// <returns>equation string</returns>
        public static string FormatEquation(double a, double b, double c)
        {
            StringBuilder equation = new StringBuilder("");

            if (a == -1)
            {
                equation.Append("-");
            }

            if (Math.Abs(a) != 1 & a != 0)
            {
                equation.Append(a.ToString());
            }  

            if (a != 0) 
            {
                equation.Append("x<sup><small>2</small></sup>");
            }

            if (b == -1)
            {
                equation.Append("-");
            }

            if (b > 0 & a != 0)
            {
                equation.Append("+");
            }

            if (Math.Abs(b) != 1 & b !=0 )
            {
                equation.Append(b.ToString());
            }
                        
            if (b != 0)
            {
                equation.Append("x");
            }

            if (c > 0 & b != 0)
            {
                equation.Append("+");
            }

            if (c != 0)
            {
                equation.Append(c.ToString());
            }

            equation.Append("=0.");
            
            return equation.ToString();
        }
    }
}