using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_Filler
{
    public partial class MainMenu : Form
    {
        
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void btnbuytkt_Click(object sender, EventArgs e)
        {
            rbtnbuytkt.Checked = true;
            rbtnupdatesubpayment.Checked = false;
            rbtnupdatesyndiwin.Checked = false;
            rbtnupdatetranslog.Checked = false;
            lblvalidationmsg.Visible = false;

        }

        private void btnupdatetranslog_Click(object sender, EventArgs e)
        {
            rbtnbuytkt.Checked = false;
            rbtnupdatesubpayment.Checked = false;
            rbtnupdatesyndiwin.Checked = false;
            rbtnupdatetranslog.Checked = true;
            lblvalidationmsg.Visible = false;
        }

        private void btnupdatesyndiwin_Click(object sender, EventArgs e)
        {
            rbtnbuytkt.Checked = false;
            rbtnupdatesubpayment.Checked = false;
            rbtnupdatesyndiwin.Checked = true;
            rbtnupdatetranslog.Checked = false;
            lblvalidationmsg.Visible = false;
        }

        private void btnupdatesubpayment_Click(object sender, EventArgs e)
        {
            rbtnbuytkt.Checked = false;
            rbtnupdatesubpayment.Checked = true;
            rbtnupdatesyndiwin.Checked = false;
            rbtnupdatetranslog.Checked = false;
            lblvalidationmsg.Visible = false;
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
           if (rbtnbuytkt.Checked == true)
            {
                TicketMenu buyticket = new TicketMenu();
                buyticket.Show();
                this.Hide();
            }
           else if(rbtnupdatesubpayment.Checked == true)
           {
               TicketMenu buyticket = new TicketMenu();
                buyticket.Show();
                this.Hide();
           }
           else if(rbtnupdatesyndiwin.Checked == true)
           {
               TicketMenu buyticket = new TicketMenu();
                buyticket.Show();
                this.Hide();
           }
           else if(rbtnupdatetranslog.Checked == true)
           {
               TicketMenu buyticket = new TicketMenu();
                buyticket.Show();
                this.Hide();
           }
           else
           {
               lblvalidationmsg.Text = "* Please select Any Option.";
               lblvalidationmsg.Visible = true;
           }
        }

        private void rbtnbuytkt_CheckedChanged(object sender, EventArgs e)
        {
            lblvalidationmsg.Visible = false;
        }

        private void rbtnupdatetranslog_CheckedChanged(object sender, EventArgs e)
        {
            lblvalidationmsg.Visible = false;
        }

        private void rbtnupdatesyndiwin_CheckedChanged(object sender, EventArgs e)
        {
            lblvalidationmsg.Visible = false;
        }

        private void rbtnupdatesubpayment_CheckedChanged(object sender, EventArgs e)
        {
            lblvalidationmsg.Visible = false;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Program.PlayerID ="";
            Program.User_ID = "";
            Program.Ticket_account = "";
            Program.Sign_In_Url = "";
            Program.state = 0;
            Program.Product_URL ="";
            Program.tkt_username = "";
            Program.tkt_password = "";
            
            frmlogin login = new frmlogin();
            login.Show();
            this.Close();

        }

      
    }
}
