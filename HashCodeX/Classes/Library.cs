using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeX.Classes
{
    class Library
    {
        public string Id { get; set; }
        public int SginUp { get; set; }
        public string BooksNum { get; set; }
        public int BookInDay { get; set; }
        public List<Book> books { get; set; }
        public List<bool> ScanBook { get; set; }
        public int RemainingScan { get; set; }
        public int bookToScan { get; set; }

        public string toString()
        {
            string LibDetail = "Lib Num " + Id + " " + BooksNum + " " + SginUp + " " + BookInDay + "\n";
            foreach (var item in books)
            {
                LibDetail += item.Id + "\n";
            }
            return LibDetail;
        }
    }
}
