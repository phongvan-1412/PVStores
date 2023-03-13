namespace WebApplication1.Models.ModelPattern
{
    public class ConvertEnum
    {
        public static string Convert(int status)
        {
            string stt = "";

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

        public static int ConvertToString(string stt)
        {
            int status = 0;
            switch (stt)
            {
                case "Inactive":
                    status = 0;
                    break;
                case "Active":
                    status = 1;
                    break;
                case "Admin":
                    status = 2;
                    break;
                case "Customer":
                    status = 3;
                    break;
            }
            return status;
        }

    }
}
