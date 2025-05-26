using Aspose.Imaging;
using System.Drawing;
using Color = System.Drawing.Color;
using Image = Aspose.Imaging.Image;


///<summary>
///1. Уменьшить размер. В данном случае мы уменьшаем его до 8х8.
///2. Убрать цвет.
///3. Найти среднее. Вычислите среднее значение для всех 64 цветов.
///4. Цепочка битов. 1 или 0 в зависимости от того, больше или меньше среднего цвета.
///5. Постройте хэш. 
///</summary>



namespace PhotoSort
{
    public class HashDuplicateSearcher
    {
        public long ColorAverage(List<FileInfo> files)
        {
            long sumA = 0, sumR = 0, sumG = 0, sumB = 0;
            Dictionary<Color, int> colorCounts = new Dictionary<Color, int>();
            foreach (FileInfo file in files)
            {
                using (Bitmap bmp = new Bitmap(file.FullName))
                {
                    for (int i = 0; i != 64; i++)
                    {
                        for (int d = 0; d != 5;)
                        {
                            Color pixelColor = bmp.GetPixel(i, d);
                            if (i % 8 == 0)
                                d++;
                            sumA += pixelColor.A;
                            sumR += pixelColor.R;
                            sumG += pixelColor.G;
                            sumB += pixelColor.B;
                        }
                    }
                }
            }

            return  (sumA + sumR + sumG + sumB) / 64;
        }

        public void RizeImage(List<FileInfo> files)
        {
            foreach (FileInfo file in files)
            {
                using (var image = (RasterImage)Image.Load(file.FullName))
                {
                    image.AdjustContrast(10);// Увеличение контрастности изображения
                    image.Resize(8, 8);// Изменение размера до 8x8 пикселей
                    image.Grayscale();// Преобразование в оттенки серого
                }
            }
            
        }

        public int ReturnBit(long numAverage, List<FileInfo> files)
        {
            long sumA = 0, sumR = 0, sumG = 0, sumB = 0;
            long sum
            foreach (FileInfo file in files)
            {
                using (Bitmap bmp = new Bitmap(file.FullName))
                {
                    for (int i = 0; i != 64; i++)
                    {
                        for (int d = 0; d != 5;)
                        {
                            Color pixelColor = bmp.GetPixel(i, d);
                            if (i % 8 == 0)
                                d++;
                            sumA += pixelColor.A;
                            sumR += pixelColor.R;
                            sumG += pixelColor.G;
                            sumB += pixelColor.B;

                        }
                    }
                }
                        
                if (sumA + sumR + sumG + sumB) / 64 > numAverage)
            }
        }
    }
}

//Aspose жадная компания и накладывает вотемарки сама!!!