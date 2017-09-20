using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class ResponseItem
    {
        public int Response_Item_ID { get; set; } //pri
        public Guid Response_ID { get; set; }
        public int Question_ID { get; set; }
        public int Question_Multi_ID { get; set; }
        public int Additional_ID { get; set; }
        public string Question_Alias { get; set; }
        public string Answer { get; set; }
    }

    public partial class XResponseItem
    {
        public bool Success { get; set; }
        public List<ResponseItem> ResponseItems { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class ResponseItem_Input
    {
        public int Response_Item_ID { get; set; } //pri

        public string Token { get; set; }
    }
    public partial class ResponseItem_Create : ResponseItem
    {
        public string Token { get; set; }
    }
}
