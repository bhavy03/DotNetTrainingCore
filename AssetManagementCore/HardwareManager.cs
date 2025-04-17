using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetApi
{
    static class HardwareManager
    {
        public static Hardware addHardware()
        {
            try
            {
                int sNo;
                while (true)
                {
                    Console.WriteLine("Enter Serial Number:");
                    sNo = Convert.ToInt32(Console.ReadLine());
                    if (AssestManager.hardwareList.Exists(h => h.SerialNumber == sNo))
                    {
                        Console.WriteLine("This Serial Number already exists. Enter another Serial Number!!");
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("Enter Model Name:");
                string mName = Console.ReadLine();
                string manu = Console.ReadLine();

                Hardware newHardware = new Hardware()
                {
                    SerialNumber = sNo,
                    Name = mName,
                    Manufacturer = manu
                };
                return newHardware;
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter values in correct format!!!");
                return null;
            }
        }

        public static void updateHardware(Hardware hardware)
        {
            try
            {
                Console.WriteLine("Select the value of assest to be updated");
                Console.WriteLine("1. Model Name\n2. Manufacturer");
                int value = Convert.ToInt32(Console.ReadLine());
                switch (value)
                {
                    case 1:
                        Console.WriteLine("Enter New name");
                        string newName = Console.ReadLine();
                        hardware.Name = newName;
                        break;
                    case 2:
                        Console.WriteLine("Enter new Manufacturer");
                        string newManu = Console.ReadLine();
                        hardware.Manufacturer = newManu;
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
