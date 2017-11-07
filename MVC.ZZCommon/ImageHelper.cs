using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.IO;
namespace MVC.ZZCommon
{
    public class ImageHelper
    {
        private static ImageCodecInfo ImageCodecInfo
        {
            get
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo codec = null;
                foreach (ImageCodecInfo c in codecs)
                {
                    if (c.FormatDescription == "JPEG")
                        codec = c;
                }
                return codec;
            }
        }
        private static EncoderParameters EncoderParams
        {
            get
            {
                EncoderParameters param = new EncoderParameters();
                param.Param[0] = new EncoderParameter(Encoder.Quality, (long)88);
                return param;
            }
        }

        public static bool Delete(string path)
        {
            bool result = false;
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    result = true;
                }
                catch
                {

                }
            }
            return result;
        }
        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="source">源图</param>
        /// <param name="dest">目标地址</param>
        /// <param name="Width">原图宽度</param>
        /// <param name="Height">原图高度</param>
        /// <param name="X">截取起点X</param>
        /// <param name="Y">截取起点Y</param>
        /// <param name="CutWidth">截取宽度</param>
        /// <param name="CutHeight">截取高度</param>
        /// <param name="SaveWidth">保存宽度</param>
        /// <param name="SaveHeight">保存高度</param>
        /// <returns></returns>
        public static void CutOut(string source, string dest, int Width, int Height, int X, int Y, int CutWidth, int CutHeight, int SaveWidth, int SaveHeight)
        {
            System.Drawing.Image image = null;
            Bitmap bitmap = null;
            Graphics graphice = null;
            try
            {
                image = System.Drawing.Image.FromFile(source);
                float WScale = image.Width * 1.0f / Width;
                float HScale = image.Height * 1.0f / Height;
                bitmap = new Bitmap(SaveWidth, SaveHeight);
                graphice = Graphics.FromImage(bitmap);
                graphice.DrawImage(image, new Rectangle(0, 0, SaveWidth, SaveHeight), X * WScale, Y * HScale, CutWidth * WScale, CutHeight * HScale, GraphicsUnit.Pixel);
                //确定存在目录
                FileInfo info = new FileInfo(dest);
                if (!info.Directory.Exists)
                    info.Directory.Create();
                image.Dispose();
                bitmap.Save(dest, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            finally
            {
                if (image != null)
                    image.Dispose();
                if (bitmap != null)
                    bitmap.Dispose();
                if (graphice != null)
                    graphice.Dispose();
            }
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="source">图片地址</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void Compress(string source, int maxWidth, int maxHeight)
        {
            Compress(source, source, maxWidth, maxHeight, true);
        }
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="source">图片地址</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        /// <param name="scale">维持比例</param>
        public static void Compress(string source, int maxWidth, int maxHeight, bool scale)
        {
            Compress(source, source, maxWidth, maxHeight, scale);
        }
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="source">图片地址</param>
        /// <param name="dest">另存地址</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void Compress(string source, string dest, int maxWidth, int maxHeight)
        {
            Compress(source, dest, maxWidth, maxHeight, true);
        }
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="source">图片地址</param>
        /// <param name="dest">另存地址</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        /// <param name="scale">维持比例</param>
        public static void Compress(string source, string dest, int maxWidth, int maxHeight, bool scale)
        {
            if (string.IsNullOrEmpty(source)) return;
            if (!File.Exists(source)) return;
            if ((maxWidth + maxHeight) == 0) return;//忽略
            System.Drawing.Image image = null;
            System.Drawing.Bitmap bitmap = null;
            try
            {
                image = System.Drawing.Image.FromFile(source);
                int width = image.Width;
                int height = image.Height;
                if (scale)
                {//按比例
                    if (maxWidth > 0)
                    {
                        if (width > maxWidth)
                        {
                            height = maxWidth * height / width;
                            width = maxWidth;
                        }
                    }
                    if (maxHeight > 0)
                    {
                        if (height > maxHeight)
                        {
                            width = maxHeight * width / height;
                            height = maxHeight;
                        }
                    }
                }
                else
                {//固定高宽
                    if (maxWidth > 0)
                        width = maxWidth;
                    if (maxHeight > 0)
                        height = maxHeight;
                }
                bitmap = new System.Drawing.Bitmap(image, width, height);
                image.Dispose();
                bitmap.Save(dest, ImageCodecInfo, EncoderParams);
                bitmap.Dispose();
            }
            finally
            {
                if (image != null)
                    image.Dispose();
                if (bitmap != null)
                    bitmap.Dispose();
            }
        }

        public static void Thumbnails(string source, string toPath, int width, int height, string backColor, string borderColor)
        {
            Thumbnails(source, toPath, width, height, backColor, borderColor, "center");
        }
        public static void Thumbnails(string source, string toPath, int width, int height, string backColor, string borderColor, string flag)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(source);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if (ow * 1.0 / oh > towidth * 1.0 / toheight)
            {//压高裁宽
                //100     
                towidth = (int)(1.0 * ow * toheight / oh);
            }
            else
            {//压宽裁高
                toheight = (int)(1.0 * oh * towidth / ow);
            }

            //新建一个bmp图片 
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(width, height);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以指定颜色填充 
            g.Clear(ColorTranslator.FromHtml(backColor));

            //在指定位置并且按指定大小绘制原图片的指定部分 
            int top = 0;
            int left = 0;
            if (flag.ToLower() == "top")
            {
                left = (width - towidth) / 2;
            }
            else if (flag.ToLower() == "center")
            {
                top = (height - toheight) / 2;
                left = (width - towidth) / 2;
            }
            else if (flag.ToLower() == "bottom")
            {
                top = height - toheight;
                left = (width - towidth) / 2;
            }
            g.DrawImage(originalImage, new System.Drawing.Rectangle(left, top, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);
            originalImage.Dispose();
            Pen pen = new Pen(ColorTranslator.FromHtml(borderColor));
            g.DrawRectangle(pen, 0, 0, width - 1, height - 1);
            try
            {
                //以jpg格式保存缩略图 
                bitmap.Save(toPath, ImageCodecInfo, EncoderParams);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                bitmap.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// 复制图
        /// </summary>
        /// <param name="source">源图</param>
        /// <param name="dest">目标文件目录</param>
        public static bool Copy(string source, string dest)
        {
            try
            {
                if (System.IO.Path.HasExtension(dest))//是否包含扩展名 文件 or 文件夹
                { //文件
                    FileInfo fileinfo = new FileInfo(dest);
                    if (!fileinfo.Directory.Exists)
                        fileinfo.Directory.Create();
                }
                else
                {//文件夹
                    FileInfo fileinfo = new FileInfo(source);
                    if (!Directory.Exists(dest))
                        Directory.CreateDirectory(dest);
                    dest = dest + "/" + fileinfo.Name;
                }

                System.Drawing.Image image = null;
                try
                {
                    image = System.Drawing.Image.FromFile(source);
                    image.Save(dest, ImageCodecInfo, EncoderParams);
                    return true;
                }
                finally
                {
                    if (image != null)
                        image.Dispose();
                }
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 移动图片
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool Move(string source, string dest)
        {
            if (Copy(source, dest))
            {
                File.Delete(source);
                return true;
            }
            else
                return false;
        }
        public static void WaterMark(string imgPath)
        {
            //添加水印
            WaterMark mark = new WaterMark();
            mark.ShowMarkImage = true;
            mark.ShowCopyright = false;
            mark.MarkPath = HttpContext.Current.Server.MapPath("~") + "/Images/1-1.png";
            mark.PhotoPath = imgPath;
            mark.SavePath = imgPath;
            mark.createMarkPhoto();//保存处理图片
        }
        public static void WaterMark(string imgPath, string markPath)
        {
            //添加水印
            WaterMark mark = new WaterMark();
            mark.ShowMarkImage = true;
            mark.ShowCopyright = false;
            mark.MarkPath = markPath;
            mark.PhotoPath = imgPath;
            mark.SavePath = imgPath;
            mark.MarkButtomSpace = 10;
            mark.MarkRightSpace = 10;
            mark.createMarkPhoto();//保存处理图片
        }
        public static void WaterMarkTopLeft(string imgPath)
        {
            WaterMark mark = new WaterMark();
            mark.ShowMarkImage = true;
            mark.ShowCopyright = false;
            mark.MarkPath = HttpContext.Current.Server.MapPath("~") + "/Images/1-1.png";
            mark.PhotoPath = imgPath;
            mark.SavePath = imgPath;
            System.Drawing.Image image = System.Drawing.Image.FromFile(imgPath);
            System.Drawing.Image img = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~") + "/Images/1-1.png");
            mark.MarkButtomSpace = image.Height - img.Height;
            mark.MarkRightSpace = image.Width - img.Width;
            image.Dispose();
            img.Dispose();
            mark.createMarkPhoto();//保存处理图片
        }
    }
}
