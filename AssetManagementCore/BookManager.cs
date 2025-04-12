using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement
{
    static class BookManager
    {
        public static Books addBook()
        {
            try
            {
                int sNo;
                while (true)
                {
                    Console.WriteLine("Enter Serial Number:");
                    sNo = Convert.ToInt32(Console.ReadLine());

                    if (AssestManager.booksList.Exists(b => b.SerialNumber == sNo))
                    {
                        Console.WriteLine("This Serial Number already exists. Enter another Serial Number!!");
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("Enter Book Name:");
                string bName = Console.ReadLine();
                Console.WriteLine("Enter Author name: ");
                string aName = Console.ReadLine();
                Console.WriteLine("Enter Date of Publish( format: dd-mm-yyyy): ");
                DateTime pDate = DateTime.Parse(Console.ReadLine());

                Books newBook = new Books()
                {
                    SerialNumber = sNo,
                    Name = bName,
                    author = aName,
                    Date = pDate
                };

                return newBook;
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter values in correct format!!!");
                return null;
            }
        }

        public static void UpdateBooks(Books book)
        {
            try
            {
                Console.WriteLine("Select the value of assest to be updated");
                Console.WriteLine("1. Book Name\n2. Author\n3. Publish Date");
                int value = Convert.ToInt32(Console.ReadLine());
                switch (value)
                {
                    case 1:
                        Console.WriteLine("Enter New name");
                        string newName = Console.ReadLine();
                        book.Name = newName;
                        break;
                    case 2:
                        Console.WriteLine("Enter new Author Name");
                        string newAuthor = Console.ReadLine();
                        book.author = newAuthor;
                        break;
                    case 3:
                        Console.WriteLine("Enter new Date");
                        DateTime newTime = DateTime.Parse(Console.ReadLine());
                        book.Date = newTime;
                        break;
                    default:
                        Console.WriteLine("Enter valid Choice");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter values in correct format!!!");
                //throw;
            }
        }
    }
}
