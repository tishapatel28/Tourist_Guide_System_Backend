﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class LocationViewModel
    {
        public Guid ID { get; set; }
        public String name { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [DataType("varchar(50)")]
        public String address { get; set; }

        [Required(ErrorMessage = "Enter City")]
        [DataType("varchar(30)")]
        public String city { get; set; }

        [Required(ErrorMessage = "Enter State")]
        [DataType("varchar(30)")]
        public String state { get; set; }

        [Required(ErrorMessage = "Enter Country")]
        [DataType("varchar(30)")]
        public String country { get; set; }

        [Required(ErrorMessage = "Enter Zipcode")]
        [DataType("varchar(30)")]
        public String zip_code { get; set; }

        [Required(ErrorMessage = "Enter Latitude")]
        [DataType("varchar(30)")]
        public String latitude { get; set; }

        [Required(ErrorMessage = "Enter Longitude")]
        [DataType("varchar(30)")]
        public String longitude { get; set; }
    }

    public class LocationInsertViewModel
    {
        public String name { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [DataType("varchar(50)")]
        public String address { get; set; }
        [Required(ErrorMessage = "Enter City")]
        [DataType("varchar(30)")]
        public String city { get; set; }
        [Required(ErrorMessage = "Enter State")]
        [DataType("varchar(30)")]
        public String state { get; set; }
        [Required(ErrorMessage = "Enter Country")]
        [DataType("varchar(30)")]
        public String country { get; set; }
        [Required(ErrorMessage = "Enter Zipcode")]
        [DataType("varchar(30)")]
        public String zip_code { get; set; }
        [Required(ErrorMessage = "Enter Latitude")]
        [DataType("varchar(30)")]
        public String latitude { get; set; }
        [Required(ErrorMessage = "Enter Longitude")]
        [DataType("varchar(30)")]
        public String longitude { get; set; }
    }

    public class LocationUpdateViewModel : LocationInsertViewModel
    {
        public Guid ID { get; set; }
    }
}
