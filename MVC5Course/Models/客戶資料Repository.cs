using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Find(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => false == p.IsDeleted);
        }

        public override void Delete(客戶資料 entity)
        {
            //base.Delete(entity);
            entity.IsDeleted = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}