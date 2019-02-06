using System;
using System.Threading;


namespace ConsoleApp20
{
    class Program
    {

        static void Main(string[] args)
        {
            short item = 0;
            Menu menu = new Menu();
            Rss rss = new Rss();
            
            while (item != -1)
            {
                Console.Clear();

                item = menu.SwitchMenu();

                switch (item)
                {
                    case 0:
                        Console.WriteLine("\n\n");
                        rss.Read();                      
                        Console.ForegroundColor = ConsoleColor.Gray;                                           
                        Console.ReadKey();
                        break;

                    case 1:
                        Console.WriteLine("\n\n");
                        rss.Save();                       
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.WriteLine("\n\n");
                        rss.Statistic();                       
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("\n\n");
                        string name, url;
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Введите URL RSS-источника: ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            url = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Введите название RSS-источника: ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            name = Console.ReadLine();
                            if(url != "" && name != "")
                            {
                                rss.AddRssSource(url, name);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Добавлено!");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Введите оба значения!");
                            }
                        }
                        catch 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ошибка!");
                        }                                          
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine("\n\n");
                        rss.DelRssSource();

                        Console.ReadKey();
                        break;

                    case -1:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Программа завершена");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
    }
}

    