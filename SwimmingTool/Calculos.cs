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
    public class Calculos
    {

        public String recomendacionBrazadas(double tiempo, double frecProm)
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

        public double calcularFrecPromedio(int cantBrazadas, double tiempoSesion)
        {
            //retorna brazadas por minuto  brazadas/minuto
            return (cantBrazadas * 60) / tiempoSesion;
        }

        public double calcularLongitudBrazadaPromedio(int cantBrazadas, double distancia)
        {
            //retorna en metros por brazada metros/brzada
            return distancia / cantBrazadas;

        }

        public double calcularVelocidadPromedio(double longBrazadaP, double frecuenciaBP)
        {
            /*las unidades del producto de la longitud por la frecuencia de brazada
             son metros por minuto, para llevar estas unidades a kilometro por hora Km/h
             se debe multiplicar por 60 minutos que tiene una hora y dividir por 1000m que
             contiene un Kilometro*/
            return longBrazadaP * frecuenciaBP / 60;

        }
    }
}