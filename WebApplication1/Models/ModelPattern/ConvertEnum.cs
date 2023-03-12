namespace WebApplication1.Models.ModelPattern
{
    public class ConvertEnum
    {
        protected string stt;

        public string Convert(int status)
        {
            switch (status)
            {
                case 0:
                    stt = EnumStatus.Inactive.ToString();
                    break;
                case 1:
                    stt = EnumStatus.Active.ToString();
                    break;
                case 2:
                    stt = EnumStatus.Admin.ToString();
                    break;
                case 3:
                    stt = EnumStatus.Customer.ToString();
                    break;
            }
            return stt;
        }
    }
}
