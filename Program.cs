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
            LINQJIONT();
            ReadcvsFile();
            GetIPAddress("gmail.com");
            Abstractclass();
        }

        private static void Abstractclass()
        {
            AbsChild absChild = new AbsChild();
            absChild.Add(10, 5);
            absChild.Sub(10, 5);
            absChild.Mul(10, 5);
            absChild.Div(10, 2);
            Console.ReadKey();
        }

        private static void ReadcvsFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\DATA.CSV";
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Employees>().ToArray();
            }
           
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
               
            }
        }
        private static void LINQJIONT()
        {
            var employees = new List<EMP>
            {
                new EMP{ DEPTid = 123,EmpName="Chris"},
                new EMP{ DEPTid = 124,EmpName="Mark"},
                new EMP{ DEPTid = 125,EmpName="Steve"},
                 new EMP{ DEPTid = 126,EmpName="Stacy"},
                 new EMP{ DEPTid = 127,EmpName="Jason"}
            };
            var dpmt = new List<DPT>
            {
                new DPT{ ID =101, Name="DEV"},
                new DPT{ ID =102, Name="QA"},
                new DPT{ ID=103, Name ="Support"}               
            };

            var output = (from dpt in dpmt
                         join emp in employees on dpt.ID equals emp.DEPTid into jioned
                         from empdata in jioned.DefaultIfEmpty()
                         select new { DeptName = dpt.Name, EmpName = empdata?.EmpName ?? string.Empty }).ToArray();           
        }
    }
    public abstract class AbsParent
    {
        public void Add(int x, int y)
        {
            Console.WriteLine($"Addition of {x} and {y} is : {x + y}");
        }
        public void Sub(int x, int y)
        {
            Console.WriteLine($"Subtraction of {x} and {y} is : {x - y}");
        }
        public abstract void Mul(int x, int y);
        public abstract void Div(int x, int y);
    }
    public class AbsChild : AbsParent
    {
        public override void Mul(int x, int y)
        {
            Console.WriteLine($"Multiplication of {x} and {y} is : {x * y}");
        }
        public override void Div(int x, int y)
        {
            Console.WriteLine($"Division of {x} and {y} is : {x / y}");
        }
    }
    public  class EMP
    {
        public string EmpName { get; set; }
        public int DEPTid { get; set; }
    }
    public class DPT
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class Employees
    {
        public string EmployeeID { get; set; }
        public string ManagerID { get; set; }
        public string Salary { get; set; }
    }
}
