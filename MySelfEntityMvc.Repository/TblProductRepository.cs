using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySelfEntityMvc.Models.Entity;
using MySelfEntityMvc.UtilityTools.Data;

namespace MySelfEntityMvc.Repository
{
    public class TblProductRepository : BaseRepository<TblProduct>
    {
        public void Test()
        {


            var list = (from product in this.Entities
                        join category in this.UnitOfWork.ObjectContext.Set<TblProductCategory>()
                      on product.CategoryId equals category.Id into productCategory
                        from categoryTemp in productCategory.DefaultIfEmpty()
                        select new
                        {
                            product.ProductName,
                            categoryTemp.CategoryName,
                            categoryTemp.Status
                        }
                            ).ToList();

        }

        public List<TblProduct> TestSql()
        {
            return this.UnitOfWork.ObjectContext.SqlQuery<TblProduct>("select A.* from tblproduct A inner join tblproductCategory B on  A.categoryId = b.Id ", new object[0]).ToList();
        }
    }
}
