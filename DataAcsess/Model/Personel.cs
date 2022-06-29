using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAcsess.Model
{

    [Table("Personel")]
    public class Personel
    {
        [Key]
        public int Id { get; set; }

        public String Adi { get; set; }

        public String Soyadi { get; set; }

        public int Maas { get; set; }     

    }

    
}
