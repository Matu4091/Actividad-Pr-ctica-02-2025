using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransaccionesLibros.services
{
    public interface IService<T,D> 
    {
        public List<T>? Get();
        public T? GetById(D value);
        public int Post(T entity);
        public int Put(T entity);
        public int Delete(D value);

    }
}
