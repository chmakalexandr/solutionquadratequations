using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SolutionEquations.App_Code
{
    public class EquationHandler:IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            try
            {
                string strParams = request.FilePath;

                if (strParams.Length>1 & strParams[strParams.Length-1] == '/')
                {
                    strParams = strParams.Remove(strParams.Length-1);
                }

                strParams = strParams.Remove(0, 1);   //delete first '/'
                
                string solution = "Solution was not found. Check the parameters.";
               
                double[] parametersEquation = {0, 0, 0};
                    
                string[] strParameters = strParams.Split('/');
                    
                if (SolutionEquation.CheckIsDoubleParameters(strParameters) & strParameters.Length<=3)
                {
                    int i = 0;
                    foreach (string parameter in strParameters)
                    {
                        StringBuilder sb = new StringBuilder(parameter);
                        sb.Replace('.', ',');
                        parametersEquation[i] = Convert.ToDouble(sb.ToString());
                        i++;
                    }
                    solution = SolutionEquation.Resolve(parametersEquation[0], parametersEquation[1], parametersEquation[2]);
                }

                string pathTemplate = String.Concat(request.MapPath("/"),  "template/"));
                StreamReader sr = new StreamReader(String.Concat(pathTemplate, "layout"));

                string template = sr.ReadToEnd();
                response.Write(String.Format(template, solution));
            }
            catch (Exception e)
            {
                response.Write(String.Format("<html><body><span style=\"color:red\"><strong>Error:</strong> {0}</span></body></html>", e));
            }
        }

       
        public bool IsReusable => false;
    }
}