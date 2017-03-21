using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class sysdiagramRepository : EFRepository<sysdiagram>, IsysdiagramRepository
	{

	}

	public  interface IsysdiagramRepository : IRepository<sysdiagram>
	{

	}
}