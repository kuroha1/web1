﻿using System.ComponentModel.DataAnnotations;

namespace online.ModelView
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage ="Vui lòng nhập Email !")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress] 
        [Display(Name ="Địa chỉ Email")]
        public string? UserName { get; set; }

        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu !")]
        [MinLength(5, ErrorMessage ="Mật khẩu tối thiểu 5 ký tự")]
        public string? Password { get; set; }
    }
}
