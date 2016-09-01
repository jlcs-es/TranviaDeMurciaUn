using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace TranviaDeMurciaUn.Model
{
    class Connection
    {
        public async static Task<string> GetData(string urlRequested)
        {
            
            try
            {
                HttpClient http = new HttpClient();
                Uri requestUri = new Uri(urlRequested);
                
                var response = await http.GetAsync(requestUri);
                var result = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("---> Connection:");
                Debug.WriteLine(urlRequested);
                Debug.WriteLine(result);

                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en GetData. " + e.Message);
                CustomNotifications.displayInfoDialog("Error de conexión", "Compruebe su conexión a Internet.");
                // TODO: throw exception
                throw;
            }

        }

    }
}
