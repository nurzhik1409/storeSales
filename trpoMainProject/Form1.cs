using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Security.Cryptography;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Diagnostics;

namespace trpoMainProject
{

    public partial class MainForm : Form
    {

        public static int idSeller = 1;
        OleDbConnection _conn;

        public MainForm()
        {
            InitializeComponent();
            changeTemplateGrid();
            dbInit();
            //autariztion();
            showTables();
        }

        

        void testAddOrder()
        {
            AddOrder form = new AddOrder(_conn, idSeller);
            form.FormClosed += (sen, obj) => showTables();
            form.ShowDialog();
        }
        private void autariztion()
        {
            AutorizationForm form = new AutorizationForm(_conn);
            var res = form.ShowDialog();
            
        }

        private void changeTemplateGrid()
        {
            productGrid.RowTemplate.Height = 40;
        }

        private void showTables()
        {
            showOrders();
            showClients();
            showType();
            showProduct();
        }

        private void showProduct()
        {
            string query = @"SELECT Товар.КодТовара, Товар.Название, ВидТовара.НазваниеВида, Товар.Стоимость, Товар.КолНаСкл, Товар.ГодПроизв, Товар.Описание 
FROM ВидТовара INNER JOIN Товар ON ВидТовара.КодВида = Товар.КодВида";
            OleDbCommand com = new OleDbCommand(query, _conn);
            var reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                productGrid.RowCount = 1;
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string typeName = reader.GetString(2);
                    decimal money = reader.GetDecimal(3);
                    int qty = reader.GetInt32(4);
                    DateTime date = reader.GetDateTime(5);
                    string description = reader.GetString(6);
                    productGrid.Rows.Add(id, name, typeName, money, qty, date.ToShortDateString(), description);
                }
            }

            //init on addPanel comboBox
            query = "Select * From ВидТовара";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, _conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            typeProductAddBox.ValueMember = "КодВида";
            typeProductAddBox.DisplayMember = "НазваниеВида";
            typeProductAddBox.DataSource = table;
            
        }

        private void showType()
        {
            string query = "Select КодВида, НазваниеВида, СфераПрименения From  ВидТовара";
            OleDbCommand com = new OleDbCommand(query, _conn);
            var reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                typeGrid.RowCount = 1;
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    typeGrid.Rows.Add(id, name, description);
                }
            }
        }

        private void showClients()
        {
            string query = "Select КодПокупателя, Фамилия & ' ' & Имя & ' ' & Отчество As ФИО, Адрес, ДатаРождения, Телефон From Покупатель";
            OleDbCommand com = new OleDbCommand(query, _conn);
            var reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                clientGrid.RowCount = 1;
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string address = reader.GetString(2);
                    DateTime date = reader.GetDateTime(3);
                    string phone = reader.GetString(4);
                    clientGrid.Rows.Add(id, name, address, date.ToShortDateString(), phone);
                }
            }
        }

        private void showOrders()
        {
            string query = "Select КодЗаказа, Фамилия & ' ' & Имя & ' ' & Отчество As ФИО, ДатаОформления, ОбщаяСтоимость From  Заказ Inner Join Покупатель On Покупатель.КодПокупателя = Заказ.КодПокупателя ";
            OleDbCommand com = new OleDbCommand(query, _conn);
            var reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                ordersGrid.RowCount = 1;
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    DateTime date = reader.GetDateTime(2);
                    object sum = reader.GetValue(3);
                    ordersGrid.Rows.Add(id, name, date.ToShortDateString(), sum);
                }
            }



        }

        private void dbInit()
        {
            try
            {
                _conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\PCShop1.accdb");
                _conn.Open();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fileOpenMenuBtn_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _conn.Close();
        }

        #region MyRegion
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchInGrid(ordersGrid, searchBoxOrder.Text);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        #endregion

        public void SearchInGrid(DataGridView grid, string value)
        {
            var selectedRows = grid.SelectedRows;
            for (int i = 0; i < grid.SelectedRows.Count; i++)
            {
                selectedRows[i].Selected = false;
            }
            if (value != "")
            {
                for (int i = 0; i < grid.RowCount; i++)
                {
                    for (int j = 0; j < grid.ColumnCount; j++)
                    {
                        if (grid[j, i].Value != null)
                        {
                            if (grid[j, i].Value.ToString().Contains(value))
                            {
                                grid.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                selectedRows = grid.SelectedRows;
                for (int i = 0; i < grid.SelectedRows.Count; i++)
                {
                    selectedRows[i].Selected = false;
                }
            }
        }

        private void searchProductBox_TextChanged(object sender, EventArgs e)
        {
            SearchInGrid(productGrid, searchProductBox.Text);
        }

        private void searchTypeBox_TextChanged(object sender, EventArgs e)
        {
            SearchInGrid(typeGrid, searchTypeBox.Text);
        }

        private void clientsPage_Click(object sender, EventArgs e)
        {

        }

        private void searchClientBox_TextChanged(object sender, EventArgs e)
        {
            SearchInGrid(clientGrid, searchClientBox.Text);
        }

        private void filtrationBtn_Click(object sender, EventArgs e)
        {
            string query = $@"Select Заказ.*, Фамилия & ' ' & Имя & ' ' & Отчество As fullname 
From Заказ Inner Join Покупатель On Покупатель.КодПокупателя = Заказ.КодПокупателя
Where Заказ.ОбщаяСтоимость  Between {(int)fromPriceNumeric.Value} And {(int)toPriceNumeric.Value} 
And  InStr(Фамилия & ' ' & Имя & ' ' & Отчество, '{fullNameBoxOrder.Text}')> 0
And ДатаОформления Between @dateFrom And @dateTo";
            OleDbCommand com = new OleDbCommand(query, _conn);
            com.Parameters.Add(new OleDbParameter("@dateFrom", dateFrom.Value.ToShortDateString()));
            com.Parameters.Add(new OleDbParameter("@dateTo", dateTo.Value.ToShortDateString()));
            var reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                ordersGrid.RowCount = 1;
                while (reader.Read()) 
                {
                    ordersGrid.Rows.Add(reader["КодЗаказа"], reader["fullname"], ((DateTime)reader["ДатаОформления"]).ToShortDateString(), reader["ОбщаяСтоимость"]);
                }
            }
            else
            {
                MessageBox.Show("Совпадений не найдено");
            }
        }

        private void recordAddMenuBtn_Click(object sender, EventArgs e)
        {
            if (tables.SelectedTab.Text == "История Заказов")
            {
                testAddOrder();
            }
            if (tables.SelectedTab.Text == "Товары")
            {
                addProductPanel.BringToFront();
            }
            if (tables.SelectedTab.Text == "Виды товаров")
            {
                addTypePanel.BringToFront();
            }
            if (tables.SelectedTab.Text == "Клиенты")
            {
                addClientPanel.BringToFront();
            }
        }

        private void ordersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ordersGrid.Rows[ordersGrid.SelectedCells[0].RowIndex].Cells[0].Value != null)
            {


                int idOrder = (int)ordersGrid.Rows[ordersGrid.SelectedCells[0].RowIndex].Cells[0].Value;
                int row = ordersGrid.SelectedCells[0].RowIndex;
                string query = $@"Select Название, КолТов, КолТов * Стоимость As Summ 
From ЗаказанныйТовар Inner Join Товар On ЗаказанныйТовар.КодТовара = Товар.КодТовара
Where КодЗаказа = {idOrder}";
                var command = new OleDbCommand(query, _conn);
                var reader = command.ExecuteReader();
                string message = $"Номер заказа: {ordersGrid.Rows[row].Cells[0].Value}\n" +
                    $"ФИО клиента: {ordersGrid.Rows[row].Cells[1].Value}\n" +
                    $"Дата оформления: {ordersGrid.Rows[row].Cells[2].Value}\n" +
                    $"Общая сумма: {ordersGrid.Rows[row].Cells[3].Value}\n" +
                    $"Заказанные товары:\n";

                while (reader.Read())
                {
                    message += $"|{reader["Название"], -50}|" +  $"{"Кол-во:" + reader["КолТов"],-15}" +  $"{"Сум:" + reader["Summ"], -10}\n";
                }
                MessageBox.Show(message);
            }
            else
            {
                MessageBox.Show("Выбрана неверная строка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void addProductBtn_Click(object sender, EventArgs e)
        {
            if (nameProductAddBox.Text == "" ||
                typeProductAddBox.Text == "" ||
                priceProductAddBox.Text == "")
            {
                MessageBox.Show("Заполните поля корректно!");
                return;
            }
            string query = $@"Insert Into Товар(Название, КодВида, Стоимость, КолНаСкл, ГодПроизв, Описание)
Values ('{nameProductAddBox.Text}', {typeProductAddBox.SelectedValue}, {priceProductAddBox.Text}, {qtyProductAddBox.Value}, '{dateAddProduct.Value}', '{descriptionAddBox.Text}')";
            OleDbCommand command = new OleDbCommand(query, _conn);
            command.ExecuteNonQuery();

            addProductPanel.SendToBack();
            showProduct();
        }

        private void tables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nameTypeAddBox.Text == "")
            {
                MessageBox.Show("Заполните название вида!");
                return;
            }
            string query = $@"Insert Into ВидТовара(НазваниеВида, СфераПрименения)
Values ('{nameTypeAddBox.Text}', '{descriptionAddBox.Text}')";
            OleDbCommand command = new OleDbCommand(query, _conn);
            command.ExecuteNonQuery();
            showType();
            addTypePanel.SendToBack();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addClientButton_Click(object sender, EventArgs e)
        {
            if (lastNameAddBox.Text == "" ||
                firstNameAddBox.Text == "" ||
                sureNameAddBox.Text == "" ||
                addressAddBox.Text == "" ||
                phoneNumAddBox.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            string query = $@"Insert Into Покупатель(Фамилия, Имя, Отчество, Адрес, ДатаРождения, Телефон)
Values ('{lastNameAddBox.Text}', '{firstNameAddBox.Text}', '{sureNameAddBox.Text}', '{addressAddBox.Text}','{dateBirthAddBox.Value.ToShortDateString()}', '{phoneNumAddBox.Text}')";
            OleDbCommand command = new OleDbCommand(query, _conn);
            command.ExecuteNonQuery();
            showClients();
            addClientPanel.SendToBack();
        }

        private void recordDelMenuBtn_Click(object sender, EventArgs e)
        {
            string query = "";
            if (tables.SelectedTab.Text == "История Заказов")
            {
                query = $"Delete From Заказ Where КодЗаказа = {ordersGrid.CurrentRow.Cells[0].Value}";
            }
            if (tables.SelectedTab.Text == "Товары")
            {
                query = $"Delete From Товар Where КодТовара = {productGrid.CurrentRow.Cells[0].Value}";
            }
            if (tables.SelectedTab.Text == "Виды товаров")
            {
                query = $"Delete From ВидТовара Where КодВида = {typeGrid.CurrentRow.Cells[0].Value}";
            }
            if (tables.SelectedTab.Text == "Клиенты")
            {
                query = $"Delete From Покупатель Where КодПокупателя = {clientGrid.CurrentRow.Cells[0].Value}";
            }
            OleDbCommand com = new OleDbCommand(query, _conn);
            
            MessageBox.Show("Удалено записей: " + com.ExecuteNonQuery());
            showTables();

        }

        private void helpMenuBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программу разработал Гарицкий Алексей.");
        }

        private void fileCloseMenuBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void incQtyBtn_Click(object sender, EventArgs e)
        {
            int tmp;
            if (productGrid.CurrentRow.Cells[0].Value != null && int.TryParse(productGrid.CurrentRow.Cells[0].Value.ToString(), out tmp))
            {
                string query = $"Update Товар Set КолНаСкл = КолНаСкл + {numericUpDown1.Value} Where КодТовара = {productGrid.CurrentRow.Cells[0].Value}";
                OleDbCommand addProduct = new OleDbCommand(query, _conn);
                addProduct.CommandText = query;
                addProduct.ExecuteNonQuery();
                showProduct();
            }
            else
            {
                MessageBox.Show("Не выбрана строка");
            }
        }

        private void ВедомостьВWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".docx";
            save.AddExtension = true;
            save.ShowDialog();
            if (save.FileName != "")
            {
               Task.Run(() =>
               {
                   var wordApp = new Word.Application();
                   Word.Document wordDoc = wordApp.Documents.Add(System.Reflection.Missing.Value, 
                       System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                   int idOrder = (int)ordersGrid.Rows[ordersGrid.SelectedCells[0].RowIndex].Cells[0].Value;
                   int row = ordersGrid.SelectedCells[0].RowIndex;
                   string query = $@"Select Название, КолТов, КолТов * Стоимость As Summ 
From ЗаказанныйТовар Inner Join Товар On ЗаказанныйТовар.КодТовара = Товар.КодТовара
Where КодЗаказа = {idOrder}";
                   var command = new OleDbCommand(query, _conn);
                   var dataAdapter = new OleDbDataAdapter(command);
                   var dataTable = new DataTable();
                   dataAdapter.Fill(dataTable);
                   int rowCount = dataTable.Rows.Count;
                   var reader = command.ExecuteReader();
                   var table = from i in dataTable.AsEnumerable()
                               select i;
                   string message = $"Номер заказа: {ordersGrid.Rows[row].Cells[0].Value}\n" +
                       $"ФИО клиента: {ordersGrid.Rows[row].Cells[1].Value}\n" +
                       $"Дата оформления: {ordersGrid.Rows[row].Cells[2].Value}\n" +
                       $"Общая сумма: {ordersGrid.Rows[row].Cells[3].Value}\n";
                   
                   int countRow = table.Count() + 2;
                   Word.Range range = wordApp.Selection.Range;
                   Object behiavor = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
                   Object autoFitBehiavor = Word.WdAutoFitBehavior.wdAutoFitFixed;
                   wordDoc.Paragraphs[1].Range.Text = message;
                   wordDoc.Tables.Add(wordDoc.Paragraphs[1].Range, countRow, 3, ref behiavor, ref autoFitBehiavor);
                   wordDoc.Tables[1].Cell(1, 1).Range.Text = "Название";
                   wordDoc.Tables[1].Cell(1, 2).Range.Text = "Кол-во товаров";
                   wordDoc.Tables[1].Cell(1, 3).Range.Text = "Общая Стоимость";
                   int index = 2;
                   foreach (var p in table)
                   {
                       wordDoc.Tables[1].Cell(index, 1).Range.Text = p.Field<string>("Название");
                       wordDoc.Tables[1].Cell(index, 2).Range.Text = p.Field<int>("КолТов").ToString();
                       wordDoc.Tables[1].Cell(index, 3).Range.Text = p.Field<decimal>("Summ").ToString();
                       index++;
                   }
                   wordDoc.Tables[1].Cell(countRow , 2).Range.Text = "Итого";
                   wordDoc.Tables[1].Cell(countRow , 3).Range.Text = (from i in table
                                                                        select i.Field<decimal>("Summ")).Sum().ToString();


                   object path = save.FileName;
                   wordDoc.SaveAs2(ref path);
                   wordDoc.Close();
                   System.Diagnostics.Process.Start(fileName: save.FileName);
               });
            }
            
        }

        private void ToPriceNumeric_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ToPriceNumeric_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ToPriceNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                return;
            }
        }

        private void FromPriceNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                return;
            }
        }

        private void ВедомостьВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.AddExtension = true;
            saveDialog.DefaultExt = ".xlsx";
            saveDialog.ShowDialog();
            string path = saveDialog.FileName;
            if (saveDialog.FileName == "")
            {
                return;
            }
            Task.Run(() =>
           {
               var excelApp = new Excel.Application();
               var workBook = excelApp.Workbooks.Add();
               Excel.Worksheet workSheet = workBook.ActiveSheet;
               workSheet.Cells[1, 1] = "Номер";
               workSheet.Columns[1].ColumnWidth = 6;
               workSheet.Cells[1, 2] = "ФИО";
               workSheet.Columns[2].ColumnWidth = 20;
               workSheet.Cells[1, 3] = "Дата";
               workSheet.Columns[3].ColumnWidth = 10;
               workSheet.Cells[1, 4] = "Стоимость заказа";
               workSheet.Columns[4].ColumnWidth = 25;
               for (int i = 0; i < ordersGrid.Rows.Count; i++)
               {
                   workSheet.Cells[i + 2, 1] = ordersGrid[0, i].Value;
                   workSheet.Cells[i + 2, 2] = ordersGrid[1, i].Value;
                   workSheet.Cells[i + 2, 3] = ordersGrid[2, i].Value;
                   workSheet.Cells[i + 2, 4] = ordersGrid[3, i].Value;
               }
               var rng = workSheet.Range[$"D{ordersGrid.Rows.Count + 1}"];
               rng.Formula = $"=SUM(D2:D{ordersGrid.Rows.Count})";
               workSheet.Range[$"C{ordersGrid.Rows.Count + 1}"].Value = "Итого:";
               rng.FormulaHidden = false;
               workBook.SaveAs(path);
               workBook.Close();
               System.Diagnostics.Process.Start(path);
           });
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            showOrders();
        }

        private void ExitAddPanelProductBtn_Click(object sender, EventArgs e)
        {
            addTypePanel.SendToBack();
        }

        private void ExitAddProductPanel_Click(object sender, EventArgs e)
        {
            addProductPanel.SendToBack();
        }

        private void ExitAddClientPanelBtn_Click(object sender, EventArgs e)
        {
            addClientPanel.SendToBack();
        }
    }
}
