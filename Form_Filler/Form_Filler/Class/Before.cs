using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Form_Filler.Class
{
    [Serializable()]
    public class Before
    {
        [XmlElement("fieldName")]
        public List<FieldName> FieldName { get; set; }
    }
}
