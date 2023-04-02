using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TakeHomeW5
{
    public partial class Form1 : Form
    {
        DataTable DTProduct = new DataTable();
        DataTable DTCategory = new DataTable();
        DataTable DTShowProduct = new DataTable();
        public List<string> listID = new List<string>();
        public List<string> listNama = new List<string>();
        public List<Int32> listHarga = new List<Int32>();
        public List<Int32> listStock = new List<Int32>();
        public List<string> ListIDCategoryProd = new List<string>();
        public List<string> ListIDCatergory = new List<string>();
        public List<string> ListNamaCategory = new List<string>();
        public List<string> CBBCategory = new List<string>();
        public List<string> CBBFilter = new List<string>();
        public string edits = "";
        public string edits2 = "";
       
        private DataRow row;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            DTProduct.Columns.Add("ID Product");
            DTProduct.Columns.Add("Nama Product");
            DTProduct.Columns.Add("Harga");
            DTProduct.Columns.Add("Stock");
            DTProduct.Columns.Add("ID Category");

            DTCategory.Columns.Add("ID Category");
            DTCategory.Columns.Add("Nama Category");

          

            listID.Add("J001");
            listID.Add("T001");
            listID.Add("T002");
            listID.Add("R001");
            listID.Add("J002");
            listID.Add("C001");
            listID.Add("C002");
            listID.Add("R002");

            listNama.Add("Jas Hitam");
            listNama.Add("T-Shirt Black Pink");
            listNama.Add("T-Shirt Obsessive");
            listNama.Add("Rok mini");
            listNama.Add("Jeans Biru");
            listNama.Add("Celana Pendek Coklat");
            listNama.Add("Cawat Blink-blink");
            listNama.Add("Rocca Shirt");

            listHarga.Add(100000);
            listHarga.Add(70000);
            listHarga.Add(75000);
            listHarga.Add(82000);
            listHarga.Add(90000);
            listHarga.Add(60000);
            listHarga.Add(100000);
            listHarga.Add(50000);

            listStock.Add(10);
            listStock.Add(20);
            listStock.Add(16);
            listStock.Add(26);
            listStock.Add(5);
            listStock.Add(11);
            listStock.Add(1);
            listStock.Add(8);

            ListIDCategoryProd.Add("C1");
            ListIDCategoryProd.Add("C2");
            ListIDCategoryProd.Add("C2");
            ListIDCategoryProd.Add("C3");
            ListIDCategoryProd.Add("C4");
            ListIDCategoryProd.Add("C4");
            ListIDCategoryProd.Add("C5");
            ListIDCategoryProd.Add("C2");

            ListIDCatergory.Add("C1");
            ListIDCatergory.Add("C2");
            ListIDCatergory.Add("C3");
            ListIDCatergory.Add("C4");
            ListIDCatergory.Add("C5");

            ListNamaCategory.Add("Jas");
            ListNamaCategory.Add("T-Shirt");
            ListNamaCategory.Add("Rok");
            ListNamaCategory.Add("Celana");
            ListNamaCategory.Add("Cawat");
            

            dataGridViewProduct.DataSource = DTProduct;
            dataGridViewCategory.DataSource = DTCategory;

            foreach (string a in listID)
            {
                int b = listID.IndexOf(a);
                DTProduct.Rows.Add(listID[b], listNama[b], listHarga[b], listStock[b], ListIDCategoryProd[b]);
            }

            foreach (String c in ListIDCatergory)
            {
                int d = ListIDCatergory.IndexOf(c);
                DTCategory.Rows.Add(ListIDCatergory[d], ListNamaCategory[d]);
            }

            for (int i = 0; i < ListNamaCategory.Count; i++)
            {
                CBBCategory.Add(ListNamaCategory[i]);
            }

            for (int i = 0; i < ListNamaCategory.Count; i++)
            {
                CBBFilter.Add(ListNamaCategory[i]);
            }

            foreach (string z in ListNamaCategory)
            {
                
                comboBoxFilter.Items.Add(z);
            }

        }
        //product
        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            if (textBoxDetailName.Text == "" && textBoxDetailHarga.Text == "" && textBoxDetailStock.Text == "" && comboBoxCategory.SelectedValue == null)
            {
                MessageBox.Show("Please fill & choose all the columns below", "ERROR" , MessageBoxButtons.OK);
            }
            else
            {
                string hurufPertama = textBoxDetailName.Text.Substring(0, 1).ToUpper();
                int nomor = 0;

                foreach (string id in listID)
                {
                    if (id.StartsWith(hurufPertama))
                    {
                        int nomor2 = int.Parse(id.Substring(1));
                        if (nomor2 > nomor)
                        {
                            nomor = nomor2;
                        }
                    }
                }
                string kode = hurufPertama + (nomor + 1).ToString("D3");
                int indeks = comboBoxCategory.SelectedIndex;
                string idKategori = ListIDCatergory[indeks];

                listID.Add(kode);
                listNama.Add(textBoxDetailName.Text);
                listHarga.Add(int.Parse(textBoxDetailHarga.Text));
                listStock.Add(int.Parse(textBoxDetailStock.Text));
                ListIDCategoryProd.Add(idKategori);

                DataRow row = DTProduct.NewRow();
                row[0] = kode;
                row[1] = textBoxDetailName.Text;
                row[2] = textBoxDetailHarga.Text;
                row[3] = textBoxDetailStock.Text;
                row[4] = idKategori;
                DTProduct.Rows.Add(row);
            }
        }

        private void dataGridViewProduct_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxDetailName.Text = dataGridViewProduct.CurrentRow.Cells["Nama Product"].Value.ToString();
            textBoxDetailHarga.Text = dataGridViewProduct.CurrentRow.Cells["Harga"].Value.ToString();
            textBoxDetailStock.Text = dataGridViewProduct.CurrentRow.Cells["Stock"].Value.ToString();

            int index = ListIDCatergory.FindIndex(a => a.Contains(dataGridViewProduct.CurrentRow.Cells["ID Category"].Value.ToString()));
            string o = ListNamaCategory[index];
            comboBoxCategory.Text = o;

            
        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in DTProduct.Rows)
            {
                int indeks = comboBoxCategory.SelectedIndex;
                int index = listID.FindIndex(a => a.Contains(dataGridViewProduct.CurrentRow.Cells["ID Product"].Value.ToString()));
                string idbaru = "";
                if (row[0].ToString() == listID[index])
                {
                    idbaru = ListIDCatergory[indeks];
                    row[1] = textBoxDetailName.Text;
                    row[2] = textBoxDetailHarga.Text;
                    row[3] = textBoxDetailStock.Text;
                    row[4] = idbaru;
                    if (row[3].ToString() == "0")
                    {
                        DTProduct.Rows.Remove(row);
                        break;
                    }
                }
            }
            textBoxDetailName.Clear();
            textBoxDetailHarga.Clear();
            textBoxDetailStock.Clear();
            comboBoxCategory.SelectedIndex = -1;
            dataGridViewProduct.ClearSelection();

        }
        private void buttonRemoveProduct_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridViewProduct.CurrentCell.RowIndex;
            dataGridViewProduct.Rows.RemoveAt(selectedIndex);

            listID.RemoveAt(selectedIndex);
            listNama.RemoveAt(selectedIndex);
            listHarga.RemoveAt(selectedIndex);
            listStock.RemoveAt(selectedIndex);
            ListIDCategoryProd.RemoveAt(selectedIndex);

            DTProduct.Clear();
            foreach (string b in listID)
            {
                int i = listID.IndexOf(b);
                DTProduct.Rows.Add(listID[i], listNama[i], listHarga[i], listStock[i], ListIDCategoryProd[i]);
            }
            textBoxCategoryName.Clear();
            textBoxDetailHarga.Clear();
            textBoxDetailStock.Clear();
            comboBoxCategory.SelectedIndex = -1;
            dataGridViewProduct.ClearSelection();
        }
        //category
        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            if (textBoxCategoryName.Text == "")
            {
                MessageBox.Show("Please Fill the columns above", "error", MessageBoxButtons.OK);
            }
            else
            {
                int num = 1;
                bool t = false;
                
                {
                    foreach (string b in ListIDCatergory)
                    {
                        if (b.StartsWith("C"))
                        {
                            int num2 = int.Parse(b.Substring(1));
                            if (num2 >= num)
                            {
                                num = num2 + 1;
                            }
                        }
                    }
                    string idcats = "C" + (num).ToString();
                    ListIDCatergory.Add(idcats);
                    dataGridViewCategory.ClearSelection();
                    comboBoxCategory.Items.Add(textBoxCategoryName.Text);
                    comboBoxFilter.Items.Add(textBoxCategoryName.Text);
                    ListNamaCategory.Add(textBoxCategoryName.Text);
                    DTCategory.Rows.Add(idcats, textBoxCategoryName.Text);
                    textBoxCategoryName.Clear();
                }
            }
        }

        private void buttonRemoveCategory_Click(object sender, EventArgs e)
        {;
            int selected = dataGridViewCategory.SelectedRows.Count;
            int removecat = dataGridViewCategory.CurrentCell.RowIndex;
            dataGridViewCategory.Rows.RemoveAt(removecat);
            ListIDCatergory.RemoveAt(dataGridViewCategory.CurrentCell.RowIndex);
        }
        //all & filter
        private void buttonAll_Click(object sender, EventArgs e)
        {
            comboBoxFilter.Enabled = false;
            comboBoxFilter.SelectedIndex = -1;
            dataGridViewProduct.DataSource = DTProduct;
            dataGridViewProduct.ClearSelection();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            comboBoxFilter.Enabled = true;
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewProduct.DataSource = DTShowProduct;
            string masuk = comboBoxFilter.Text;
            DTShowProduct.Clear();
            foreach (DataRow row in DTCategory.Rows)
            {
                if (masuk == row[1].ToString())
                {
                    foreach (DataRow dataRow in DTProduct.Rows)
                    {
                        if (dataRow["ID Category"].ToString() == row[0].ToString())
                        {
                            DTShowProduct.Rows.Add(dataRow["ID Product"], dataRow["Nama Product"], dataRow["Harga"], dataRow["Stock"], dataRow["ID Category"]);
                        }
                    }
                }
            }
            dataGridViewProduct.ClearSelection();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}

