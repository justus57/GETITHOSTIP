using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GETITHOSTIP
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            ReadcvsFile();
            GetIPAddress("gmail.com");
        }

        private static void ReadcvsFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\DATA.CSV";
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Employees>().ToArray();
            }
            Console.ReadKey();
        }

        [Obsolete]
        private static void GetIPAddress(string IpAddress)
        {
            IPAddress[] iPs = Dns.GetHostAddresses(IpAddress);
            Console.WriteLine($"GetHostAddresses({IpAddress}) Returns:");
            foreach (IPAddress address in iPs)
            {
                Console.WriteLine($"Adress:{address}");
                var info = Dns.GetHostByAddress(address);
                Console.WriteLine($"Host by Address {info.HostName}");
                Console.ReadKey();
            }

        }
    }
    public class Employees
    {
        public string EmployeeID { get; set; }
        public string ManagerID { get; set; }
        public string Salary { get; set; }
    }
}
