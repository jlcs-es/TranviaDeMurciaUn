using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace TranviaDeMurciaUn.Model
{
    class Estaciones
    {
        public List<Parada> estaciones_tranvía;

        public Estaciones()
        {
            estaciones_tranvía = new List<Parada>();
        }

        public async Task actualizar()
        {
            // Request data
            string url;
            string jsonString;
            //string jsonString = "[{\"id\":\"0\",\"nombre\":\"Estadio Nueva Condomina\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.04054300\",\"lng\":\"-1.14641300\",\"saeid\":\"12\"},{\"id\":\"1\",\"nombre\":\"La Ladera\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.03472800\",\"lng\":\"-1.14372700\",\"saeid\":\"11\"},{\"id\":\"2\",\"nombre\":\"Infantas\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.03165000\",\"lng\":\"-1.14599000\",\"saeid\":\"10\"},{\"id\":\"3\",\"nombre\":\"Pr\u00edncipe Felipe\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.02691500\",\"lng\":\"-1.14242300\",\"saeid\":\"9\"},{\"id\":\"4\",\"nombre\":\"Churra\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.02159500\",\"lng\":\"-1.13886400\",\"saeid\":\"8\"},{\"id\":\"5\",\"nombre\":\"Alameda\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01757000\",\"lng\":\"-1.13728900\",\"saeid\":\"7\"},{\"id\":\"6\",\"nombre\":\"Los Cubos\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01236000\",\"lng\":\"-1.13194900\",\"saeid\":\"6\"},{\"id\":\"7\",\"nombre\":\"Santiago y Zaraiche\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.00772400\",\"lng\":\"-1.12956400\",\"saeid\":\"5\"},{\"id\":\"8\",\"nombre\":\"Pr\u00edncipe de Asturias\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.00361000\",\"lng\":\"-1.12906400\",\"saeid\":\"4\"},{\"id\":\"9\",\"nombre\":\"Abenarabi\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99837800\",\"lng\":\"-1.12668900\",\"saeid\":\"3\"},{\"id\":\"10\",\"nombre\":\"Marina Espa\u00f1ola\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99397900\",\"lng\":\"-1.12454600\",\"saeid\":\"2\"},{\"id\":\"11\",\"nombre\":\"Plaza Circular\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99214800\",\"lng\":\"-1.12906000\",\"saeid\":\"1\"},{\"id\":\"12\",\"nombre\":\"Juan Carlos I\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99444200\",\"lng\":\"-1.13266500\",\"saeid\":\"13\"},{\"id\":\"13\",\"nombre\":\"Biblioteca Regional\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99847900\",\"lng\":\"-1.13663700\",\"saeid\":\"14\"},{\"id\":\"14\",\"nombre\":\"Senda de Granada\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.00299700\",\"lng\":\"-1.14120500\",\"saeid\":\"15\"},{\"id\":\"15\",\"nombre\":\"Parque Empresarial\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.00831200\",\"lng\":\"-1.14657500\",\"saeid\":\"16\"},{\"id\":\"16\",\"nombre\":\"El Puntal\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.01272100\",\"lng\":\"-1.15215900\",\"saeid\":\"17\"},{\"id\":\"17\",\"nombre\":\"Espinardo\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.01361600\",\"lng\":\"-1.15566200\",\"saeid\":\"18\"},{\"id\":\"18\",\"nombre\":\"Los Rectores - Terra Natura\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.01250100\",\"lng\":\"-1.16209100\",\"saeid\":\"19\"},{\"id\":\"19\",\"nombre\":\"Guadalupe\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.00320300\",\"lng\":\"-1.17266200\",\"saeid\":\"20\"},{\"id\":\"20\",\"nombre\":\"Reyes Cat\u00f3licos\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.00178500\",\"lng\":\"-1.17694000\",\"saeid\":\"21\"},{\"id\":\"21\",\"nombre\":\"El Port\u00f3n\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"37.99737600\",\"lng\":\"-1.18416900\",\"saeid\":\"22\"},{\"id\":\"22\",\"nombre\":\"UCAM - Los Jer\u00f3nimos\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"37.99284200\",\"lng\":\"-1.18654300\",\"saeid\":\"23\"},{\"id\":\"23\",\"nombre\":\"Universidad de Murcia\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01487900\",\"lng\":\"-1.16760400\",\"saeid\":\"24\"},{\"id\":\"24\",\"nombre\":\"Servicios de Investigaci\u00f3n\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01865800\",\"lng\":\"-1.16667700\",\"saeid\":\"25\"},{\"id\":\"25\",\"nombre\":\"Centro Social\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.02176000\",\"lng\":\"-1.16885100\",\"saeid\":\"26\"},{\"id\":\"26\",\"nombre\":\"Biblioteca General\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01854100\",\"lng\":\"-1.17208600\",\"saeid\":\"27\"},{\"id\":\"27\",\"nombre\":\"Residencia Universitaria\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01419300\",\"lng\":\"-1.17368700\",\"saeid\":\"28\"}]";
            try
            {
                url = "http://www.tranviademurcia.es/aplicacion_movil/api/get_paradas";
                jsonString = await Connection.GetData(url);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Estaciones error GetData: " + e.Message);
                jsonString = "[{\"id\":\"0\",\"nombre\":\"Estadio Nueva Condomina\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.04054300\",\"lng\":\"-1.14641300\",\"saeid\":\"12\"},{\"id\":\"1\",\"nombre\":\"La Ladera\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.03472800\",\"lng\":\"-1.14372700\",\"saeid\":\"11\"},{\"id\":\"2\",\"nombre\":\"Infantas\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.03165000\",\"lng\":\"-1.14599000\",\"saeid\":\"10\"},{\"id\":\"3\",\"nombre\":\"Pr\u00edncipe Felipe\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.02691500\",\"lng\":\"-1.14242300\",\"saeid\":\"9\"},{\"id\":\"4\",\"nombre\":\"Churra\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.02159500\",\"lng\":\"-1.13886400\",\"saeid\":\"8\"},{\"id\":\"5\",\"nombre\":\"Alameda\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01757000\",\"lng\":\"-1.13728900\",\"saeid\":\"7\"},{\"id\":\"6\",\"nombre\":\"Los Cubos\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01236000\",\"lng\":\"-1.13194900\",\"saeid\":\"6\"},{\"id\":\"7\",\"nombre\":\"Santiago y Zaraiche\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.00772400\",\"lng\":\"-1.12956400\",\"saeid\":\"5\"},{\"id\":\"8\",\"nombre\":\"Pr\u00edncipe de Asturias\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.00361000\",\"lng\":\"-1.12906400\",\"saeid\":\"4\"},{\"id\":\"9\",\"nombre\":\"Abenarabi\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99837800\",\"lng\":\"-1.12668900\",\"saeid\":\"3\"},{\"id\":\"10\",\"nombre\":\"Marina Espa\u00f1ola\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99397900\",\"lng\":\"-1.12454600\",\"saeid\":\"2\"},{\"id\":\"11\",\"nombre\":\"Plaza Circular\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99214800\",\"lng\":\"-1.12906000\",\"saeid\":\"1\"},{\"id\":\"12\",\"nombre\":\"Juan Carlos I\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99444200\",\"lng\":\"-1.13266500\",\"saeid\":\"13\"},{\"id\":\"13\",\"nombre\":\"Biblioteca Regional\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"37.99847900\",\"lng\":\"-1.13663700\",\"saeid\":\"14\"},{\"id\":\"14\",\"nombre\":\"Senda de Granada\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.00299700\",\"lng\":\"-1.14120500\",\"saeid\":\"15\"},{\"id\":\"15\",\"nombre\":\"Parque Empresarial\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.00831200\",\"lng\":\"-1.14657500\",\"saeid\":\"16\"},{\"id\":\"16\",\"nombre\":\"El Puntal\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.01272100\",\"lng\":\"-1.15215900\",\"saeid\":\"17\"},{\"id\":\"17\",\"nombre\":\"Espinardo\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.01361600\",\"lng\":\"-1.15566200\",\"saeid\":\"18\"},{\"id\":\"18\",\"nombre\":\"Los Rectores - Terra Natura\",\"tarifa\":\"tarifa_urbana\",\"lat\":\"38.01250100\",\"lng\":\"-1.16209100\",\"saeid\":\"19\"},{\"id\":\"19\",\"nombre\":\"Guadalupe\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.00320300\",\"lng\":\"-1.17266200\",\"saeid\":\"20\"},{\"id\":\"20\",\"nombre\":\"Reyes Cat\u00f3licos\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.00178500\",\"lng\":\"-1.17694000\",\"saeid\":\"21\"},{\"id\":\"21\",\"nombre\":\"El Port\u00f3n\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"37.99737600\",\"lng\":\"-1.18416900\",\"saeid\":\"22\"},{\"id\":\"22\",\"nombre\":\"UCAM - Los Jer\u00f3nimos\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"37.99284200\",\"lng\":\"-1.18654300\",\"saeid\":\"23\"},{\"id\":\"23\",\"nombre\":\"Universidad de Murcia\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01487900\",\"lng\":\"-1.16760400\",\"saeid\":\"24\"},{\"id\":\"24\",\"nombre\":\"Servicios de Investigaci\u00f3n\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01865800\",\"lng\":\"-1.16667700\",\"saeid\":\"25\"},{\"id\":\"25\",\"nombre\":\"Centro Social\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.02176000\",\"lng\":\"-1.16885100\",\"saeid\":\"26\"},{\"id\":\"26\",\"nombre\":\"Biblioteca General\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01854100\",\"lng\":\"-1.17208600\",\"saeid\":\"27\"},{\"id\":\"27\",\"nombre\":\"Residencia Universitaria\",\"tarifa\":\"tarifa_interurbana\",\"lat\":\"38.01419300\",\"lng\":\"-1.17368700\",\"saeid\":\"28\"}]";
            }

            //Debug.WriteLine("Estaciones preParsing jsonString");
            JsonArray ja = JsonArray.Parse(jsonString);
            //Debug.WriteLine("Estaciones postParsing jsonString: " + ja.ToString());

            estaciones_tranvía = new List<Parada>();

            foreach (IJsonValue jsonValue in ja)
            {
                if (jsonValue.ValueType == JsonValueType.Object)
                {
                    //Debug.WriteLine("Estaciones parsing jsonValue: " + jsonValue.ToString());
                    estaciones_tranvía.Add(new Parada(jsonValue.GetObject()));
                }
            }
            

        }


    }
}
