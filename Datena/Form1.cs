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

namespace Datena
{
    public partial class Form1 : Form
    {
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\forme.mdf;Integrated Security=True";
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        DataSet sDs;
        DataTable sTable;

        SqlCommand sCommand2;
        SqlDataAdapter sAdapter2;
        SqlCommandBuilder sBuilder2;
        DataSet sDs2;
        DataTable sTable2;

        SqlCommand sCommand3;
        SqlDataAdapter sAdapter3;
        SqlCommandBuilder sBuilder3;
        DataSet sDs3;
        DataTable sTable3;

        Form2 f2 = new Form2();

        public Form1()
        {
            InitializeComponent();
        }

        private void Mishanea(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            sCommand2 = new SqlCommand(sql, connection);

            sAdapter2 = new SqlDataAdapter(sCommand2);
            sBuilder2 = new SqlCommandBuilder(sAdapter2);
            connection.Open();
            sDs2 = new DataSet();
            sAdapter2.Fill(sDs2, "Tranzactii");
            sTable2 = sDs2.Tables["Tranzactii"];
            connection.Close();
        }

        private void Mishanea2(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            sCommand3 = new SqlCommand(sql, connection);

            sAdapter3 = new SqlDataAdapter(sCommand3);
            sBuilder3 = new SqlCommandBuilder(sAdapter3);
            connection.Open();
            sDs3 = new DataSet();
            sAdapter3.Fill(sDs3, "Curs");
            sTable3 = sDs3.Tables["Curs"];
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            f2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
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
            dataGridView1.DataSource = sDs.Tables["Tranzactii"];
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string sql = "Select * From Tranzactii;";
            Mishanea(sql);
            dataGridView1.DataSource = sDs2.Tables["Tranzactii"];
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string sql = "Select * From Curs;";
            Mishanea2(sql);
            dataGridView1.DataSource = sDs3.Tables["Curs"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            this.Hide();
            f3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime here = Convert.ToDateTime(textBox1.Text);
            string date = here.ToString("yyyy-MM-dd");
            string sql = "Select * From Tranzactii Where Data>=" + "'" + date + "'";
            Mishanea(sql);
            dataGridView1.DataSource = sDs2.Tables["Tranzactii"];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "Select * From Curs";
            Mishanea2(sql);

            var lines = new List<string>();
            string[] columnNames = sTable3.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = sTable3.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            File.WriteAllLines("Excel.csv", lines);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "Select Sum(Suma_out_lei)*0.02 As Comision_total From Tranzactii";
            Mishanea(sql);
            dataGridView1.DataSource = sDs2.Tables["Tranzactii"];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "Select Valuta_out,Count(Valuta_out) From Tranzactii Group by Valuta_out Order by Count(Valuta_out) Desc";
            Mishanea(sql);
            dataGridView1.DataSource = sDs2.Tables["Tranzactii"];
            MessageBox.Show("Cea mai solicitata valuta este valuta ce este afisata pe primul loc in tabelul obtinut");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DateTime here = Convert.ToDateTime(textBox2.Text);
            string date = here.ToString("yyyy-MM-dd");
            string sql = "Select * From Tranzactii Where Data=" + "'" + date + "' Order by Suma_out_lei Desc";
            Mishanea(sql);
            dataGridView1.DataSource = sDs2.Tables["Tranzactii"];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sql = "Select Data From Tranzactii Where Suma_out_lei=(Select Max(Suma_out_lei) From Tranzactii);";
            Mishanea(sql);
            dataGridView1.DataSource = sDs2.Tables["Tranzactii"];
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DateTime here = DateTime.Now;
            string date = here.ToString("yyyy-MM-dd");
            string sql = "Select * From Tranzactii Where Data=" + "'" + date + "'";
            Mishanea(sql);
            dataGridView1.DataSource = sDs2.Tables["Tranzactii"];
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DateTime here = DateTime.Now;
            string date = here.ToString("yyyy-MM-dd");
            string sql = "Select * From Tranzactii Where (Data<'2018-05-22') And (Data>'2018-04-22')";
            Mishanea(sql);
            dataGridView1.DataSource = sDs2.Tables["Tranzactii"];
        }
    }
}
