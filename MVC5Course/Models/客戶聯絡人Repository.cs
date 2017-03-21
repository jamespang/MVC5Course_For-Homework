using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Find(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => false == p.IsDeleted);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            //base.Delete(entity);
            entity.IsDeleted = true;
        }

        public 客戶聯絡人 SearchEmail(int? customerId, string email)
        {
            return this.All().FirstOrDefault(p => p.客戶Id == customerId && p.Email == email);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}