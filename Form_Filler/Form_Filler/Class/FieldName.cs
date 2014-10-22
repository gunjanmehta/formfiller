using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Form_Filler.Class
{
    [Serializable()]
    public class FieldName
    {
        [XmlAttribute("value")]
        public string value { get; set; }

        [XmlText]
        public string value1 { get; set; }
    }
}
