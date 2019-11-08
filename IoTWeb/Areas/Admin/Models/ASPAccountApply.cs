using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ASPAccountApply
    {
        [Required]
        [Display(Name = "左邊住戶名字")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "右邊無綁定帳號")]
        public string ASPAccount { get; set; }
    }
}