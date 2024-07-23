using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NewsApiApp
{
    class Program
    {
        private static readonly string apiKey = "dc06c39a2ae4451fb4467cf79c0de49c";
        private static readonly string apiUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey="+ apiKey;

        static async Task Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        NewsApiResponse newsResponse = JsonConvert.DeserializeObject<NewsApiResponse>(jsonResponse);

                        if (newsResponse.Status == "ok")
                        {
                            Console.WriteLine("Artigos de Notícias:");
                            foreach (var article in newsResponse.Articles)
                            {
                                Console.WriteLine("Autor: " + (article.Author ?? "Desconhecido"));
                                Console.WriteLine("Título: " + article.Title);
                                Console.WriteLine("Descrição: " + article.Description);
                                Console.WriteLine(new string('-', 40));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao acessar a API. Código de status: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exceção ao acessar a API: {ex.Message}");
                }
            }
        }
    }

    public class NewsApiResponse
    {
        public string ?Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article> ?Articles { get; set; }
    }

    public class Article
    {
        public string ?Author { get; set; }
        public string ?Title { get; set; }
        public string ?Description { get; set; }
    }
}

