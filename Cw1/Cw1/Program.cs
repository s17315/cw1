using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("Missing url argument");
            }

            Regex urlRegex = new Regex(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");
            
            if (urlRegex.Matches(args[0]).Count != 1)
            {
                throw new ArgumentException("Argument is not correct url");
            }

            HttpClient httpClient = new HttpClient();

            try
            {
                String response = await httpClient.GetStringAsync(args[0]);

                Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                MatchCollection emailMatches = emailRegex.Matches(response);

                if (emailMatches.Count == 0)
                {
                    Console.WriteLine("Ńie znaleziono adresów email");
                    return;
                }

                foreach (String email in emailMatches.Select(m => m.Value).Distinct())
                {
                    Console.WriteLine(email);
                }
            } catch (Exception e)
            {
                Console.WriteLine("Błąd w czasie pobierania strony");
            } finally
            {
                httpClient.Dispose();
            }
        }
    }
}
