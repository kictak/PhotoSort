using System.Drawing;
using System.Drawing.Imaging;

namespace PhotoSort
{
    public class Watermark
    {
        public void WatermarkCreate(List<FileInfo> files, string watermark)
        {
            Parallel.ForEach(files, file =>
            {
                try
                {
                    using (var watermarkImage = Image.FromFile(watermark))
                    {
                        // Загружаем оригинал безопасно в память
                        using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var temp = Image.FromStream(fs))
                        using (var originalImage = new Bitmap(temp))
                        using (Bitmap bmp = new Bitmap(originalImage.Width, originalImage.Height))
                        {
                            using (Graphics g = Graphics.FromImage(bmp))
                            {
                                g.DrawImage(originalImage, 0, 0);

                                // Прозрачность
                                ColorMatrix matrix = new ColorMatrix();
                                matrix.Matrix33 = 0.5f;

                                ImageAttributes attributes = new ImageAttributes();
                                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                Rectangle destRect = new Rectangle(
                                    (bmp.Width - watermarkImage.Width) / 2,
                                    (bmp.Height - watermarkImage.Height) / 2,
                                    watermarkImage.Width,
                                    watermarkImage.Height);

                                g.DrawImage(watermarkImage, destRect, 0, 0, watermarkImage.Width, watermarkImage.Height,
                                    GraphicsUnit.Pixel, attributes);
                            }

                            bmp.Save(file.FullName, originalImage.RawFormat); // сохраняем корректно
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"!!! Ошибка обработки файла {file.FullName}: {ex.Message}");
                }
            });
        }
    }
}