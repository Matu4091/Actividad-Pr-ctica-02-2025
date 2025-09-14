using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaConsola.entities;

namespace LibreriaConsola.data
{
    internal class BookRepository : IRepository<Book, string>
    {
        public bool Delete(string isbn)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("@isbn", isbn));

            var (affectedRows, newId) = DataHelper.GetInstance().ExecuteSPModify("DAR_BAJA_LIBRO", parameters);

            return affectedRows > 0;
        }

        public List<Book>? GetAll()
        {
            List<Book> books = new List<Book>();
            DataTable dt = new DataTable();

            dt = DataHelper.GetInstance().ExecuteSPRead("OBTENER_LIBROS");
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    books.Add(new Book()
                    {
                        Isbn = (string)row["isbn"],
                        Title = (string)row["titulo"],
                        Author = (string)row["autor"],
                        Pags_Number = Convert.ToInt32(row["nro_paginas"]),
                        Stock = Convert.ToInt32(row["stock"]),
                        Price = Convert.ToDouble(row["precio"])
                    });
                }

                return books;
            }
            else
            {
                return null;
            }
        }

        public Book? GetById(string isbn)
        {
            DataTable dt = new DataTable();
            Parameter? p;

            p = new Parameter("@isbn", isbn);

            dt = DataHelper.GetInstance().ExecuteSPRead("OBTENER_LIBRO_X_ISBN", p);          

            if (dt.Rows.Count > 0)
            {
                Book book = new Book()
                {
                    Isbn = (string)dt.Rows[0]["isbn"],
                    Title = (string)dt.Rows[0]["titulo"],
                    Author = (string)dt.Rows[0]["autor"],
                    Pags_Number = Convert.ToInt32(dt.Rows[0]["nro_paginas"]),
                    Stock = Convert.ToInt32(dt.Rows[0]["stock"]),
                    Price = Convert.ToDouble(dt.Rows[0]["precio"])
                };
                return book;
            }
            else
            {
                return null;
            }
        }

        public bool Save(Book book)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("@isbn", book.Isbn));
            parameters.Add(new Parameter("@titulo", book.Title));
            parameters.Add(new Parameter("@autor", book.Author));
            parameters.Add(new Parameter("@nro_paginas", book.Pags_Number));
            parameters.Add(new Parameter("@stock", book.Stock));
            parameters.Add(new Parameter("@precio", book.Price));

            var (affectedRows, newId) = DataHelper.GetInstance().ExecuteSPModify("MODIFICAR_LIBROS", parameters);

            return affectedRows > 0;          
        }
    }
}
