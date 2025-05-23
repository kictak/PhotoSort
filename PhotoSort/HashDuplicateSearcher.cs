using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;

namespace PhotoSort
{
    public class HashDuplicateSearcher
    {


        public List<FileInfo> PhotoСompress(List<FileInfo> files)
        {
            List<FileInfo> editfile = new List<FileInfo>();
            foreach (FileInfo file in files)
            {
                string tempFile = Path.GetTempFileName();

                using (Image originalImage = Image.FromFile(file.FullName))
                using (Bitmap smallImage = new Bitmap(64, 64))
                {
                    using (Graphics g = Graphics.FromImage(smallImage))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(originalImage, 0, 0, 64, 64);
                    }
                    smallImage.Save(tempFile, ImageFormat.Jpeg);
                }
                foreach (FileInfo f in editfile)
                    File.Move(tempFile, f.FullName);
            }
            return editfile;
        }


    }
}