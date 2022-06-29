using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAcsess.Model
{
    public class PersonelDepartmanView
    {
        [Key]
        public int Id { get; set; }
        public string AdiSoyadi { get; set; }
        public string Gorev { get; set; }
        public string Departman { get; set; }
    }
}
