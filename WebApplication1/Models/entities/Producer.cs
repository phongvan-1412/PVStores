using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.entities
{
    public class Producer
    {
        [Key]
        public int pro_id { get; set; }
        public string pro_name { get; set; }
        public string pro_description { get; set; }
        public int pro_status { get; set; }

        public Producer() { }

    }
}
