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
            while (exit != null)
            {
                Option();
                switch (Console.ReadLine())
                {
                    case "1": //Вставка пути к файлу или замена пути
                        Console.WriteLine();
                        Console.WriteLine("||   Вставьте, пожалуйста, путь к папке.      ||");
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

                    case "2": //Показать все файлы
                        if (ValidatePath(x))
                            s.SearchFile(x);
                        break;

                    case "3"://Сжатие файлов
                        hash.ReduceImageSize(s.SearchFile(x));
                        break;

                    case "4"://...
                        if (ValidatePath(x))
                            s.SearchFile(x);
                        break;

                    case "5"://...
                        if (ValidatePath(x))
                            s.SearchFile(x);
                        break;

                    case "Exit":
                        exit = true;
                        s.SearchFile(x);
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
            Console.WriteLine("--->[1.Указать путь к папке или сменить его.");
            Console.WriteLine("--->[2.Показать содержимое папки.");
            Console.WriteLine("--->[3.Удалbnm дубликаты.");
            Console.WriteLine("--->[4.");
            Console.WriteLine("--->[5.");
            Console.WriteLine("--->[6.");
            Console.WriteLine("--->Exit - выход из программы");
            Console.Write("---->");
        }

        private static void head()
        {
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