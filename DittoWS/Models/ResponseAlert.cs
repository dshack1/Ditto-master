using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class ResponseAlert
    {
        public Guid Response_ID { get; set; } //pri
        public int? EmailQueue_ID { get; set; }
        public int? AuthUser_ID { get; set; }
        public DateTime? Acknowledged { get; set; }
    }

    public partial class XResponseAlert
    {
        public bool Success { get; set; }
        public List<ResponseAlert> ResponseAlerts { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class ResponseAlert_Input
    {
        public Guid Response_ID { get; set; } //pri

        public string Token { get; set; }
    }
    public partial class ResponseAlert_Create : ResponseAlert
    {
        public string Token { get; set; }
    }
}
