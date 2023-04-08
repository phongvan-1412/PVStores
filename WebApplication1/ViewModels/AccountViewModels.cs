using WebApplication1.Utilities;
using WebApplication1.Models.entities;

namespace WebApplication1.ViewModels
{
    public class AccountViewModels
    {
        public int ID { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }

        public string Birth { get; set; }

        public string Phone { get; set; }

        public string Avatar { get; set; }

        public IFormFile AvatarFile { get; set; }

        public string AvatarBase64 { get; set; }

        public string History { get; set; }

        public string Location { get; set; }

        public bool Status { get; set; }

        public int Type { get; set; }

        public string DeliAddress { get; set; }

        public string IP { get; set; }

        public string GoogleID { get; set; }

        public string FacebookID { get; set; }

        public AccountViewModels(Account account)
        {
            this.Email = account.Email;
            this.Password = account.Password;
            this.Name = account.Name;
            this.Birth = account.Birth;
            this.Phone = account.Phone;
            this.Avatar = account.Avatar;
            this.AvatarBase64 = account.AvatarBase64;
            this.History = account.History;
            this.Location = account.Location;
            this.Status = false;
            this.Type = (int)EnumStatus.Customer;
            this.DeliAddress = account.DeliAddress;
            this.IP = account.IP;
            this.FacebookID = account.FacebookID;
            this.GoogleID = account.GoogleID;
        }
    }
}
