using System;
using System.Xml.Serialization;

namespace Form_Filler.Class
{
    [Serializable()]
    public class Detail
    {
        [XmlAttribute("PickID")]
        public int PickID { get; set; }

        [XmlElement("Pick_List_ID")]
        public int Pick_List_ID { get; set; }

        [XmlElement("ProductID")]
        public int ProductID { get; set; }

        [XmlElement("ticketID")]
        public int ticketID { get; set; }

        [XmlElement("Num1")]
        public int Num1 { get; set; }

        [XmlElement("Num2")]
        public int Num2 { get; set; }

        [XmlElement("Num3")]
        public int Num3 { get; set; }

        [XmlElement("Num4")]
        public int Num4 { get; set; }

        [XmlElement("Num5")]
        public int Num5 { get; set; }

        [XmlElement("Num6")]
        public int Num6 { get; set; }

        [XmlElement("Num7")]
        public int Num7 { get; set; }

        [XmlElement("Remaining")]
        public int Remaining { get; set; }

        [XmlElement("Draw_Day")]
        public string Draw_Day { get; set; }

        [XmlElement("Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
