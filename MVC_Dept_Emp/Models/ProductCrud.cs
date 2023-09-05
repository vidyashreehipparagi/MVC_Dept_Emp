using System.Data.SqlClient;

namespace MVC_Dept_Emp.Models
{
    public class ProductCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public ProductCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }


        public IEnumerable<Product> GetProducts()
        {
            List<Product> list = new List<Product>();
            string qry = "select p.*, c.Cname from product p inner join Category c on c.Cid = p.Cid";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(dr["id"]);
                    product.Name = dr["name"].ToString();
                    product.Price = Convert.ToDouble(dr["price"]);
                    product.Imageurl = dr["imageUrl"].ToString();
                    product.Cid = Convert.ToInt32(dr["Cid"]);
                    product.Cname = dr["Cname"].ToString();
                    list.Add(product);
                }
            }
            con.Close();
            return list;
        }
        public Product GetProductById(int id)
        {
            Product product = new Product();
            string qry = "select p.*, c.Cname from product p inner join Category c on c.Cid = p.Cid where p.id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    product.Id = Convert.ToInt32(dr["id"]);
                    product.Name = dr["name"].ToString();
                    product.Price = Convert.ToDouble(dr["price"]);
                    product.Imageurl = dr["imageUrl"].ToString();
                    product.Cid = Convert.ToInt32(dr["Cid"]);
                    product.Cname = dr["Cname"].ToString();
                }
            }
            con.Close();
            return product;
        }


        public int AddProduct(Product product)
        {
            int result = 0;
            string qry = "insert into product values(@name,@price,@imageUrl,@Cid)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@imageUrl", product.Imageurl);
            cmd.Parameters.AddWithValue("@Cid", product.Cid);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;


        }
        public int UpdateProduct(Product product)
        {
            int result = 0;
            string qry = "update product set name=@name,price=@price,imageUrl=@imageUrl,Cid=@Cid where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@imageUrl", product.Imageurl);
            cmd.Parameters.AddWithValue("@Cid", product.Cid);
            cmd.Parameters.AddWithValue("@id", product.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "delete from product where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

    }
}

