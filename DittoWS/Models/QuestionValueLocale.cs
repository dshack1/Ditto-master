using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class QuestionValueLocale
    {
        public int Question_ID { get; set; } //pri
        public int Value_ID { get; set; } //pri
        public string Locale_Code { get; set; } //pri
        public string Value_Text { get; set; }
    }

    public partial class XQuestionValueLocale
    {
        public bool Success { get; set; }
        public List<QuestionValueLocale> QuestionValueLocales { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class QuestionValueLocale_Input
    {
        public int Question_ID { get; set; } //pri
        public int Value_ID { get; set; } //pri
        public string Locale_Code { get; set; } //pri
        public string Token { get; set; }
    }
    public partial class QuestionValueLocale_Create : QuestionValueLocale
    {
        public string Token { get; set; }
    }
}
