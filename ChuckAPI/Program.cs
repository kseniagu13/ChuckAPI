using System;
using System.Collections.Generic;
using System.IO;
using System.Net; // kuna ühendame internetiga
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; //konverteerime objektiks
using Nancy.Json;

namespace ChuckAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowCategories(); //tegime sellest funktsiooni, et kutsuda kategooriad välja
            Console.WriteLine("Choose category: ");
            string userInput = Console.ReadLine();

            ShowCategoryJoke(userInput);






            Console.ReadLine();
            
        }

        public static void ShowCategories ()
        {

            // loome uus päring et saada kätte andmed. Kutsume välja. Selle jaoks on vaja HttpWebRequest
            string categoryUrl = "https://api.chucknorris.io/jokes/categories";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryUrl); //lõime kutset serveriga
            //täpsustame meetodi ehk GET meetod
            request.Method = "GET";

            var webResponse = request.GetResponse(); //var on üldine, wevresponse-l pole kindlat tüüpi andmeid,
            //tagastab vastuse serverist ja salvestab sinna webResponse sisse.
            var webStream = webResponse.GetResponseStream(); // teeb ühenduse lahti ja saab andmeid lugeda (teeb
            //lahti loeb ja paneb kinni). Nii on turvalisem kui hoida andmed alati lahti

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd(); //loeb maha kõik sümbolid mida ta saab serverist  kätte
                //Console.WriteLine(response);
                JavaScriptSerializer ser = new JavaScriptSerializer(); //lõime uue objekti,konverteerib sõna massiiviks
                var categories = ser.Deserialize<List<string>>(response); //loob listi, saab responsist andmed kätte saada

                foreach (string category in categories)
                {
                    Console.WriteLine(category);
                }
            }
        }

        public static void ShowRandomJokes()
        {
            string randomJokeUrl = "https://api.chucknorris.io/jokes/random";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(randomJokeUrl); //lõime kutset serveriga
            //täpsustame meetodi ehk GET meetod
            request.Method = "GET";

            var webResponse = request.GetResponse(); //var on üldine, wevresponse-l pole kindlat tüüpi andmeid,
            //tagastab vastuse serverist ja salvestab sinna webResponse sisse.
            var webStream = webResponse.GetResponseStream(); // teeb ühenduse lahti ja saab andmeid lugeda (teeb
            //lahti loeb ja paneb kinni). Nii on turvalisem kui hoida andmed alati lahti
            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd(); //loeb maha kõik sümbolid mida ta saab serverist  kätte
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);
            }
        }

        public static void ShowCategoryJoke(string userInput)
        {
            string categoryChoiceUrl = $"https://api.chucknorris.io/jokes/random?category={userInput}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryChoiceUrl);
            request.Method = "GET";
            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();
            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd(); //loeb maha kõik sümbolid mida ta saab serverist  kätte
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);


            }
        }
    }
}
