using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soft98.Core.ViewModels
{
    public class ForgetViewModel
    {
        [Display(Name ="شماره موبایل")]
        [Required(ErrorMessage ="مقدار {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage ="مقدار {0} نمی تواند بیشتر از {1} باشد")]
        public string Mobile { get; set; }
    }
}
