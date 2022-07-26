﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UserPayment.API.Models
{
    public class UserForUpdateDto
    {
        [Required]
        [MaxLength(20)]
        public string AccountName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        public double Balance { get; set; } = 0;
        [Required]
        public string AdeptReference { get; set; }
        public string DebtType { get; set; }
    }
}
