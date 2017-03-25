using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class 公司基本資料Repository : EFRepository<公司基本資料>, I公司基本資料Repository
	{

	}

	public  interface I公司基本資料Repository : IRepository<公司基本資料>
	{

	}
}