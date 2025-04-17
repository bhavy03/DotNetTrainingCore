using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetApi
{
    class SoftwareLicense : Assets
    {
        //public int serialNumber { get; set; }
        //public string softwareName { get; set; }
        public string licenseKey { get; set; }
        public DateTime expiryDate { get; set; }

        public void display()
        {
            Console.WriteLine($"{SerialNumber,-15} {Name,-20} {licenseKey,-20} {expiryDate,-15}");
        }

    }
}
