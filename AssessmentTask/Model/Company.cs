using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentTask.Model
{
    public class Company
    {
        [DeserializeAs(Name = "Id")]
        public int Id { get; set; }


        [DeserializeAs(Name = "Name")]
        public string Name { get; set; }
    }
}
