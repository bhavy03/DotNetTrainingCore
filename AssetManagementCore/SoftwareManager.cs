using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement
{
    static class SoftwareManager
    {
        public static SoftwareLicense addSoftware()
        {
            try
            {
                int sNo;
                while (true)
                {
                    Console.WriteLine("Enter Serial Number:");
                    sNo = Convert.ToInt32(Console.ReadLine());
                    if (AssestManager.licenseList.Exists(s => s.SerialNumber == sNo))
                    {
                        Console.WriteLine("This Serial Number already exists. Enter another Serial Number!!");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.WriteLine("Enter Software Name:");
                string sName = Console.ReadLine();
                Console.WriteLine("Enter License key: ");
                string lKey = Console.ReadLine();
                Console.WriteLine("Enter Date of Expiry ");
                DateTime eDate = DateTime.Parse(Console.ReadLine());

                SoftwareLicense newLicense = new SoftwareLicense()
                {
                    SerialNumber = sNo,
                    Name = sName,
                    licenseKey = lKey,
                    expiryDate = eDate
                };
                return newLicense;

            }
            catch (Exception)
            {
                Console.WriteLine("Please enter values in correct format!!!");
                return null;

            }
        }

        public static void updateSoftware(SoftwareLicense software)
        {
            try
            {

                Console.WriteLine("Select the value of assest to be updated");
                Console.WriteLine("1. Software Name\n2. License Key\n3. Expiry Date");
                int value = Convert.ToInt32(Console.ReadLine());
                switch (value)
                {
                    case 1:
                        Console.WriteLine("Enter New name");
                        string newName = Console.ReadLine();
                        software.Name = newName;
                        break;
                    case 2:
                        Console.WriteLine("Enter new License Key");
                        string newKey = Console.ReadLine();
                        software.licenseKey = newKey;
                        break;
                    case 3:
                        Console.WriteLine("Enter new Date");
                        DateTime newTime = DateTime.Parse(Console.ReadLine());
                        software.expiryDate = newTime;
                        break;
                    default:
                        Console.WriteLine("Enter valid Choice");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter values in correct format!!!");
                throw;
            }
        }
    }
}
