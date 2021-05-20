using ArcFaceSharp.Model;
using ArcFaceSharp.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPDemo
{
    public class ImageHelper
    {
        /// <summary>
        /// 从Image图片中提取特征
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static IntPtr GetFeatureFromImage(IntPtr pEngine, Image img)
        {
            Image image = img;
            if (image.Width % 4 != 0)
            {
                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }
            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pEngine, image);
            if (multiFaceInfo.faceNum > 0)
            {
                //裁剪照片到识别人脸的框的大小
                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
            }
            //提取人脸特征
            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
            IntPtr feature = FaceUtil.ExtractFeature(pEngine, image, out singleFaceInfo);
            if (!(singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0))
                return feature;//成功提取到的图像特征值
            return IntPtr.Zero;
        }

        /// <summary>
        /// 根据图片路径返回图片的字节流byte[]
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <returns>返回的字节流</returns>
        public static byte[] getImageByte(string imagePath)
        {
            FileStream files = new FileStream(imagePath, FileMode.Open);
            byte[] imgByte = new byte[files.Length];
            files.Read(imgByte, 0, imgByte.Length);
            files.Close();
            return imgByte;
        }
    }
}
