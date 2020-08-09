using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soft98.DataAccessLayer.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name="نام نقش")]
        [Required(ErrorMessage ="مقدار  {0} را وارد نمایید")]
        [MaxLength(20,ErrorMessage ="مقدار {0} نمی تواند بیشتر از {1} باشد")]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    } // end public class Role

} // end namespace Soft98.DataAccessLayer.Entities
