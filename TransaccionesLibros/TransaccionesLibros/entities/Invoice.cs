using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransaccionesLibros.entities
{
    public class Invoice
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public Payment_Method Payment_Method { get; set; }
        public string Client { get; set; }
        public List<Invoice_Details>? ListDetails { get; set; }

        public Invoice()
        {
            Number = 0;
            Date = DateTime.Today;
            Payment_Method = new Payment_Method();
            Client = string.Empty;
            ListDetails = new List<Invoice_Details>();
        }

        public Invoice(int Number, DateTime Date, Payment_Method Payment_Method, string Client, List<Invoice_Details> ListDetails)
        {
            this.Number = Number;
            this.Date = Date;
            this.Payment_Method = Payment_Method;
            this.Client = Client;
            this.ListDetails = ListDetails;
        }

        public override string ToString()
        {
            return "Number: " + Number + " |Date: " + Date + " |Payment Method: " + Payment_Method.Description + " |Client: " + Client;
        }
    }
}
