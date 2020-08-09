using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Soft98.DataAccessLayer.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public int IdRole { get; set; }
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "مقدار  {0} را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نمی تواند بیشتر از {1} باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نمی تواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "کد فعالسازی")]
        [MaxLength(6, ErrorMessage = "مقدار {0} نمی تواند بیشتر از {1} باشد")]
        public string Code { get; set; }

        [Display(Name ="فعال/غیرفعال")]
        public bool IsActive { get; set; }

        [ForeignKey("IdRole")]
        public virtual Role Role { get; set; }
    } // end public class User

} // end namespace Soft98.DataAccessLayer.Entities
