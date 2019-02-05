using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyWebSite.DAL.MyEntities;

namespace MyWebSite.Models
{
    public class UserVM
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Lütfen isim alanını boş geçmeyiniz")]
        [MaxLength(20, ErrorMessage = "İsim alanı maksimum 20 karakter olabilir")]
        [DisplayName("Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen isim alanını boş geçmeyiniz")]
        [MaxLength(20, ErrorMessage = "Soyisim alanı maksimum 20 karakter olabilir")]
        [DisplayName("Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lütfen isim alanını boş geçmeyiniz")]
        [MaxLength(50, ErrorMessage = "Email alanı maksimum 50 karakter olabilir")]
        [EmailAddress(ErrorMessage = "Lütfen mail formatında giriş yapınız")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifre alanını boş geçmeyiniz")]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen cinsiyet alanını boş geçmeyiniz")]
        [DisplayName("Cinsiyet")]
        public bool Gender { get; set; }
    }
}