using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string inputString;

            MyClient client = new MyClient();

            while (true)
            {
                inputString = Console.ReadLine();
                if (Regex.IsMatch(inputString, "^get *$", RegexOptions.IgnoreCase))
                {
                    await client.GetAllData(false);
                }
                else if (Regex.IsMatch(inputString, "^get --sorted *$", RegexOptions.IgnoreCase))
                {
                    await client.GetAllData(true);
                }
                else if (Regex.IsMatch(inputString, "^get --id .+ *$", RegexOptions.IgnoreCase))
                {
                    await client.GetDataByID(inputString.Substring(9));
                }
                else if (Regex.IsMatch(inputString, "^post {.+} *$", RegexOptions.IgnoreCase))
                {
                    await client.PostData(inputString.Substring(5));
                }
                else if (Regex.IsMatch(inputString, "^put \\S+ {.+} *$", RegexOptions.IgnoreCase))
                {
                    string[] commandArgs = inputString.Split(' ');
                    await client.PutData(commandArgs[1], inputString.Substring(4 + commandArgs[1].Length + 1)); // 4 is length of "put "
                }
                else if (Regex.IsMatch(inputString, "^delete \\S+ *$", RegexOptions.IgnoreCase))
                {
                    await client.DeleteData(inputString.Substring(7));
                }
                else if (Regex.IsMatch(inputString, "^exit *$", RegexOptions.IgnoreCase))
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }
    }
}
