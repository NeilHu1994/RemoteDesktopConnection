
using System;
using System.Text.RegularExpressions;

namespace Telsafe
{
    class Program
    {
        static void Main(string[] args)
        {

            var info = new LoginInfo();
        #if debug
            Console.WriteLine("please enter ipAddress");
            while (true)
            {
                info.Ipaddress = Console.ReadLine();
                if (new Regex(RdpConstant.IpaddressPatten).IsMatch(info.Ipaddress))
                {
                    break;
                }
            }
            Console.WriteLine("please enter username");
            info.Username = Console.ReadLine();
            Console.WriteLine("please enter password");
            info.Password = Console.ReadLine();
         #else
            info.Ipaddress = "120";
            info.Username = "Adm";
            info.Password = "wu";
        #endif
            RdpHandler.Rrocess(info);
        }
    }
}
