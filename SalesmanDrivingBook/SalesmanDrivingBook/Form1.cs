using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.Sql;

namespace SalesmanDrivingBook
{
    public partial class Form1 : Form
    {
        #region Globals
        static public string sql;
        static public string constr = "dsn=sql; name=sql; pwd=";
        static public OdbcConnection con;
        static public OdbcCommand cmd;
        static public OdbcDataReader rd;
        static public int[] ind = new int[100];
        int meterstartcum, meterendcum = 0;
        Int32 meterdelta = 0;
        string searchtext;
        #endregion


        public Form1()
        {
            InitializeComponent();      
            listBox1.Items.Clear();
            // updlb();
        }

        #region Update list box
        public void updlb()
        {
            int x = 0;
            listBox1.Items.Clear();
            sql = "SELECT * FROM Drivingbook;";
            dbr();
            while (rd.Read())
            {
                listBox1.Items.Add(rd["date"].ToString());
                listBox1.Items.Add(rd["name"].ToString());
                listBox1.Items.Add(rd["posstart"].ToString());
                listBox1.Items.Add(rd["posend"].ToString());
                listBox1.Items.Add(rd["meterstart"].ToString());
                listBox1.Items.Add(rd["meterend"].ToString());
                ind[x++] = Convert.ToInt16(rd["i"]);
            }
            rd.Close();
        }
        #endregion

        private void dbw()
        {
            con = new OdbcConnection(constr);
            cmd = new OdbcCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void dbr()
        {
            con = new OdbcConnection(constr);
            cmd = new OdbcCommand(sql, con);
            con.Open();
            rd = cmd.ExecuteReader();
        }

        #region Creates db tbl
        private void button5_Click(object sender, EventArgs e)
        {
            //create table - DO ONLY ONCE! DONE!

            //{
            //    sql = "CREATE TABLE Drivingbook ( ";
            //    sql += "i int primary key auto_increment, ";
            //    sql += "date date,";
            //    sql += "name varchar(30), ";
            //    sql += "posstart varchar(30), ";
            //    sql += "posend varchar(30), ";
            //    sql += "meterstart int, ";
            //    sql += "meterend int, ";
            //    sql += "meterdelta int);";
            //    dbw();
            //}
        }
        #endregion

        #region Calculate km
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                listBox1.Items.Clear();
                string a = ""; // Hmm ja det er jo ik php så lige et par ekstra strings
                sql = "SELECT meterstart FROM Drivingbook;";
                dbr();
                while (rd.Read())
                {
                    a = rd["meterstart"].ToString();
                    bool parsed = Int32.TryParse(a, out meterstartcum);
                    if (!parsed)
                        Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", a);
                    meterstartcum = meterstartcum + meterstartcum;
                    Console.WriteLine("Meterstart: '{0}' ", meterstartcum);

                }
                rd.Dispose();
                con.Close();

                string b = null;
                sql = "SELECT * FROM `drivingbook` ORDER BY `drivingbook`.`meterend`;";
                dbr();
                while (rd.Read())
                {
                    b = rd["meterend"].ToString();
                    Console.WriteLine("Passing B: {0} ", b);
                    bool parsed = Int32.TryParse(b, out meterendcum);
                    if (!parsed)
                        Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", b);
                    Console.WriteLine("Meterendcum: '{0}' ", meterendcum);
                    meterendcum = meterendcum + meterendcum;
                    Console.WriteLine("Meterendcum + mec: '{0}' ", meterendcum);

                }
                //Console.WriteLine("======================================");
                //Console.WriteLine("Meterstart: '{0}' ", meterstartcum);
                //Console.WriteLine("Meterendcum '{0}' ", meterendcum);
                meterdelta = meterendcum - meterstartcum;
                Console.WriteLine("Meterdelta: {0} ", meterdelta);
                rd.Dispose();
                con.Close();
                listBox1.Items.Add("Total number of kilometers driven:");
                listBox1.Items.Add(meterdelta.ToString());
            }
            catch { }
        }
        #endregion 

        #region Insert / save
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // insert - save
                sql = "INSERT INTO Drivingbook (date, name, posstart,posend, meterstart, meterend) VALUES ('";
                sql += dateTimePicker1.Value.ToString("yyyy-MM-dd");
                sql += "','";
                sql += textBox2.Text; // Name
                sql += "','";
                sql += textBox3.Text; // Pos Start
                sql += "','";
                sql += textBox4.Text; // Pos end
                sql += "','";
                sql += int.Parse(textBox5.Text); // Meter start
                sql += "','";
                sql += int.Parse(textBox6.Text); // Meter end
                sql += "');";
                dbw();
                dateTimePicker1.Value = DateTime.Today;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                updlb();
            }
            catch { }

        }
        #endregion

        #region Search Customer
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            searchtext = textBox7.Text;
            Console.WriteLine("Searchtext {0} ", searchtext);
            // string c = null;
            // c = Convert.ToString(searchtext);
            // c = "abc";
            sql = "SELECT * FROM Drivingbook WHERE name LIKE '%"+searchtext+"%';";
            dbr();
            while (rd.Read())
            {
                listBox1.Items.Add("==================");
                listBox1.Items.Add(rd["name"]);
                listBox1.Items.Add(rd["posstart"]);
                listBox1.Items.Add(rd["posend"]);
                listBox1.Items.Add(rd["meterstart"]);
                listBox1.Items.Add(rd["meterend"]);
                                
            }

        }
        #endregion

        #region Test knap
        private void button6_Click(object sender, EventArgs e)
        {
            string d = null; // så lazy programmering, laver vars ad hoc :D
            d = Convert.ToString(dateTimePicker1.Value);
            Console.WriteLine("Date {0} - {1}", d, dateTimePicker1.Value);
        }
        #endregion
      private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            searchtext = textBox7.Text;
            Console.WriteLine("Searchtext {0} ", searchtext);
            sql = "SELECT * FROM Drivingbook WHERE date LIKE '%" + searchtext + "%';";
            dbr();
            DialogResult result1 = MessageBox.Show("Dates must be entered in the format YYYY-mm-dd eg 2015-05-17. \n Its also possible to search on month only - eg. for December enter 12", "Important Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            while (rd.Read())
            {
                listBox1.Items.Add("=================");
                listBox1.Items.Add(rd["name"]);
                listBox1.Items.Add(rd["date"]);
                listBox1.Items.Add(rd["posstart"]);
                listBox1.Items.Add(rd["posend"]);
                listBox1.Items.Add(rd["meterstart"]);
                listBox1.Items.Add(rd["meterend"]);

            }

        }
        #region Just ignore / empty
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void label8_Click(object sender, EventArgs e)
        {

        }

  
        private void label7_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }

    }

