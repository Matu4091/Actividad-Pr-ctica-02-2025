using LibreriaConsola.data;
using LibreriaConsola.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransaccionesLibros.services;

namespace LibreriaConsola.services
{
    public class UnitOfWork : IService<Invoice, int>
    {
        private InvoiceRepository invoiceRepo;
        private Invoice_DetailsRepository invoiceDetailsRepo;
        DataHelper dataHelper;

        public UnitOfWork()
        {
            invoiceRepo = new InvoiceRepository();
            invoiceDetailsRepo = new Invoice_DetailsRepository();
            dataHelper = DataHelper.GetInstance();
        }

        //1: Ok(Operacion Exitosa)
        //2: BadRequest(Error con Factura)
        //3: BadRequest(Error con Detalles de la Factura)
        //4: NotFound(No se Encontro la Factura)

        public int Post(Invoice i)
        {
            try
            {
                dataHelper.BeginTransaction();

                if (!invoiceRepo.Save(i)) { dataHelper.RollBack(); return 2; }

                if (i.ListDetails != null && i.ListDetails.Count > 0)
                {
                    foreach (Invoice_Details detail in i.ListDetails)
                    {
                        detail.Invoice_Number = i.Number;
                    }
                    if (invoiceDetailsRepo.Save(i.ListDetails))
                    {
                        dataHelper.Commit();
                        return 1;
                    }
                    else
                    {
                        dataHelper.RollBack();
                        return 3;
                    }
                }
                else
                {
                    dataHelper.Commit();
                    return 1;
                }
            }
            catch (Exception)
            {
                dataHelper.RollBack();
                throw;
            }
            finally
            {
                dataHelper.Cleanup();
            }
        }

        public List<Invoice>? Get()
        {
            return invoiceRepo.GetAll();
        }

        public Invoice GetById(int number)
        {
            throw new NotImplementedException();
        }

        private bool ValidateExisting(int id)
        {
            Invoice? i = invoiceRepo.GetById(id);
            if (i == null) { return true; }
            else { return false; }
        }

        public int Put(Invoice i)
        {
            throw new NotImplementedException();
        }

        public int Delete(int number)
        {
            if (!ValidateExisting(number))
            {
                if (invoiceRepo.Delete(number))
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
