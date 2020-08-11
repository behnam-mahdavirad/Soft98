using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soft98.Core.ViewModels
{
    public class ActiveViewModel
    {
        [Display(Name ="کد فعالسازی")]
        [MaxLength(6, ErrorMessage ="مقدار {0} نمی تواند بیشتر از {1} باشد")]
        public string Code { get; set; }
    }
}
