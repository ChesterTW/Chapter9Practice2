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

namespace Chapter9Practice2
{
    public partial class Form1 : Form
    {
        SqlConnection linkToDB;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshEverything();
        }

        private void RefreshEverything()
        {

            linkToDB = OpenConnection();

            if (linkToDB == null) return;

            label1MSG.Text = "資料庫連接成功";

        }

        private SqlConnection OpenConnection()
        {
            try
            {
                string connectionString = GetConnectionString();
                SqlConnection myLocalLink = new SqlConnection(connectionString);
                myLocalLink.Open();
                return myLocalLink;
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料庫連接失敗: " + ex.Message);
                return null;
            }

        }

        private string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(local)\SQLExpress";
            builder.InitialCatalog = "StepSample";
            builder.IntegratedSecurity = true;
            return builder.ConnectionString;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            linkToDB.Close();
            linkToDB.Dispose();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            string SQLCmd = "INSERT INTO Customer (FullName, AnnualFee) OUTPUT INSERTED.ID VALUES ('" +
                textBoxName.Text.Trim() +
                "', " +
                textBoxFee.Text.Trim() + ")";

            SqlCommand dataAction = new SqlCommand(SQLCmd, linkToDB);
            try
            {
                // dataAction.ExecuteNonQuery();
                // int newID = (int)dataAction,ExecuteScalar();
                MessageBox.Show("成功新增ID=" + dataAction.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failure: " + ex.Message);
            }

        }
    }
}