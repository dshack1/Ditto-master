using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public class FunctionClasses
    {
        public partial class ValidAuthToken
        {
            public bool Valid { get; set; }
            public int AuthUser_ID { get; set; }
            public string Token { get; set; }
            public DateTime Issued { get; set; }
            public DateTime Last_Used { get; set; }
            public List<Error> Errors { get; set; }
        }
    }
}
