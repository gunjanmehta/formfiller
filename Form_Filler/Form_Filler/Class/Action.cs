using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Form_Filler.Class
{
    [Serializable()]
    public class Action
    {
        [XmlElement("before")]
        public Before before { get; set; }

        [XmlElement("after")]
        public After after { get; set; }
    }
}
