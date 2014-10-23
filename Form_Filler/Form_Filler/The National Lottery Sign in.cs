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

                if (Program.chkpageRefresh == "login" && !(websignin.DocumentTitle.ToString().Trim() == "Here's your ticket | Lotto | The National Lottery" || websignin.DocumentTitle.ToString().Trim() == "Here's your ticket | EuroMillions | The National Lottery"))
                {
                    if (websignin.DocumentTitle.ToString().Trim() == "Sign in | The National Lottery")
                    {
                        if (issignpage())
                        {
                           websignin.Document.GetElementById("form_username").SetAttribute("Value", Program.tkt_username);
                          websignin.Document.GetElementById("form_password").SetAttribute("Value", Program.tkt_password);
                         //   websignin.Document.GetElementById("form_username").SetAttribute("Value","almiramohamed");
                          //  websignin.Document.GetElementById("form_password").SetAttribute("Value","harlie41");
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
                    if (websignin.DocumentTitle.ToString().Trim() == "Play Lotto | Games | The National Lottery" || websignin.DocumentTitle.ToString().Trim() == "Play EuroMillions | Games | The National Lottery")
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


                    ////for (int i = 0; i < websignin.Document.GetElementsByTagName("a").Count; i++)
                    ////{
                    ////    try
                    ////    {
                    ////        if (websignin.Document.GetElementsByTagName("a")[i].InnerText.Trim() == "Add lines")
                    ////        {
                    ////            websignin.Document.GetElementsByTagName("a")[i].Focus();
                    ////            SendKeys.SendWait("{ENTER}");
                    ////            break;
                    ////        }
                    ////    }
                    ////    catch 
                    ////    {
                            
                    ////        continue;
                    ////    }                        
                    ////}

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
                            buybutnclick();

                        }
                        else
                        {
                            lblmessage.Text = "Ticket Datails are not correct..";
                            Program.chkpageRefresh = "product page check";
                            for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                            {
                                for (int j = 0; j < Program.picklist.GetUpperBound(1) + 1; j++)
                                {
                                    Program.picklist[i, j] = "";
                                }
                            }
                            Program.lines = "";
                            Program.weeks = "";
                            Program.amount = "";
                            txturl.Text = Program.Product_URL;
                            btnpaste.Enabled = true;
                            OpenURLInBrowser(txturl.Text);
                            ((Control)websignin).Enabled = true;
                            lblmessage.Text = "Select draw , weeks and Add extra lines & then click on 'Paste' button.";

                        }
                    }
                }
                else if (Program.chkpageRefresh == "Buy now" || websignin.DocumentTitle.ToString().Trim() == "Here's your ticket | Lotto | The National Lottery" || websignin.DocumentTitle.ToString().Trim() == "Here's your ticket | EuroMillions | The National Lottery")
                {
                    if (websignin.DocumentTitle.ToString().Trim() == "Here's your ticket | Lotto | The National Lottery" || websignin.DocumentTitle.ToString().Trim() == "Here's your ticket | EuroMillions | The National Lottery")
                    {
                        if (issuccesspage())
                        {
                            lblmessage.Text = "Ticket Purchase details are correct...Updating Ticket details";
                            ((Control)websignin).Enabled = false;

                            //Mukesh
                            updatePickListData(websignin);
                            //Mukesh

                            SerializeAndDeserialize sd = new SerializeAndDeserialize();
                            if (updatepicklist(sd.picklistXML(websignin)))
                            {

                                lblmessage.Text = "Ticket details updated. click on 'Go' to buy next set of tickets";
                                btngo.Enabled = true;
                                Program.gobuttonflag = "purchase more ticket";
                                txturl.Text = Program.Product_URL;
                                
                            }
                            else
                            {
                                btngo.Enabled = true;
                                lblmessage.Text = "All Ticket details updated. click on 'Go' to move login page";
                                Program.gobuttonflag = "login page";
                            }
                        }
                        else
                        {
                            lblmessage.Text = "Ticket Purchase details are not correct";
                            MessageBox.Show("Unable to collect ticket details. Please capture screenshot & email to support@lottobytext.co.uk");
                            Program.gobuttonflag = "error in ticket purchase";
                            errorupdate();
                           // ((Control)websignin).Enabled = false;
                            btngo.Enabled = true;
                            //browser session expire
                            lblmessage.Text = "Click on 'Go' when ready to move main menu";
                        }

                    }
                }
                //else if (Program.chkpageRefresh == "Enable paste button")
                //{
                //    for (int i = 0; i < websignin.Document.GetElementsByTagName("a").Count; i++)
                //    {
                //        try
                //        {
                //            if (websignin.Document.GetElementsByTagName("a")[i].InnerText.Trim() == "Add lines")
                //            {
                //                websignin.Document.GetElementsByTagName("a")[i].Focus();
                //                SendKeys.SendWait("{ENTER}");
                //                break;
                //            }
                //        }
                //        catch
                //        {

                //            continue;
                //        }
                //    }
                //}



            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void updatePickListData(WebBrowser websignin)
        {
            foreach (var item in Program.mainContainer.detail)
            {
                
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

                string product_type = "";
                if (Program.Product_type == "Lotto")
                {
                    product_type = "lotto";
                }
                else if (Program.Product_type == "Euro")
                {
                    product_type = "euromillions";
                }


                string draw = "";
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

                string total = (Convert.ToInt32(Program.lines) * Convert.ToInt32(Program.amount)).ToString() + ".00";


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
                bool isticketcorrect = false;
                if (Program.Product_type == "Lotto")
                {
                    if (ispaysliplottoticket())
                    {
                        isticketcorrect = true;
                    }

                }
                else if (Program.Product_type == "Euro")
                {
                    if (ispayslipeuroticket())
                    {
                        isticketcorrect = true;
                    }

                }


                if (isticketcorrect == true && isvalidtitle == true && playerid == true && product == true && draw_day == true && week == true)
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
        public bool ispaysliplottoticket()
        {
            try
            {
                String[,] palylisttickets = new String[Program.picklist.GetUpperBound(0) + 1, Program.picklist.GetUpperBound(1) + 1];
                string id = "";
                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {

                    id = "pool0_board" + i + "_col0";
                    palylisttickets[i, 3] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col1";
                    palylisttickets[i, 4] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col2";
                    palylisttickets[i, 5] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col3";
                    palylisttickets[i, 6] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col4";
                    palylisttickets[i, 7] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col5";
                    palylisttickets[i, 8] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    palylisttickets[i, 9] = "00";

                }

                string isexist = "true";
                bool isticketcorrect = true;
                String[,] storecompare = new String[Program.picklist.GetUpperBound(0) + 1, Program.picklist.GetUpperBound(1) + 1];

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int k = 3; k < Program.picklist.GetUpperBound(1) + 1; k++)
                    {

                        for (int j = 3; j < Program.picklist.GetUpperBound(1) + 1; j++)
                        {
                            if (palylisttickets[i, k] == Program.picklist[i, j])
                            {
                                storecompare[i, k] = "1";
                            }

                        }

                    }
                }

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 3; j < Program.picklist.GetUpperBound(1) + 1; j++)
                    {
                        if (storecompare[i, j] == "" || storecompare[i, j] == null)
                        {
                            isexist = "false";
                        }
                    }
                }
                if (isexist == "false")
                {
                    isticketcorrect = false;
                }

                if (isticketcorrect == true)
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
        public bool ispayslipeuroticket()
        {
            try
            {
                String[,] palylisttickets = new String[Program.picklist.GetUpperBound(0) + 1, Program.picklist.GetUpperBound(1) + 1];
                string id = "";
                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {

                    id = "pool0_board" + i + "_col0";
                    palylisttickets[i, 3] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col1";
                    palylisttickets[i, 4] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col2";
                    palylisttickets[i, 5] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col3";
                    palylisttickets[i, 6] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col4";
                    palylisttickets[i, 7] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool1_board" + i + "_col0";
                    palylisttickets[i, 8] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool1_board" + i + "_col1";
                    palylisttickets[i, 9] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();

                }



                string isexist = "true";
                bool isticketcorrect = true;
                String[,] storecompare = new String[Program.picklist.GetUpperBound(0) + 1, Program.picklist.GetUpperBound(1) + 1];

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int k = 3; k < Program.picklist.GetUpperBound(1) + 1; k++)
                    {

                        for (int j = 3; j < Program.picklist.GetUpperBound(1) + 1; j++)
                        {
                            if (palylisttickets[i, k] == Program.picklist[i, j])
                            {
                                storecompare[i, k] = "1";
                            }

                        }

                    }
                }

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 3; j < Program.picklist.GetUpperBound(1) + 1; j++)
                    {
                        if (storecompare[i, j] == "" || storecompare[i, j] == null)
                        {
                            isexist = "false";
                        }
                    }
                }
                if (isexist == "false")
                {
                    isticketcorrect = false;
                }

                if (isticketcorrect == true)
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
        public bool issuccesspage()
        {
            try
            {
                lblmessage.Text = "Checking ticket details....";
                txturl.Text = websignin.Url.ToString();
                ((Control)websignin).Enabled = false;
                string page_name = "";
                if (Program.Product_type == "Lotto")
                {
                    page_name = "Lotto success";
                }
                else if (Program.Product_type == "Euro")
                {
                    page_name = "Euro success";
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

                string product_type = "";
                if (Program.Product_type == "Lotto")
                {
                    product_type = "lotto";
                }
                else if (Program.Product_type == "Euro")
                {
                    product_type = "euromillions";
                }


                string draw = "";
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

                string total = (Convert.ToInt32(Program.lines) * Convert.ToInt32(Program.amount)).ToString() + ".00";


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

                bool isticketcorrect = false;
                if (Program.Product_type == "Lotto")
                {
                    if (issuccesslottoticket())
                    {
                        isticketcorrect = true;
                    }
                }
                else if (Program.Product_type == "Euro")
                {
                    if (issuccesseuroticket())
                    {
                        isticketcorrect = true;
                    }
                }

                if (isticketcorrect == true && isvalidtitle == true && playerid == true && product == true && draw_day == true && week == true)
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
        public bool issuccesslottoticket()
        {
            try
            {
                String[,] palylisttickets = new String[Program.picklist.GetUpperBound(0) + 1, Program.picklist.GetUpperBound(1) + 1];
                string id = "";
                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {

                    id = "pool0_board" + i + "_col0";
                    palylisttickets[i, 3] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col1";
                    palylisttickets[i, 4] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col2";
                    palylisttickets[i, 5] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col3";
                    palylisttickets[i, 6] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col4";
                    palylisttickets[i, 7] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col5";
                    palylisttickets[i, 8] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    palylisttickets[i, 9] = "00";

                }

                string isexist = "true";
                bool isticketcorrect = true;
                String[,] storecompare = new String[Program.picklist.GetUpperBound(0) + 1, Program.picklist.GetUpperBound(1) + 1];

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int k = 3; k < Program.picklist.GetUpperBound(1) + 1; k++)
                    {

                        for (int j = 3; j < Program.picklist.GetUpperBound(1) + 1; j++)
                        {
                            if (palylisttickets[i, k] == Program.picklist[i, j])
                            {
                                storecompare[i, k] = "1";
                            }

                        }

                    }
                }

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 3; j < Program.picklist.GetUpperBound(1) + 1; j++)
                    {
                        if (storecompare[i, j] == "" || storecompare[i, j] == null)
                        {
                            isexist = "false";
                        }
                    }
                }
                if (isexist == "false")
                {
                    isticketcorrect = false;
                }

                if (isticketcorrect == true)
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
        public bool issuccesseuroticket()
        {
            try
            {
                String[,] palylisttickets = new String[Program.picklist.GetUpperBound(0) + 1, Program.picklist.GetUpperBound(1) + 1];
                string id = "";
                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {

                    id = "pool0_board" + i + "_col0";
                    palylisttickets[i, 3] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col1";
                    palylisttickets[i, 4] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col2";
                    palylisttickets[i, 5] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col3";
                    palylisttickets[i, 6] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool0_board" + i + "_col4";
                    palylisttickets[i, 7] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool1_board" + i + "_col0";
                    palylisttickets[i, 8] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                    id = "pool1_board" + i + "_col1";
                    palylisttickets[i, 9] = websignin.Document.GetElementById(id).InnerText.ToString().Trim();
                }

                string isexist = "true";
                bool isticketcorrect = true;
                String[,] storecompare = new String[Program.picklist.GetUpperBound(0) + 1, Program.picklist.GetUpperBound(1) + 1];

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int k = 3; k < Program.picklist.GetUpperBound(1) + 1; k++)
                    {

                        for (int j = 3; j < Program.picklist.GetUpperBound(1) + 1; j++)
                        {
                            if (palylisttickets[i, k] == Program.picklist[i, j])
                            {
                                storecompare[i, k] = "1";
                            }

                        }

                    }
                }

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 3; j < Program.picklist.GetUpperBound(1) + 1; j++)
                    {
                        if (storecompare[i, j] == "" || storecompare[i, j] == null)
                        {
                            isexist = "false";
                        }
                    }
                }
                if (isexist == "false")
                {
                    isticketcorrect = false;
                }

                if (isticketcorrect == true)
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
        public bool collectticketdetails()
        {
            try
            {
                string Ticket_no = websignin.Document.GetElementById("ticket_serial").ToString();
                string Product_name = "";
                if (Program.Product_type == "Euro")
                {
                    Product_name = "EuroMillions";
                }
                else if (Program.Product_type == "Lotto")
                {
                    Product_name = "Lotto";
                }







                return true;
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
            else if (Program.gobuttonflag == "error in ticket purchase")
            {
                Program.gobuttonflag = "";
                Program.product_Name = "";
                Program.Product_type = "";
                Program.Draw_day = "";
                Program.Product_URL = "";
                Program.product_id = "";
                Program.chkpageRefresh = "";
                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < Program.picklist.GetUpperBound(1) + 1; j++)
                    {
                        Program.picklist[i, j] = "";
                    }
                }
                Program.lines = "";
                Program.weeks = "";
                Program.amount = "";
                MainMenu main = new MainMenu();
                main.Show();
                this.Close();


            }
            else if (Program.gobuttonflag == "purchase more ticket")
            {
                Program.gobuttonflag = "";
                Program.chkpageRefresh = "product page check";

                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < Program.picklist.GetUpperBound(1) + 1; j++)
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
                btnpaste.Enabled = true;
            }
            else if (Program.gobuttonflag == "login page")
            {
                Program.Ticket_account = "";
                Program.User_ID = "";
                Program.state = 0;
                Program.PlayerID = "";
                Program.Sign_In_Url = "";
                Program.Product_URL = "";
                Program.login_session_Id = "";
                Program.Product_type = "";
                Program.Draw_day = "";
                Program.product_Name = "";
                Program.tkt_username = "";
                Program.tkt_password = "";
                Program.chkpageRefresh = "";
                Program.product_id = "";
                Program.gobuttonflag = "";
                Program.lines = "";
                Program.weeks = "";
                Program.amount = "";
                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < Program.picklist.GetUpperBound(1) + 1; j++)
                    {
                        Program.picklist[i, j] = "";
                    }
                }
                frmlogin login = new frmlogin();
                login.Show();
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
            string lineIds = "";
            if (Program.Draw_day == "Sat")
            {
                drawdayid = "lotto_playslip_draw_days_SATURDAY";
                drawdayid2 = "lotto_playslip_draw_days_WEDNESDAY";
            }
            else if (Program.Draw_day == "Wed")
            {
                drawdayid = "lotto_playslip_draw_days_WEDNESDAY";
                drawdayid2 = "lotto_playslip_draw_days_SATURDAY";
            }
            else if (Program.Draw_day == "Fri")
            {
                drawdayid = "euromillions_playslip_draw_days_FRIDAY";
                drawdayid2 = "euromillions_playslip_draw_days_TUESDAY";
            }
            else if (Program.Draw_day == "Tue")
            {
                drawdayid = "euromillions_playslip_draw_days_TUESDAY";
                drawdayid2 = "euromillions_playslip_draw_days_FRIDAY";
            }
            string weekid = "";
            if (Program.Product_type == "Euro")
            {
                weekid = "euromillions_duration";
                lineIds = "euromillions_playslip_line_6_pool_1_col_0";
            }
            else if (Program.Product_type == "Lotto")
            {
                weekid = "lotto_duration";
                lineIds = "lotto_playslip_line_6_pool_0_col_1";
            }

            try
            {
                HtmlElement lines = websignin.Document.GetElementById(lineIds);

                if (lines == null)
                {
                    MessageBox.Show("Trying to buy wrong product - Please choose correct product!");
                    Program.Ticket_account = "";
                    Program.User_ID = "";
                    Program.state = 0;
                    Program.PlayerID = "";
                    Program.Sign_In_Url = "";
                    Program.Product_URL = "";
                    Program.login_session_Id = "";
                    Program.Product_type = "";
                    Program.Draw_day = "";
                    Program.product_Name = "";
                    Program.tkt_username = "";
                    Program.tkt_password = "";
                    Program.chkpageRefresh = "";
                    Program.product_id = "";
                    Program.gobuttonflag = "";
                    Program.lines = "";
                    Program.weeks = "";
                    Program.amount = "";
                    for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                    {
                        for (int j = 0; j < Program.picklist.GetUpperBound(1) + 1; j++)
                        {
                            Program.picklist[i, j] = "";
                        }
                    }
                    frmlogin login = new frmlogin();
                    login.Show();
                    this.Close();
                    return;
                }
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
                                string id = "";
                                string firstpart = "";
                                if (Program.Product_type == "Lotto")
                                {
                                    firstpart = "lotto_playslip_line_";
                                }
                                else if (Program.Product_type == "Euro")
                                {
                                    firstpart = "euromillions_playslip_line_";
                                }


                                //** To read all unreadable records.
                                List<Detail> listDetails = Program.mainContainer.detail.Where(s => s.isRead == false).ToList();
                                int eCounter = 7;
                                if (listDetails.Count() < 7)
                                {
                                    eCounter = listDetails.Count();
                                }
                                //**


                                //** PreservesSet picklist array.
                                if (listDetails.Count() != 0)
                                {
                                    Program.picklist = new String[eCounter, Program.picklist.GetUpperBound(1) + 1];
                                }
                                //**
                                

                                for (int i = 0; i < eCounter; i++)
                                {

                                    Detail d = listDetails[i];
                                    d.isRead = true;
                                    d.processed = "Processing";
                                    if (d.ProductID.ToString() == Program.product_id && d.Draw_Day.ToString() == Program.Draw_day)
                                    {

                                        id = firstpart + i.ToString() + "_pool_0_col_0";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", "");
                                        websignin.Document.GetElementById(id).Focus();                                        
                                        pressKey(d.Num1.ToString());

                                        id = firstpart + i.ToString() + "_pool_0_col_1";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", "");
                                        websignin.Document.GetElementById(id).Focus();
                                        pressKey(d.Num2.ToString());

                                        id = firstpart + i.ToString() + "_pool_0_col_2";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", "");
                                        websignin.Document.GetElementById(id).Focus();
                                        pressKey(d.Num3.ToString());

                                        id = firstpart + i.ToString() + "_pool_0_col_3";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", "");
                                        websignin.Document.GetElementById(id).Focus();
                                        pressKey(d.Num4.ToString());

                                        id = firstpart + i.ToString() + "_pool_0_col_4";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", "");
                                        websignin.Document.GetElementById(id).Focus();
                                        pressKey(d.Num5.ToString());
                                        if (Program.Product_type == "Euro")
                                            id = firstpart + i.ToString() + "_pool_1_col_0";
                                        else
                                            id = firstpart + i.ToString() + "_pool_0_col_5";
                                        websignin.Document.GetElementById(id).SetAttribute("Value", "");
                                        websignin.Document.GetElementById(id).Focus();
                                        pressKey(d.Num6.ToString());

                                        if (Program.Product_type == "Euro")
                                        {
                                            //id = firstpart + i.ToString() + "_pool_0_col_6";
                                            id = firstpart + i.ToString() + "_pool_1_col_1";
                                            websignin.Document.GetElementById(id).SetAttribute("Value", "");
                                            websignin.Document.GetElementById(id).Focus();
                                            pressKey(d.Num7.ToString());
                                        }
                                        Program.picklist[i, 0] = d.PickID.ToString();
                                        Program.picklist[i, 1] = d.Pick_List_ID.ToString();
                                        Program.picklist[i, 2] = d.ticketID.ToString();
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

                                        websignin.Document.GetElementById(id).RemoveFocus();
                                    }
                                }

                                //** To balnk text
                                if (eCounter < 7)
                                {
                                    for (int i = eCounter; i < 7; i++)
                                    {
                                        try
                                        {
                                            id = firstpart + i.ToString() + "_pool_0_col_0";
                                            websignin.Document.GetElementById(id).Focus();
                                            pressKey("");
                                            
                                            id = firstpart + i.ToString() + "_pool_0_col_1";
                                            websignin.Document.GetElementById(id).Focus();
                                            pressKey("");

                                            id = firstpart + i.ToString() + "_pool_0_col_2";
                                            websignin.Document.GetElementById(id).Focus();
                                            pressKey("");

                                            id = firstpart + i.ToString() + "_pool_0_col_3";
                                            websignin.Document.GetElementById(id).Focus();
                                            pressKey("");

                                            id = firstpart + i.ToString() + "_pool_0_col_4";
                                            websignin.Document.GetElementById(id).Focus();
                                            pressKey("");

                                            id = firstpart + i.ToString() + "_pool_0_col_5";
                                            websignin.Document.GetElementById(id).Focus();
                                            pressKey("");
                                            SendKeys.SendWait("{TAB}");
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        

                                    }
                                }
                                //**

                                for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
                                {
                                    for (int j = 0; j < Program.picklist.GetUpperBound(1) + 1; j++)
                                    {
                                        if (Program.picklist[i, j].Length == 1)
                                        {
                                            Program.picklist[i, j] = "0" + Program.picklist[i, j];
                                        }

                                    }
                                }

                                Program.weeks = isweekselected;
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
        public void errorupdate()
        {

            lblmessage.Text = "Capturing page source.";
            string source = websignin.DocumentText.ToString();
            source = source.Replace("'", "''");
            string Ticket_Details = "";
            for (int i = 0; i < Program.picklist.GetUpperBound(0) + 1; i++)
            {
                Ticket_Details = Ticket_Details + i + ")";
                for (int j = 0; j < Program.picklist.GetUpperBound(1) + 1; j++)
                {
                    Ticket_Details = Ticket_Details + Program.picklist[i, j];
                }
            }
            lblmessage.Text = "Updating error record.";


            int Player_ID = Convert.ToInt16(Program.PlayerID);
            int Ticket_Account_ID = Convert.ToInt16(Program.Ticket_account);
            string Product = Program.product_Name;
            string Ticket_Account_Username = Program.tkt_username;
            string Page_URL = websignin.Url.ToString();
            string Page_Source = source;
            string Session_ID = Program.login_session_Id;

            string geterrorID = Program.updateerror(Player_ID, Ticket_Account_ID, Product, Ticket_Account_Username, Page_URL, Page_Source, Ticket_Details, Session_ID);
            lblmessage.Text = "Error record captured.";

        }
        public bool updatepicklist(string xml)
        {
            bool flag = false;
            SqlConnection con = new SqlConnection(Program.getConnectionString());
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1200;
                cmd.CommandText = "proc_updategram";
                cmd.Parameters.AddWithValue("@MyXMLVar", xml);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                List<Detail> listDetails = Program.mainContainer.detail.Where(s => s.isRead == false).ToList();
                if (listDetails.Count() == 0)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            catch
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        public void pressKey(string str)
        {
            SendKeys.SendWait("{DEL}");
            SendKeys.SendWait("{DEL}");

            foreach (char c in str)
            {
                SendKeys.SendWait(c.ToString());
            }
        }
    }
}
