using System;
using SQLite;

namespace SwimmingTool
{
    [Table("registros")]
    public class Registro
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }
        public int cantidadBrazadas { get; set; }
        public String tiempo { get; set; }
        public String documento { get; set; }
        public String fechaSesion { get; set; }
        public String frecuenciaBrazadaPromedio { get; set; }
        public String velocidadPromedio { get; set; }
        public String longitudBrazadaPromedio { get; set; }
        public String recomendacion { get; set; }
        public  Registro(){

        }
    }
}
