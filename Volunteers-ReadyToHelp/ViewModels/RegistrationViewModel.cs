﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name= "First Name")]
        [Required(ErrorMessage="Firstname is required")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "The Name field does not accept symbol or digit.")]
        public string FirstName { get; set; }
        [Display(Name= "Last Name")]
        public string LastName { get; set; }
        //[Required]
        //[Display(Name= "Username")]
        //public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        //[Display(Name= "Mobile Number")]
        //[RegularExpression("[0-9]{10}")]
        //public string Mobile { get; set; }
        
        public string DOB { get; set; }
        [Required]
        [Display(Name= "Country")]
        public string CountryId { get; set; }
        [Required]
        [Display(Name = "State")]
        public string StateId { get; set; }
        [Required]
        [Display(Name = "Role Type")]
        public string RoleType { get; set; }
        public string RoleId { get; set; }
        [Display(Name= "User Avatar")]
        public byte[] Avatar { get; set; }
        public string AvatarId { get; set; }
        
        [Display(Name= "About Organization")]
        public string AboutOrganization { get; set; }
        
        [Display(Name= "Established On")]
        public string Establishedon { get; set; }
        public int AbbreviationId { get; set; }
    }
}