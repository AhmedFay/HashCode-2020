using HashCodeX.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HashCodeX
{
    class Program
    {
        static void Main(string[] args)
        {
            //input
            List<Book> All_Books = new List<Book>();
            List<Library> libs = new List<Library>();
            int CurrentDay = 0;

            string[] lines = File.ReadLines("D:/HashCode/c_incunabula.txt").ToArray();

            string[] fl = lines[0].Split(' ');

            int booksNum = int.Parse(fl[0]);
            int LibbNum = int.Parse(fl[1]);
            int DaysNum = int.Parse(fl[2]);

            string[] bookScore = lines[1].Split(' ');

            for (int i = 0; i < booksNum; i++)
            {
                var book = new Book();
                book.Id = "" + i;
                book.DupCount = 0;
                book.isScanned = false;
                book.Score = int.Parse(bookScore[i]);
                All_Books.Add(book);
            }

            int id = 0;
            for (int i = 2; i < lines.Length - 1; i += 2)
            {
                string[] sibline = lines[i].Split(' ');

                var tempLib = new Library();
                tempLib.books = new List<Book>();
                tempLib.ScanBook = new List<bool>();
                tempLib.Id = "" + id;
                tempLib.BooksNum = sibline[0];
                tempLib.SginUp = int.Parse(sibline[1]);
                tempLib.BookInDay = int.Parse(sibline[2]);
                tempLib.bookToScan = 0;

                foreach (string BookId in lines[i + 1].Split(' '))
                {
                    var bo = All_Books[int.Parse(BookId)];
                    ++bo.DupCount;
                    tempLib.books.Add(bo);
                    tempLib.ScanBook.Add(false);
                }
                tempLib.books.Sort(new Comparison<Book>((i1, i2) => i2.DupCount - i1.DupCount));

                libs.Add(tempLib);

                ++id;
            }
            //libs.Sort(new Comparison<Library>((i1, i2) => i1.BookInDay - i2.BookInDay));
            libs.Sort(new Comparison<Library>((i1, i2) => i2.books.Sum(x => x.Score) - i1.books.Sum(x => x.Score)));
            libs.Sort(new Comparison<Library>((i1, i2) => i1.SginUp - i2.SginUp));


            //SginUp operation
            int RegisterLib = 0;
            foreach (var item in libs)
            {
                CurrentDay += item.SginUp;
                item.RemainingScan = (DaysNum - CurrentDay) * item.BookInDay;
                if (item.RemainingScan > 0)
                {
                    for (int i = 0; i < item.books.Count(); i++)
                    {
                        if (item.RemainingScan <= 0)
                        {
                            break;
                        }
                        else if (!item.books[i].isScanned)
                        {
                            item.books[i].isScanned = true;
                            item.ScanBook[i] = true;
                            ++item.bookToScan;
                            --item.RemainingScan;
                        }
                    }

                    if (item.bookToScan <= 0)
                    {
                        continue;
                    }

                    ++RegisterLib;
                }
            }

            //output
            Console.WriteLine("\n" + RegisterLib);
            string result = RegisterLib + "\n";

            foreach (var item in libs)
            {

                if (item.bookToScan <= 0)
                {
                    continue;
                }

                Console.WriteLine(item.Id + " " + item.bookToScan);
                result += item.Id + " " + item.bookToScan + "\n";

                string books = "";
                for (int i = 0; i < item.books.Count(); i++)
                {
                    if (item.bookToScan <= 0)
                    {
                        break;
                    }
                    else if (item.ScanBook[i])
                    {
                        books += item.books[i].Id + " ";
                        --item.bookToScan;
                    }
                }

                //Console.WriteLine(books);
                result += books + "\n";

            }

            File.WriteAllText("D:/HashCode/out_final_c.txt", result);
        }
    }

}
