using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data.OleDb;

namespace trpoMainProject
{
    public partial class AddOrder : Form
    {
        private OleDbConnection _con;
        private ObservableCollection<Product> products;
        private decimal sum;
        private int idSeller;

        public class Product
        {


            private int id;

            public int Id
            {
                get { return id; }
                set
                {

                    if (value <= 0)
                    {
                        throw new Exception("Данного товара не существует");
                    }
                    id = value;
                }
            }
            private int qty;

            private decimal price;

            public decimal Price
            {
                get { return price; }
                set
                {
                    if (value <= 0)
                    {
                        throw new Exception("Цена не может быть отрицательной или равной нулю!");
                    }
                    price = value;
                }
            }


            public int Qty
            {
                get
                {
                    return qty;
                }
                set
                {
                    if (value <= 0)
                    {
                        throw new Exception("Устанавливаемое значение меньше либо равно нулю.");
                    }
                    else
                    {
                        qty = value;
                    }
                }
            }

            public string Name { get; }

            public Product(int id, int qty, string name, decimal price)
            {
                this.id = id;
                this.qty = qty;
                Name = name;
                this.price = price;
            }

            

        }

        public AddOrder(OleDbConnection con, int idseller)
        {
            InitializeComponent();
            _con = con;
            products = new ObservableCollection<Product>();
            idSeller = idseller;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
            InitBox();
            products = new ObservableCollection<Product>();
            products.CollectionChanged += Products_CollectionChanged;
            sum = 0;
        }

        private void Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            decimal tmpSum = 0;
            productGrid.RowCount = 1;
            foreach (var item in products)
            {
                tmpSum += item.Price * item.Qty;
                productGrid.Rows.Add(item.Id, item.Name, item.Qty, item.Price);
            }
            toolStripStatusLabel1.Text = "Сумма: " + tmpSum.ToString();
            sum = tmpSum;
        }

        private void InitBox()
        {
            //Init product comboBox
            string query = "Select * From Товар";
            OleDbCommand com = new OleDbCommand(query, _con);
            OleDbDataAdapter adapter = new OleDbDataAdapter(com);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            productBox.DataSource = dataSet.Tables[0];
            productBox.DisplayMember = "Название";
            productBox.ValueMember = "КодТовара";

            //Init Client comboBox
            query = "Select КодПокупателя, Фамилия & ' ' & Имя & ' ' & Отчество As ФИО From Покупатель";
            com = new OleDbCommand(query, _con);
            dataSet = new DataSet();
            adapter.SelectCommand = com;
            adapter.Fill(dataSet);
            clientBox.DataSource = dataSet.Tables[0];
            clientBox.ValueMember = "КодПокупателя";
            clientBox.DisplayMember = "ФИО";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idClient = (int)clientBox.SelectedValue;
            int idOrder = 0;
            string addOrderQuery = $"Insert Into Заказ (КодПродавца, КодПокупателя, ОбщаяСтоимость, ДатаОформления) Values ({idSeller}, {idClient}, {sum}, '{dateTimePicker1.Value.ToShortDateString()}' )";
            OleDbCommand comAddOrder = new OleDbCommand(addOrderQuery, _con);
            MessageBox.Show(comAddOrder.ExecuteNonQuery().ToString());

            string idOrderQuery = "SELECT MAX(КодЗаказа) AS Id FROM Заказ";
            OleDbCommand comLastId = new OleDbCommand(idOrderQuery, _con);
            var readerId = comLastId.ExecuteScalar();
            if (readerId != null)
            {
                idOrder = (int)readerId;

                foreach (var item in products)
                {
                    string query = $"Insert Into ЗаказанныйТовар(КодЗаказа, КодТовара, КолТов) Values({idOrder}, {item.Id}, {item.Qty})";

                    OleDbCommand addProduct = new OleDbCommand(query, _con);
                    addProduct.ExecuteNonQuery();
                    query = $"Update Товар Set КолНаСкл = КолНаСкл - {item.Qty} Where КодТовара = {item.Id}";
                    addProduct.CommandText = query;
                    addProduct.ExecuteNonQuery();
                }
                Close();
            }

        }

        private void addProductBtn_Click(object sender, EventArgs e)
        {
            int id = (int)productBox.SelectedValue;
            string query = "Select Название, КолНаСкл, Стоимость  From Товар Where КодТовара = @id And КолНаСкл >= @qty";
            OleDbParameter idParam = new OleDbParameter("@id", id);
            OleDbParameter qtyParam = new OleDbParameter("@qty", (int)qtyNumeric.Value);
            OleDbCommand com = new OleDbCommand(query, _con);
            com.Parameters.Add(idParam);
            com.Parameters.Add(qtyParam);
            Product product;
            var reader = com.ExecuteReader();
            if (reader.HasRows || qtyNumeric.Value == 0)
            {
                reader.Read();
                product = new Product(id, (int)qtyNumeric.Value, reader.GetString(0), reader.GetDecimal(2));
                for (int i = 0; i < products.Count; i++)
                {
                    if (product.Id == products[i].Id)
                    {
                        products[i].Qty += product.Qty;
                        query = $"Select * From Товар Where Товар.КодТовара = {product.Id} And Товар.КолНаСкл >= {products[i].Qty}";
                        com = new OleDbCommand(query, _con);
                        reader = com.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("На складе нет столько товара");
                            products.RemoveAt(i);
                        }
                        Products_CollectionChanged(this, null);
                        return;
                    }
                }
                products.Add(product);
            }
            else
            {
                MessageBox.Show("Такого товара нет на складе в таком количестве");
                InitBox();
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}
