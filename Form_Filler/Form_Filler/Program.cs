using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Form_Filler
{
    static class Program
    {
        public static string Ticket_account { get; set; }
        public static string User_ID { get; set; }
        public static int state { get; set; }
        public static string PlayerID { get; set; }
        public static string Sign_In_Url { get; set; }
        public static string Product_URL { get; set; }
        public static string login_session_Id { get; set; }
        public static string Product_type { get; set; }
        public static string Draw_day { get; set; }
        public static string product_Name { get; set; }
        public static string tkt_username { get; set; }
        public static string tkt_password { get; set; }
        public static string chkpageRefresh { get; set; }
        public static string product_id { get; set; }
        public static string gobuttonflag { get; set; }
        public static String[,] picklist = new String[7, 10];
        public static string lines { get; set; }
        public static string weeks { get; set; }
        public static string amount { get; set; }

        public static string getConnectionString()
        {
            return "Data Source=83.170.82.67;Initial Catalog=lottobytext_staging;User ID=rahul;Password=R91j1Diza;Persist Security Info=False;";
        }

        public static string getResponseXML(string xml)
        {
            string response_XML = "";
            SqlConnection con = new SqlConnection(getConnectionString());
            try
            {
                
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_XML_parsing_joomla";
                cmd.Parameters.AddWithValue("@XMLvalue", xml);
                cmd.Parameters.Add("@TempXMLID", SqlDbType.VarChar, 30);
                cmd.Parameters["@TempXMLID"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                string XMLID = cmd.Parameters["@TempXMLID"].Value.ToString();

                SqlCommand getresponsexml = new SqlCommand("SELECT XML_response FROM tbl_temp_XML_details WHERE XMLID ='" + XMLID + "'", con);
                SqlDataAdapter adresponsexml = new SqlDataAdapter(getresponsexml);
                DataTable dtresponsexml = new DataTable();
                DataSet dsresponsexml = new DataSet();
                adresponsexml.Fill(dsresponsexml);
                dtresponsexml = dsresponsexml.Tables[0];
               
                response_XML = dsresponsexml.Tables[0].Rows[0][0].ToString();
                return response_XML;               
                con.Close();
            }
            catch
            {
                con.Close();
                return response_XML;
            }
            finally
            {
                con.Close();
            }
        }
      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmlogin());
        }
    }
}