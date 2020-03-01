using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            String url = "https://www.pja.edu.pl/";

            if (args.Length > 0 && args[0] != null)
            {
                url = args[0];
                Console.WriteLine("Url passed. Using: " + url);
            } else
            {
                Console.WriteLine("Url not passed. Using default: " + url);
            }

            HttpClient httpClient = new HttpClient();
            String response = await httpClient.GetStringAsync(url);

            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(response);


            Console.WriteLine();
            Console.WriteLine("Found emails: ");
            foreach (Match emailMatch in emailMatches)
            {
                Console.WriteLine(emailMatch.Value);
            }
        }
    }
}
