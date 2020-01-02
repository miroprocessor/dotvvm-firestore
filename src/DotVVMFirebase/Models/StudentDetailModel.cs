﻿using Google.Cloud.Firestore;
using System;
using System.ComponentModel.DataAnnotations;

namespace DotVVMFirebase.Models
{
    public class StudentDetailModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }
        public string About { get; set; }

    }
}
