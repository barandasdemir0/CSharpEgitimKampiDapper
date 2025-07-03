using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpEgitimKampiDapper.Dtos;
using Dapper;

namespace CSharpEgitimKampiDapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Server=BARAN;initial Catalog=EgitimKampiDapper;integrated security=true");
        private async void Form1_Load(object sender, EventArgs e)
        {
            string query = "Select count(*) from TblProduct";
            var productTotal = await connection.QueryFirstOrDefaultAsync<int>(query);
            lbl_toplam.Text = productTotal.ToString();


            string query2 = "Select ProductName from TblProduct where ProductPrice=(select max(ProductPrice) from TblProduct)";
            var maxPriceProductName = await connection.QueryFirstOrDefaultAsync<string>(query2);
            lblmaxkitap.Text = maxPriceProductName.ToString();

            string query3 = "select max(Distinct(ProductCategory)) from TblProduct";
            var maxCategory = await connection.QueryFirstOrDefaultAsync<string>(query3);
            lbl_max_category.Text = maxCategory.ToString();

        }


        private async void btnlist_Click(object sender, EventArgs e)
        {
            string query = "Select * From TblProduct"; // SQL sorgusu
            var values = await connection.QueryAsync<ResultProductDto>(query);// Dapper kullanarak verileri çekiyoruz asenkronun amacı performansı artırmaktır.
            dataGridView1.DataSource = values;
        }

        private async void btnAdd_Click(object sender, EventArgs e) // Butona tıklandığında çalışacak kod bloğu ama async zorunda çünkü Dapper asenkron çalışıyor.
        {
            string query = "Insert Into TblProduct (ProductName,ProductStock,ProductPrice,ProductCategory) Values (@Name,@Stock,@Price,@Category)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", txtad.Text);
            parameters.Add("@Stock", txtstock.Text);
            parameters.Add("@Price", txtprice.Text);
            parameters.Add("@Category", txtkategori.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("eklendi la");

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "Delete From TblProduct Where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", txtId.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("silindi la");

        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update TblProduct set ProductName=@Name, ProductStock=@Stock,ProductPrice=@Price,ProductCategory=@Category where ProductId = @ProductId  ";
            var parameters = new DynamicParameters();// Dapper kullanarak parametreleri tanımlıyoruz. dynamicparameters sınıfı Dapper'ın sağladığı bir sınıftır. 
            parameters.Add("@Name", txtad.Text);
            parameters.Add("@Stock", txtstock.Text);
            parameters.Add("@Price", txtprice.Text);
            parameters.Add("@Category", txtkategori.Text);
            parameters.Add("@ProductId", txtId.Text); // Güncelleme işlemi için ProductId parametresini de ekliyoruz.
            await connection.ExecuteAsync(query, parameters); // Dapper kullanarak güncelleme işlemini gerçekleştiriyoruz.
            MessageBox.Show("güncellendi la");
        }
    }
}
