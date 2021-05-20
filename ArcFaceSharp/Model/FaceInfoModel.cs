using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFaceSharp.Model
{
    public class FaceInfoModel
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int gender { get; set; }

        public ASF_Face3DAngle face3dAngle { get; set; }
        /// <summary>
        /// 人脸框
        /// </summary>
        public MRECT faceRect { get; set; }

        /// <summary>
        /// 人脸角度
        /// </summary>
        public int faceOrient { get; set; }

        /// <summary>
        /// 单人脸特征
        /// </summary>
        public IntPtr feature { get; set; }
    }
}
