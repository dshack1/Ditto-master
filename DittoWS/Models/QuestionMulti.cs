using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class QuestionMulti
    {
        public int Question_Multi_ID { get; set; } //pri
        public int Question_ID { get; set; }
        public string Question_Multi_Alias { get; set; }
        public bool Has_Comment_Field { get; set; }
        public bool Always_Show_Comment { get; set; }
        public int Ord_By { get; set; }
    }

    public partial class XQuestionMulti
    {
        public bool Success { get; set; }
        public List<QuestionMulti> QuestionMultis { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class QuestionMulti_Input
    {
        public int Question_Multi_ID { get; set; } //pri
        public string Token { get; set; }
    }
    public partial class QuestionMulti_Create : QuestionMulti
    {
        public string Token { get; set; }
    }
}
