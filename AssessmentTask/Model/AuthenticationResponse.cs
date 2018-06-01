using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentTask.Model
{
    public class AuthenticationResponse
    {
        [DeserializeAs(Name = "access_token")]
        public string AccessToken { get; set; }

        [DeserializeAs(Name = "token_type")]
        public string TokenType { get; set; }

        [DeserializeAs(Name = "expires_in")]
        public int ExpirationTime { get; set; }

        [DeserializeAs(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DeserializeAs(Name = "displayName")]
        public string DisplayName { get; set; }

        [DeserializeAs(Name = ".issued")]
        public DateTime Issued { get; set; }

        [DeserializeAs(Name = ".expires")]
        public DateTime Expires { get; set; }

    }
}
