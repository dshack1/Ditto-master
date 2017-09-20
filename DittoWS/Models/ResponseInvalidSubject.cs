using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class ResponseInvalidSubject
    {
        public Guid Response_ID { get; set; } //pri
        public string Subject_ID { get; set; }
        public int? EmailQueue_ID { get; set; }
        public int? AuthUser_ID { get; set; }
        public DateTime? Acknowledged { get; set; }
    }

    public partial class XResponseInvalidSubject
    {
        public bool Success { get; set; }
        public List<ResponseInvalidSubject> ResponseInvalidSubjects { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class ResponseInvalidSubject_Input
    {
        public Guid Response_ID { get; set; } //pri

        public string Token { get; set; }
    }
    public partial class ResponseInvalidSubject_Create : ResponseInvalidSubject
    {
        public string Token { get; set; }
    }
}
