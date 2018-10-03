using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SolutionEquations.App_Code
{
    /// <summary>
    /// Class Complex number
    ///</summary>
    sealed public class Complex
    {
        private double real; 
        private double imaginary; 

        public Complex(double x, double y)
        {
            real = x;
            imaginary = y;
        }

        public override string ToString()
        {
            StringBuilder complexStr = new StringBuilder("");

            if (real != 0)
            {
                complexStr.Append(real.ToString());
            }
            
            if (imaginary != 0)
            { 
                if (imaginary > 0 & real != 0)
                {
                    complexStr.Append("+");
                }
                complexStr.Append(imaginary.ToString());
                complexStr.Append("*i");
            }

            return complexStr.ToString();
        }
    }
}