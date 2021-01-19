using botClientBot.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace botClientBot
{
    public class Program
    {
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
            client = new TelegramBotClient(Config.Token);

            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.WriteLine("[Log]: Bot started;");
            Console.ReadLine();
            client.StopReceiving();
        }

        private async static void OnMessageHandler(object sender, MessageEventArgs e)
        {
            /*
                         var person = new Person();
            person.Name = "John Doe";
            person.Occupation = "gardener";

            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://httpbin.org/post";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
             */
            if (e.Message.Text != null)
            {
                var message = e.Message;
                Console.WriteLine($"[{e.Message.From.FirstName ?? ""} {e.Message.From.LastName ?? ""}]: {e.Message.Text ?? ""}");
                var mess = new Message();
                mess.Text = message.Text ?? "";
                mess.Telegramchatid = message.Chat.Id;
                mess.Telegramuserid = message.From.Id;
                mess.Username = message.From.Username ?? "";
                if(message.Chat.Id < 0)
                {
                    mess.Username = message.Chat.Title ?? "";
                }
                mess.Firstname = message.From.FirstName ?? "";
                mess.Lastname = message.From.LastName ?? "";

                var json = JsonConvert.SerializeObject(mess);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://botclientapi.dobbikov.ga/api/Messages";
                var response = await FetchMessage(mess, url, data);

                string result = response.Content.ReadAsStringAsync().Result;
            }
        }
        public async static Task<HttpResponseMessage> FetchMessage(Message message, string url, StringContent data)
        {
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            return response;
        }
        public async static void SendMessage(long chatid, string text)
        {
            await client.SendTextMessageAsync(chatid, text);
        }
    }
}
