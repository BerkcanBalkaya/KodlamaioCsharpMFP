using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.DataAccess
{
    //Core katmanı diğer katmanları referans almaz bağımlı olmamalı diğer katmanlara evrensel olmalı

    //generic constraint
    //class : referans tip olabilir demek
    //IEntity :IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //new() : new'lenebilir 
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        //Expression Linq ile gelen ve GetAll metoduna linq filtreleri verebilmemizi sağlayan sınıftır.
        //Örneğin alttaki metod için eğer biz ProductManager içerisinde GetAll(p=> p.productId==1) yazabiliriz.
        //Yazmadığımız takdirde de filtrelemeye tabii tutulmadan bütün ürünler döndürülür.
        //filter = null ise filter vermesekte olabilir gibidir
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        //Tek bir data getirmek içinse
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //List<T> GetAllCategory(int categoryId); yukarıda filter yazabildiğimiz için artık böyle bir koda ihtiyacımız yok
    }
}
