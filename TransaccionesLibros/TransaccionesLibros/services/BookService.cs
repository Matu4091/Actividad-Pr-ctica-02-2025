using TransaccionesLibros.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransaccionesLibros.entities;
using TransaccionesLibros.services;

namespace TransaccionesLibros.services
{
    public class BookService : IService<Book, string>
    {
        private IRepository<Book, string> BookRepo;

        public BookService()
        {
            BookRepo = new BookRepository();
        }

        //1: Ok (Operacion Exitosa)
        //2: BadRequest (Operacion Fallida)
        //3: BadRequest (Libro Existente)
        //4: NotFound (Libro no Encontrado)

        public List<Book>? Get()
        {
            return BookRepo.GetAll();
        }

        public Book? GetById(string isbn)
        {
            return BookRepo.GetById(isbn);
        }

        private bool ValidateExisting(string isbn)
        {
            Book? b = BookRepo.GetById(isbn);
            if (b == null) { return true; }
            else { return false; }
        }
     
        public int Post(Book book)
        {
            if (ValidateExisting(book.Isbn))
            {
                if (BookRepo.Save(book))
                {
                    return 1;
                }
                else { return 2; }
            }
            else
            {
                return 3;
            }
        }

        public int Put(Book book)
        {
            if (!ValidateExisting(book.Isbn))
            {
                if (BookRepo.Save(book))
                {
                    return 1;
                }
                else { return 2; }
            }
            else
            {
                return 4;
            }
        }

        public int Delete(string isbn)
        {
            if (!ValidateExisting(isbn))
            {
                if (BookRepo.Delete(isbn))
                {
                    return 1;
                }
                else { return 2; }
            }
            else
            {
                return 4;
            }
        }
    }
}
