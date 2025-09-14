using LibreriaConsola.entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibreriaConsola.data
{
    internal class Invoice_DetailsRepository
    {
        public bool Delete(int invoiceNumber)
        {
            throw new NotImplementedException();
        }

        public List<Invoice_Details>? GetAll(int idInvoice)
        {
            List<Invoice_Details> invoice_Details = new List<Invoice_Details>();

            Parameter p = new Parameter("@nro_factura", idInvoice);


            foreach (DataRow row in DataHelper.GetInstance().ExecuteSPRead("OBTENER_DETALLES_FACTURAS", p).Rows)
            {
                invoice_Details.Add(new Invoice_Details()
                {
                    Book = new Book()
                    {
                        Isbn = (string)row["isbn"],
                        Title = (string)row["titulo"],
                        Author = (string)row["autor"],
                        Pags_Number = Convert.ToInt32(row["nro_paginas"]),
                        Stock = Convert.ToInt32(row["stock"])
                    },
                    Amount = Convert.ToInt32(row["cantidad"]),
                    Invoice_Number = Convert.ToInt32(row["nro_factura"])
                });
            }

            return invoice_Details;
        }

        public Invoice_Details? GetById(int idDetail)
        {
            throw new NotImplementedException();
        }

        public bool Save(List<Invoice_Details> details)
        {
            if (details != null)
            {
                int rowAffected = 0;
               
                string sp = "AGREGAR_DETALLE";

                foreach (Invoice_Details i in details)
                {
                    List<Parameter> p = new List<Parameter>();

                    p.Add(new Parameter("@isbn", i.Book.Isbn));
                    p.Add(new Parameter("@cantidad", i.Amount));
                    p.Add(new Parameter("@nro_factura", i.Invoice_Number));

                    var (AffectedRows, newId) = DataHelper.GetInstance().ExecuteSPModify(sp, p);

                    rowAffected += AffectedRows;
                }               

                return rowAffected == details.Count;
            }
            return false;
        }
    }
}
