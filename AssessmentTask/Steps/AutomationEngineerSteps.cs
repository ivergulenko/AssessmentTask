using AssessmentTask.Model;
using AssessmentTask.Utils;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AssessmentTask.Steps
{
    [Binding]
    class AutomationEngineerSteps
    {
        RestApiHelper helper = new RestApiHelper();

        [Given(@"I'm authorized to API")]
        public void GivenIMAuthorizedToAPI(Table table)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                parameters.Add(row["Parameter"], row["Value"]);
            }

            helper.Authorize("/token", parameters);
        }

        [When(@"I send Post request to create ""(.*)""")]
        public void WhenISendPostRequestToCreate(string entity, Table table)
        {
            string resource = helper.GetResourseLocation(entity, false);
            foreach (var row in table.Rows)
            {
                var parameterAsJsonString = "{\'" + row["Parameter"] + "\':\'" + row["Value"] + "\'}";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("undefined", parameterAsJsonString);
                IRestResponse response = helper.ExecuteRequest(resource, Method.POST, false, parameters, true);
                helper.CheckResponseSuccessful(response);
            }
        }

        [When(@"I get all ""(.*)""")]
        public void WhenIGetAll(string entity)
        {
            string resource = helper.GetResourseLocation(entity, false);
            IRestResponse response = helper.ExecuteRequest(resource, Method.GET);
            if (helper.CheckResponseSuccessful(response))
            {
                if (entity == "company")
                {
                    RestApiHelper.companyResponse = helper.ParseResponse<List<Company>>(response);
                }
                else if (entity == "employee")
                {
                    RestApiHelper.employeeResponse = helper.ParseResponse<List<Employee>>(response);
                }
            }
        }

        [When(@"I send GetById request for ""(.*)"" with name ""(.*)""")]
        public void WhenISendGetByIdRequestForWithName(string entity, string name)
        {
            string resource = helper.GetResourseLocation(entity, true);

            dynamic addedEntity = new Company();
            if (entity == "company")
            {
                addedEntity = RestApiHelper.companyResponse.FirstOrDefault(x => x.Name == name);
            }
            else if (entity == "employee")
            {
                addedEntity = RestApiHelper.employeeResponse.FirstOrDefault(x => x.Name == name);
            }

            Assert.NotNull(addedEntity);
            IRestResponse response = helper.ExecuteRequest(resource, Method.GET, false, null, false, addedEntity.Id);
            if (helper.CheckResponseSuccessful(response))
            {
                if (entity == "company")
                {
                    RestApiHelper.companyResponse = helper.ParseResponse<List<Company>>(response);
                }
                else if (entity == "employee")
                {
                    RestApiHelper.employeeResponse = helper.ParseResponse<List<Employee>>(response);
                }
            }
        }

        [When(@"I send DeleteById request for ""(.*)"" with name ""(.*)""")]
        public void WhenISendDeleteByIdRequestForWithName(string entity, string name)
        {
            string resource = helper.GetResourseLocation(entity, true);

            dynamic addedEntity = new Company();
            if (entity == "company")
            {
                addedEntity = RestApiHelper.companyResponse.FirstOrDefault(x => x.Name == name);
            }
            else if (entity == "employee")
            {
                addedEntity = RestApiHelper.employeeResponse.FirstOrDefault(x => x.Name == name);
            }

            Assert.NotNull(addedEntity);
            IRestResponse response = helper.ExecuteRequest(resource, Method.DELETE, false, null, false, addedEntity.Id);
            helper.CheckResponseSuccessful(response);
        }

        [When(@"I send GetById request for ""(.*)"" with id (.*)")]
        public void WhenISendGetByIdRequestForWithId(string entity, int id)
        {
            string resource = helper.GetResourseLocation(entity, true);
            IRestResponse response = helper.ExecuteRequest(resource, Method.GET, false, null, false, id);
            helper.CheckResponseSuccessful(response);
        }

        [When(@"I send DeleteById request for ""(.*)"" with id (.*)")]
        public void WhenISendDeleteByIdRequestForWithId(string entity, int id)
        {
            string resource = helper.GetResourseLocation(entity, true);
            IRestResponse response = helper.ExecuteRequest(resource, Method.DELETE, false, null, false, id);
            helper.CheckResponseSuccessful(response);
        }

        [Then(@"companies response contains correct values")]
        public void ThenCompaniesResponseContainsCorrectValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                Assert.AreEqual(row["Name"], RestApiHelper.companyResponse[0].Name);
            };
        }

        [Then(@"employees response contains correct values")]
        public void ThenEmployeesResponseContainsCorrectValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                Assert.AreEqual(row["Name"], RestApiHelper.employeeResponse[0].Name);
            };
        }

        [Then(@"(.*) companies are returned in response")]
        public void ThenCompaniesAreReturnedInResponse(int numberOfCompanies)
        {
            Assert.AreEqual(numberOfCompanies, RestApiHelper.companyResponse.Count);
        }

        [Then(@"(.*) employees are returned in response")]
        public void ThenEmployeesAreReturnedInResponse(int numberOfEmployees)
        {
            Assert.AreEqual(numberOfEmployees, RestApiHelper.employeeResponse.Count);
        }


        [Then(@"companies list contains companies")]
        public void ThenCompaniesListContainsCompanies(Table table)
        {
            foreach (var row in table.Rows)
            {
                var expectedCompany = RestApiHelper.companyResponse.FirstOrDefault(x => x.Name == row["Name"]);
                Assert.NotNull(expectedCompany);
            }
        }

        [Then(@"employees list contains employees")]
        public void ThenEmployeesListContainsEmployees(Table table)
        {
            foreach (var row in table.Rows)
            {
                var expectedCompany = RestApiHelper.employeeResponse.FirstOrDefault(x => x.Name == row["Name"]);
                Assert.NotNull(expectedCompany);
            }
        }


        [Then(@"companies list doesn't contain company")]
        public void ThenCompaniesListDoesnTContainCompany(Table table)
        {
            foreach (var row in table.Rows)
            {
                var expectedCompany = RestApiHelper.companyResponse.FirstOrDefault(x => x.Name == row["Name"]);
                Assert.Null(expectedCompany);
            }
        }

        [Then(@"employees list doesn't contain employee")]
        public void ThenEmployeesListDoesnTContainEmployee(Table table)
        {
            foreach (var row in table.Rows)
            {
                var expectedCompany = RestApiHelper.employeeResponse.FirstOrDefault(x => x.Name == row["Name"]);
                Assert.Null(expectedCompany);
            }
        }

        [Then(@"error status code ""(.*)"" is returned in response")]
        public void ThenErrorStatusCodeIsReturnedInResponse(string statusCode)
        {
            Assert.AreEqual(statusCode, RestApiHelper.failureResponse.StatusCode.ToString());
        }

        [Then(@"response contains error status code ""(.*)"" and error message ""(.*)""")]
        public void ThenResponseContainsErrorStatusCodeAndErrorMessage(string statusCode, string message)
        {
            Assert.AreEqual(statusCode, RestApiHelper.failureResponse.StatusCode.ToString());
            Assert.AreEqual(message, RestApiHelper.failureResponse.Content.ToString());
        }


    }
}

