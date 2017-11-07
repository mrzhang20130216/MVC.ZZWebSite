using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MVC.ZZCommon
{
    public class WaterMark
    {
        #region  param
        private string strCopyright, strMarkPath, strPhotoPath, strSavePath;
        private int iMarkRightSpace, iMarkButtomSpace, iDiaphaneity;
        private int iFontRightSpace = 0, iFontButtomSpace = 0, iFontDiaphaneity = 80;
        private int iFontSize = 10;
        private bool bShowCopyright = true, bShowMarkImage = true;
        #endregion

        #region WaterMark
        public WaterMark()
        {
            this.strCopyright = "";
            this.strMarkPath = null;
            this.strPhotoPath = null;
            this.strSavePath = null;
            this.iDiaphaneity = 70;
            this.iMarkRightSpace = 0;
            this.iMarkButtomSpace = 0;
        }

        /// <summary>
        /// 主要用两样都加的
        /// </summary>
        public WaterMark(string copyright, string markPath, string photoPath, string savePath)
        {
            this.strCopyright = copyright;
            this.strMarkPath = markPath;
            this.strPhotoPath = photoPath;
            this.strSavePath = savePath;
            this.iDiaphaneity = 70;
            this.iMarkRightSpace = 0;
            this.iMarkButtomSpace = 0;
        }
        #endregion

        #region property

        /// <summary>
        /// 设置是否显示水印文字
        /// </summary>
        public bool ShowCopyright
        {
            set { this.bShowCopyright = value; }
        }

        /// <summary>
        /// 设置是否显示水印图片
        /// </summary>
        public bool ShowMarkImage
        {
            set { this.bShowMarkImage = value; }
        }
        /// <summary>
        /// 获取或设置要加入的文字
        /// </summary>
        public string Copyright
        {
            set { this.strCopyright = value; }
        }

        /// <summary>
        /// 获取或设置加水印后的图片路径
        /// </summary>
        public string SavePath
        {
            get { return this.strSavePath; }
            set { this.strSavePath = value; }
        }

        /// <summary>
        /// 获取或设置水印图片路径
        /// </summary>
        public string MarkPath
        {
            get { return this.strMarkPath; }
            set { this.strMarkPath = value; }
        }

        /// <summary>
        /// 获取或设置要加水印图片的路径
        /// </summary>
        public string PhotoPath
        {
            get { return this.strPhotoPath; }
            set { this.strPhotoPath = value; }
        }

        /// <summary>
        /// 设置水印图片的透明度
        /// </summary>
        public int Diaphaneity
        {
            set
            {
                if (value > 0 && value <= 100)
                    this.iDiaphaneity = value;
            }
        }

        /// <summary>
        /// 设置水印字体的透明度0-255
        /// </summary>
        public int FontDiaphaneity
        {
            set
            {
                if (value >= 0 && value <= 255)
                    this.iFontDiaphaneity = value;
            }
        }

        /// <summary>
        /// 设置水印图片在修改图片中距左边的高度
        /// </summary>
        public int MarkRightSpace
        {
            set { this.iMarkRightSpace = value; }
        }

        /// <summary>
        /// 设置水印图片在修改图片中距底部的高度
        /// </summary>
        public int MarkButtomSpace
        {
            set { this.iMarkButtomSpace = value; }
        }

        /// <summary>
        /// 设置水印字体在修改图片中距左边的距离
        /// </summary>
        public int FontRightSpace
        {
            set { iFontRightSpace = value; }
        }

        /// <summary>
        /// 设置水印字体在修改图片中距底部的高度
        /// </summary>
        public int FontButtomSpace
        {
            set { iFontButtomSpace = value; }
        }

        #endregion


        /// <summary>
        /// 生成水印图片
        /// </summary>
        /// <returns></returns>
        public void createMarkPhoto()
        {
            Bitmap bmWatermark = null;
            Image gPhoto = Image.FromFile(this.strPhotoPath);
            Image temp = null;
            int PhotoWidth = gPhoto.Width;
            int PhotoHeight = gPhoto.Height;
            Bitmap bitPhoto = new Bitmap(PhotoWidth, PhotoHeight, PixelFormat.Format24bppRgb);
            bitPhoto.SetResolution(gPhoto.HorizontalResolution, gPhoto.VerticalResolution);

            try
            {
                #region
                if (bShowCopyright)
                {
                    Graphics grPhoto = Graphics.FromImage(bitPhoto);
                    grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                    grPhoto.DrawImage(gPhoto, new Rectangle(0, 0, PhotoWidth, PhotoHeight), 0, 0, PhotoWidth, PhotoHeight, GraphicsUnit.Pixel);

                    Font crFont = new Font("楷体", iFontSize, FontStyle.Bold);
                    SizeF crSize = grPhoto.MeasureString(strCopyright, crFont);

                    //设置字体在图片中的位置
                    float yPosFromBottom = PhotoHeight - iFontButtomSpace - (crSize.Height);

                    //float xCenterOfImg = (phWidth/2);
                    float xCenterOfImg = PhotoWidth - iFontRightSpace - (crSize.Width / 2);
                    //设置字体居中

                    StringFormat StrFormat = new StringFormat();
                    StrFormat.Alignment = StringAlignment.Center;

                    //设置绘制文本的颜色和纹理 (Alpha=153)
                    SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(this.iFontDiaphaneity, 0, 0, 0));

                    //将版权信息绘制到图象上
                    grPhoto.DrawString(strCopyright, crFont, semiTransBrush2, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);
                    gPhoto = bitPhoto;
                    grPhoto.Dispose();
                }
                #endregion

                if (bShowMarkImage)
                {
                    //创建一个需要填充水银的Image对象
                    Image imgWatermark = new Bitmap(strMarkPath);
                    int iMarkWidth = imgWatermark.Width;
                    int iMarkmHeight = imgWatermark.Height;

                    Graphics grWatermark = null;
                    if (bShowCopyright)
                    {
                        //在原来修改过的bmPhoto上创建一个水银位图
                        bmWatermark = new Bitmap(bitPhoto);
                        bmWatermark.SetResolution(gPhoto.HorizontalResolution, gPhoto.VerticalResolution);
                    }
                    else
                    {
                        bmWatermark = new Bitmap(gPhoto);
                    }

                    //将位图bmWatermark加载到Graphics对象
                    grWatermark = Graphics.FromImage(bmWatermark);
                    ImageAttributes imageAttributes = new ImageAttributes();

                    ColorMap colorMap = new ColorMap();

                    colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                    colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                    ColorMap[] remapTable = { colorMap };

                    imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                    float[][] colorMatrixElements = {
                    new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                    new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                    new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                    new float[] {0.0f, 0.0f, 0.0f, (float)iDiaphaneity/100f, 0.0f},
                    new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}};
                    ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                    imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    iMarkRightSpace = PhotoWidth - iMarkWidth - iMarkRightSpace;
                    iMarkButtomSpace = PhotoHeight - iMarkmHeight - iMarkButtomSpace;
                    grWatermark.DrawImage(imgWatermark, new Rectangle(iMarkRightSpace, iMarkButtomSpace, iMarkWidth, iMarkmHeight), 0, 0, iMarkWidth, iMarkmHeight, GraphicsUnit.Pixel, imageAttributes);

                    temp = bmWatermark;
                    gPhoto.Dispose();
                    temp.Save(strSavePath, ImageFormat.Jpeg);
                    grWatermark.Dispose();
                    imgWatermark.Dispose();
                }


            }
            finally
            {
                gPhoto.Dispose();
                if (temp != null)
                    temp.Dispose();
                if (bitPhoto != null)
                    bitPhoto.Dispose();

                if (bmWatermark != null)
                    bmWatermark.Dispose();
            }

        }
    }

}
