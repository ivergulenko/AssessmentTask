using AssessmentTask.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace AssessmentTask.Utils
{
    public class RestApiHelper
    {

        public static RestClient client = new RestClient(URL);
        private const string URL = "https://mobilewebserver9-pokertest8ext.installprogram.eu/TestApi";
        private const string basePath = "/api/automation";
        private const string companyEntityResourceLocator = "/companies";
        private const string companyEntityResourceLocatorById = "/companies/id/{id}";
        private const string employeeEntityResourceLocator = "/employees";
        private const string employeeEntityResourceLocatorById = "/employees/id/{id}";
        public static AuthenticationResponse authenticationResponse;
        public static List<Company> companyResponse;
        public static List<Employee> employeeResponse;
        public static IRestResponse failureResponse;
        public static JsonDeserializer deserial = new JsonDeserializer();


        public IRestResponse ExecuteRequest(string resource, Method method, bool isAuthentication = false,
            Dictionary<string, string> parameters = null, bool isRequestBody = false, int resourceId = 0)
        {
            var request = new RestRequest(resource, method);

            if (resource.Contains("{id}"))
            {
                request.AddUrlSegment("id", resourceId);
            }

            if (isAuthentication)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            }
            else
            {
                request.AddHeader("Authorization", authenticationResponse.TokenType + " " + authenticationResponse.AccessToken);
            }

            if (parameters != null)
            {
                if (isRequestBody)
                {
                    foreach (var parameter in parameters)
                    {
                        request.AddParameter(parameter.Key, parameter.Value, "application/json", ParameterType.RequestBody);
                    }
                }
                else {
                    foreach (var parameter in parameters)
                    {
                        request.AddParameter(parameter.Key, parameter.Value);
                    }
                }
            }

            IRestResponse response = client.Execute(request);
            return response;
        }


        public bool CheckResponseSuccessful(IRestResponse response)
        {
            if (!response.IsSuccessful)
            {
                failureResponse = response;
                return false;
            }
            else
            {
                return true;
            }
        }

        public T ParseResponse<T>(IRestResponse response)
        {
            T parsedResponse = deserial.Deserialize<T>(response);
            return parsedResponse;
        }

        public void Authorize(string resource, Dictionary<string, string> parameters)
        {
            IRestResponse response = ExecuteRequest(resource, Method.POST, true, parameters);
            if (CheckResponseSuccessful(response))
            {
                authenticationResponse = ParseResponse<AuthenticationResponse>(response);
            }
        }

        public string GetResourseLocation(string entity, bool isById)
        {
            string resource = "";

            if (entity == "company")
            {
                if (isById)
                {
                    resource = basePath + companyEntityResourceLocatorById;
                }
                else
                {
                    resource = basePath + companyEntityResourceLocator;
                }
            }
            else if (entity == "employee")
            {
                if (isById)
                {
                    resource = basePath + employeeEntityResourceLocatorById;
                }
                else
                {
                    resource = basePath + employeeEntityResourceLocator;
                }
            }
            return resource;
        }

        public void CleanAllRespones()
        {
            RestApiHelper.authenticationResponse = null;
            if (RestApiHelper.companyResponse != null)
            {
                RestApiHelper.companyResponse.Clear();
            }
            if (RestApiHelper.employeeResponse != null)
            {
                RestApiHelper.employeeResponse.Clear();
            }
            RestApiHelper.failureResponse = null;
        }
    }
}
