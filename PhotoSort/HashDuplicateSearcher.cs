using Aspose.Imaging;
using System.Collections.Concurrent;
using System.Drawing;
using Image = Aspose.Imaging.Image;

namespace PhotoSort
{
    public class HashDuplicateSearcher
    {
        public void FindAndPrintImageDuplicates(List<FileInfo> imageFiles)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                Console.WriteLine("Список файлов пуст.");
                return;
            }

            Console.WriteLine("Поиск дубликатов изображений...");
            Console.WriteLine($"Проанализировано файлов: {imageFiles.Count}");

            var duplicateGroups = HashEditParallel(imageFiles);

            int duplicateGroupsCount = 0;
            int totalDuplicates = 0;

            foreach (var group in duplicateGroups.Where(g => g.Value.Count > 1).OrderByDescending(g => g.Value.Count))
            {
                duplicateGroupsCount++;
                totalDuplicates += group.Value.Count - 1;

                Console.WriteLine($"\nГруппа дубликатов #{duplicateGroupsCount} (хэш {group.Key:X16})");
                Console.WriteLine($"Найдено копий: {group.Value.Count - 1}");

                // Первый файл — считаем оригиналом
                var original = group.Value.First();
                Console.WriteLine($"Оригинал сохранён: {original}");

                foreach (var file in group.Value.Skip(1))
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine($"Удалено: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при удалении {file}: {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"\nИтоговый отчет:");
            Console.WriteLine($"- Всего файлов проанализировано: {imageFiles.Count}");
            Console.WriteLine($"- Найдено групп дубликатов: {duplicateGroupsCount}");
            Console.WriteLine($"- Удалено файлов-дубликатов: {totalDuplicates}");
        }

        public Dictionary<ulong, List<string>> HashEditParallel(List<FileInfo> files)
        {
            var hashes = new ConcurrentDictionary<ulong, ConcurrentBag<string>>();

            Parallel.ForEach(files, file =>
            {
                try
                {
                    using (var image = (RasterImage)Image.Load(file.FullName))
                    {
                        image.AdjustContrast(10);
                        image.Resize(8, 8);

                        var grayscale = GetGrayscaleValues(image);
                        var avg = grayscale.Average();

                        var bits = new bool[64];
                        for (int i = 0; i < 64; i++)
                        {
                            bits[i] = grayscale[i] > avg;
                        }

                        ulong hash = BitsToHash(bits);
                        hashes.AddOrUpdate(hash,
                            a => new ConcurrentBag<string> { file.FullName },
                            (a, list) =>
                            {
                                list.Add(file.FullName);
                                return list;
                            });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"!!! Ошибка обработки файла {file.FullName}: {ex.Message}");
                }
            });

            return hashes.ToDictionary(pair => pair.Key, pair => pair.Value.ToList());
        }

        private double[] GetGrayscaleValues(RasterImage image)
        {
            var grayscale = new double[64];

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var color = image.GetPixel(x, y);
                    grayscale[y * 8 + x] = 0.299 * color.R + 0.587 * color.G + 0.114 * color.B;
                }
            }

            return grayscale;
        }

        private ulong BitsToHash(bool[] bits)
        {
            ulong hash = 0;
            for (int i = 0; i < 64; i++)
            {
                if (bits[i])
                {
                    hash |= (1UL << (63 - i));
                }
            }
            return hash;
        }
    }
}

//Aspose жадная компания и накладывает вотемарки сама!!!