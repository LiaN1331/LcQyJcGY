// 代码生成时间: 2025-09-23 20:40:15
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ImageProcessing
{
    /// <summary>
    /// 图片尺寸批量调整器
    /// </summary>
    public class ImageResizer
    {
        private readonly string _sourceFolder;
        private readonly string _destinationFolder;
        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// 初始化图片尺寸批量调整器
        /// </summary>
        /// <param name="sourceFolder">源文件夹路径</param>
        /// <param name="destinationFolder">目标文件夹路径</param>
        /// <param name="width">目标图片宽度</param>
        /// <param name="height">目标图片高度</param>
        public ImageResizer(string sourceFolder, string destinationFolder, int width, int height)
        {
            _sourceFolder = sourceFolder;
            _destinationFolder = destinationFolder;
            _width = width;
            _height = height;
        }

        /// <summary>
        /// 批量调整图片尺寸
        /// </summary>
        public void ResizeImages()
        {
            if (!Directory.Exists(_sourceFolder))
            {
                Console.WriteLine("源文件夹不存在");
                return;
            }

            if (!Directory.Exists(_destinationFolder))
            {
                Directory.CreateDirectory(_destinationFolder);
            }

            var files = Directory.GetFiles(_sourceFolder, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                try
                {
                    ResizeImage(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"处理图片 {file} 出错：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 调整单个图片尺寸
        /// </summary>
        /// <param name="filePath">图片文件路径</param>
        private void ResizeImage(string filePath)
        {
            using (var image = Image.FromFile(filePath))
            {
                var ratio = Math.Min(_width / (double)image.Width, _height / (double)image.Height);
                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                using (var resizedImage = new Bitmap(newWidth, newHeight))
                {
                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;

                        graphics.DrawImage(image, 0, 0, newWidth, newHeight);

                        var fileName = Path.GetFileName(filePath);
                        var destinationPath = Path.Combine(_destinationFolder, fileName);
                        resizedImage.Save(destinationPath, ImageFormat.Png);
                    }
                }
            }
        }
    }
}
