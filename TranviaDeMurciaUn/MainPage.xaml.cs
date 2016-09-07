using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TranviaDeMurciaUn.Model;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TranviaDeMurciaUn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        List<Parada> listaEstaciones = new List<Parada>();
        Parada myParada;

        private Geopoint center = new Geopoint(new BasicGeoposition()
        {
            Latitude = 38.01272100,
            Longitude = -1.15215900

        });


        public MainPage()
        {
            this.InitializeComponent();
            centrarMapa();
            actualizarListaEstaciones();
        }

        public async void actualizarListaEstaciones()
        {
            LoadingRing.Visibility = Visibility.Visible;
            Estaciones es = new Estaciones();
            await es.actualizar();
            listaEstaciones = es.estaciones_tranvía;
            //foreach(Parada p in listaEstaciones)
            //{
            //    p.actualizar(); //await
            //}
            this.Bindings.Update();
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private void centrarMapa()
        {
            myMap.Center = center;
            myMap.ZoomLevel = 13;
        }


        private async void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            var status = await Geolocator.RequestAccessAsync();
            if (status == GeolocationAccessStatus.Allowed)
            {
                var locator = new Geolocator();
                var position = await locator.GetGeopositionAsync();
                var lat = position.Coordinate.Point.Position.Latitude;
                var lon = position.Coordinate.Point.Position.Longitude;
                var center = new Geopoint(new BasicGeoposition()
                {
                    Latitude = lat,
                    Longitude = lon
                });

                await myMap.TrySetViewAsync(center, 15);
                MapControl.SetLocation(posicion, center);
                posicion.Visibility = Visibility.Visible;
            }

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadingRing.Visibility = Visibility.Visible;
            foreach(Parada p in listaEstaciones)
            {
                if (p.codigo_parada.Equals(((Button)sender).Tag))
                    myParada = p;
            }
            await myParada.actualizar();
            this.Bindings.Update();
            LoadingRing.Visibility = Visibility.Collapsed;
            myFlyout.ShowAt(myMap);
        }
    }
}
