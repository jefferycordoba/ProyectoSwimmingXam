
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
using System.Timers;
using Android.Hardware;
using Android.Util;

namespace SwimmingTool
{
    [Activity(Label = "Brazada", ScreenOrientation =Android.Content.PM.ScreenOrientation.Landscape)]
    public class Brazada : Activity
    {

        private int numeroBrazadas = 0;
        private string recomendacion = "";
        private double longitudBrazadaProm = 0, frecBrazadaProm = 0, velocidadProm = 0, distancia = 100;
        private TextView numBrazadaTV;
        private int mins = 0, secs = 0, milliseconds = 0, segundos = 0;
        private Timer timer;
        private TextView txtTimer;
        private Boolean isStart = false;
        private String documento;
        
        public RegistroRepository registroDB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Brazadas);
            documento = Intent.GetStringExtra("documento");
            

            string dbPath = FileAccessHelper.GetLocalFilePath("people.db3");
            registroDB = new RegistroRepository(dbPath);

            FindViewById<Button>(Resource.Id.finishButton).Click += OnFinishClick;
            FindViewById<Button>(Resource.Id.brazadaButton).Click += OnBrazadaClick;
            FindViewById<Button>(Resource.Id.startStopButton).Click += OnStartStopClick;
            FindViewById<Button>(Resource.Id.resetButton).Click += OnResetClick;

            LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.ll_container_surface_view);
            Camera _camera = SetUpCamera();
            ll.AddView(new CameraPreview(this, _camera));
            _camera.StartPreview();

            numBrazadaTV = FindViewById<TextView>(Resource.Id.numBrazadaTextView);
            txtTimer = FindViewById<TextView>(Resource.Id.timeTextView);


        }

        void OnFinishClick(object sender, EventArgs e)
        {
            
            if (!isStart)
            {
                if ((txtTimer.Text !="") && (numeroBrazadas != 0))
                {
                    frecBrazadaProm = calcularFrecPromedio(numeroBrazadas, segundos);
                    frecBrazadaProm = Math.Round(frecBrazadaProm, 2);
                    longitudBrazadaProm = calcularLongitudBrazadaPromedio(numeroBrazadas, distancia);
                    longitudBrazadaProm = Math.Round(longitudBrazadaProm, 2);
                    velocidadProm = calcularVelocidadPromedio(longitudBrazadaProm, frecBrazadaProm);
                    velocidadProm = Math.Round(velocidadProm, 2);
                    recomendacion = recomendacionBrazadas(segundos, frecBrazadaProm);
                    Registro registro = new Registro();
                    registro.documento = documento;
                    registro.numBrazadas = numeroBrazadas;
                    registro.feha = DateTime.Now.ToString();
                    registro.time = txtTimer.Text;
                    registro.frecProm = frecBrazadaProm.ToString() + " brazadas/min";
                    registro.longitudBrazadaProm = longitudBrazadaProm.ToString() + " metros/brazada";
                    registro.velocidadProm = velocidadProm.ToString() + "metros/segundo";
                    registro.recomendacion = recomendacion;
                    segundos = 0;
                    Bundle bundle = new Bundle();
                    registroDB.addNewRegistro(registro);
                    var intent = new Intent(this, typeof(Resultado));

                    bundle.PutString("recomendacion", recomendacion);
                    intent.PutExtras(bundle);
                    StartActivity(intent);

                }
                else
                {
                    Toast.MakeText(ApplicationContext, "No hay datos validos para grabar la sesión", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(ApplicationContext, "Debes detener el cronometro", ToastLength.Long).Show();
            }



        }

        void OnBrazadaClick(object sender, EventArgs e)
        {
            numeroBrazadas = numeroBrazadas + 1;
            numBrazadaTV.Text = numeroBrazadas.ToString();
        }

        void OnStartStopClick(object sender, EventArgs e)
        {
            if (isStart){
                isStart = false;
                timer.Stop();
                timer = null;
            }
            else
            {
                timer = new Timer();
                timer.Interval = 1;
                timer.Elapsed += Timer_Elapsed;
                isStart = true;
                timer.Start();
            }
        }

        void OnResetClick(object sender, EventArgs e)
        {

            if (!isStart)
            {
                numeroBrazadas = 0;
                numBrazadaTV.Text = numeroBrazadas.ToString();

                timer = new Timer();
                isStart = false;
                timer.Stop();
                timer = null;
                milliseconds = 0;
                secs = 0;
                mins = 0;
                segundos = 0;
                frecBrazadaProm = 0;
                longitudBrazadaProm = 0;
                velocidadProm = 0;
                recomendacion = "";
                txtTimer.Text = String.Format("{0}:{1:00}:{2:000}", mins, secs, milliseconds);

            }
            else
            {
                Toast.MakeText(ApplicationContext, "Debes detener el cronometro", ToastLength.Long).Show();
            }
        }




        void Timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                milliseconds++;
                if (milliseconds > 1000)
                {
                    secs++;
                    segundos++;
                    milliseconds = 0;
                }
                if (secs == 59)
                {
                    mins++;
                    secs = 0;
                }
                RunOnUiThread(() =>
                {
                    txtTimer.Text = String.Format("{0}:{1:00}:{2:000}", mins, secs, milliseconds);
                });
            
        }


        Camera SetUpCamera()
        {
            Camera c = null;
            try
            {
                c = Camera.Open();
            }
            catch (Exception e)
            {
                Log.Debug("", "Device camera not available now.");
            }

            return c;
        }

        String recomendacionBrazadas(double tiempo, double frecProm)
        {
            String rec1 = "Debes de mejorar tu longitud de brazada y disminuir la freceuencia de brazada para obtener mejores resultados durante tu sesión";
            String rec2 = "La ralación entre tu frecuencia de brazada y tu longitud de brazada es equilibrada";
            String rec3 = "Debes aumentar tu frecuencia de brazada para obtener mejores resultados durante tu sesión";

            if (tiempo <= 60)
            {

                if (frecProm > 110)
                {
                    return rec1;

                }
                else if (frecProm < 67)
                {
                    return rec3;

                }
                else if ((frecProm >= 67) && (frecProm <= 110))
                {
                    return rec2;

                }
            }
            else if (tiempo <= 65)
            {

                if (frecProm > 105)
                {
                    return rec1;

                }
                else if (frecProm < 65)
                {
                    return rec3;

                }
                else if ((frecProm >= 65 && frecProm <= 105))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 70)
            {

                if (frecProm > 97)
                {
                    return rec1;

                }
                else if (frecProm < 61)
                {
                    return rec3;

                }
                else if ((frecProm >= 61 && frecProm <= 97))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 75)
            {

                if (frecProm > 90)
                {
                    return rec1;

                }
                else if (frecProm < 59)
                {
                    return rec3;

                }
                else if ((frecProm >= 59 && frecProm <= 90))
                {
                    return rec2;

                }


            }
            else if (tiempo <= 80)
            {

                if (frecProm > 82)
                {
                    return rec1;

                }
                else if (frecProm < 57)
                {
                    return rec3;

                }
                else if ((frecProm >= 57 && frecProm <= 82))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 85)
            {

                if (frecProm > 74)
                {
                    return rec1;

                }
                else if (frecProm < 56)
                {
                    return rec3;

                }
                else if ((frecProm >= 56 && frecProm <= 74))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 90)
            {

                if (frecProm > 72)
                {
                    return rec1;

                }
                else if (frecProm < 54)
                {
                    return rec3;

                }
                else if ((frecProm >= 54 && frecProm <= 72))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 95)
            {

                if (frecProm > 71)
                {
                    return rec1;

                }
                else if (frecProm < 53)
                {
                    return rec3;

                }
                else if ((frecProm >= 53 && frecProm <= 71))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 100)
            {

                if (frecProm > 68)
                {
                    return rec1;

                }
                else if (frecProm < 52)
                {
                    return rec3;

                }
                else if ((frecProm >= 52 && frecProm <= 68))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 105)
            {

                if (frecProm > 67)
                {
                    return rec1;

                }
                else if (frecProm < 51)
                {
                    return rec3;

                }
                else if ((frecProm >= 51 && frecProm <= 67))
                {
                    return rec2;

                }


            }
            else if (tiempo <= 110)
            {
                if (frecProm > 66)
                {
                    return rec1;

                }
                else if (frecProm < 51)
                {
                    return rec3;

                }
                else if ((frecProm >= 51 && frecProm <= 66))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 115)
            {

                if (frecProm > 65)
                {
                    return rec1;

                }
                else if (frecProm < 51)
                {
                    return rec3;

                }
                else if ((frecProm >= 51 && frecProm <= 65))
                {
                    return rec2;

                }


            }
            else if (tiempo <= 120)
            {

                if (frecProm > 64)
                {
                    return rec1;

                }
                else if (frecProm < 51)
                {
                    return rec3;

                }
                else if ((frecProm >= 51 && frecProm <= 64))
                {
                    return rec2;

                }

            }
            else if (tiempo <= 125)
            {

                if (frecProm > 63)
                {
                    return rec1;

                }
                else if (frecProm < 51)
                {
                    return rec3;

                }
                else if ((frecProm >= 51 && frecProm <= 63))
                {
                    return rec2;

                }


            }
            else if (tiempo <= 130)
            {

                if (frecProm > 62)
                {
                    return rec1;

                }
                else if (frecProm < 50)
                {
                    return rec3;

                }
                else if ((frecProm >= 50) && (frecProm <= 62))
                {
                    return rec2;

                }
            }
            return "Tiempo muy alto, debes entrenar más para mejorarlo";

        }

        double calcularFrecPromedio(int cantBrazadas, double tiempoSesion)
        {
            //retorna brazadas por minuto  brazadas/minuto
            return (cantBrazadas * 60) / tiempoSesion;
        }

        double calcularLongitudBrazadaPromedio(int cantBrazadas, double distancia)
        {
            //retorna en metros por brazada metros/brzada
            return distancia / cantBrazadas;

        }

        double calcularVelocidadPromedio(double longBrazadaP, double frecuenciaBP)
        {
            /*las unidades del producto de la longitud por la frecuencia de brazada
             son metros por minuto, para llevar estas unidades a kilometro por hora Km/h
             se debe multiplicar por 60 minutos que tiene una hora y dividir por 1000m que
             contiene un Kilometro*/
            return longBrazadaP * frecuenciaBP / 60;

        }

    
}
}
