//C:\Users\stepr\Desktop\Фото\
//C:\Users\stepr\Desktop\копия\
//C:\Users\stepr\Desktop\копия 2\
namespace PhotoSort
{
    internal class Program
    {
        private static void Main()
        {
            head();
            FileList s = new FileList();
            HashDuplicateSearcher hash = new HashDuplicateSearcher();
            string x = string.Empty;

            bool exit = false;
            while (!exit)
            {
                Option();
                switch (Console.ReadLine())
                {
                    case "1"://Указание папки для работы с ней
                        Console.WriteLine("\n||   Вставьте, пожалуйста, путь к папке.      ||");
                        Console.WriteLine("|| Пример пути: C:\\Users\\stepr\\Desktop\\Фото\\  ||");
                        Console.Write("---->");
                        x = Console.ReadLine()?.Trim();
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

                    case "2"://Вывод всех файлов в директории
                        if (ValidatePath(x))
                            s.SearchFile(x);
                        break;

                    case "3"://Поиск дубликатов и вывод их в терминале
                        if (ValidatePath(x))
                            hash.FindAndPrintImageDuplicates(s.SearchFile(x));
                        break;

                    case "4"://Удаление дубликатов
                        if (ValidatePath(x))
                        {
                            Console.Write("Вы уверены, что хотите удалить дубликаты? (y/n): ");
                            string confirm = Console.ReadLine()?.Trim().ToLower();
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
            Console.WriteLine("--->[1. Указать путь к папке или сменить его.");
            Console.WriteLine("--->[2. Показать содержимое папки.");
            Console.WriteLine("--->[3. Найти дубликаты изображений.");
            Console.WriteLine("--->[4. Удалить дубликаты изображений.");
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
                    "|||        |||    |||   |||   |||      ||||      |||   |||           ,` /^_ .-~^~-.__\\#",
                    "|||        |||    |||   |||   |||      ||||      |||   |||          /    ^\\/,,@@@,, ;|",
                    "|||        |||    |||   |||   |||      ||||      |||   |||         |      \\!!@@@@@!! ^,",
                    "|||        |||    |||     `OOO`        ||||        `OOO`          #.    .\\; '9@@@P'   ^,",
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