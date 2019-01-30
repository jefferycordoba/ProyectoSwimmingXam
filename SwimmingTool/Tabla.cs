
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Syncfusion.SfDataGrid;

namespace SwimmingTool
{
    [Activity(Label = "Tabla", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class Tabla : Activity
    {

        public RegistroRepository registroDB;
        EditText documentEditText;
        ListView resultListView;
        TextView documentoTextView;
        TextView nombreTextView;
        TextView sexoTextView;
        TextView estaturaTexView;
        TextView longitudTextView;
        SfDataGrid sfDataGrid;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Tabla);
            string dbPath = FileAccessHelper.GetLocalFilePath("people.db3");
            registroDB = new RegistroRepository(dbPath);
            FindViewById<Button>(Resource.Id.homeButton).Click += OnFinishClick;
            FindViewById<Button>(Resource.Id.consultarButton).Click += OnConsultarClick;
            documentEditText = FindViewById<EditText>(Resource.Id.documentoEditText);
            resultListView = FindViewById<ListView>(Resource.Id.resultListView);
            //sfDataGrid = FindViewById<SfDataGrid>(Resource.Id.resultDataGrid);
            documentoTextView = FindViewById<TextView>(Resource.Id.documentoTextView);
            nombreTextView = FindViewById<TextView>(Resource.Id.nombreTextView);
            sexoTextView = FindViewById<TextView>(Resource.Id.sexoTextView);
            estaturaTexView = FindViewById<TextView>(Resource.Id.estaturaTextView);
            longitudTextView = FindViewById<TextView>(Resource.Id.brazadaTextView);

            List<Nadador> nadadores = registroDB.GetAllNadadores();
            List<Registro> registros = registroDB.GetAllRegistros();

            //var items = new List<String>();
            //foreach(var listing in nadadores){
            //    items.Add(listing.documentId + " / " + listing.nombre + " / " + listing.sexo + " / " + listing.estatura + " / " + listing.longitudBrazo);
            //}
            //var adapter =new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
            //resultListView.Adapter = adapter;
        }

        void OnFinishClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        void OnConsultarClick(object sender, EventArgs e)
        {
            Nadador nadador = registroDB.GetNadador(documentEditText.Text);
            if (nadador != null)
            {
                documentoTextView.Text = nadador.documentId;
                nombreTextView.Text = nadador.nombre;
                sexoTextView.Text = Convert.ToString(nadador.sexo);
                estaturaTexView.Text = Convert.ToString(nadador.estatura);
                longitudTextView.Text = Convert.ToString(nadador.longitudBrazo);

                List<Registro> registros = registroDB.GetRegistrosByDocument(documentEditText.Text);
                var items = new List<String>();
                foreach (var listing in registros)
                {
                    items.Add("Fecha y Hora: "+listing.feha + " / " + "Tiempo de sesión: " + listing.time + " / " + "Brazadas: " + listing.numBrazadas + " / " + "Longitud de brazada promedio: " + listing.longitudBrazadaProm + " / " + "Velocidad promedio: " + listing.velocidadProm + " / " + "Frecuencia de brazada promedio: " + listing.frecProm + " / "+ "Recomendación: " + listing.recomendacion);
                }
                var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
                resultListView.Adapter = adapter;
                //sfDataGrid.SetItemsSource(items);
            }
            else {
                Toast.MakeText(ApplicationContext, "Éste nadador no está registrado", ToastLength.Long).Show();

            }
        }
    }
}
