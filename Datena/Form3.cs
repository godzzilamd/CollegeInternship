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
    public partial class Form3 : Form
    {
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\forme.mdf;Integrated Security=True";
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        DataSet sDs;
        DataTable sTable;

        public Form3()
        {
            InitializeComponent();
        }

        private void Mishanea2(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            sCommand = new SqlCommand(sql, connection);

            sAdapter = new SqlDataAdapter(sCommand);
            sBuilder = new SqlCommandBuilder(sAdapter);
            connection.Open();
            sDs = new DataSet();
            sAdapter.Fill(sDs, "Curs");
            sTable = sDs.Tables["Curs"];
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "Update Curs Set CHF=" + float.Parse(textBox1.Text);
            Mishanea2(sql);
            sql = "Update Curs Set EUR=" + float.Parse(textBox2.Text);
            Mishanea2(sql);
            sql = "Update Curs Set GBP=" + float.Parse(textBox3.Text);
            Mishanea2(sql);
            sql = "Update Curs Set RON=" + float.Parse(textBox4.Text);
            Mishanea2(sql);
            sql = "Update Curs Set RUB=" + float.Parse(textBox5.Text);
            Mishanea2(sql);
            sql = "Update Curs Set UAH=" + float.Parse(textBox6.Text);
            Mishanea2(sql);
            sql = "Update Curs Set USD=" + float.Parse(textBox7.Text);
            Mishanea2(sql);

            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string sql = "Select * FROM Curs";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            sCommand = new SqlCommand(sql, connection);
            sAdapter = new SqlDataAdapter(sCommand);
            sBuilder = new SqlCommandBuilder(sAdapter);
            sDs = new DataSet();
            sAdapter.Fill(sDs, "Curs");
            sTable = sDs.Tables["Curs"];
            connection.Close();
        }
    }
}
