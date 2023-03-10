﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.entities
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("acc_id", TypeName = "int")]
        public int ID { get; set; }

        [Column("acc_email", TypeName = "varchar")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Category name must bigger than 10")]
        public string Email { get; set; }

        [Column("acc_password", TypeName = "varchar")]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Column("acc_name", TypeName = "nvarchar")]
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please fill out your name")]
        public string Name { get; set; }

        [Column("acc_birth", TypeName = "varchar")]
        [Display(Name = "Birth")]
        public string Birth { get; set; }

        [Column("acc_phone", TypeName = "varchar")]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Column("acc_image", TypeName = "varchar")]
        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        [Column("acc_image_base64", TypeName = "ntext")]
        public string AvatarBase64 { get; set; }

        [Column("acc_history", TypeName = "ntext")]
        public string History { get; set; }

        [Column("acc_location", TypeName = "ntext")]
        public string Location { get; set; }

        [Column("acc_status", TypeName = "bit")]
        public bool Status { get; set; }

        [Column("acc_type", TypeName = "int")]
        public int Type { get; set; }

        [Column("acc_deli_address", TypeName = "nvarchar")]
        [Required(ErrorMessage = "Please fill out your delivery address")]
        public string DeliAddress { get; set; }

        [Column("acc_ip", TypeName = "varchar")]
        public string IP { get; set; }
        public Account()
        {

        }


    }
}
