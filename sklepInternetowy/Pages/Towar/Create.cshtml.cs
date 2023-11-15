using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace sklepInternetowy.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.price = Request.Form["price"];
            clientInfo.descr = Request.Form["descr"];

            if (clientInfo.name.Length == 0 || clientInfo.price.Length == 0 || 
                clientInfo.descr.Length == 0)
            {
                errorMessage = "Wszystkie pola musz¹ byæ pe³ne";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Sklepik;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO towar " +
                                 "(name, price, descr) VALUES " +
                                 "(@name, @price, @descr);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@price", clientInfo.descr);
                        command.Parameters.AddWithValue("@descr", clientInfo.descr);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.price = ""; clientInfo.descr = "";
            successMessage = "Pomyœlnie dodano nowy towar";
        }
    }
}
