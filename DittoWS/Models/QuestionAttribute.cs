using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class QuestionAttribute
    {
        public int Question_ID { get; set; } //pri
        public string Attribute_Code { get; set; } //pri
        public string Attribute_Value { get; set; }
    }

    public partial class XQuestionAttribute
    {
        public bool Success { get; set; }
        public List<QuestionAttribute> QuestionAttributes { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class QuestionAttribute_Input
    {
        public string Attribute_Code { get; set; }
        public int Question_ID { get; set; }
        public string Token { get; set; }
    }
    public partial class QuestionAttribute_Create : QuestionAttribute
    {
        public string Token { get; set; }
    }
}
