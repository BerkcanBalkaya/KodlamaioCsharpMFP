using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>:IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext: DbContext,new()
    {
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList() //DbSetteki product tablosuna yerleş tüm tabloyu listeye çevir ve bunu bize ver 
                    : context.Set<TEntity>().Where(filter).ToList();

            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public void Add(TEntity entity)
        {
            //using içerisine yazdığımız nesneleri bittiği gibi garbage collector toplar çünkü bunun içerisine yazacağımız şeyler pahalıdır
            //IDisposable pattern implementation of c#
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);  //referansı yakala çünkü ilk önce referansa ulaşılmalı. Northwind contexte bağla bu entryi diyor sağ taraf
                addedEntity.State = EntityState.Added; //eklenecek bir nesne olarak göster
                context.SaveChanges(); //işlemi kaydet
                
                /*
                Parametrede verilen nesneyi tablodaki bir kayıt ile eşleştirmeye yarar.
                Add kısmında bunu yaparken eşleştirme anlamında yapmıyor ama silme veya güncelleme işlemlerinde
                listeden silerken referansı yakalamamız gerektiği gibi databaseden silerken de bunu yapmamız gerekiyor.
                Sonraki satır ise bunun silinecek, güncellenecek, eklenecek eleman olduğunu contexte bildirir.
                */

            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity); //referansı yakala çünkü ilk önce referansa ulaşılmalı. Northwind contexte bağla bu entryi diyor sağ taraf
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges(); //işlemi kaydet
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity); //referansı yakala çünkü ilk önce referansa ulaşılmalı. Northwind contexte bağla bu entryi diyor sağ taraf
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges(); //işlemi kaydet
            }
        }
    }
}
