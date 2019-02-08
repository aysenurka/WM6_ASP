using Admin.Models.Entities;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class AddProductViewModel
    {
        public Product Product { get; set; }
        public string AvatarPath { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
