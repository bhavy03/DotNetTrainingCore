using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetApi
{
    class Books : Assets
    {
        //public int serialNumber { get; set; }
        //public string nameOfBook { get; set; }
        public string author { get; set; }
        public DateTime Date { get; set; }


        public void display()
        {
            Console.WriteLine($"{SerialNumber,-15} {Name,-20} {author,-20} {Date,-15}");
        }

    }
}
