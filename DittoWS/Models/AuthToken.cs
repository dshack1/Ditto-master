using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class AuthToken
    {
        public int AuthUser_ID { get; set; }
        public string Token { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Last_Used { get; set; }
        public bool Expired { get; set; }
    }
    public partial class XAuthToken
    {
        public bool Success { get; set; }
        public List<AuthToken> AuthToken { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class AuthToken_Input
    {
        public string Token { get; set; }
    }
}
