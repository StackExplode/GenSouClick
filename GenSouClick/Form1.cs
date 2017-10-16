using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GenSouClick.Helpers;
using GenSouClick.Enterty;
using GenSouClick.BLL;
using System.IO;
using System.Diagnostics;

namespace GenSouClick
{
    public partial class Form1 : Form
    {
        Timer tm = new Timer();

        public Form1()
        {
            InitializeComponent();
            tm.Interval = 100;
            tm.Tick += tm_Tick;
            //comb_map.SelectedIndex = 0;
            string vvv = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
            string ss = "a";
#else
            string ss = "b";
#endif
            this.Text += "(" + vvv + ss + ")";
        }

        void tm_Tick(object sender, EventArgs e)
        {
            YeShen yy = new YeShen();
            Point p = yy.GetMouseRelativePos();

            //APIHelper.GetCursorPos(out p);
            label4.Text = p.X.ToString() + "," + p.Y.ToString(); ;
            

            int w, h;
            yy.GetYshenBig(out w,out h);
            label2.Text = w.ToString() + "x" + h.ToString();

            label5.Text = ((double)p.X / w).ToString("0.00%") + "," + ((double)p.Y / h).ToString("0.00%");

            Color cl = yy.GetPixColor(p.X, p.Y);
            label1.Text = cl.R.ToString() + ",";
            label1.Text += cl.G.ToString() + ",";
            label1.Text += cl.B.ToString();

            /*Color cl2 = yy.GetAvgColor(p.X, p.Y);
            label19.Text = cl2.R.ToString() + ",";
            label19.Text += cl2.G.ToString() + ",";
            label19.Text += cl2.B.ToString();*/
            label19.Text = "暂不支持！";

            //button2_Click_1(null, null);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DisplayMember = "Name";
            comboBox1.DataSource = Helpers.HelpUrls.AllHelps;
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            APIHelper.RegisterHotKey(Handle, 100, APIHelper.KeyModifiers.Alt, Keys.S);

            AllMaps.Init_AllMaps();
            comb_map.Items.Clear();
            comb_map.DisplayMember = "NameDetail";
            comb_map.ValueMember = "Key";
            var dt =  AllMaps.Data.Select(x =>  x.Value );
            comb_map.DataSource = dt.ToArray();
            comb_map.SelectedIndex = 0;

            AllPlaces.Init_Place();
            AllPoints.Init_Points();

            button7_Click(null, null);
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                Helpers.HelpItem hi = (Helpers.HelpItem)comboBox1.SelectedItem;
                System.Diagnostics.Process.Start(hi.Url);
            }
        }


        void Finish_Maoyu(bool fail, string err)
        {
            
            this.Invoke((MethodInvoker)(() =>
            {
                if (fail)
                    MessageBox.Show(err);
                else
                    MessageBox.Show(this, "成功完成任务！");
                button4.Enabled = true;
                button5.Enabled = button6.Enabled = false;
                Change_Enable(true);
            }));
        }

        ShuaBase mk ;
        private void button4_Click(object sender, EventArgs e)
        {
            int sub_map, n, ot, team, hard;
            string map;
            GetStartConst(out map,out sub_map, out hard, out team, out n, out ot);
            
            lbl_rest.Text = n.ToString();
            mk = new MaoYuKiller(n,map,sub_map,hard,team,ot);            
            mk.onFinished += Finish_Maoyu;
            mk.onOneTurnFinished += () =>
                {
                    this.Invoke((MethodInvoker)(() => { lbl_rest.Text = (mk.Total - mk.Count).ToString(); }));
                };
            mk.Start();
            button4.Enabled = false;
            button5.Enabled = button6.Enabled = true;
            Change_Enable(false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mk.Stop();
            button4.Enabled = false;
            button5.Enabled = button6.Enabled = false;
            Change_Enable(false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mk.Abort();
            button4.Enabled = true;
            button5.Enabled = button6.Enabled = false;
            Change_Enable(true);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tm.Enabled = !tm.Enabled;
            button1.Text = tm.Enabled.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Game gm = new Game();
            //gm.Click_Taiji();
            label3.Text = gm.GetCurrPlaceName();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            YeShen yy = new YeShen();
            double x = Convert.ToDouble(textBox1.Text);
            double y = Convert.ToDouble(textBox2.Text);
            Color cl = yy.GetPixColor(x, y);
            label6.Text = cl.R.ToString() + ",";
            label6.Text += cl.G.ToString() + ",";
            label6.Text += cl.B.ToString();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Change_Enable(bool en)
        {
            if (en)
            {
                button4.Text = "简单刷";
                btn_scr.Text = "刷脚本";
            }
            else
            {
                btn_scr.Text = button4.Text = "正在刷...";
            }
            groupBox2.Enabled = en;
            groupBox3.Enabled = en;
            groupBox4.Enabled = en;
            textBox3.Enabled = en;
            txt_out.Enabled = en;
            btn_scr.Enabled = en;
            groupBox5.Enabled = en;
            comboBox2.Enabled = en;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://bbs.dahexie365.com");
        }

        private void GetStartConst(out string map,out int submap, out int hard,out int team, out int n, out int ot)
        {
            submap = 0;
            hard = 1;
            team = 0;
            map = ((MapEnterty)comb_map.SelectedItem).Key;
            foreach (Control ct in groupBox2.Controls)
            {
                if (ct is RadioButton)
                {
                    if (((RadioButton)ct).Checked)
                    {
                        submap = Convert.ToInt16(ct.Tag);
                        break;
                    }
                }
            }
            foreach (Control ct in groupBox3.Controls)
            {
                if (ct is RadioButton)
                {
                    if (((RadioButton)ct).Checked)
                    {
                        hard = Convert.ToInt16(ct.Tag);
                        break;
                    }
                }
            }
            foreach (Control ct in groupBox4.Controls)
            {
                if (ct is RadioButton)
                {
                    if (((RadioButton)ct).Checked)
                    {
                        team = Convert.ToInt16(ct.Tag);
                        break;
                    }
                }
            }

            n = Convert.ToInt16(textBox3.Text);
            ot = Convert.ToInt16(txt_out.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认要关闭吗？如果您正在运行脚本可能出现意想不到的结果",
                "提示",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dr == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
               
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            APIHelper.UnregisterHotKey(Handle, 100);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:
                            button1_Click_1(null, null);        
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        
        public void WriteLog(string s, int lv)
        {
            LogHelper lh = new LogHelper(this.rich_log);
            this.Invoke((MethodInvoker)(() => { lh.Write(s, lv); }));        
        }

        private void btn_scr_Click(object sender, EventArgs e)
        {
            mk = new ShuaLua("./Script/"+comboBox2.SelectedItem.ToString(), WriteLog);
            mk.onFinished += (a,b) => 
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    button4.Enabled = true;
                    Change_Enable(true);
                    button5.Enabled = button6.Enabled = false;
                }));
                
            };
            ShuaLua sl = (ShuaLua)mk;
            sl.SetLable = (a) =>
            {
                this.Invoke((MethodInvoker)(() => { lbl_rest.Text = a.ToString(); }));
            };
            sl.ShowMsg = (a) =>
            {
                this.Invoke((MethodInvoker)(() => {MessageBox.Show(a); }));
            };
            button5.Enabled = button6.Enabled = true;
            button4.Enabled = false;
            Change_Enable(false);
            mk.Start();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rich_log.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string [] strs = Directory.GetFiles("./Script");
            comboBox2.Items.Clear();
            foreach (string s in strs)
            {
                comboBox2.Items.Add(Path.GetFileName(s));
            }
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("file:///"+ Path.GetDirectoryName(Application.ExecutablePath)+"/Document");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:admin@dahexie365.com");
        }

        

        
    }
}
