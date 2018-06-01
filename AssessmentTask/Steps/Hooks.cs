using AssessmentTask.Model;
using AssessmentTask.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AssessmentTask.Steps
{
    [Binding]
    class Hooks
    {
        [BeforeScenario("@CleanTestData")]
        [AfterScenario("@CleanTestData")]
        public void CleanCompaniesTestData()
        {
            //Authorize to API
            RestApiHelper helper = new RestApiHelper();
            AutomationEngineerSteps stepsObject = new AutomationEngineerSteps();

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"username","test" },
                {"password", "password1" },
                {"grant_type","password" }

            };

            helper.Authorize("/token", parameters);

            //Clean all companies
            stepsObject.WhenIGetAll("company");
            if (RestApiHelper.companyResponse != null)
            {
                foreach (Company company in RestApiHelper.companyResponse)
                {
                    stepsObject.WhenISendDeleteByIdRequestForWithName("company", company.Name);

                }
            }

            //Clean all employees
            stepsObject.WhenIGetAll("employee");
            if (RestApiHelper.employeeResponse != null)
            {
                foreach (Employee employee in RestApiHelper.employeeResponse)
                {
                    stepsObject.WhenISendDeleteByIdRequestForWithName("employee", employee.Name);

                }
            }

            helper.CleanAllRespones();
        }

    }
}
