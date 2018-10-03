using System;
using System.IO;
using System.Text;
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
                string solution = "Solution was not found. Check the parameters quadratic equation.";
                string equation = "";

                string[] strParameters = new string[3];
                double[] parametersEquation = { 0, 0, 0 };

                if (request.HttpMethod == "POST")
                {
                    var form = request.Form;
                    if (form.HasKeys())
                    {
                        string[] arrKey = form.AllKeys;
                        for (int i = 0; i < arrKey.Length; i++)
                        {
                            strParameters[i] = form.Get(i); ;                                   
                        }
                    }
                }
                else if (request.HttpMethod == "GET")
                {
                    StringBuilder strParams = new StringBuilder(request.FilePath);

                    if (strParams.Length > 1 & strParams[strParams.Length - 1] == '/')
                    {
                        strParams.Remove(strParams.Length - 1, 1); //delete in the end '/'
                    }

                    strParams = strParams.Remove(0, 1); //delete beginning '/'
                    
                    strParameters = strParams.ToString().Split('/');
                }
                

                if (SolutionEquation.StrIsDouble(strParameters) & strParameters.Length<=3)
                {
                    int i = 0;
                    foreach (string parameter in strParameters)
                    {
                        StringBuilder sb = new StringBuilder(parameter);
                        sb.Replace('.', ',');
                        parametersEquation[i] = Convert.ToDouble(sb.ToString());
                        i++;
                    }

                    equation = SolutionEquation.FormatEquation(parametersEquation[0], parametersEquation[1], parametersEquation[2]);

                    if (SolutionEquation.Resolve(parametersEquation[0], parametersEquation[1], parametersEquation[2])[0] != null)
                    {
                        solution = String.Format("First root is {0}.</br> Second root is {1}.", SolutionEquation.Resolve(parametersEquation[0], parametersEquation[1], parametersEquation[2]));
                    }
                }
                
                response.Write(String.Format(LoadTemplate(request, "template", "layout"), equation, solution));
            }
            catch (Exception e)
            {
                response.Write(String.Format("<html><body><span style=\"color:red\"><strong>Error:</strong> {0}</span></body></html>", e));
            }
        }

        private string LoadTemplate(HttpRequest request, string dirTemplate, string template)
        {
            try
            {
                StringBuilder pathTemplate = new StringBuilder(request.MapPath("/"));
                pathTemplate.Append(dirTemplate);
                pathTemplate.Append("/");
                pathTemplate.Append(template);

                StreamReader sr = new StreamReader(pathTemplate.ToString());
                
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Template error:" + e);
            }
        }

        public bool IsReusable => false;
    }
}