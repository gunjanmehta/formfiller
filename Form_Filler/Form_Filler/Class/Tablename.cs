using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Form_Filler.Class
{
    [Serializable()]
    public class Tablename
    {
        [XmlAttribute("value")]
        public string value { get; set; }

        [XmlElement("action")]
        public List<Action> action { get; set; }
    }
}
