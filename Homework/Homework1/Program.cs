using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{

    internal class Program
    {
        /*
        private static void Delete()
        {
            using(MySqlConnection connection = GetConnection())
            {
                using(MySqlCommand cmd=new MySqlCommand("delete from authors where Name='莫言'", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(
                "datasource=localhost;username=root;"
                + "password=zy000000;database=mysql;charset=utf8");
            connection.Open();
            return connection;
        }
        */
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("input the arithmatic expression");
            String expressing = Console.ReadLine();
            int n = 0;
            for (int index = 0; index < expressing.Length; index++)
            {
                if (expressing[index] == '+' | expressing[index] == '/' | expressing[index] == '-' | expressing[index] == '*')
                {
                    n = index;
                }
            }

            float firstNum = float.Parse(expressing.Substring(0, n));
            float secondNum = float.Parse(expressing.Substring(n + 1));
            Console.WriteLine("result is ");
            Console.WriteLine(Calculater.calculate(firstNum, secondNum, expressing[n]));
            */
            using (var db=new BlogginContext())
            {
                var blog = new Blog { Url = "http://", Rating = 3 };
                blog.Posts = new List<Post>()
                {
                    new Post(){Title="Test1"},
                    new Post(){Title="Test2"}
                };
                db.blogs.Add(blog);
                db.SaveChanges();
            }
            
        
        }
    }
    class Calculater
    {
        
        public static float calculate(float firstNum, float secondNum, char operater)
        {
            switch (operater)
            {
                case '+': return firstNum + secondNum;
                case '-': return firstNum - secondNum;
                case '*': return firstNum * secondNum;
                case '/': return firstNum / secondNum;
                default: return 0;
            }

        }
    }

    public class Blog
    {
        public int BlogID { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }

        public int BlogID { get; set; }
        public Blog blog { get; set; }
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BlogginContext : DbContext
    {
        public BlogginContext() : base("BlogDB")
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<BlogginContext>());
        }

        public DbSet<Blog> blogs { get; set; }
        public DbSet<Post> posts { get; set; }
    }

}
