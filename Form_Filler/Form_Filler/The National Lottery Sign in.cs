using Form_Filler.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class The_National_Lottery_Sign_in : Form
    {
        public The_National_Lottery_Sign_in()
        {
            InitializeComponent();
        }
        private void The_National_Lottery_Sign_in_Load(object sender, EventArgs e)
        {

            txturl.Text = "https://www.national-lottery.co.uk/sign-in";
            Program.chkpageRefresh = "login";
            OpenURLInBrowser(txturl.Text);

            lblmessage.Text = "Loading User details.... Click ‘Sign-In' when ready";
        }
        private void OpenURLInBrowser(string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "http://" + url;
            }
            try
            {
                websignin.Navigate(new Uri(url));
                Program.chkpageRefresh = "login";
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }
        private void websignin_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            try
            {
                if (websignin.DocumentTitle.ToString().Trim() == "")
                {
                    return;
                }

                if (Program.chkpageRefresh == "login")
                {
                     if (issignpage())
                        {
                            websignin.Document.GetElementById("form_username").SetAttribute("Value", Program.tkt_username);
                            websignin.Document.GetElementById("form_password").SetAttribute("Value", Program.tkt_password);

                            lblmessage.Text = "Click on ‘Sign-In'";

                            signinclick();
                        }
                        else
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
                    

                }
                else if (Program.chkpageRefresh == "Home")
                {
                    if (ishomepage())
                        {

                            btngo.Enabled = true;
                            Program.gobuttonflag = "move product page";
                            txturl.Text = Program.Product_URL;
                            lblmessage.Text = "Please Click on 'Go' Button";
                            ((Control)websignin).Enabled = false;
                            Program.chkpageRefresh = "signedin";


                        }
                        else if (isinvaliduser())
                        {
                            txturl.Text = websignin.Url.ToString();
                            Program.chkpageRefresh = "login";
                            lblmessage.Text = "Click ‘Sign-In' when ready";
                            websignin.Document.GetElementById("form_username").SetAttribute("Value", Program.tkt_username);
                            websignin.Document.GetElementById("form_password").SetAttribute("Value", Program.tkt_password);

                        }
                   
                }
                else if (Program.chkpageRefresh == "product page check")
                {
                     if (isvalidproductpage())
                        {
                           
                            Program.chkpageRefresh = "Enable paste button";
                            lblmessage.Text = "Select draw , weeks and Add extra lines & then click on 'Paste' button.";
                            ((Control)websignin).Enabled = true;
                            btnpaste.Enabled = true;
                        }
                        else
                        {
                            lblmessage.Text = "Details are incorrect . Please click on 'Go' ";
                            Program.gobuttonflag = "Go click to main menu";
                            btngo.Enabled = true;

                        }
                    
                }
                else if (Program.chkpageRefresh == "basket URL")
                {
                     if (isvalidproductpage())
                        {
                            Program.chkpageRefresh = "product page check";
                            txturl.Text=Program.Product_URL;
                            OpenURLInBrowser(txturl.Text);
                        }
                     else if (ispayslippage())
                        {
                            

                        }
                    
                }

                
            }
            catch (Exception ex)
            {
                return;
            }
        }
        protected void signinclick()
        {
            HtmlElement signinclick = websignin.Document.GetElementById("login_submit_bttn");
            signinclick.AttachEventHandler("onclick", (sender, args) => OnElementClicked(signinclick, EventArgs.Empty));
        }
        protected void OnElementClicked(object sender, EventArgs args)
        {
            //if (sender.ToString() == "btnlottoplay")
            //{
            //    Program.chkpageRefresh = "basket URL";

            //}
            //else if (sender.ToString() == "signinclick")
            //{
                Program.chkpageRefresh = "Home";
                lblmessage.Text = "";
            //}
            //else if (sender.ToString() == "btnEuroplay")
            //{
            //    Program.chkpageRefresh = "basket URL";
            //}

        }
        public bool issignpage()
        {
            string getpagetitle = "<XMLPost><TakeAction value=\"FETCHDATA\"><ProcessName value=\"proc_formfillergetpagetitle\"><fieldName value=\"Page_Name\">Sign-in</fieldName></ProcessName></TakeAction></XMLPost>";
            string pagetitle = Program.getResponseXML(getpagetitle);

            //Set response to proper xml formate.  
            int sPoint = -1;
            sPoint = pagetitle.IndexOf("<XMLPost_Response>");
            pagetitle = pagetitle.Substring(sPoint, pagetitle.Length - sPoint);


            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(pagetitle);
            writer.Flush();
            stream.Position = 0;


            XMLPost_Response XMLPost_Response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(XMLPost_Response));

            StreamReader reader = new StreamReader(stream);
            XMLPost_Response = (XMLPost_Response)serializer.Deserialize(reader);
            string Page_title = XMLPost_Response.TakeAction.ProcessName.Field[0].Value;
            reader.Close();

            //string Page_title="Sign in | The National Lottery";


            string titles = websignin.DocumentTitle;
            if (Page_title == titles)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ishomepage()
        {

            string titles = websignin.DocumentTitle;

            string getpagetitle = "<XMLPost><TakeAction value=\"FETCHDATA\"><ProcessName value=\"proc_formfillergetpagetitle\"><fieldName value=\"Page_Name\">Home</fieldName></ProcessName></TakeAction></XMLPost>";
            string pagetitle = Program.getResponseXML(getpagetitle);
            //Set response to proper xml formate.  
            int sPoint = -1;
            sPoint = pagetitle.IndexOf("<XMLPost_Response>");
            pagetitle = pagetitle.Substring(sPoint, pagetitle.Length - sPoint);


            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(pagetitle);
            writer.Flush();
            stream.Position = 0;


            XMLPost_Response XMLPost_Response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(XMLPost_Response));

            StreamReader reader = new StreamReader(stream);
            XMLPost_Response = (XMLPost_Response)serializer.Deserialize(reader);
            string Page_title = XMLPost_Response.TakeAction.ProcessName.Field[0].Value;
            reader.Close();
            // string Page_title = "Home | The National Lottery";

            HtmlElementCollection playerids = websignin.Document.GetElementsByTagName("meta");
            bool playerid = false;
            foreach (HtmlElement el in playerids)
            {
                if (el.GetAttribute("name").ToString() == "player-id" && el.GetAttribute("content").ToString() == Program.PlayerID)
                {
                    playerid = true;
                }
            }

            if (playerid == true && titles == Page_title)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public bool isinvaliduser()
        {
            string getpagetitle = "<XMLPost><TakeAction value=\"FETCHDATA\"><ProcessName value=\"proc_formfillergetpagetitle\"><fieldName value=\"Page_Name\">login fail</fieldName></ProcessName></TakeAction></XMLPost>";
            string pagetitle = Program.getResponseXML(getpagetitle);
            //Set response to proper xml formate.  
            int sPoint = -1;
            sPoint = pagetitle.IndexOf("<XMLPost_Response>");
            pagetitle = pagetitle.Substring(sPoint, pagetitle.Length - sPoint);


            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(pagetitle);
            writer.Flush();
            stream.Position = 0;


            XMLPost_Response XMLPost_Response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(XMLPost_Response));

            StreamReader reader = new StreamReader(stream);
            XMLPost_Response = (XMLPost_Response)serializer.Deserialize(reader);
            string Page_title = XMLPost_Response.TakeAction.ProcessName.Field[0].Value;
            reader.Close();

            // string Page_title = "Home | The National Lottery";
            string titles = websignin.DocumentTitle;


            if (titles.Trim() == Page_title)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isvalidproductpage()
        {
            lblmessage.Text = "Checking Data";
            string getpagetitle = "";
            string Page_title = "";
            string titles = websignin.DocumentTitle;
            string page_name = "";

            if (Program.Product_type == "Euro")
            {
                page_name = "Euro product";
            }
            else if (Program.Product_type == "Lotto")
            {
                page_name = "Lotto product";
            }

            getpagetitle = "<XMLPost><TakeAction value=\"FETCHDATA\"><ProcessName value=\"proc_formfillergetpagetitle\"><fieldName value=\"Page_Name\">" + page_name + "</fieldName></ProcessName></TakeAction></XMLPost>";
            string pagetitle = Program.getResponseXML(getpagetitle);
            //Set response to proper xml formate.  
            int sPoint = -1;
            sPoint = pagetitle.IndexOf("<XMLPost_Response>");
            pagetitle = pagetitle.Substring(sPoint, pagetitle.Length - sPoint);


            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(pagetitle);
            writer.Flush();
            stream.Position = 0;


            XMLPost_Response XMLPost_Response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(XMLPost_Response));

            StreamReader reader = new StreamReader(stream);
            XMLPost_Response = (XMLPost_Response)serializer.Deserialize(reader);
            Page_title = XMLPost_Response.TakeAction.ProcessName.Field[0].Value;
            reader.Close();



            HtmlElementCollection playerids = websignin.Document.GetElementsByTagName("meta");
            bool playerid = false;
            foreach (HtmlElement el in playerids)
            {
                if (el.GetAttribute("name").ToString() == "player-id" && el.GetAttribute("content").ToString() == Program.PlayerID)
                {
                    playerid = true;
                }
            }

            string browser_URL = websignin.Url.ToString();
            bool URL = false;

            if (browser_URL == Program.Product_URL)
            {
                URL = true;

            }


            if (titles.Trim() == Page_title && playerid == true && URL == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ispayslippage()
        {
            lblmessage.Text = "Checking Pay slip Page details";
            string page_name = "";
            if (Program.Product_type == "Euro")
            {
                page_name = "Euro payslip";
            }
            else if (Program.Product_type == "Lotto")
            {
                page_name = "Lotto payslip";
            }
            string getpagetitle = "";
            string Page_title = "";
            getpagetitle = "<XMLPost><TakeAction value=\"FETCHDATA\"><ProcessName value=\"proc_formfillergetpagetitle\"><fieldName value=\"Page_Name\">" + page_name + "</fieldName></ProcessName></TakeAction></XMLPost>";
            string pagetitle = Program.getResponseXML(getpagetitle);
            //Set response to proper xml formate.  
            int sPoint = -1;
            sPoint = pagetitle.IndexOf("<XMLPost_Response>");
            pagetitle = pagetitle.Substring(sPoint, pagetitle.Length - sPoint);


            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(pagetitle);
            writer.Flush();
            stream.Position = 0;


            XMLPost_Response XMLPost_Response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(XMLPost_Response));

            StreamReader reader = new StreamReader(stream);
            XMLPost_Response = (XMLPost_Response)serializer.Deserialize(reader);
            Page_title = XMLPost_Response.TakeAction.ProcessName.Field[0].Value;
            reader.Close();
            bool isvalidtitle= false;
            if (Page_title == websignin.DocumentTitle)
            {
                isvalidtitle = true;
            }
           



            HtmlElementCollection playerids = websignin.Document.GetElementsByTagName("meta");
            bool playerid = false;
            bool product = false;
            bool lines = false;
            bool draw_day = false;
            bool week = false;
            bool amont = false;


            foreach (HtmlElement el in playerids)
            {
                if (el.GetAttribute("name").ToString() == "player-id" && el.GetAttribute("content").ToString() == Program.PlayerID)
                {
                    playerid = true;
                }
                else if (el.GetAttribute("name").ToString() == "dbg-game" && el.GetAttribute("content").ToString() == Program.Product_type)
                {
                    product = true;
                }
                else if (el.GetAttribute("name").ToString() == "dbg-nb-lines" && el.GetAttribute("content").ToString() == "7")
                {
                    lines = true;
                }
                else if (el.GetAttribute("name").ToString() == "dbg-draw-dates" && el.GetAttribute("content").ToString() ==Program.Draw_day)
                {
                    draw_day = true;
                }
                else if (el.GetAttribute("name").ToString() == "dbg-weeks" && el.GetAttribute("content").ToString() == "1")
                {
                    week = true;
                }
                else if (el.GetAttribute("name").ToString() == "transaction-amount" && el.GetAttribute("content").ToString() == "14")
                {
                    amont = true;
                }
            }

          







            return false;
        }
        private void btngo_Click(object sender, EventArgs e)
        {
            if (Program.gobuttonflag == "move product page")
            {
                OpenURLInBrowser(txturl.Text);
                lblmessage.Text = "Moving to Product Page";
                Program.chkpageRefresh = "product page check";
                btngo.Enabled = false;
            }
            else if (Program.gobuttonflag == "Go click to main menu")
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
        private void btnpaste_Click(object sender, EventArgs e)
        {

            string drawdayid = "";
            string drawdayid2 = "";
            if (Program.Draw_day == "Sat")
            {
                    drawdayid="lotto_playslip_draw_days_SATURDAY";
                    drawdayid2 = "lotto_playslip_draw_days_WEDNESDAY";
            }
            else if (Program.Draw_day == "Wed")
            {
                     drawdayid= "lotto_playslip_draw_days_WEDNESDAY";
                     drawdayid2 = "lotto_playslip_draw_days_SATURDAY";
            }
            else if (Program.Draw_day == "Fri")
            {
                    drawdayid= "euromillions_playslip_draw_days_FRIDAY";
                    drawdayid2 = "euromillions_playslip_draw_days_TUESDAY";
            }
            else if (Program.Draw_day == "Tue")
            {
                     drawdayid="euromillions_playslip_draw_days_TUESDAY";
                     drawdayid2 = "euromillions_playslip_draw_days_FRIDAY";
            }
            string weekid = "";
            if (Program.Product_type == "Euro")
            {
                weekid = "euromillions_duration";
            }
            else if (Program.Product_type == "Lotto")
            {
                weekid = "lotto_duration";
            }

            try
            {
                HtmlElement lines = websignin.Document.GetElementById("lotto_playslip_line_6_pool_0_col_1");
                string ischecked = websignin.Document.GetElementById(drawdayid).GetAttribute("checked");
                string ischecked2 = websignin.Document.GetElementById(drawdayid2).GetAttribute("checked");
                string isweekselected = websignin.Document.GetElementById(weekid).GetAttribute("Value");




                if (lines != null)
                {
                    if (ischecked == "True" && ischecked2 == "False")
                    {
                        if (isweekselected == "1")
                        {
                            if (isvalidproductpage())
                            {

                                string fileName = "";
                                if (Program.Product_type == "Lotto")
                                {
                                    if (Program.Draw_day == "Wed")
                                    {
                                        fileName = "http://interact.austere.co.in/Form%20filler%20XML/Wednesday.xml";
                                    }
                                    else if (Program.Draw_day == "Sat")
                                    {
                                        fileName = "http://interact.austere.co.in/Form%20filler%20XML/Saturday.xml";
                                    }
                                }
                                else if (Program.Product_type == "Euro")
                                {

                                    if (Program.Draw_day == "Fri")
                                    {
                                        fileName = "http://interact.austere.co.in/Form%20filler%20XML/Friday.xml";
                                    }
                                    else if (Program.Draw_day == "Tue")
                                    {
                                        fileName = "http://interact.austere.co.in/Form%20filler%20XML/Tuesday.xml";
                                    }
                                }

                                string fileData = GetWebsiteHtml(fileName);
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
                                string id = "";
                                string firstpart ="";
                                if(Program.Product_type=="Lotto")
                                {
                                    firstpart="lotto_playslip_line_";
                                }
                                else if (Program.Product_type == "Euro")
                                {
                                    firstpart="euromillions_playslip_line_";
                                }
                                

                                for (int i = 0; i < 7; i++)
                                {

                                    Detail d = container.detail[i];
                                    if (d.ProductID.ToString() == Program.product_id && d.Draw_Day.ToString() == Program.Draw_day)
                                    {

                                        id = firstpart + i.ToString() + "_pool_0_col_0";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", d.Num1.ToString());
                                        
                                        id = firstpart + i.ToString() + "_pool_0_col_1";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", d.Num2.ToString());
                                        id = firstpart + i.ToString() + "_pool_0_col_2";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", d.Num3.ToString());
                                        id = firstpart + i.ToString() + "_pool_0_col_3";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", d.Num4.ToString());
                                        id = firstpart + i.ToString() + "_pool_0_col_4";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", d.Num5.ToString());
                                        id = firstpart + i.ToString() + "_pool_0_col_5";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", d.Num6.ToString());
                                        if (d.Num7.ToString() != "0")
                                        {
                                            id = firstpart + i.ToString() + "_pool_0_col_6";
                                            websignin.Document.GetElementById(id).SetAttribute("Value", d.Num7.ToString());
                                        }
                                        Program.members[i, 0] = d.PickID.ToString();
                                        Program.members[i, 1] = d.Pick_List_ID.ToString();
                                        Program.members[i, 2] = d.ticketID.ToString();
                                        Program.members[i, 3] = d.Num1.ToString();
                                        Program.members[i, 4] = d.Num2.ToString();
                                        Program.members[i, 5] = d.Num3.ToString();
                                        Program.members[i, 6] = d.Num4.ToString();
                                        Program.members[i, 7] = d.Num5.ToString();
                                        Program.members[i, 8] = d.Num6.ToString();
                                        Program.members[i, 9] = d.Num7.ToString();
                                    
                                    }
                                }
                                lblmessage.Text = "Details are correct.. Click on 'Play' button .";
                                btnpaste.Enabled = false;
                              

                            }
                            else
                            {
                                lblmessage.Text = "Details are incorrect.. moving to product URL";
                                txturl.Text = Program.Product_URL;
                                Program.chkpageRefresh = "product page check";
                                OpenURLInBrowser(txturl.Text);


                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select '1' as week.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select draw day as '" + Program.Draw_day + "' Only.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please click on 'Add lines' Link ");
                    return;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
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

        //protected void Lottoplayclick()
        //{
        //    HtmlElement btnlottoplay = websignin.Document.GetElementById("lotto_playslip_confirm");
        //    btnlottoplay.AttachEventHandler("onclick", (sender, args) => OnElementClicked(btnlottoplay, EventArgs.Empty));
        //}
        //protected void Europlayclick()
        //{
        //    HtmlElement btnEuroplay = websignin.Document.GetElementById("lotto_playslip_confirm");
        //    btnEuroplay.AttachEventHandler("onclick", (sender, args) => OnElementClicked(btnEuroplay, EventArgs.Empty));
        //}
      
    }
}
