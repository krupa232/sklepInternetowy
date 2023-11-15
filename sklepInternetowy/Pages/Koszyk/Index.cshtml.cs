using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace sklepInternetowy.Pages.Koszyk
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public String finalPrice;
        public void OnGet()
        {
            double fullPrice = 0;
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Sklepik;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM koszyk";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = "" + reader.GetString(1);
                                clientInfo.price = "" + reader.GetString(2);
                                clientInfo.descr = "" + reader.GetString(3);
                                fullPrice += Convert.ToDouble(clientInfo.price);
                             
                                listClients.Add(clientInfo);

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {

            }

            finalPrice = string.Format("{0:N2}", fullPrice);

        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String price;
        public String descr;
    }
}
