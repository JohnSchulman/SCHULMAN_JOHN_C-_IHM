using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6.Models
{
    public class IPPORT
    {
        // la classe que j'ai recuperer dans le Demo XML
        // pour  ne pas le mettre au milieu on le met dans un repertoire 
        public string IP { get; set; }
        public string PORT { get; set; }

        public override string ToString() => $"{IP}: {PORT}";
    }
}

