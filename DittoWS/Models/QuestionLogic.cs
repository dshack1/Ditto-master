using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class QuestionLogic
    {
        public int Question_Logic_ID { get; set; }
        public int Question_ID { get; set; }
        public string Logic { get; set; }
        public int Target_Question_ID { get; set; }
        public bool Target_Visible { get; set; } //true
        public string Target_Value { get; set; }
    }

    public partial class XQuestionLogic
    {
        public bool Success { get; set; }
        public List<QuestionLogic> QuestionLogics { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class QuestionLogic_Input
    {
        public int Question_Logic_ID { get; set; } //pri
        public string Token { get; set; }
    }
    public partial class QuestionLogic_Create : QuestionLogic
    {
        public string Token { get; set; }
    }
}
