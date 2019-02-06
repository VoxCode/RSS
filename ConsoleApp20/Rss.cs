using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ConsoleApp20
{
    class Rss
    {
        private RssContext db = new RssContext();
        List<int> readNews = new List<int>();
        List<int> saveNews = new List<int>();

        public void AddRssSource(string url, string name)
        {
            RssSource rssSource = new RssSource()
            {
                URL = url,
                Name = name
            };

            db.RssSources.Add(rssSource);
            db.SaveChanges();
        }

        public void DelRssSource()
        {          
            var rssSources = db.RssSources.ToList();

            if (rssSources.Count != 0)
            {
                int[] arr = new int[rssSources.Count];
                int count = 0;
                foreach (var i in rssSources)
                {
                    arr[count] = i.Id;
                    Console.WriteLine("Id: {0} | Имя: {1} | URL: {2}", count, i.Name, i.URL);
                    count++;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\nПожалуйста, введите Id источника, который необходимо удалить!");
                Console.ForegroundColor = ConsoleColor.Gray;
                try
                {
                    int id;
                    int idNews;
                    id = Convert.ToInt32(Console.ReadLine());
                    idNews = arr[id];
                    var item = db.RssSources.FirstOrDefault(p => p.Id == idNews);

                    db.RssSources.Remove(item);
                    db.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nИсточник удален!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка! Недопустимое значение Id!");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Добавте хотя-бы один RSS источник!");
            }
        }

        public void Read()
        {
            var rssSources = db.RssSources.ToList();
            readNews.Clear();

            if (rssSources.Count != 0)
            {
                foreach (var i in rssSources)
                {
                    int count = 0;
                    string xml;
                    string feed = i.URL;

                    //https://www.interfax.by/news/feed
                    //https://habr.com/ru/rss/all/all/

                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            webClient.Encoding = Encoding.UTF8;
                            xml = webClient.DownloadString(feed).TrimStart();
                        }
                        StringReader reader = new StringReader(xml);
                        XmlReader xmlReader = XmlReader.Create(reader);
                        SyndicationFeed channel = SyndicationFeed.Load(xmlReader);

                        foreach (var item in channel.Items)
                        {
                            Console.WriteLine(item.Title.Text);
                            string content = Regex.Replace(item.Summary.Text, "<[^>]+>", string.Empty);
                            Console.WriteLine(content);
                            Console.WriteLine(item.PublishDate.DateTime);
                            Console.WriteLine(item.Id);
                            Console.WriteLine(feed);
                            Console.WriteLine(i.Name);
                            Console.WriteLine();
                            count++;
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: {0}", ex.Message);
                    }
                    readNews.Add(count);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Прочитано!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Добавте хотя-бы один RSS источник!");
            }  
        }

        public void Save()
        {
            var rssSources = db.RssSources.ToList();
            saveNews.Clear();
            if (rssSources.Count != 0)
            {
                foreach (var i in rssSources)
                {
                    int count = 0;
                    string xml;
                    string feed = i.URL;

                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            webClient.Encoding = Encoding.UTF8;
                            xml = webClient.DownloadString(feed).TrimStart();
                        }
                        StringReader reader = new StringReader(xml);
                        XmlReader xmlReader = XmlReader.Create(reader);
                        SyndicationFeed channel = SyndicationFeed.Load(xmlReader);

                        foreach (var item in channel.Items)
                        {
                            var note = db.RssNews.FirstOrDefault(p => p.Title == item.Title.Text && p.DateTime == item.PublishDate.DateTime);

                            if (note == null)
                            {
                                string content = Regex.Replace(item.Summary.Text, "<[^>]+>", string.Empty);
                                RssNews rssNews = new RssNews()
                                {
                                    Title = item.Title.Text,
                                    DateTime = item.PublishDate.DateTime,
                                    Description = content,
                                    NewsUrl = item.Id,
                                    RssSourceId = i.Id
                                };
                                db.RssNews.Add(rssNews);
                                db.SaveChanges();
                                count++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: {0}", ex.Message);
                    }
                    saveNews.Add(count);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Сохранено!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Добавте хотя-бы один RSS источник!");
            }  
        }

        public void Statistic()
        {          
            int count = 0;
            var rssSources = db.RssSources.ToList();
            if(rssSources.Count != 0)
            {
                foreach (var i in rssSources)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Источник: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(i.Name);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Прочитано новостей: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    try
                    {
                        Console.WriteLine(readNews[count]);
                    }
                    catch
                    {
                        Console.WriteLine(0);
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Сохранено новостей: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    try
                    {
                        Console.WriteLine(saveNews[count]);
                    }
                    catch
                    {
                        Console.WriteLine(0);
                    }
                    Console.WriteLine();
                    count++;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Добавте хотя-бы один RSS источник!");
            }
        }
    }
}
