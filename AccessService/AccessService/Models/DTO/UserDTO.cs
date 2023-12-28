﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using DbContextUtility.Models;

namespace AccessService.Model.DTO
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone Number")]
        public string Phonenumber { get; set; }

        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'.")]
        public string Gender { get; set; }


        [DataType(DataType.Date)]
        public DateOnly DOB { get; set; }

        public Boolean Status { get; set; } = true;

        public static explicit operator UserDTO(UserEntity data)
        {
            return new UserDTO { Id = data.Id, Name = data.Name, RoleId = data.RoleId, Email = data.Email, Password = data.Password, Phonenumber = data.Phonenumber, Gender = data.Gender, DOB = data.DOB, Status = data.Status };
        }

        public static implicit operator UserEntity(UserDTO data)
        {
            return new UserEntity { Id = data.Id, Name = data.Name, RoleId = data.RoleId, Email = data.Email, Password = data.Password, Phonenumber = data.Phonenumber, Gender = data.Gender, DOB = data.DOB, Status = data.Status };
        }
    }
}
