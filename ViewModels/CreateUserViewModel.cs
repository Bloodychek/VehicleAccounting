﻿using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.ViewModels
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
