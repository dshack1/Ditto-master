using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class QuestionLocale
    {
        public int Question_ID { get; set; } //pri
        public string Locale_Code { get; set; } //pri
        public string Question_Text { get; set; }
        public string Question_Subtext { get; set; }
        public string Question_Short { get; set; }
    }

    public partial class XQuestionLocale
    {
        public bool Success { get; set; }
        public List<QuestionLocale> QuestionLocales { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class QuestionLocale_Input
    {
        public int Question_ID { get; set; } //pri
        public string Locale_Code { get; set; } //pri
        public string Token { get; set; }
    }
    public partial class QuestionLocale_Create : QuestionLocale
    {
        public string Token { get; set; }
    }
}
