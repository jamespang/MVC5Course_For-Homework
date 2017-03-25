namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(公司基本資料MetaData))]
    public partial class 公司基本資料
    {
    }
    
    public partial class 公司基本資料MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        [Required]
        public string 客戶分類 { get; set; }
    }
}
