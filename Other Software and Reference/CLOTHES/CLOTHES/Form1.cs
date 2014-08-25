using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
using Impinj.OctaneSdk;
using System.Threading;

namespace CLOTHES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          //  CheckForIllegalCrossThreadCalls = false;
        }

        private void B1_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=CLOTHES.db;Pooling=true;FaillMissing=false"))
            {
                conn.Open();

                SQLiteDataAdapter storeDA = new SQLiteDataAdapter("select * from clothes", conn);

                storeDA.FillSchema(DS, SchemaType.Source, "TB");

                DS.Tables["TB"].Rows.Clear();

                storeDA.Fill(DS, "TB");

                foreach (DataRow row in DS.Tables["TB"].Rows)
                {
                    DS.Tables["temp"].Rows.Add(row.ItemArray);
                }
            //    for (int i=0; i < DS.Tables["TB"].Rows.Count; i++)
             //   {
             //       DS.Tables["temp"].Rows.Add(DS.Tables["TB"].Rows[i]);
             //   }

                BS.DataSource = DS.Tables["temp"];

                conn.Close();

            }
        }

        private void B2_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=CLOTHES.db;Pooling=true;FaillMissing=false"))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;

                 //   cmd.CommandText = "create table clothes('品牌' text, '型号' text, '大小' text, '价格' double, '数量' integer, '其他' text);";
                //    cmd.ExecuteNonQuery();
                }


                SQLiteDataAdapter storeDA = new SQLiteDataAdapter("select * from clothes", conn);

                storeDA.Fill(DS, "TB");                     //---------link the dataset table and the database table

                DataTable storetb = DS.Tables["TB"];        //---------create var as datatable style for the adapter's table's property//just for delivering the property

                DbTransaction trans = conn.BeginTransaction();//start a transaction

                //--------------------------------------------------------------------------------------//
                try
                {
                   
                     if(RBA.Checked==true)   
                        {
                            int al = DS.Tables["temp"].Rows.Count;
                            for (int j = 0; j < al; j++)
                            {
                                DataRow datarow = storetb.NewRow();
                                for (int i = 0; i < DGV.Columns.Count; i++)
                                {
                                    datarow[i] = DS.Tables["temp"].Rows[j].ItemArray[i];
                                }
                                storetb.Rows.Add(datarow);

                                storeDA.InsertCommand = new SQLiteCommand("insert into clothes values('" + datarow["brand"] + "','" + datarow["style"] + "','" + datarow["size"] + "','" + datarow["price"] + "','" + datarow["amount"] + "','" + datarow["others"] + "')", conn);

                                storeDA.Update(DS, "TB");
                            }

                        }
                     else if(RBD.Checked==true)
                        {
                          //  DataRow datarow;


                            //  int al = DS.Tables["temp"].Rows.Count;
                            foreach (DataRow datarowtemp in DS.Tables["temp"].Rows)
                            {
                                // datarowtemp = DS.Tables["temp"].Rows[i];
                                /*   for (int s = 0; s < DS.Tables["temp"].Columns.Count; s++)
                                    {
                                        datarowtemp[s] = DS.Tables["temp"].Rows[i].ItemArray[s];
                                    }*/

                           //     for (int j = 0; j < storetb.Rows.Count; j++)
                                foreach( DataRow datarow in storetb.Rows)
                                {
                                   // datarow = storetb.Rows[j];
                                    if (datarow["brand"].ToString() == datarowtemp["brand"].ToString() && datarow["style"].ToString() == datarowtemp["style"].ToString() && datarow["size"].ToString() == datarowtemp["size"].ToString())//datarow["brand"].ToString() == "nike" && datarow["style"].ToString() == "n3115" && datarow["size"].ToString() == "xxxl")//DS.Tables["temp"].Rows[j].ItemArray[0] && datarow[1] == DS.Tables["temp"].Rows[j].ItemArray[1] && datarow[2] == DS.Tables["temp"].Rows[j].ItemArray[2])
                                    {
                                        datarow.Delete();// storetb.Rows[j].Delete();
                                    }
                                    else
                                        continue;
                                    //              storeDA.InsertCommand = new SQLiteCommand("delete from clothes where '品牌'= datarow["品牌"] and '型号'='" + datarow["型号"] + "' and '大小'='" + datarow["大小"] + "'", conn);// + "','" + datarow["型号"] + "','" + datarow["大小"] + "','" + datarow["价格"] + "','" + datarow["数量"] + "','" + datarow["其他"] + "')", conn);
                                    storeDA.DeleteCommand = new SQLiteCommand("delete from clothes where rowid in (select rowid from clothes where brand = '" + datarowtemp["brand"] + "' and style = '" + datarowtemp["style"] + "' and size= '" + datarowtemp["size"] + "' and amount= '" + datarowtemp["amount"] + "' limit 1)", conn);// + "','" + datarow["型号"] + "','" + datarow["大小"] + "','" + datarow["价格"] + "','" + datarow["数量"] + "','" + datarow["其他"] + "')", conn);

                                    if (DS.GetChanges() != null)
                                    {
                                        storeDA.Update(DS, "TB");
                                        DS.AcceptChanges();
                                        break;
                                    }
                                    else
                                        break;

                                }
                            }

                        }
                     else if (RBU.Checked == true)
                     {
                         foreach (DataRow datarowtemp in DS.Tables["temp"].Rows)
                         {
                             foreach (DataRow datarow in storetb.Rows)
                             {
                                 // datarow = storetb.Rows[j];
                                 if (datarow["brand"].ToString() == datarowtemp["brand"].ToString() && datarow["style"].ToString() == datarowtemp["style"].ToString() && datarow["size"].ToString() == datarowtemp["size"].ToString() && datarow["amount"].ToString() == datarowtemp["amount"].ToString() && (datarow["price"].ToString() != datarowtemp["price"].ToString() || datarow["others"].ToString() != datarowtemp["others"].ToString()))//datarow["brand"].ToString() == "nike" && datarow["style"].ToString() == "n3115" && datarow["size"].ToString() == "xxxl")//DS.Tables["temp"].Rows[j].ItemArray[0] && datarow[1] == DS.Tables["temp"].Rows[j].ItemArray[1] && datarow[2] == DS.Tables["temp"].Rows[j].ItemArray[2])
                                 {
                                     datarow.Delete();// storetb.Rows[j].Delete();
                                     storetb.Rows.Add(datarowtemp.ItemArray);
                                 }
                                 else
                                     continue;

                                 storeDA.UpdateCommand = new SQLiteCommand("update clothes set price = '" + datarowtemp["price"] + "', others = '" + datarowtemp["others"] + "' where rowid in (select rowid from clothes where brand = '" + datarowtemp["brand"] + "' and style = '" + datarowtemp["style"] + "' and size= '" + datarowtemp["size"] + "' and amount= '" + datarowtemp["amount"] + "')", conn);//"delete from clothes where rowid in (select rowid from clothes where brand = '" + datarowtemp["brand"] + "' and style = '" + datarowtemp["style"] + "' and size= '" + datarowtemp["size"] + "' limit 1)", conn);// + "','" + datarow["型号"] + "','" + datarow["大小"] + "','" + datarow["价格"] + "','" + datarow["数量"] + "','" + datarow["其他"] + "')", conn);

                                 if (DS.GetChanges() != null)
                                 {
                                     storeDA.Update(DS, "TB");
                                     DS.AcceptChanges();
                                     break;
                                 }
                                 else
                                     break;
                             }
                         }
                     }

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }

                //--------------------------------------------------------------------------------------//

                DS.Tables["TB"].Rows.Clear();
                DS.Tables["temp"].Rows.Clear();

                conn.Close();

            }
        }

     /*   private void BT_Click(object sender, EventArgs e)
        {
            DS.Tables["temp"].Rows.Clear();
            DS.Tables["temp"].Rows.Add("addidas", "a3115", "xxxl", 256, 580.00, null);
            DS.Tables["temp"].Rows.Add("nike", "n3115", "xxxl", 256, 680.00, null);
            DS.Tables["temp"].Rows.Add("报喜鸟", "b3115", "xxxl", 256, 480.00, null);
            BS.DataSource = DS.Tables["temp"];
        }*/

        private void BC_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=CLOTHES.db;Pooling=true;FaillMissing=false"))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                     cmd.Connection = conn;

                     cmd.CommandText = "create table clothes('brand' text, 'style' text, 'size' text, 'price' double, 'amount' text, 'others' text);";
                     cmd.ExecuteNonQuery();
                }

                conn.Close();

            }
        }

        private SpeedwayReader Reader = new SpeedwayReader();

        private void BCONN_Click(object sender, EventArgs e)
        {
        //    SpeedwayReader Reader = new SpeedwayReader();

            if (BCONN.Text == "连接阅读器")
            {
                try
                {
                    Reader.Connect("speedwayr-10-75-87.local");

                    BCONN.Text = "断开阅读器";

                    Reader.ClearSettings();

                    Settings settings = Reader.QueryFactorySettings();

                    settings.Report.IncludeAntennaPortNumber = true;

                    settings.Report.Mode = ReportMode.Individual;

                    settings.Session = 2;

                    Reader.ApplySettings(settings);

                }
                catch (OctaneSdkException ex)
                {
                    MessageBox.Show(ex.Message);//("Octane SDK exception : {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);//("Exception : {0}", ex.Message);
                }
            }
            else
            {
                try
                {
                    Reader.Start();
                    Reader.Stop();
                    Reader.Disconnect();
                    BCONN.Text = "连接阅读器";
                }
                catch (OctaneSdkException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
      /*  private static void tagsreport(object sender, TagsReportedEventArgs args)
        {
            foreach (Tag tag in args.TagReport.Tags)
            {
                Console.WriteLine("EPC : {0} Antenna : {1}", tag.Epc, tag.AntennaPortNumber);
            }
        }*/

        private Thread readtags;

        private void BI_Click(object sender, EventArgs e)
        {
        //    SpeedwayReader Reader = new SpeedwayReader();

            if (BI.Text == "清点货物")
            {
                try
                {
                    DS.Tables["TEMPI"].Rows.Clear();
                    DS.Tables["temp"].Rows.Clear();

                    Reader.TagsReported += new EventHandler<TagsReportedEventArgs>(readtagsfunc);

                    Reader.Start();
                    
                    BI.Text = "停止清点";

             //       readtags = new Thread(new ThreadStart(threadfunc));

              //      readtags.Start();
             //       Thread.Sleep(1);
                }
                catch (OctaneSdkException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
           //         readtags.Abort();
                    Reader.Stop();
                    BI.Text = "清点货物";

                    foreach (DataRow row in DS.Tables["temp"].Rows)
                    {
                        DS.Tables["TEMPI"].Rows.Add(row.ItemArray);
                    }

                }
                catch (OctaneSdkException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            
        }
     /*   private void threadfunc()
        {
            

        }*/
     //   private delegate void DGV();
        private void readtagsfunc(object sender, TagsReportedEventArgs args)
        {
           // Invoke(DGV, null);
            foreach (Tag tag in args.TagReport.Tags)
            {
             //   object[] sss=new object[4];
             //   sss[0] = tag.Epc.Substring(0, 4);
             //   sss[1] = tag.Epc.Substring(4, 2);
             //   sss[1] = tag.Epc.Substring(6, 2);
             //   sss[1] = tag.Epc.Substring(8, 4);
                object sss = tag.Epc;

                DGV.BeginInvoke(new EventHandler(updataui), sss);
                //   byte[] abc = new byte[6];
           //     char aaa = Convert.ToChar(tag.Epc.Substring(4, 1));
          //      char bbb = Convert.ToChar(tag.Epc.Substring(5, 1));
           //     UInt16 sss = Convert.ToUInt16(StringToChar(aaa) + StringToChar(bbb));//(Convert.ToChar(aaa)+Convert.ToChar(bbb));
                /*  //  byte[] abc = new byte[6];
                    abc[0] = Convert.ToByte(Convert.ToInt64(tag.Epc) & 0x003 / 0x001);//Convert.ToByte(Convert.ToUInt32(tag.Epc) & 0x010 / 0x010);
                    abc[1] = Convert.ToByte(Convert.ToInt64(tag.Epc) & 0x00c / 0x004);
                    abc[2] = Convert.ToByte(Convert.ToInt64(tag.Epc) & 0x030 / 0x010);
                    abc[3] = Convert.ToByte(Convert.ToInt64(tag.Epc) & 0x0c0 / 0x040);
                    abc[4] = Convert.ToByte(Convert.ToInt64(tag.Epc) & 0x300 / 0x100);
                    abc[5] = Convert.ToByte(Convert.ToInt64(tag.Epc) & 0xc00 / 0x400);*/
           //     Console.WriteLine("EPC : {0} Antenna : {1}", sss, tag.AntennaPortNumber);

              //  DS.Tables["temp"].Rows.Clear();
     //           DS.Tables["temp"].Rows.Add(tag.Epc.Substring(0, 4), tag.Epc.Substring(4, 2), tag.Epc.Substring(6, 2), 580.00, tag.Epc.Substring(8, 4), null);
         //       DS.Tables["temp"].Rows.Add("nike", "n3115", "xxxl", 256, 680.00, tag.Epc.Substring(0, 4));
              //  DS.Tables["temp"].Rows.Add("报喜鸟", "b3115", "xxxl", 256, 480.00, null);
    //            BS.DataSource = DS.Tables["temp"];
            }
        }
        private void updataui(object ob, System.EventArgs e)
        {
           // ob.ToString().Substring(0, 4);
            int al = DS.Tables["temp"].Rows.Count;
            if (ob.ToString().Length == 12)
            {
                if (al == 0)
                    DS.Tables["temp"].Rows.Add(ob.ToString().Substring(0, 4), ob.ToString().Substring(4, 2), ob.ToString().Substring(6, 2), 580.00, ob.ToString().Substring(8, 4), null);
                else
                {
                    for (int i = (al - 1); i >= 0; i--)
                    {
                        DataRow row = DS.Tables["temp"].Rows[i];
                        if (row["brand"].ToString() != ob.ToString().Substring(0, 4) || row["style"].ToString() != ob.ToString().Substring(4, 2) || row["size"].ToString() != ob.ToString().Substring(6, 2) || row["amount"].ToString() != ob.ToString().Substring(8, 4))
                        {
                            if (i == 0)
                                DS.Tables["temp"].Rows.Add(ob.ToString().Substring(0, 4), ob.ToString().Substring(4, 2), ob.ToString().Substring(6, 2), 580.00, ob.ToString().Substring(8, 4), null);
                            else
                                continue;
                        }
                        else
                            break;

                        BS.DataSource = DS.Tables["temp"];
                    }
                }
            }
            else
                return;
            
         //   BS.DataSource = DS.Tables["temp"];
        }
    /*    private char StringToChar(char ch) //功能：若是在0-F之间的字符，则转换为相应的十六进制字符，否则返回-1
        {
            if ((ch >= '0') && (ch <= '9'))
                return Convert.ToChar(ch - 0x30);
            else if ((ch >= 'A') && (ch <= 'F'))
                return Convert.ToChar(ch - 'A' + 10);
            else if ((ch >= 'a') && (ch <= 'f'))
                return Convert.ToChar(ch - 'a' + 10);
            else return (Convert.ToChar(-1));
        }*/

        private void BCOUNT_Click(object sender, EventArgs e)
        {
            TBCOUNT.Text = Convert.ToInt16(DS.Tables["temp"].Rows.Count).ToString();
        }

        private void BCHECK_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=CLOTHES.db;Pooling=true;FaillMissing=false"))
            {
                conn.Open();

                SQLiteDataAdapter storeDA = new SQLiteDataAdapter("select * from clothes", conn);

                conn.Close();

                storeDA.FillSchema(DS, SchemaType.Source, "TB");

                DS.Tables["TB"].Rows.Clear();

                storeDA.Fill(DS, "TB");

                foreach (DataRow row in DS.Tables["TB"].Rows)
                {
                    for (int j = 0; j < DS.Tables["TEMPI"].Rows.Count; j++)// in DS.Tables["TEMPI"].Rows)
                    {
                        DataRow row2 = DS.Tables["TEMPI"].Rows[j];
                        if (row["brand"].ToString() != row2["brand"].ToString() || row["style"].ToString() != row2["style"].ToString() || row["size"].ToString() != row2["size"].ToString() || row["amount"].ToString() != row2["amount"].ToString())
                        {
                            if (j == (DS.Tables["TEMPI"].Rows.Count - 1))
                            {
                                DS.Tables["CTB"].Rows.Add(row.ItemArray);
                            }
                            else
                                continue;
                        }
                        else
                            break;
                        
                    }
                    
                }
                //    for (int i=0; i < DS.Tables["TB"].Rows.Count; i++)
                //   {
                //       DS.Tables["temp"].Rows.Add(DS.Tables["TB"].Rows[i]);
                //   }


            }

            BS.DataSource = DS.Tables["CTB"];
        }
        
    }
}
