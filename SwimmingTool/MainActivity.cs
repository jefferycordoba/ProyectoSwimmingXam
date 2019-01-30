using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Content.PM;
using Android;
using Android.Support.V4.App;
using System.Threading;

namespace SwimmingTool
{
    [Activity(Label = "SwimmingTool", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        private string[] requiredPermissions = { Manifest.Permission.Camera };

        public static RegistroRepository PersonRepo { get; private set; }
        public static Boolean isCammeraPermissionEnabled = false;
        public RegistroRepository registroDB;
        private EditText nombreEditText;
        private EditText documentoEditText;
        private RadioButton hombreRadioButon;
        private EditText brazoEditText;
        private EditText estatutaEditText;
        private double estatura;
        private double longBrazo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            string dbPath = FileAccessHelper.GetLocalFilePath("people.db3");
            registroDB = new RegistroRepository(dbPath);

            FindViewById<Button>(Resource.Id.acceptButton).Click += OnAcceptClick;
            FindViewById<Button>(Resource.Id.cancelButton).Click += OnCancelClick;
            FindViewById<Button>(Resource.Id.verSesionButton).Click += OnVerSesionlClick;
            FindViewById<Button>(Resource.Id.ingresarButton).Click += OnIngresarClick;
            nombreEditText = FindViewById<EditText>(Resource.Id.nombreEditText);
            documentoEditText = FindViewById<EditText>(Resource.Id.documentEditText);
            brazoEditText = FindViewById<EditText>(Resource.Id.brazoEditText);
            estatutaEditText= FindViewById<EditText>(Resource.Id.estaturaEditText);
            hombreRadioButon = FindViewById<RadioButton>(Resource.Id.hombreRadioButton);

            if (((int)Build.VERSION.SdkInt) >= (int)BuildVersionCodes.M)
            {
                RequestAllPersmissions();
            }
            else
            {
                isCammeraPermissionEnabled = true;
            }
        }

        void OnAcceptClick(object sender, EventArgs e)
        {
            
            if ((documentoEditText.Text == "") || (nombreEditText.Text == "") || (brazoEditText.Text == "") || (estatutaEditText.Text == ""))
            {
                Toast.MakeText(ApplicationContext, "Todos los campos son obligatorios", ToastLength.Long).Show();
            }
            else
            {
                

                    if (!isCammeraPermissionEnabled)
                    {
                        Toast.MakeText(this, "Debes aceptar permisos para usar la cámara!", ToastLength.Short).Show();
                        return;
                    }

                    Nadador nadador = new Nadador();
                    

                    if (registroDB.GetNadador(documentoEditText.Text) == null)
                    {
                        nadador.documentId = documentoEditText.Text;
                        nadador.nombre = nombreEditText.Text;
                        if (hombreRadioButon.Checked)
                        {
                            nadador.sexo = "Masculino";
                        }
                        else
                        {
                            nadador.sexo = "Femenino";
                        }
                        nadador.estatura = Convert.ToInt32(estatutaEditText.Text);
                        nadador.longitudBrazo = Convert.ToInt32(brazoEditText.Text); ;
                        registroDB.addNewNadador(nadador);
                        Bundle bundle = new Bundle();
                        var intent = new Intent(this, typeof(Brazada));
                        bundle.PutString("documento", documentoEditText.Text);
                        intent.PutExtras(bundle);
                        StartActivity(intent);

                    }
                    else
                    {
                                                    
                    Toast.MakeText(ApplicationContext, "Ya existe un nadador con este documento", ToastLength.Long).Show();
                    
                    }
                
                
            }
          
                
        }
        
        void OnCancelClick(object sender, EventArgs e)
        {
            this.nombreEditText.Text = "";
            this.documentoEditText.Text = "";
            this.brazoEditText.Text = "";
            this.estatutaEditText.Text = "";
        }

        void OnVerSesionlClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Tabla));
            StartActivity(intent);
        }
        void OnIngresarClick(object sender, EventArgs e)
        {
            if (documentoEditText.Text != "")
            {
                if (registroDB.GetNadador(documentoEditText.Text) != null)
                {
                    Bundle bundle = new Bundle();
                    var intent = new Intent(this, typeof(Resultado));
                    bundle.PutString("documento", documentoEditText.Text);
                    intent.PutExtras(bundle);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(ApplicationContext, "No existe un nadador con ese documento", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(ApplicationContext, "Debes ingresar un número de documento para poder ingresar", ToastLength.Long).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case 1000:
                    {
                        // If request is cancelled, the result arrays are empty.
                        if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                        {
                            isCammeraPermissionEnabled = true;
                        }
                        else
                        {
                            isCammeraPermissionEnabled = false;
                        }
                        return;
                    }
            }
        }

        private void RequestAllPersmissions()
        {
            ActivityCompat.RequestPermissions(this, requiredPermissions, 1000);
        }

    }
}

