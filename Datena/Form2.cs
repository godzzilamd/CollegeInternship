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

namespace Datena
{
    public partial class Form2 : Form
    {
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\forme.mdf;Integrated Security=True";
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        DataSet sDs;
        DataTable sTable;
        

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string sql = "Select * FROM Tranzactii";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            sCommand = new SqlCommand(sql, connection);
            sAdapter = new SqlDataAdapter(sCommand);
            sBuilder = new SqlCommandBuilder(sAdapter);
            sDs = new DataSet();
            sAdapter.Fill(sDs, "Tranzactii");
            sTable = sDs.Tables["Tranzactii"];
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow newlinie = sDs.Tables["Tranzactii"].NewRow();

            newlinie["Id"] = textBox1.Text;
            newlinie["Valuta_in"] = textBox2.Text;
            newlinie["Suma_in"] = textBox3.Text;
            newlinie["Valuta_out"] = textBox4.Text;
            newlinie["Suma_out"] = textBox5.Text;
            newlinie["Suma_out_lei"] = textBox6.Text;
            newlinie["Data"] = textBox7.Text;

            sDs.Tables["Tranzactii"].Rows.Add(newlinie);

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            sAdapter.Update(sDs.Tables["Tranzactii"]);
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
        }
    }
}
