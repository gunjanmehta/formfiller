using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace Form_Filler
{

    public partial class frmlogin : Form
    {


       
        public frmlogin()
        {
            InitializeComponent();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isusername())
                {
                    lblvalidationmsg.Text = " * Please Enter Username";
                    lblvalidationmsg.Visible = true;
                }
                else if (!ispassword())
                {
                    lblvalidationmsg.Text = " * Please Enter Password";
                    lblvalidationmsg.Visible = true;
                }
                else if (isuservalid())
                {
                    MainMenu main = new MainMenu();
                    main.Show();
                    this.Hide();

                }
                else
                {
                    lblvalidationmsg.Text = " * Please Enter Valid Username/password";
                    lblvalidationmsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }


        public bool isuservalid()
        {
            try
            {
                string Login_XML = "<XMLPost><TakeAction value=\"FETCHDATA\"><ProcessName value=\"proc_formfillerloginxml\"><fieldName value=\"username\">" + txtusername.Text + "</fieldName><fieldName value=\"password\">" + txtpassword.Text + "</fieldName><fieldName value=\"session_ID\">" + getsessionid() + "</fieldName></ProcessName></TakeAction></XMLPost>";
                string response = Program.getResponseXML(Login_XML);

                //Set response to proper xml formate.  
                int sPoint = -1;
                sPoint = response.IndexOf("<XMLPost_Response>");
                response = response.Substring(sPoint, response.Length - sPoint);


                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(response);
                writer.Flush();
                stream.Position = 0;


                XMLPost_Response XMLPost_Response = null;
                XmlSerializer serializer = new XmlSerializer(typeof(XMLPost_Response));

                StreamReader reader = new StreamReader(stream);
                XMLPost_Response = (XMLPost_Response)serializer.Deserialize(reader);

                Program.PlayerID = XMLPost_Response.TakeAction.ProcessName.Field[0].Value;
                Program.User_ID=XMLPost_Response.TakeAction.ProcessName.Field[1].Value;
                Program.Ticket_account = XMLPost_Response.TakeAction.ProcessName.Field[2].Value;
                Program.Sign_In_Url = XMLPost_Response.TakeAction.ProcessName.Field[3].Value;
                Program.state = Convert.ToInt32(XMLPost_Response.TakeAction.ProcessName.Field[4].Value);
                int uservalid = Convert.ToInt32(XMLPost_Response.TakeAction.ProcessName.Field[5].Value);
                Program.tkt_username = XMLPost_Response.TakeAction.ProcessName.Field[6].Value;
                Program.tkt_password = XMLPost_Response.TakeAction.ProcessName.Field[7].Value;
                reader.Close();

             
                if (uservalid == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                 return false;
            }
            finally
            {
                GC.SuppressFinalize(this);
            }

         }
        public bool ispassword()
        {
            if (txtpassword.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool isusername()
        {
            if (txtusername.Text == "")
            {
                // var a
                return false;
            }
            else
            {
                return true;
            }
        }
        public string getsessionid()
        {
            Guid g;
            // Create and display the value of two GUIDs.
            g = Guid.NewGuid();
            return g.ToString();

        }

        private void frmlogin_Load(object sender, EventArgs e)
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
