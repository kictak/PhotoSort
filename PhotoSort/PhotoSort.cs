using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public async Task SortPhotosAsync(string sourcePath, int sortOption)
        {
            var files = SearchFile(sourcePath);
            var tasks = new List<Task>();

            foreach (var file in files)
            {
                tasks.Add(Task.Run(() =>
                {
                    string targetFolder = GetTargetFolder(file, sortOption);
                    if (!string.IsNullOrEmpty(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder); // Создаем папку, если она еще не существует
                        string targetPath = Path.Combine(targetFolder, file.Name);
                        if (!File.Exists(targetPath))
                        {
                            file.MoveTo(targetPath); // Переносим файл
                        }
                    }
                }));
            }

            await Task.WhenAll(tasks);

            // Удаляем пустые папки после сортировки
            CleanEmptyFolders(sourcePath);
        }

        private string GetTargetFolder(FileInfo file, int sortOption)
        {
            string basePath = Path.GetDirectoryName(file.FullName);
            string relativePath = Path.GetRelativePath(basePath, file.DirectoryName ?? basePath);
            string targetFolder = null;

            switch (sortOption)
            {
                case 1: // По дням
                    targetFolder = Path.Combine(basePath, relativePath, file.LastWriteTime.ToString("yyyy-MM-dd"));
                    break;

                case 2: // По неделям
                    int weekNum = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(file.LastWriteTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    targetFolder = Path.Combine(basePath, relativePath, file.LastWriteTime.ToString("yyyy"), $"Week_{weekNum}");
                    break;

                case 3: // По месяцам
                    targetFolder = Path.Combine(basePath, relativePath, file.LastWriteTime.ToString("yyyy-MM"));
                    break;
            }

            // Проверяем, есть ли файлы в целевой папке, чтобы не создавать пустые
            if (Directory.Exists(targetFolder) && !Directory.EnumerateFiles(targetFolder).Any())
            {
                return null;
            }

            return targetFolder;
        }

        private void CleanEmptyFolders(string sourcePath)
        {
            var directories = new Queue<string>(Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories));
            while (directories.Count > 0)
            {
                string dir = directories.Dequeue();
                if (Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    foreach (string subDir in Directory.GetDirectories(dir))
                    {
                        directories.Enqueue(subDir);
                    }
                }
                else
                {
                    try
                    {
                        Directory.Delete(dir);
                        Console.WriteLine($"Удалена пустая папка: {dir}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка удаления папки {dir}: {ex.Message}");
                    }
                }
            }
        }
    }
}