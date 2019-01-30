
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

namespace SwimmingTool
{
    [Activity(Label = "Resultado", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class Resultado : Activity
    {
        public String reco;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Resultado);
            reco = Intent.GetStringExtra("recomendacion");
            var recomendacion = FindViewById<TextView>(Resource.Id.TextRecomendacion);
            FindViewById<Button>(Resource.Id.sessionButton).Click += OnSessionClick;
            FindViewById<Button>(Resource.Id.HomeButton).Click += OnHomeClick;
            
            recomendacion.Text = reco;

        }


        void OnSessionClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Tabla));
            StartActivity(intent);
        }

        void OnHomeClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}
