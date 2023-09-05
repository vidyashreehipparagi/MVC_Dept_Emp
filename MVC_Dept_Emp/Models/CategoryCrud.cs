using System.Data.SqlClient;

namespace MVC_Dept_Emp.Models
{
    public class CategoryCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public CategoryCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }


        public int AddCategory(Category category)
        {
            int result = 0;
            string str = "insert into Category values(@Cname)";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@Cname", category.Cname);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateCategory(Category category)
        {
            int result = 0;
            string str = "update Category set Cname=@Cname where Cid=@Cid";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@Cname", category.Cname);
            cmd.Parameters.AddWithValue("@Cid", category.Cid);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteCategory(int id)
        {
            int result = 0;
            string str = "delete from  Category where Cid=@Cid";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@Cid", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public IEnumerable<Category> GetAllCategory()
        {
            List<Category> list = new List<Category>();
            string qry = "select * from Category";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Category category = new Category();
                    category.Cid = Convert.ToInt32(dr["Cid"]);
                    category.Cname = dr["Cname"].ToString();
                    list.Add(category);
                }
            }
            con.Close();
            return list;
        }
        public Category GetCategoryById(int id)
        {
            Category category = new Category();
            string qry = "select * from Category where Cid=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    category.Cid = Convert.ToInt32(dr["Cid"]);
                    category.Cname = dr["Cname"].ToString();
                }
            }
            con.Close();
            return category;
        }
    }
}
