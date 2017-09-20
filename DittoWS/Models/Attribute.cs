using System;
using System.Collections.Generic;

namespace DittoWS.Models
{
    public partial class Attribute
    {
        public string Attribute_Code { get; set; }
        public string Attribute_Desc { get; set; }
    }
    public partial class XAttribute
    {
        public bool Success { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<Error> Errors { get; set; }
    }
    public partial class Attribute_Input
    {
        public string Attribute_Code { get; set;} /// <summary>
        /// primary key
        /// </summary>
        public string Token { get; set; }
    }
    public partial class Attribute_Create : Attribute
    {
        public string Token { get; set; }
    }
}
