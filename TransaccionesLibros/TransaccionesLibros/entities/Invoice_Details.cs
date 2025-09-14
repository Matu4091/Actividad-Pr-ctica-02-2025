using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransaccionesLibros.entities
{
    public class Invoice_Details
    {
        public Book Book { get; set; }
        public int Amount { get; set; }
        public int Invoice_Number { get; set; }

        public Invoice_Details()
        {
            Book = new Book();
            Amount = 0;
            Invoice_Number = 0;
        }

        public Invoice_Details(Book Book, int Amount, int Invoice_Number)
        {
            this.Book = Book;
            this.Amount = Amount;
            this.Invoice_Number = Invoice_Number;
        }

        public override string ToString()
        {
            return "Book: " + Book + " - Amount: " + Amount;
        }
    }
}
