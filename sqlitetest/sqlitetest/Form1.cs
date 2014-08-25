using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SQLite;

namespace sqlitetest
{
    public partial class Form1 : Form
    {
       
        
        public Form1()
        {
            InitializeComponent();
        }
        private void form1_Load(object sender, EventArgs e)
        {
            input.Enabled = false;
            using (SQLiteConnection conn = new SQLiteConnection("data source=student_information.db"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    //cmd.CommandText = string.Format("insert into student values(" + 111111111+ "," +123+ ")");
                    //cmd.ExecuteNonQuery();
                }
            }
        }

        private void input_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=student_information.db;Pooling=true;FailIfMissing=false"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format("insert into student(studentnum,studentname) values('{0}','{1}')", this.studentnum.Text, this.studentname.Text);
                    cmd.ExecuteNonQuery();
                    this.output_information.Text = "更新数据库成功";
                }
            }           
        }
        
        private void output_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("data source=student_information.db;Pooling=true;FailIfMissing=false"))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "select * from student";
                        cmd.ExecuteNonQuery();
                        SQLiteDataReader rd = cmd.ExecuteReader();
                        StringBuilder wr = new StringBuilder();
                        while (rd.Read())
                        {
                            wr.Append("学号:").Append(rd.GetInt64(0)).Append("\t");
                            wr.Append("姓名:").Append(rd.GetString(1)).Append("\r\n");
                        }
                        rd.Close();
                        this.output_information.Text = wr.ToString();
                    }
                }
            }
           catch(Exception ex) 
            {
               MessageBox.Show("数据库错误！");
           }
                                                                   
        }

        private void clear_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=student_information.db;Pooling=true;FailIfMissing=false"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "delete  from student  ";
                    cmd.Parameters.Clear();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void studentnum_Keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57||e.KeyChar==8)
                e.Handled = false;
            else
                e.Handled = true;
                       
        }

        private void studentnum_Textchanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(this.studentnum.Text) > 100000000 && Convert.ToInt64(this.studentnum.Text) < 999999999)
                {
                    this.studentnum.BackColor = System.Drawing.SystemColors.Window;
                    input.Enabled = true;

                }
                else
                {
                    this.studentnum.BackColor = Color.Red;
                    this.output_information.Text = "学号为9位数，请正确输入！！！";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
           

        }

        private void output_Keypress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        
    }
}
