﻿using System.ComponentModel.DataAnnotations;

namespace Project.MVCUI.Models.ViewModels.AppUsers
{
    public class UserRegisterRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        //[Required(ErrorMessage ="Email alanı gereklidir")]
        public string Email { get; set; }

    }
}
