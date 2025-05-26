using System.Text.RegularExpressions;

namespace PhotoSort
{
    public class FileList
    {
        public List<FileInfo> SearchFile(string start_path)
        {
            List<FileInfo> allFiles = GetRecursFiles(start_path);
            FileCount(allFiles);
            return allFiles;
        }

        private List<FileInfo> GetRecursFiles(string startPath)
        {
            List<FileInfo> allFiles = new List<FileInfo>();
            Regex regex = new Regex(@"\w+\.jpg$", RegexOptions.IgnoreCase);
            Console.WriteLine("=====");
            Console.WriteLine($"Папка:{startPath}");
            Console.WriteLine('>');
            try
            {
                // Получаем все файлы в текущей папке
                string[] files = Directory.GetFiles(startPath);
                foreach (string file in files)
                {
                    if (regex.IsMatch(file))
                    {
                        FileInfo fileinfo = new FileInfo(file);
                        allFiles.Add(fileinfo);
                        Console.WriteLine($"Имя файла: {fileinfo.Name} || Дата создания: {fileinfo.LastWriteTime} || {(double)fileinfo.Length / 1000000} мб");
                    }
                }

                // Рекурсивно обходим вложенные папки
                string[] folders = Directory.GetDirectories(startPath);
                foreach (string folder in folders)
                {
                    Console.WriteLine("=====");
                    Console.WriteLine($"Папка: {folder}");
                    Console.WriteLine('>');
                    allFiles.AddRange(GetRecursFiles(folder));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Console.WriteLine();
            return allFiles;
        }

        private static void FileCount(List<FileInfo> files)
        {
            Console.WriteLine();
            Console.WriteLine("======================");
            Console.WriteLine($"^ Всего файлов {files.Count}^");
            Console.WriteLine("======================");
            Console.WriteLine();
        }
    }
}