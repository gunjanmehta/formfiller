using System;
using System.Xml.Serialization;

namespace Form_Filler.Class
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("container")]
    public class container
    {
        [XmlElement("detail")]
        public Detail[] detail { get; set; }
    }
}
