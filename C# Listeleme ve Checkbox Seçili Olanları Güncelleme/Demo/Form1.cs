using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        string constring = ("Data source = localhost; initial catalog = WindowsForm; integrated security = True;");
        SqlConnection cn = null;
        public Form1()
        {
            InitializeComponent();

        }
        
       
        private void Form1_Load(object sender, EventArgs e)
        {
            constring = ConfigurationManager.AppSettings["connectionstrings"];
            cn = new SqlConnection(constring);
            listele();
        }
        public void listele()
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from Kullanici", cn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr["ID"].ToString();
                ekle.SubItems.Add(dr["Name"].ToString());
                ekle.SubItems.Add(dr["Surname"].ToString());
                ekle.SubItems.Add(dr["BirthDate"].ToString());
                listView1.Items.Add(ekle);
            }
            cn.Close();
        }
        private void listBtn_Click(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void updateBtn_Click(object sender, EventArgs e)
        {
            cn.Open();
            try
            {
                int i = 0;
                foreach (ListViewItem item in listView1.Items)
                {

                    if (item.Checked == true)
                    {
                        i++;
                        SqlCommand cmd = new SqlCommand("Update Kullanici set Name='Ali' where ID=" + item.Text, cn);
                        cmd.ExecuteNonQuery();
                    }
                }
                if (i == 0)
                {
                    MessageBox.Show("Kayıt Seçiniz");
                    listView1.Items.Clear();
                    cn.Close();
                }
                else
                {
                    MessageBox.Show("Kayıt Güncellendi.");
                    listView1.Items.Clear();
                    cn.Close();
                }
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
