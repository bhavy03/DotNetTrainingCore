using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetApi
{
    static class AssestManager
    {
        public static List<Books> booksList = new List<Books>();
        public static List<SoftwareLicense> licenseList = new List<SoftwareLicense>();
        public static List<Hardware> hardwareList = new List<Hardware>();

        public static void AddAssest()
        {
            Console.WriteLine("Select the assest you want to add");
            Console.WriteLine("1. Book\n2. Software license\n3. Hardware");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Books newBook = BookManager.addBook();
                    if (newBook != null)
                    {
                        booksList.Add(newBook);
                    }
                    break;
                case 2:
                    SoftwareLicense newLicense = SoftwareManager.addSoftware();
                    if (newLicense != null)
                    {
                        licenseList.Add(newLicense);
                    }
                    break;
                case 3:
                    Hardware newHardware = HardwareManager.addHardware();
                    if (newHardware != null)
                    {
                        hardwareList.Add(newHardware);
                    }
                    break;
                default:
                    Console.WriteLine("Enter valid choice");
                    break;
            }
        }

        public static void UpdateAssest()
        {
            Console.WriteLine("Enter the type of assest you want to Update");
            Console.WriteLine("1. Books\n2. Softwares\n3. Hardwares");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the serial Number");
                    int sNO = Convert.ToInt32(Console.ReadLine());
                    var book = booksList.Find(b => b.SerialNumber == sNO);
                    Console.WriteLine(book != null ? $"Found:" : "Book not found\n");
                    if (book != null)
                    {
                        BookManager.UpdateBooks(book);
                        Console.WriteLine("Here is the Updated record");
                        book.display();
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter the serial Number");
                    int seNO = Convert.ToInt32(Console.ReadLine());
                    var software = licenseList.Find(s => s.SerialNumber == seNO);
                    Console.WriteLine(software != null ? $"Found:" : "Software not found\n");
                    if (software != null)
                    {
                        SoftwareManager.updateSoftware(software);
                        Console.WriteLine("Here is the Updated record");
                        software.display();
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter the serial Number");
                    int serNO = Convert.ToInt32(Console.ReadLine());
                    var hardware = hardwareList.Find(h => h.SerialNumber == serNO);
                    Console.WriteLine(hardware != null ? $"Found:\n" : "Hardware not found\n");
                    if (hardware != null)
                    {
                        HardwareManager.updateHardware(hardware);
                        Console.WriteLine("Here is the Updated record");
                        hardware.display();
                    }
                    break;
                default:
                    Console.WriteLine("Enter valid choice");
                    break;
            }
        }

        public static void DeleteAssest()
        {
            Console.WriteLine("Select the type of Assest you want to Delete");
            Console.WriteLine("1. Books\n2. Softwares\n3. Hardwares");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the serial Number");
                    int sNO = Convert.ToInt32(Console.ReadLine());
                    if (booksList.Exists(b => b.SerialNumber == sNO))
                    {
                        var book = booksList.RemoveAll(b => b.SerialNumber == sNO);
                        Console.WriteLine("Item Removed successfully");

                    }
                    else
                    {
                        Console.WriteLine("Item not found");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter the serial Number");
                    int seNO = Convert.ToInt32(Console.ReadLine());
                    if (licenseList.Exists(b => b.SerialNumber == seNO))
                    {
                        var software = licenseList.RemoveAll(s => s.SerialNumber == seNO);
                        Console.WriteLine("Item Removed successfully");
                    }
                    else
                    {
                        Console.WriteLine("Item not found");
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter the serial Number");
                    int serNO = Convert.ToInt32(Console.ReadLine());
                    if (hardwareList.Exists(b => b.SerialNumber == serNO))
                    {
                        var hardware = hardwareList.Find(h => h.SerialNumber == serNO);
                        Console.WriteLine("Item Removed successfully");
                    }
                    else
                    {
                        Console.WriteLine("Item not found");
                    }
                    break;
                default:
                    Console.WriteLine("Enter valid choice");
                    break;
            }
        }
        public static void SearchAssest()
        {
            Console.WriteLine("Enter the type of assest you want to search");
            Console.WriteLine("1. Books\n2. Softwares\n3. Hardwares");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the serial Number");
                    int sNO = Convert.ToInt32(Console.ReadLine());
                    var book = booksList.Find(b => b.SerialNumber == sNO);
                    Console.WriteLine(book != null ? $"Found:" : "Book not found\n");
                    if (book != null)
                    {
                        book.display();
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter the serial Number");
                    int seNO = Convert.ToInt32(Console.ReadLine());
                    var software = licenseList.Find(s => s.SerialNumber == seNO);
                    Console.WriteLine(software != null ? $"Found:" : "Software not found\n");
                    if (software != null)
                    {
                        software.display();
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter the serial Number");
                    int serNO = Convert.ToInt32(Console.ReadLine());
                    var hardware = hardwareList.Find(h => h.SerialNumber == serNO);
                    Console.WriteLine(hardware != null ? $"Found:\n" : "Hardware not found\n");
                    if (hardware != null)
                    {
                        hardware.display();
                    }
                    break;
                default:
                    Console.WriteLine("Enter valid choice");
                    break;
            }
        }

        public static void ListAssest()
        {

            Console.WriteLine("Books");
            Console.WriteLine($"{"Serial Number",-15} {"Book Name",-20} {"Author Name",-20} {"Publish Date",-15}");
            foreach (var book in booksList)
            {
                book.display();
            }
            Console.WriteLine("\nSoftwares");
            Console.WriteLine($"{"Serial Number",-15} {" Model Name",-20} {"Manufacturer",-15}");
            foreach (var software in licenseList)
            {
                software.display();
            }
            Console.WriteLine("\nHardwares");
            Console.WriteLine($"{"Serial Number",-15} {"Software Name",-20} {"License Key",-15} {"Expiry Date",-15}");
            foreach (var hardware in hardwareList)
            {
                hardware.display();
            }
            Console.WriteLine("\n");
        }
    }
}
