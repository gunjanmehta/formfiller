using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Form_Filler.Class
{
    public class SerializeAndDeserialize
    {
        public string picklistXML(WebBrowser websignin)
        {
            string str = "";
            try
            {
                XMLPost xmlPost = new Class.XMLPost();
                xmlPost.processName = new ProcessName();
                xmlPost.processName.value = "Update";
                xmlPost.processName.tablename = new Tablename();
                xmlPost.processName.tablename.value = "tbl_Pick_List";
                xmlPost.processName.tablename.action = new List<Class.Action>();

                string[] raffelnumbers  =new string[8];
                int count = 0;
               
                System.Windows.Forms.HtmlElementCollection theHtmlElementCollection = websignin.Document.GetElementsByTagName("li");
                foreach (System.Windows.Forms.HtmlElement htmlElement in theHtmlElementCollection)
                {
                    if (htmlElement.GetAttribute("classname").ToString().ToLower() == "raffle_number")
                    {
                        raffelnumbers[count] = htmlElement.InnerHtml.ToString();
                        count++;
                    }
                }
               
                count = 0;

                //cc
                foreach (Detail item in Program.mainContainer.detail.Where(s => s.processed == "Processing"))
                {
                    string pickID = item.PickID.ToString();
                    string pick_List_ID = item.Pick_List_ID.ToString();
                    string productID = item.ProductID.ToString();
                    string ticketID = item.ticketID.ToString();
                    string username = Program.tkt_username;
                    string acctno = Program.Ticket_account;
                    string firstname = Program.firstname;
                    string lastname = Program.lastname;
                    string product_name = Program.product_Name;
                    string ticket_date = "";
                    string ticket_Time = "";
                    string first_draw = "";
                    string last_draw = "";
                    string ticket_no = "";
                    string timestamp_update = "";
                    string raffle_num = "";
                    if (Program.Product_type == "Lotto")
                    {
                        product_name = Program.product_Name;

                        ticket_date = websignin.Document.GetElementById("ticket_purchase_date").InnerHtml.ToString().Split(new string []{"at"},StringSplitOptions.None)[0].Trim();
                        ticket_Time = websignin.Document.GetElementById("ticket_purchase_date").InnerHtml.ToString().Split(new string[] { "at" }, StringSplitOptions.None)[1].Trim();
                        DateTime getFirstDrawDate = Convert.ToDateTime(websignin.Document.GetElementById("ticket_draw_date_summary").InnerHtml.ToString());
                        first_draw = getFirstDrawDate.ToString("MM/dd/yyyy");
                        last_draw = getFirstDrawDate.ToString("MM/dd/yyyy");
                        ticket_no = websignin.Document.GetElementById("ticket_serial").InnerHtml.ToString();
                        timestamp_update = DateTime.Now.ToString();
                        raffle_num = raffelnumbers[count];
                    }
                    else if (Program.Product_type == "Euro")
                    {
                        ticket_date = websignin.Document.GetElementById("ticket_purchase_date").InnerHtml.ToString().Split(new string[] { "at" }, StringSplitOptions.None)[0].Trim();
                        ticket_Time = websignin.Document.GetElementById("ticket_purchase_date").InnerHtml.ToString().Split(new string[] { "at" }, StringSplitOptions.None)[1].Trim();
                        DateTime getFirstDrawDate = Convert.ToDateTime(websignin.Document.GetElementById("ticket_draw_date_summary").InnerHtml.ToString());
                        first_draw = getFirstDrawDate.ToString("MM/dd/yyyy");
                        last_draw = getFirstDrawDate.ToString("MM/dd/yyyy");
                        ticket_no = websignin.Document.GetElementById("ticket_serial").InnerHtml.ToString();
                        timestamp_update = DateTime.Now.ToString();
                        raffle_num = raffelnumbers[count];
                    }
                  
                    string status = Program.state.ToString();
                    item.processed = "Complete";
                    Class.Action action1 = fillProcessData(pickID, pick_List_ID, productID, ticketID, username, acctno, firstname, lastname, product_name, ticket_date, ticket_Time, first_draw, last_draw, ticket_no, timestamp_update, raffle_num, status);
                    xmlPost.processName.tablename.action.Add(action1);
                    count++;
                }
                

                str = CreateXML(xmlPost);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);
                XmlElement root = doc.DocumentElement;
                root.RemoveAttribute("xmlns:xsi");
                root.RemoveAttribute("xmlns:xsd");
                doc.InnerXml = doc.InnerXml.Replace("ProcessName_x0020_", "ProcessName");

                str = doc.GetElementsByTagName("XMLPost")[0].OuterXml;

            }
            catch (Exception ex)
            {
                str = ex.Message.ToString();
            }

            return str;
        }

        public string CreateXML(Object myClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(myClassObject.GetType());
            
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, myClassObject);
                xmlStream.Position = 0;

                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);

                return xmlDoc.InnerXml;
            }
        }

        public Object CreateObject(string XMLString, Object myClassObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(myClassObject.GetType());
            myClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            return myClassObject;
        }

        public Class.Action fillProcessData(string pickID, string pick_List_ID, string productID, string ticketID, string username, string acctno, string firstname, string lastname, string product_name, string ticket_date, string ticket_Time, string first_draw, string last_draw, string ticket_no, string timestamp_update, string raffle_num, string status)
        {
            Before b = new Before();
            b.FieldName = new List<FieldName>();

            After a = new After();
            a.FieldName = new List<FieldName>();

            Class.Action action = new Class.Action();
            action.after = a;
            action.before = b;

            FieldName fieldName = new FieldName();
            fieldName.value = "PickID";
            fieldName.value1 = pickID;
            b.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Pick_List_ID";
            fieldName.value1 = pick_List_ID;
            b.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "ProductID";
            fieldName.value1 = productID;
            b.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "ticketID";
            fieldName.value1 = ticketID;
            b.FieldName.Add(fieldName);




            fieldName = new FieldName();
            fieldName.value = "username";
            fieldName.value1 = username;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Acctno";
            fieldName.value1 = acctno;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Firstname";
            fieldName.value1 = firstname;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Lastname";
            fieldName.value1 = lastname;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Product_name";
            fieldName.value1 = product_name;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Ticket_date";
            fieldName.value1 = ticket_date;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Ticket_Time";
            fieldName.value1 = ticket_Time;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "First_draw";
            fieldName.value1 = first_draw;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Last_draw";
            fieldName.value1 = last_draw;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Ticket_no";
            fieldName.value1 = ticket_no;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "Timestamp_update";
            fieldName.value1 = timestamp_update;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "raffle_num";
            fieldName.value1 = raffle_num;
            a.FieldName.Add(fieldName);

            fieldName = new FieldName();
            fieldName.value = "status";
            fieldName.value1 = status;
            a.FieldName.Add(fieldName);

            return action;
        }
    }
}
