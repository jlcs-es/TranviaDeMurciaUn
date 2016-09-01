using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;

namespace TranviaDeMurciaUn.Model
{
    class Parada
    {
        public string codigo_parada;
        public string nombre { get; set; }
        public string tarifa { get; set; }
        public double longitud { get; set; }
        public double latitud { get; set; }

        public Geopoint posicion { get; set; }

        public List<TiempoReal> tiempo_real { get; set; }
        public List<Horario> horarios { get; set; }

        private const string codigo_paradaKey = "id";
        private const string nombreKey = "nombre";
        private const string longitudKey = "lng";
        private const string latitudKey = "lat";
        private const string tarifaKey = "tarifa";
        private const string statusKey = "status";
        private const string tiempo_realKey = "tiempo_real";
        private const string horariosKey = "horarios";
        private const string dataKey = "data";


        public string tiempoRealString {
            get
            {
                string s = "";
                foreach(TiempoReal tr in tiempo_real)
                {
                    s += tr.direccion;
                    s += Environment.NewLine;
                    s += tr.tiempo_real_expandido;
                    s += Environment.NewLine;
                }
                return s;
            }
        }


        public Parada(JsonObject jo)
        {
            Debug.WriteLine("Parsing parada");

            codigo_parada = jo.GetNamedString(codigo_paradaKey, "-1");
            nombre = jo.GetNamedString(nombreKey, "Error");
            tarifa = jo.GetNamedString(tarifaKey, "");
            longitud = Double.Parse(jo.GetNamedString(longitudKey), System.Globalization.CultureInfo.InvariantCulture);
            latitud = Double.Parse(jo.GetNamedString(latitudKey), System.Globalization.CultureInfo.InvariantCulture);

            tiempo_real = new List<TiempoReal>();
            horarios = new List<Horario>();

            Debug.WriteLine("Parsing parada post new List. Lat=" + latitud + " Lon=" + longitud);

            posicion = new Geopoint(new BasicGeoposition()
            {
                Latitude = latitud,
                Longitude = longitud
            });

            Debug.WriteLine("Parsing parada post new Geopoint");
        }


        public async Task actualizar()
        {
            Debug.WriteLine("Actualizando parada: " + codigo_parada + " - " + nombre);

            // Request data
            string url, jsonString ="{}";
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            ;
            try
            {
                url = "http://www.tranviademurcia.es/aplicacion_movil/api/get_detalle_parada_sae?stop_id=" + codigo_parada + "&timestamp=" + unixTimestamp;
                jsonString = await Connection.GetData(url);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en GetData " + e.Message);
                return;
            }

            // Parse Json Data

            JsonObject jo = JsonObject.Parse(jsonString);

            bool status =  jo.GetNamedBoolean(statusKey);
            //if (!status)
            //    CustomNotifications.displayInfoDialog("Variable status", "Recibida variable status no true.\nSi vuelve a suceder notifique al desarrollador.");

            JsonObject dataJO = jo.GetNamedObject(dataKey);

            tiempo_real = new List<TiempoReal>();
            horarios = new List<Horario>();


            // TODO: usar ValueType, si es un objeto en vez de
            // un array, sacar manualmente el 0 y el 1 y meterlos
            try
            {
                JsonArray tiempo_realJA = dataJO.GetNamedArray(tiempo_realKey, new JsonArray());
                JsonArray horariosJA = dataJO.GetNamedArray(horariosKey, new JsonArray());

                // Recorrer cada array y guardar en su variable

                foreach (IJsonValue jsonValue in tiempo_realJA)
                {
                    if (jsonValue.ValueType == JsonValueType.Object)
                    {
                        tiempo_real.Add(new TiempoReal(jsonValue.GetObject()));
                    }
                }

                foreach (IJsonValue jsonValue in horariosJA)
                {
                    if (jsonValue.ValueType == JsonValueType.Object)
                    {
                        horarios.Add(new Horario(jsonValue.GetObject()));
                    }
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Parsing Parada " + codigo_parada + ": " + e.Message);
            }



        }

    }




    public class TiempoReal
    {
        public string error { get; set; }
        public int error_code { get; set; }
        public string tiempo_real { get; set; }
        public List<string> tiempo_real_panel { get; set; }
        public string direccion { get; set; }
        public string tiempo_real_expandido { get; set; }
        public string direccion_sentido { get; set; }
        public List<object> servicios { get; set; }

        public const string errorKey = "error";
        public const string error_codeKey = "error_code";
        public const string tiempo_realKey = "tiempo_real";
        public const string tiempo_real_panelKey = "tiempo_real_panel";
        public const string direccionKey = "direccion";
        public const string tiempo_real_expandidoKey = "tiempo_real_expandido";
        public const string direccion_sentidoKey = "direccion_sentido";
        public const string serviciosKey = "servicios";

        public TiempoReal(JsonObject jo)
        {

            Debug.WriteLine("Parsing tiempo real");

            error = jo.GetNamedString(errorKey);
            error_code = (int)jo.GetNamedNumber(error_codeKey);
            //if (error_code!=0)
            //    CustomNotifications.displayInfoDialog("Error " + error_code, error);

            //tiempo_real = jo.GetNamedString(tiempo_realKey);
            direccion = jo.GetNamedString(direccionKey);
            tiempo_real_expandido = jo.GetNamedString(tiempo_real_expandidoKey);
            //El resto no lo usamos


        }

    }



    public class Horario
    {
        public string linea { get; set; }
        public string destino { get; set; }
        public string primero { get; set; }
        public string ultimo { get; set; }
        public string horario { get; set; }
        public string primero_text { get; set; }
        public string ultimo_text { get; set; }
        public string normal_text { get; set; }

        public const string lineaKey = "linea";
        public const string destinoKey = "destino";
        public const string primeroKey = "primero";
        public const string ultimoKey = "ultimo";
        public const string horarioKey = "horario";
        public const string primero_textKey = "primero_text";
        public const string ultimo_textKey = "ultimo_text";
        public const string normal_textKey = "normal_text";


        public Horario(JsonObject jo)
        {

            Debug.WriteLine("Parsing horario");

            linea = jo.GetNamedString(lineaKey, "");
            destino = jo.GetNamedString(destinoKey, "");
            primero = jo.GetNamedString(primeroKey, "");
            ultimo = jo.GetNamedString(ultimoKey, "");
            //horario = jo.GetNamedString(horarioKey, "");

            JsonArray horarioJA = jo.GetNamedArray(horarioKey, new JsonArray());

            foreach (IJsonValue jsonValue in horarioJA)
            {
                if (jsonValue.ValueType == JsonValueType.String)
                {
                    horario += jsonValue.GetString() + Environment.NewLine;
                }
            }

            primero_text = jo.GetNamedString(primero_textKey, "");
            ultimo_text = jo.GetNamedString(ultimo_textKey, "");
            normal_text = jo.GetNamedString(normal_textKey, "");

        }

    }


}
