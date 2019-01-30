using System;
using SQLite;

namespace SwimmingTool
{
    [Table("registros")]
    public class Registro
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }
        public int numBrazadas { get; set; }
        public String time { get; set; }
        public String documento { get; set; }
        public String feha { get; set; }
        public String frecProm { get; set; }
        public String velocidadProm { get; set; }
        public String longitudBrazadaProm { get; set; }
        public String recomendacion { get; set; }
        public  Registro(){

        }
    }
}
