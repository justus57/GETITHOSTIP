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
            //Abstractclass();
            writinginWord();
        }

        private static void writinginWord()
        {
            string isNegative = "";
            try
            {
                Console.WriteLine("Enter a Number to convert to currency");               
                string number = Console.ReadLine();
                number = Convert.ToDouble(number).ToString();

                if (number.Contains("-"))
                {
                    isNegative = "Minus ";
                    number = number.Substring(1, number.Length - 1);
                }
                if (number.Contains(","))
                {
                    number = number.Replace(",", "");
                }
                if (number == "0")
                {
                    Console.WriteLine("The number in currency fomat is \nZero Only");
                }
                else
                {
                    Console.WriteLine("The number in currency fomat is \n{0}", isNegative + ConvertToWords(number));
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    String place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:
                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        case 13:
                        case 14:
                        case 15:
                            pos = (numDigits % 13) + 1;
                            place = " Trillion ";
                            break;
                        case 16:
                        case 17:
                        case 18:
                            pos = (numDigits % 16) + 1;
                            place = " Quadrillion ";
                            break;
                        case 19:
                        case 20:
                        case 21:
                            pos = (numDigits % 19) + 1;
                            place = " Quintillion ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }
        private static String ConvertToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = "Only";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and";// just to separate whole numbers from points/cents    
                        endStr = "Cents " + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return val;
        }
        private static String ConvertDecimals(String number)
        {
            String cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
        }
        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }
        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
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
