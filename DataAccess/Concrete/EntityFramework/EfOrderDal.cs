using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal:EfEntityRepositoryBase<Order,NorthwindContext>,IOrderDal
    {
        //iOrderDal kullanılmasının sebebi businessda injection yaptığımız için belli ve IOrderDala özel fonksiyonlar olabilir
    }
}
