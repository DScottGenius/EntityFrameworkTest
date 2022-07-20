﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}