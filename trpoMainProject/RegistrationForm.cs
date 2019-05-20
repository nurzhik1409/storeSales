using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace trpoMainProject
{
    public partial class RegistrationForm : Form
    {
        OleDbConnection _con;
        public RegistrationForm(OleDbConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lastNameBox.Text != "" &&
                firstNameBox.Text != "" &&
                sureNameBox.Text != "" &&
                addressBox.Text != "" &&
                loginBox.Text != "" &&
                passwordBox.Text != "" &&
                expNumeric.Value > 0 && expNumeric.Value < 80)
            {
                string hash = AutorizationForm.ComputeHash(passwordBox.Text);
                string query = $@"Insert Into Продавец(Фамилия, Имя, Отчество, Стаж, Разряд, Адрес, Телефон, Логин, Хэш)
Values('{lastNameBox.Text}', '{firstNameBox.Text}', '{sureNameBox.Text}', {(int)expNumeric.Value},
{(int)rankNumeric.Value}, '{addressBox.Text}', '{maskedTextBox1.Text}', '{loginBox.Text}', '{hash}')";
                OleDbCommand command = new OleDbCommand(query, _con);
                var result = command.ExecuteNonQuery();
                MessageBox.Show("Добавлено записей: " + result);
                Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля.");
            }
        }

        private void LastNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsPunctuation(e.KeyChar) || Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void FirstNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsPunctuation(e.KeyChar) || Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void SureNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsPunctuation(e.KeyChar) || Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
