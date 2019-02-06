using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp20
{
    class Menu
    {
        String[] MenuItem = new String[]{
      "Прочитать новости из всех RSS-источников",
      "Сохранить в БД свежие новости",
      "Вывести информацию по каждому источнику",
      "Добавить новый источник в БД",
      "Удалить источник из БД"

    };
        short MenuNumber;
        short NItem;
        ConsoleKeyInfo NN;

        String Header = "Используйте стрелки для перемещения по пунктам меню:";
        String Footer = "Нажмите 'ESC' для выхода из программы";

        public Menu()
        {
            MenuNumber = Convert.ToInt16(MenuItem.Length);
            NItem = 0;
        }

        private void PrintMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Header + "\n");

            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < MenuNumber; i++)
            {
                if (i == NItem) Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0}. {1}", i + 1, MenuItem[i]);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + Footer + "\n");
        }

        public short SwitchMenu()
        {
            PrintMenu();
            NN = Console.ReadKey();

            switch (NN.Key)
            {
                case ConsoleKey.Escape:
                    return -1;

                case ConsoleKey.Enter:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return NItem;


                case ConsoleKey.UpArrow:
                    if (NItem > 0) NItem--;
                    else NItem = Convert.ToInt16(MenuNumber - 1);
                    return SwitchMenu();


                case ConsoleKey.DownArrow:
                    if (NItem < MenuNumber - 1) NItem++;
                    else NItem = 0;
                    return SwitchMenu();


                default: return SwitchMenu();
            }
        }

    }
}
