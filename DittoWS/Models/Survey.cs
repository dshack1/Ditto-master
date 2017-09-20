using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class Survey
    {
        public int Survey_ID { get; set; } //pri
        public string Survey_Alias { get; set; }
        public bool Is_Active { get; set; }
        public string Addtl_JQuery { get; set; }
        public int? Task_ID { get; set; }
        public double? Score_Limit { get; set; }
    }

    public partial class XSurvey
    {
        public bool Success { get; set; }
        public List<Survey> Surveys { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class Survey_Input
    {

        public int Survey_ID { get; set; } //pri

        public string Token { get; set; }
    }
    public partial class Survey_Create : Survey
    {
        public string Token { get; set; }
    }
}
