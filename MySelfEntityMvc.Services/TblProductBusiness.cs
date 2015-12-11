using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySelfEntityMvc.Models.Entity;
using MySelfEntityMvc.Repository;

namespace MySelfEntityMvc.Services
{
    public class TblProductBusiness 
    {
        TblProductRepository tblProductRepository = new TblProductRepository();

        public void TestJoin()
        {
            tblProductRepository.Test();
          
        }


        public void TestSql()
        {
            var aa = tblProductRepository.TestSql();
        }

        public List<TblProduct> GetAllTest()
        {
            return tblProductRepository.GetAll().ToList();
        }

    }
}
