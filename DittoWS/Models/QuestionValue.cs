using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class QuestionValue
    {
        public int Question_ID { get; set; }
        public string Value_Alias { get; set; }
        public int Value_ID { get; set; }
        public bool Is_Selected { get; set; }
        public int Ord_By { get; set; }
    }

    public partial class XQuestionValue
    {
        public bool Success { get; set; }
        public List<QuestionValue> QuestionValues { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class QuestionValue_Input
    {
        public int Question_ID { get; set; } //pri
        public int Value_ID { get; set; } //pri

        public string Token { get; set; }
    }
    public partial class QuestionValue_Create : QuestionValue
    {
        public string Token { get; set; }
    }
}
