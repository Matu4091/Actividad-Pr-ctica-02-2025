using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransaccionesLibros.entities
{
    public class Book
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pags_Number { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }

        public Book()
        {
            Isbn = string.Empty;
            Title = string.Empty;
            Author = string.Empty;
            Pags_Number = 0;
            Stock = 0;
            Price = 0;
        }

        public Book(string Isbn, string Title, string Author, int Pags_Number, int Stock, double Price)
        {
            this.Isbn = Isbn;
            this.Title = Title;
            this.Author = Author;
            this.Pags_Number = Pags_Number;
            this.Stock = Stock;
            this.Price = Price;
        }

        public override string ToString()
        {
            return "ISBN: " + Isbn + " |Title: " + Title + " |Author: " + Author;
        }
    }
}
