using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentTask.Model
{
    public class Employee
    {
        #region Fields

        int id;
        string name;
        #endregion

        #region Properties

        [DeserializeAs(Name = "Id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DeserializeAs(Name = "Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
    }
}
