using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaConsola.data;
using LibreriaConsola.entities;
using TransaccionesLibros.services;

namespace LibreriaConsola.services
{
    public class Payment_MethodService : IService<Payment_Method, int>
    {

        private IRepository<Payment_Method, int> PaymentMethodRepo;

        public Payment_MethodService()
        {
            PaymentMethodRepo = new Payment_MethodRepository();
        }

        public List<Payment_Method>? Get()
        {
            return PaymentMethodRepo.GetAll();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Payment_Method? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Post(Payment_Method p)
        {
            throw new NotImplementedException();
        }

        public int Put(Payment_Method p)
        {
            throw new NotImplementedException();
        }
    }
}
