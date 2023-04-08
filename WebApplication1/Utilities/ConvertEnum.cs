namespace WebApplication1.Utilities
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
                case 4:
                    stt = EnumStatus.Stripe.ToString();
                    break;
                case 5:
                    stt = EnumStatus.VNPay.ToString();
                    break;
                case 6:
                    stt = EnumStatus.Paid.ToString();
                    break;
                case 7:
                    stt = EnumStatus.Unpaid.ToString();
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
                case "Stripe":
                    status = 4;
                    break;
                case "VNPay":
                    status = 5;
                    break;
                case "Paid":
                    status = 6;
                    break;
                case "Unpaid":
                    status = 7;
                    break;
            }
            return status;
        }

    }
}
