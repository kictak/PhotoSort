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
            Console.WriteLine($"Поиск файлов в: {startPath}");

            try
            {
                string[] files = Directory.GetFiles(startPath);
                foreach (string file in files)
                {
                    if (regex.IsMatch(file))
                    {
                        FileInfo fileinfo = new FileInfo(file);
                        allFiles.Add(fileinfo);
                        Console.WriteLine($"Файл: {fileinfo.Name} | Дата: {fileinfo.LastWriteTime} | Размер: {(double)fileinfo.Length / 1_000_000:F2} МБ");
                    }
                }

                string[] folders = Directory.GetDirectories(startPath);
                foreach (string folder in folders)
                {
                    allFiles.AddRange(GetRecursFiles(folder));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }

            return allFiles;
        }

        private static void FileCount(List<FileInfo> files)
        {
            Console.WriteLine($"\n>>> Всего найдено файлов: {files.Count}\n");
        }
    }
}