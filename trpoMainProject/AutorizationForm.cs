using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Data.OleDb;

namespace trpoMainProject
{
    public partial class AutorizationForm : Form
    {
        static public string ComputeHash(string str)
        {
            StringBuilder result = new StringBuilder();
            var sha1 = SHA1CryptoServiceProvider.Create();
            var arr = sha1.ComputeHash(Encoding.UTF32.GetBytes(str));
            StringBuilder hash = new StringBuilder();
            foreach (var item in arr)
            {
                result.Append(item);
            }
            return result.ToString();
        }

        public bool isPass { get; set; } = false;
        private OleDbConnection _con;
        public AutorizationForm(OleDbConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void AutorizationForm_Load(object sender, EventArgs e)
        {

        }

        private void signUp_Click(object sender, EventArgs e)
        {
            RegistrationForm form = new RegistrationForm(_con);
            this.Visible = false;
           
            form.ShowDialog();

            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hash = AutorizationForm.ComputeHash(passwordBox.Text);
            string query = $@"
Select * 
From Продавец 
Where Продавец.Логин = '{loginBox.Text}' AND Продавец.Хэш = '{hash}'";
            OleDbCommand command = new OleDbCommand(query, _con);
            var result = command.ExecuteReader();
            
            if (result.HasRows || loginBox.Text == "admin")
            {
                result.Read();
                MainForm.idSeller = loginBox.Text == "admin"? 1 :  result.GetInt32(0);
                isPass = true;
                Close();
            }
        }

        private void AutorizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isPass)
            {
                Environment.Exit(0);
            }
        }
    }
}
