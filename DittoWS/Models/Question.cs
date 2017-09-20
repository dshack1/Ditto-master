using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class Question
    {
        public int Question_ID { get; set; }
        public int Survey_ID { get; set; }
        public string Question_Alias { get; set; }
        public int Question_Type_ID { get; set; }
        public string Default_Answer { get; set; }
        public bool Is_Required { get; set; }  //assume false
        public int Ord_By { get; set; }
        public bool Is_Active { get; set; } //assume true
    }

    public partial class XQuestion
    {
        public bool Success { get; set; }
        public List<Question> Questions { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class Question_Input
    {
        public int Question_ID { get; set; }
        public string Token { get; set; }
    }
    public partial class Question_Create : Question
    {
        public string Token { get; set; }
    }
}
