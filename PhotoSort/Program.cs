//C:\Users\stepr\Desktop\Фото\
//C:\Users\stepr\Desktop\копия\
//C:\Users\stepr\Desktop\копия 2\
//C:\Users\stepr\Desktop\копия 3\
//C:\Users\stepr\Desktop\марка\istockphoto-1286277665-612x612.jpg
//C:\Users\stepr\Desktop\марка\
using System;
using System.IO;

namespace PhotoSort
{
    internal class Program
    {
        public static string folderPath = "";
        public static string foldermark = "";

        private static async Task Main()
        {
            head();
            FileList s = new FileList();
            Watermark w = new Watermark();
            HashDuplicateSearcher hash = new HashDuplicateSearcher();
            string x = string.Empty;

            bool exit = false;
            while (!exit)
            {
                Option();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": // Указание папки для работы с ней
                        Console.WriteLine("\n||   Вставьте, пожалуйста, путь к папке.      ||");
                        Console.WriteLine("|| Пример пути: C:\\Users\\stepr\\Desktop\\Фото\\  ||");
                        Console.Write("---->");
                        folderPath = x = Console.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(x) || !Directory.Exists(x))
                        {
                            Console.WriteLine("Введён некорректный путь или папка не существует. Попробуйте снова.");
                            x = string.Empty;
                        }
                        else
                        {
                            Console.WriteLine($"Путь установлен: {x}");
                        }
                        break;

                    case "2": // Показать содержимое папки
                        if (ValidatePath(x))
                            s.SearchFile(x);
                        break;

                    case "3": // Найти дубликаты изображений
                        if (ValidatePath(x))
                            hash.FindAndPrintImageDuplicates(s.SearchFile(x));
                        break;

                    case "4": // Удалить дубликаты
                        if (ValidatePath(x))
                        {
                            Console.Write("Вы уверены, что хотите удалить дубликаты? (y/n): ");
                            string confirm = Console.ReadLine()?.Trim().ToLower();
                            foldermark = confirm;
                            if (confirm == "y" || confirm == "д")
                            {
                                hash.FindAndPrintImageDuplicates(s.SearchFile(x));
                            }
                            else
                            {
                                Console.WriteLine("Удаление отменено.");
                            }
                        }
                        break;

                    case "5": // Вставить водяной знак
                        Console.WriteLine("Введите путь к фото с водяным знаком");
                        Console.Write("--->");
                        string pathWatermark = Console.ReadLine()?.Trim().ToLower();
                        if (ValidatePath(x))
                            w.WatermarkCreate(s.SearchFile(x), pathWatermark);
                        break;

                    case "6": // Сортировка фото
                        if (ValidatePath(x))
                        {
                            Console.WriteLine("\nВыберите способ сортировки:");
                            Console.WriteLine("1. По дням");
                            Console.WriteLine("2. По неделям");
                            Console.WriteLine("3. По месяцам");
                            Console.Write("---->");
                            if (int.TryParse(Console.ReadLine(), out int sortOption) && sortOption >= 1 && sortOption <= 3)
                            {
                                await s.SortPhotosAsync(x, sortOption);
                                Console.WriteLine("Сортировка завершена.");
                            }
                            else
                            {
                                Console.WriteLine("Неверный выбор. Введите 1, 2 или 3.");
                            }
                        }
                        break;

                    case "exit":
                    case "Exit":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод, попробуйте снова.");
                        break;
                }
            }
        }

        private static bool ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Путь не задан. Сначала выберите опцию 1 и введите путь.");
                return false;
            }
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Папка по заданному пути не найдена. Проверьте путь и попробуйте снова.");
                return false;
            }
            return true;
        }

        private static void Option()
        {
            Console.WriteLine();
            Console.WriteLine("             [Опции PhotoSort]             ");
            if (folderPath != string.Empty)
            {
                Console.WriteLine("                                           ");
                Console.WriteLine($"Выбранная папка => {folderPath} ");
                Console.WriteLine("                                           ");
            }
            if (foldermark != string.Empty)
            {
                Console.WriteLine("                                           ");
                Console.WriteLine($"Выбранная вотемарка => {foldermark} ");
                Console.WriteLine("                                           ");
            }
            Console.WriteLine("--->[1. Указать путь к папке или сменить его.");
            Console.WriteLine("--->[2. Показать содержимое папки.");
            Console.WriteLine("--->[3. Найти дубликаты изображений.");
            Console.WriteLine("--->[4. Удалить дубликаты изображений.");
            Console.WriteLine("--->[5. Вставить водяной знак на каждое изображение.");
            Console.WriteLine("--->[6. Сортировать фото по дате.");
            Console.WriteLine("--->[Exit - выход из программы]");
            Console.Write("---->");
        }

        private static void head()
        {
            Console.WriteLine("====== PhotoSort v1.0 ======");
            string[] photo = new string[]
            {
                "||||||||   |||    |||     .OOO.     |!|!||!|!|     .OOO.                o###########o",
                "|||   ||   |||    |||   |||   |||   ||!||||!||   |||   |||             o#############o",
                "|||   ||   |||    |||   |||   |||      ||||      |||   |||            ################# ",
                "||||||||   ||||||||||   |||   |||      ||||      |||   |||            ######  \\########o",
                "|||        ||||||||||   |||   |||      ||||      |||   |||           '#;^ _^,/---\\#####!",
                "|||        |||    |||   |||   |||      ||||      |||   |||           , /^_ .-~^~-.__\\#",
                "|||        |||    |||   |||   |||      ||||      |||   |||          /    ^\\/,,@@@,, ;|",
                "|||        |||    |||   |||   |||      ||||      |||   |||         |      \\!!@@@@@!! ^,",
                "|||        |||    |||     OOO        ||||        OOO          #.    .\\; '9@@@P'   ^,",
                "                                                                  ###./^ ----,_^^      /@-._",
                "                                                                                ^--._,o@@@@@@",
                "                                                                                   ^;@@@@@@@@@",
                "                                                                                     ^-;@@@@ ",
                "//////////   ||||||||||     .|||||.     ||||||||      |||||||||||    |||    |||",
                "|||          |||          |||     |||   |||    ||     |||||||||||    |||    |||",
                "|||          |||          |||     |||   |||    ||     ||||           |||    ||| ",
                "|||          |||          |||     |||   |||    ||     ||||           |||||||||| ",
                "||||||||||   ||||||||||   |||||||||||   ||||||||      ||||           |||||||||| ",
                "       |||   |||          |||     |||   |||  \\\\\\      ||||           |||    ||| ",
                "       |||   |||          |||     |||   |||   \\\\\\     ||||           |||    ||| ",
                "       |||   |||          |||     |||   |||    \\\\\\    |||||||||||    |||    |||",
                "/////////    ||||||||||   |.|     |.|   |||     \\\\\\   |||||||||||    |||    |||",
            };
            foreach (var line in photo)
            {
                Console.WriteLine(line);
            }
        }
    }
}