using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace sklepInternetowy.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Sklepik;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM towar WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {                             
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = "" + reader.GetString(1);
                                clientInfo.price = "" + reader.GetString(2);
                                clientInfo.descr = "" + reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.price = Request.Form["price"];
            clientInfo.descr = Request.Form["descr"];

            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 || clientInfo.price.Length == 0 ||
                clientInfo.descr.Length == 0)
            {
                errorMessage = "Wszystkie pola s¹ wymagane";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Sklepik;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE towar " +
                                 "SET name=@name, price=@price, descr=@descr " +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@price", clientInfo.price);
                        command.Parameters.AddWithValue("@descr", clientInfo.descr);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage=e.Message;
                return;
            }

            Response.Redirect("/Towar/Index");
        }
    }
}
