using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RDPdemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string address = AddressComboBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            string filename = NameTextBox.Text.Trim()+".rdp";

            AddressComboBox.Items.Add(address);

            var TemplateStr = RDPdemo.Properties.Resources.TemplateRDP;//获取RDP模板字符串
            //用DataProtection加密密码,并转化成二进制字符串
            var pwstr = BitConverter.ToString(DataProtection.ProtectData(Encoding.Unicode.GetBytes(password), ""));
            pwstr = pwstr.Replace("-", "");
            //替换模板里面的关键字符串,生成当前的drp字符串
            var NewStr = TemplateStr.Replace("{#address}", address).Replace("{#username}", username).Replace("{#password}", pwstr);
            //将drp保存到文件，并放在程序目录下，等待使用
            StreamWriter sw = new StreamWriter(filename);
            sw.Write(NewStr);
            sw.Close();
            //利用CMD命令调用MSTSC
            ProcCmd("mstsc " + filename);
        }
        /// <summary>
        /// 执行命令行命令
        /// </summary> 
        /// <param name="cmd"></param>
        /// <returns></returns>
        void ProcCmd(String cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(cmd);
            p.StandardInput.WriteLine("exit");
        }
    }
}
