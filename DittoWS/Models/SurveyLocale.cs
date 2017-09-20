using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class SurveyLocale
    {
        public int Survey_ID { get; set; }  //pri
        public string Locale_Code { get; set; } //pri
        public string Survey_Name { get; set; }
        public string Survey_ShortName { get; set; }
    }

    public partial class XSurveyLocale
    {
        public bool Success { get; set; }
        public List<SurveyLocale> SurveyLocales { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class SurveyLocale_Input
    {
        public int Survey_ID { get; set; }  //pri
        public string Locale_Code { get; set; } //pri

        public string Token { get; set; }
    }
    public partial class SurveyLocale_Create : SurveyLocale
    {
        public string Token { get; set; }
    }
}
