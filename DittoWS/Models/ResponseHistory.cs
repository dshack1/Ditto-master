using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class ResponseHistory
    {
        public Guid Response_History_ID { get; set; } //pri
        public Guid Response_Item_ID { get; set; }
        public string Old_Answer { get; set; }
        public int Edit_AuthUser_ID { get; set; }
        public DateTime Edited { get; set; }
    }

    public partial class XResponseHistory
    {
        public bool Success { get; set; }
        public List<ResponseHistory> ResponseHistorys { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class ResponseHistory_Input
    {
        public Guid Response_History_ID { get; set; } //pri

        public string Token { get; set; }
    }
    public partial class ResponseHistory_Create : ResponseHistory
    {
        public string Token { get; set; }
    }
}
