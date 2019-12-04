using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    public class StaffRegisterViewModel
    {
        [Required]
        [Display(Name = "帳號")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符。")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "{0} 的長度必須至少為 {2} 個字元", MinimumLength = 1)]
        [Display(Name = "警衛編號")]
        public string StaffID { get; set; }
    }
}