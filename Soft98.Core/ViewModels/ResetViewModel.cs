using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soft98.Core.ViewModels
{
    public class ResetViewModel
    {
        [Display(Name ="کد تایید")]
        [MaxLength(6,ErrorMessage ="مقدار {0} نمی تواند بیشتر از {1} باشد")]
        public string Code { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نمی تواند بیشتر از {1} باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نمی تواند بیشتر از {1} باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="کلمه های عبور با یکدیگر همخوانی ندارند")]
        public string RePassword { get; set; }
    }
}
