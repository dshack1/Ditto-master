using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class MiscLocale
    {
        public string Misc_Code { get; set; }
        public string Locale_Code { get; set; }
        public string Misc_Text { get; set; }
    }
    public partial class XMiscLocale
    {
        public bool Success { get; set; }
        public List<MiscLocale> MiscLocales { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class MiscLocale_Input
    {
        public string Misc_Code { get; set; }
        public string Locale_Code { get; set; }
        public string Token { get; set; }
    }
    public partial class MiscLocale_Create : MiscLocale
    {
        public string Token { get; set; }
    }
}
