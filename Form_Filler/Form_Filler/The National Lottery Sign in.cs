﻿using Form_Filler.Class;
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
                    if (websignin.DocumentTitle.ToString().Trim() == "Sign in | The National Lottery")
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

                }
                else if (Program.chkpageRefresh == "Home")
                {
                    if (websignin.DocumentTitle.ToString().Trim() == "Home | The National Lottery")
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
                   
                }
                else if (Program.chkpageRefresh == "product page check")
                {
                    if (websignin.DocumentTitle.ToString().Trim() == "Play Lotto | Games | The National Lottery" ||websignin.DocumentTitle.ToString().Trim() == "Play EuroMillions | Games | The National Lottery")
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
                }
                else if (Program.chkpageRefresh == "basket URL")
                {
                    if (websignin.DocumentTitle.ToString().Trim() == "Check your play slip | Lotto | The National Lottery" || websignin.DocumentTitle.ToString().Trim() == "Check your play slip | EuroMillions | The National Lottery")
                    {
                        if (ispayslippage())
                        {
                            Program.chkpageRefresh = "pay slip page";
                            lblmessage.Text = "Ticket Datails are correct.. please click on 'Buy Now' button";
                            ((Control)websignin).Enabled = true;
                            // buybutnclick();

                        }
                        else
                        {
                            lblmessage.Text = "Ticket Datails are not correct..";
                            Program.chkpageRefresh = "product page check";
                            for (int i = 0; i < 7; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    Program.picklist[i, j] = "";
                                }
                            }
                            Program.lines = "";
                            Program.weeks = "";
                            Program.amount = "";
                            txturl.Text = Program.Product_URL;
                            OpenURLInBrowser(txturl.Text);
                            ((Control)websignin).Enabled = true;
                            lblmessage.Text = "Select draw , weeks and Add extra lines & then click on 'Paste' button.";
                            
                        }
                    }
                }
                else if (Program.chkpageRefresh == "Buy now")
                {
                    if (isvalidproductpage())
                    {
                      
                    }
                    else if (ispayslippage())
                    {
                    }
                    else
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
            if (((System.Windows.Forms.HtmlElement)(sender)).Id.ToString() == "lotto_playslip_confirm")
            {
                Program.chkpageRefresh = "basket URL";

            }
            else if (((System.Windows.Forms.HtmlElement)(sender)).Id.ToString() == "login_submit_bttn")
            {
                Program.chkpageRefresh = "Home";
                lblmessage.Text = "";
            }
            else if (((System.Windows.Forms.HtmlElement)(sender)).Id.ToString() == "euromillions_playslip_confirm")
            {
                Program.chkpageRefresh = "basket URL";
            }
            else if (((System.Windows.Forms.HtmlElement)(sender)).Id.ToString() == "confirm")
            {
                Program.chkpageRefresh = "Buy now";
            }

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
            try
            {
                lblmessage.Text = "Checking Pay slip Page details and ticket details";
                txturl.Text = websignin.Url.ToString();
                ((Control)websignin).Enabled = false;
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
                bool isvalidtitle = false;
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

                string product_type="";
                if (Program.Product_type == "Lotto")
                {
                    product_type = "lotto";
                }
                else if (Program.Product_type == "Euro")
                {
                    product_type = "euromillions";
                }


                string draw ="";
                if (Program.Draw_day == "Sat")
                {
                    draw = "SATURDAY";
                }
                else if (Program.Draw_day == "Fri")
                {
                    draw = "FRIDAY";
                }
                else if (Program.Draw_day == "Wed")
                {
                    draw = "WEDNESDAY";
                }
                else if (Program.Draw_day == "Tue")
                {
                    draw = "TUESDAY";
                }

                string total = (Convert.ToInt32(Program.lines) * Convert.ToInt32(Program.amount)).ToString()+".00";


                foreach (HtmlElement el in playerids)
                {
                    if (el.GetAttribute("name").ToString() == "player-id" && el.GetAttribute("content").ToString() == Program.PlayerID)
                    {
                        playerid = true;
                    }
                    else if (el.GetAttribute("name").ToString() == "dbg-game" && el.GetAttribute("content").ToString() == product_type)
                    {
                        product = true;
                    }
                    else if (el.GetAttribute("name").ToString() == "dbg-nb-lines" && el.GetAttribute("content").ToString() == Program.lines)
                    {
                        lines = true;
                    }
                    else if (el.GetAttribute("name").ToString() == "dbg-draw-dates" && el.GetAttribute("content").ToString() == draw)
                    {
                        draw_day = true;
                    }
                    else if (el.GetAttribute("name").ToString() == "dbg-weeks" && el.GetAttribute("content").ToString() == Program.weeks)
                    {
                        week = true;
                    }
                    else if (el.GetAttribute("name").ToString() == "transaction-amount" && el.GetAttribute("content").ToString() == total)
                    {
                        amont = true;
                    }
                }

                String[,] palylisttickets = new String[7, 10];
                string id="";
                for (int i = 0; i < 7; i++)
                {
                    id="pool0_board"+i+"_col0";
                    palylisttickets[i, 3] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id="pool0_board"+i+"_col1";
                    palylisttickets[i, 4] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id="pool0_board"+i+"_col2";
                    palylisttickets[i, 5] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id="pool0_board"+i+"_col3";
                    palylisttickets[i, 6] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id="pool0_board"+i+"_col4";
                    palylisttickets[i, 7] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id="pool0_board"+i+"_col5";
                    palylisttickets[i, 8] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                   
                    if (Program.Product_type=="Euro")
                    {
                        id = "pool0_board" + i + "_col6";
                        palylisttickets[i, 9] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    }
                    else
                    {
                        palylisttickets[i, 9] = "00";
                    }
                }

                string isexist = "true";
                bool isticketcorrect = true;
                String [,] storecompare = new String[7,10];

                for (int i = 0; i < 7; i++)
                {
                    for (int k = 3; k < 10; k++)
			        {
                        
			             for (int j =3 ; j < 10; j++)
                            {
                                if (palylisttickets[i, k] == Program.picklist[i, j])
                                {
                                    storecompare[i, k] = "1";
                                }
                                
                            }
                         
                    }
               }

                for (int i = 0; i < 7; i++)
                {
                    for (int j = 3; j < 10; j++)
                    {
                        if (storecompare[i, j] == "")
                        {
                            isexist = "false";
                        }
                    }
                }
                if (isexist == "false")
                {
                    isticketcorrect = false;
                }



                string url = websignin.Url.ToString();


                if (isticketcorrect == true && isvalidtitle == true && playerid == true && product== true && lines==true && draw_day== true && week== true && amont == true)
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
                                        if (Program.Product_type=="Euro")
                                        {
                                            id = firstpart + i.ToString() + "_pool_0_col_6";
                                            websignin.Document.GetElementById(id).SetAttribute("Value", d.Num7.ToString());
                                        }
                                        Program.picklist[i, 0] = d.PickID.ToString();
                                        Program.picklist[i, 1] = d.Pick_List_ID.ToString();
                                        Program.picklist[i, 2] =d.ticketID.ToString();
                                        Program.picklist[i, 3] = d.Num1.ToString();
                                        Program.picklist[i, 4] = d.Num2.ToString();
                                        Program.picklist[i, 5] = d.Num3.ToString();
                                        Program.picklist[i, 6] = d.Num4.ToString();
                                        Program.picklist[i, 7] = d.Num5.ToString();
                                        Program.picklist[i, 8] = d.Num6.ToString();
                                        if (Program.Product_type == "Euro")
                                        {
                                            Program.picklist[i, 9] = d.Num7.ToString();
                                        }
                                        else
                                        {
                                            Program.picklist[i, 9] = "00";
                                        }

                                    }
                                }

                                for (int i = 0; i < 7; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (Program.picklist[i, j].Length == 1)
                                        {
                                            Program.picklist[i, j] = "0" + Program.picklist[i, j];
                                        }

                                    }
                                }

                                Program.weeks = "1";
                                Program.lines = "7";
                                Program.amount = "2";
                                lblmessage.Text = "Details are correct.. Click on 'Play' button .";
                                btnpaste.Enabled = false;
                                if (Program.Product_type == "Lotto")
                                {
                                    Lottoplayclick();
                                }
                                else if (Program.Product_type == "Lotto")
                                {
                                    Europlayclick();
                                }
                                                         

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
        protected void Lottoplayclick()
        {
            HtmlElement btnlottoplay = websignin.Document.GetElementById("lotto_playslip_confirm");
            btnlottoplay.AttachEventHandler("onclick", (sender, args) => OnElementClicked(btnlottoplay, EventArgs.Empty));
        }
        protected void Europlayclick()
        {
            HtmlElement btnEuroplay = websignin.Document.GetElementById("euromillions_playslip_confirm");
            btnEuroplay.AttachEventHandler("onclick", (sender, args) => OnElementClicked(btnEuroplay, EventArgs.Empty));
        }
        protected void buybutnclick()
        {
            HtmlElement btnbuynow = websignin.Document.GetElementById("confirm");
            btnbuynow.AttachEventHandler("onclick", (sender, args) => OnElementClicked(btnbuynow, EventArgs.Empty));
        }

        public void updatepicklist()
        {

        }
        
      
    }
}