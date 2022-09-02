using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ_UYGULAMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Çalışanlarımın ad soyadlarını  birleştirip datagridview'de görüntüleyin
            dataGridView1.DataSource=db.Employees
                .Select(x => new
                {
                    AdSoyad = x.FirstName + " " + x.LastName,

                }).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Adı andrew olan ya da Nancy olna kullanıcıların bilgilerini getirin
            dataGridView1.DataSource = db.Employees
               .Where(x => x.FirstName=="Andrew" || x.FirstName == "Nancy").ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Ülkesi Brazil olan müşterilerin görüntülenmesi
            dataGridView1.DataSource = db.Customers
                .Where(x => x.Country == "Brazil").ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Müşterilerin şehri México D.F. olan ve iletişim kurduğum kişinin  'Owner' olan kşilerin getirilmesi
            dataGridView1.DataSource = db.Customers
                .Where(x => x.City == "México D.F."&& x.ContactTitle=="Owner").ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Ürün adı ve ürünün bulunduğu kategori bilgilerinin getirilmesi
            dataGridView1.DataSource = db.Products
                .Select(x => new
                {
                    x.ProductName,
                    x.Category.CategoryName

                }).ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Contains metodunu kullanarak kategori bilgisi Beverages olan ürünlerin listelenmesi
            dataGridView1.DataSource = db.Products
                .Where(x => x.Category.CategoryName.Contains("Beverages")).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //C ile başlayan ürünleri getiriniz.//Contains Metodu ile
            dataGridView1.DataSource = db.Products
                .Where(x => x.ProductName.Contains("C")).ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Beverages kategorisine sahip olacak ve ürün salep fiyat 30 stock bilgisi 350 olan bir ürün ekleyin ve database'e bu ürünü kaydedin
            //sonrada bu kayıtların hepsini görüntüleyin
            var ID = db.Categories.Where(x => x.CategoryName == "Beverages").Select(x=>x.CategoryID).FirstOrDefault();
            Product urun = new Product();
            urun.ProductName = "Salep";
            urun.UnitPrice = 350.00M;
            urun.UnitsInStock = 30;
            urun.CategoryID = ID;

            db.Products.Add(urun);
            db.SaveChanges();
            dataGridView1.DataSource = db.Products;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //QuantitPerUnit bilgisinde boxes geçen ürünlerimin, productName,ShipCounty,QuantityPerUnit bilgilerini getriniz
            dataGridView1.DataSource = db.Order_Details
               .Where(x => x.Product.QuantityPerUnit.Contains("boxes"))
               .Select(x => new
               {
                   x.Product.ProductName,
                   x.Product.QuantityPerUnit,
                   x.Order.ShipCountry
               }).ToList();
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //CustomerID'si AA içerenleri gösteriniz
            dataGridView1.DataSource = db.Customers
               .Where(x => x.CustomerID.Contains("AA")).ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Dün eklediğiniz adınızı ve soyadınız bulun ondan sonra gidin o bulduğunuz kişinin TitleOfCourtesy bilgisi REİZ olarak değiştiriniz.Eğer siz kendinizi eklemediyseniz gidin kendinizi ekleyin sonra yukarıda yazan işlemi gerçekleştiriniz.
            //Employees
            Employee calısan = new Employee();
            calısan.FirstName = "Ceren";
            calısan.LastName = "Kal";
            calısan.TitleOfCourtesy = "Ms.";
            db.Employees.Add(calısan);
            db.SaveChanges();

            int employeeID = db.Employees.FirstOrDefault(x => x.FirstName == "Ceren").EmployeeID;
            
            calısan.TitleOfCourtesy = "REİZ.";
            db.SaveChanges();
            dataGridView1.DataSource = db.Employees.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //hangi siparişi hangi müşterim vermiş hangi çalışanım siparişi almış
            dataGridView1.DataSource = db.Orders
                 .Select(x => new
                 {
                     x.OrderID,
                     x.Customer.CompanyName,
                     AdSoyad=x.Employee.FirstName+" "+x.Employee.LastName,
               }).ToList();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //Nancy'nin almış olduğu siparişleri listeleyiniz
            dataGridView1.DataSource = db.Orders
                .Where(x => x.EmployeeID == 1).ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //Hangi ürünüm,hangi kategoride ve hangi tedarikçiden elde etmekteyim
            dataGridView1.DataSource = db.Products
                .Select(x => new
                {
                    ÜrünAdı=x.ProductName,
                    Kategori=x.Category.CategoryName,
                    Tedarikçisi=x.Supplier.ContactName
                }).ToList();
                
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Exotic Liquids tarafından tedarik edilen ürünlerimi listeleyiniz.
            dataGridView1.DataSource = db.Products
                .Where(x => x.Supplier.CompanyName == "Exotic Liquids").ToList();
        }
    }
}
