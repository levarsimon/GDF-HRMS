﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GDF_HRMS_v1.Models
{
    public class CountryDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
