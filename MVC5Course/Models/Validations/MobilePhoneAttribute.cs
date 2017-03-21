using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVC5Course.Models.Validations
{
    public class MobilePhoneAttribute : DataTypeAttribute
    {
        public MobilePhoneAttribute() : base(DataType.Text)
        {
            this.ErrorMessage = "行動電話格式錯誤！";
        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value); // 避免引發"並未將物件參考設定為物件的執行個體"的例外
             
            if (str != "")
            {
                Regex rgx = new Regex(@"\d{4}-\d{6}");
                return rgx.IsMatch(value.ToString()) ? true : false;
            }
            return true;
        }
    }
}