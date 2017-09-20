using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class Response
    {
        public Guid Response_ID { get; set; } //pri
        public string Subject_ID { get; set; }
        public int Survey_ID { get; set; }
        public DateTime Start_DateTime { get; set; }
        public DateTime? End_DateTime { get; set; }
        public string Locale_Code { get; set; }
        public string Notes { get; set; }
        public bool Is_Deleted { get; set; }
    }

    public partial class XResponse
    {
        public bool Success { get; set; }
        public List<Response> Responses { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class Response_Input
    {
        public Guid Response_ID { get; set; } //pri

        public string Token { get; set; }
    }
    public partial class Response_Create : Response
    {
        public string Token { get; set; }
    }
}
