using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class QuestionMultiLocale
    {
        public int Question_Multi_ID { get; set; }//pri
        public string Locale_Code { get; set; }//pri
        public string Question_Text { get; set; }
    }

    public partial class XQuestionMultiLocale
    {
        public bool Success { get; set; }
        public List<QuestionMultiLocale> QuestionMultiLocales { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class QuestionMultiLocale_Input
    {
        public int Question_Multi_ID { get; set; } //pri
        public string Locale_Code { get; set; } //pri
        public string Token { get; set; }
    }
    public partial class QuestionMultiLocale_Create : QuestionMultiLocale
    {
        public string Token { get; set; }
    }
}
