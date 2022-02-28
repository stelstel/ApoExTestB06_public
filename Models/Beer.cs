using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApoExTestB01.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [DisplayName("ABV")]
        public string Abv { get; set; }

        public string Description { get; set; }
        public string[] Food_pairing { get; set; }


    }
}
