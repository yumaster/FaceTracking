using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ArcFaceSharp.Model
{
    /// <summary>
    /// 性别结构体
    /// </summary>
    public struct ASF_GenderInfo
    {
        /// <summary>
        /// 0男，1女，-1未知
        /// </summary>
        public IntPtr genderArray;
        /// <summary>
        /// 检测到的人脸的个数
        /// </summary>
        public int num;

        /// <summary>
        /// 性别信息转化为List
        /// </summary>
        /// <param name="self">性别句柄handle</param>
        /// <param name="length">句柄长度</param>
        /// <returns></returns>
        public List<int> PtrToGenderArray(IntPtr self, int length)
        {
            var size = Marshal.SizeOf(typeof(int));
            List<int> genderArray = new List<int>();
            for (var i = 0; i < length; i++)
            {
                int gender = 0;
                var iPtr = new IntPtr(self.ToInt32() + i * size);
                gender = (int)Marshal.PtrToStructure(iPtr, typeof(int));
                genderArray.Add(gender);
            }
            return genderArray;
        }
    }
}
