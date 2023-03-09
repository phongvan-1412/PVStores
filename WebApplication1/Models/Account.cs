using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Account
    {
        [Key]
        public int acc_id { get; set; }
        public string acc_email { get; set; }
        public string acc_password { get; set; }
        public string acc_name { get; set; }
        public string acc_birth { get; set; }
        public string acc_phone { get; set; }
        public string acc_image { get; set; }
        public string acc_image_base64 { get; set; }
        public string acc_history { get; set; }
        public string acc_location { get; set; }
        public int acc_status { get; set; }
        public int acc_type { get; set; }
        public string acc_deli_address { get; set; }
        public string acc_ip { get; set; }
        public Account()
        {

        }


    }
}
