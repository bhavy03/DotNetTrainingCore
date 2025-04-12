using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement
{
    class Hardware : Assets
    {
        //public int SerialNumber { get; set; }
        //public string modelName { get; set; }
        public string Manufacturer { get; set; }

        public void display()
        {
            Console.WriteLine($"{SerialNumber,-15} {Name,-20} {Manufacturer,-15}");
        }

    }
}
