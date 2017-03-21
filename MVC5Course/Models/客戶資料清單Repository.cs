using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class 客戶資料清單Repository : EFRepository<客戶資料清單>, I客戶資料清單Repository
	{

	}

	public  interface I客戶資料清單Repository : IRepository<客戶資料清單>
	{

	}
}