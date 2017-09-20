using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DittoWS.Models
{
    public partial class Error
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public string Exception { get; set; }
        public string InnerException { get; set; }
        public string Input { get; set; }
    }
}
