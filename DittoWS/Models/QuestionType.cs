using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class QuestionType
    {
        public int Question_Type_ID { get; set; } //pri
        public string Question_Type_Name { get; set; }
    }

    public partial class XQuestionType
    {
        public bool Success { get; set; }
        public List<QuestionType> QuestionTypes { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class QuestionType_Input
    {
        public int Question_Type_ID { get; set; } //pri
      
        public string Token { get; set; }
    }
    public partial class QuestionType_Create : QuestionType
    {
        public string Token { get; set; }
    }
}
