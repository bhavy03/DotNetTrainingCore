using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Assest Management System");

            while (true)
            {
                Console.WriteLine("1. Add an assest\n2. Search an asset\n3. Update an asset\n4. Delete an asset\n5. List of all available assets.\n6. Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AssestManager.AddAssest();
                        break;
                    case 2:
                        AssestManager.SearchAssest();
                        break;
                    case 3:
                        AssestManager.UpdateAssest();
                        break;
                    case 4:
                        AssestManager.DeleteAssest();
                        break;
                    case 5:
                        AssestManager.ListAssest();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Enter a valid choice!");
                        break;
                }
            }
        }
    }
}
