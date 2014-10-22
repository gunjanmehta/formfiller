using Form_Filler.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Form_Filler
{
    public partial class TicketMenu : Form
    {
        
        SqlConnection con = new SqlConnection(Program.getConnectionString()); 
        public TicketMenu()
        {
            InitializeComponent();
        }

        private void btnwednesday_Click(object sender, EventArgs e)
        {
            rbtnwednesday.Checked = true;
            rbtnfriday.Checked = false;
            rbtnsaturday.Checked = false;
            rbtntuesday.Checked = false;
            lblvalidationmsg.Visible = false;
        }

        private void btnfriday_Click(object sender, EventArgs e)
        {
            rbtnwednesday.Checked = false;
            rbtnfriday.Checked = true;
            rbtnsaturday.Checked = false;
            rbtntuesday.Checked = false;
            lblvalidationmsg.Visible = false;
        }

        private void btnsaturday_Click(object sender, EventArgs e)
        {
            rbtnwednesday.Checked = false;
            rbtnfriday.Checked = false;
            rbtnsaturday.Checked = true;
            rbtntuesday.Checked = false;
            lblvalidationmsg.Visible = false;
        }

        private void btntuesday_Click(object sender, EventArgs e)
        {
            rbtnwednesday.Checked = false;
            rbtnfriday.Checked = false;
            rbtnsaturday.Checked = false;
            rbtntuesday.Checked = true;
            lblvalidationmsg.Visible = false;
        }

        private void btnbackmain_Click(object sender, EventArgs e)
        {
            navigatetomainmenu();
            
        }

        private void btnproceed_Click(object sender, EventArgs e)
        {
            string getXmlFromDB = "";
            if (rbtnfriday.Checked == true || rbtnsaturday.Checked == true || rbtntuesday.Checked == true || rbtnwednesday.Checked == true)
            {
                if (countchecklist())
                {
                    getXmlFromDB = Program.getResponseXMLFromPickList(Program.product_id);
                    //Kamlesh
                    //string fileName = "";
                    //if (Program.Product_type == "Lotto")
                    //{
                    //    if (Program.Draw_day == "Wed")
                    //    {
                    //        fileName = "http://interact.austere.co.in/Form%20filler%20XML/Wednesday.xml";
                    //    }
                    //    else if (Program.Draw_day == "Sat")
                    //    {
                    //        //fileName = "http://interact.austere.co.in/Form%20filler%20XML/Saturday.xml";

                    //        getXmlFromDB = Program.getResponseXMLFromPickList(Program.product_id);
                    //    }
                    //}
                    //else if (Program.Product_type == "Euro")
                    //{

                    //    if (Program.Draw_day == "Fri")
                    //    {
                    //        fileName = "http://interact.austere.co.in/Form%20filler%20XML/Friday.xml";
                    //    }
                    //    else if (Program.Draw_day == "Tue")
                    //    {
                    //        fileName = "http://interact.austere.co.in/Form%20filler%20XML/Tuesday.xml";
                    //    }
                    //}

                   // string fileData = GetWebsiteHtml(fileName);
                    string fileData = getXmlFromDB;
                    if (fileData != "" || fileData != null)
                    {
                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        writer.Write(fileData);
                        writer.Flush();
                        stream.Position = 0;


                        container container = null;
                        XmlSerializer serializer = new XmlSerializer(typeof(container));

                        StreamReader reader = new StreamReader(stream);
                        container = (container)serializer.Deserialize(reader);
                        reader.Close();


                        Program.mainContainer = container;
                        //Kamlesh


                        The_National_Lottery_Sign_in NLbrowser = new The_National_Lottery_Sign_in();
                        NLbrowser.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No more Ticket to paste.");
                    }

                }
                else
                {
                    MessageBox.Show("This product not available for logged in user. ");
                    navigatetomainmenu();
                }
            }
            else
            {
                lblvalidationmsg.Text = "* Please select any Product. ";
                lblvalidationmsg.Visible = true;
            }
        }

        private string GetWebsiteHtml(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            return result;

        }

        private void rbtnwednesday_CheckedChanged(object sender, EventArgs e)
        {
            lblvalidationmsg.Visible = false;
        }

        private void rbtnfriday_CheckedChanged(object sender, EventArgs e)
        {
            lblvalidationmsg.Visible = false;
        }

        private void rbtnsaturday_CheckedChanged(object sender, EventArgs e)
        {
            lblvalidationmsg.Visible = false;
        }

        private void rbtntuesday_CheckedChanged(object sender, EventArgs e)
        {
            lblvalidationmsg.Visible = false;
        }

        public bool countchecklist()
       {
           try
           {
               if (rbtnfriday.Checked == true)
               {
                   Program.product_Name = "Euro Fri";
                   Program.Product_type = "Euro";
                   Program.Draw_day = "Fri";
               }
               else if (rbtnsaturday.Checked == true)
               {
                   Program.product_Name = "Lotto Sat";
                   Program.Product_type = "Lotto";
                   Program.Draw_day = "Sat";
               }
               else if (rbtntuesday.Checked == true)
               {
                   Program.product_Name = "Euro Tue";
                   Program.Product_type = "Euro";
                   Program.Draw_day = "Tue";
               }
               else if (rbtnwednesday.Checked == true)
               {
                   Program.product_Name = "Lotto Wed";
                   Program.Product_type = "Lotto";
                   Program.Draw_day = "Wed";
               }

               string countpicklist_XML = "<XMLPost><TakeAction value=\"FETCHDATA\"><ProcessName value=\"proc_formfillerPicklistcountXML\"><fieldName value=\"Ticket_account\">" + Program.Ticket_account + "</fieldName><fieldName value=\"Product_type\">" + Program.Product_type + "</fieldName><fieldName value=\"Draw_day\">" + Program.Draw_day + "</fieldName><fieldName value=\"product_Name\">" + Program.product_Name + "</fieldName></ProcessName></TakeAction></XMLPost>";

               string Responsexml=Program.getResponseXML(countpicklist_XML);

               //Set response to proper xml formate.  
               int sPoint = -1;
               sPoint = Responsexml.IndexOf("<XMLPost_Response>");
               Responsexml = Responsexml.Substring(sPoint, Responsexml.Length - sPoint);


               MemoryStream stream = new MemoryStream();
               StreamWriter writer = new StreamWriter(stream);
               writer.Write(Responsexml);
               writer.Flush();
               stream.Position = 0;


               XMLPost_Response XMLPost_Response = null;
               XmlSerializer serializer = new XmlSerializer(typeof(XMLPost_Response));

               StreamReader reader = new StreamReader(stream);
               XMLPost_Response = (XMLPost_Response)serializer.Deserialize(reader);

               int countvalid = Convert.ToInt32(XMLPost_Response.TakeAction.ProcessName.Field[0].Value);
               Program.Product_URL = XMLPost_Response.TakeAction.ProcessName.Field[1].Value;
               Program.product_id = XMLPost_Response.TakeAction.ProcessName.Field[2].Value;
               reader.Close();


               //int countvalid = 1;

              // Program.Product_URL = "https://www.national-lottery.co.uk/games/lotto";

               
              if (countvalid == 1)
              {
                   return true;
              }
              else
              {
                   return false;
              }
           }
           catch
           {
               return false;
           }
           finally
           {
               GC.SuppressFinalize(this);
           }
         
       }
        public void navigatetomainmenu()
        {
            Program.product_Name = "";
            Program.Product_type = "";
            Program.Draw_day = "";
            Program.Product_URL = "";
            Program.product_id = "";
            MainMenu main = new MainMenu();
            main.Show();
            this.Close();
        }

        private void TicketMenu_Load(object sender, EventArgs e)
        {

        }
        #region Declare class for Serialization and DeSerialization

        [Serializable()]
        public class Field
        {
            [XmlAttribute("Name")]
            public string Name { get; set; }

            [XmlAttribute("Value")]
            public string Value { get; set; }
        }


        [Serializable()]
        public class ProcessName
        {
            [XmlAttribute("value")]
            public string value { get; set; }

            [XmlElement("Field")]
            public Field[] Field { get; set; }
        }

        [Serializable()]
        public class TakeAction
        {
            [XmlAttribute("value")]
            public string value { get; set; }

            [XmlElement("ProcessName")]
            public ProcessName ProcessName { get; set; }
        }

        [Serializable()]
        public class sessionid_xml
        {
            [XmlAttribute("value")]
            public string value { get; set; }
        }

        [Serializable()]
        public class XMLPost_Response
        {
            [XmlElement("TakeAction")]
            public TakeAction TakeAction { get; set; }

            [XmlElement("sessionid_xml")]
            public sessionid_xml sessionid_xml { get; set; }
        }




        #endregion

    }

}
