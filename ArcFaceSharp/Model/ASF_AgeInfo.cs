using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ArcFaceSharp.Model
{
    /// <summary>
    /// 年龄结果结构体
    /// </summary>
    public struct ASF_AgeInfo
    {
        /// <summary>
        /// 年龄检测结果集合
        /// </summary>
        public IntPtr ageArray;
        /// <summary>
        /// 人脸个数
        /// </summary>
        public int num;


        /// <summary>
        /// 年龄信息转化为List
        /// </summary>
        /// <param name="self">年龄句柄handle</param>
        /// <param name="length">句柄长度</param>
        /// <returns></returns>
        public List<int> PtrToAgeArray(IntPtr self, int length)
        {
            var size = Marshal.SizeOf(typeof(int));
            List<int> ageArray = new List<int>();
            for (var i = 0; i < length; i++)
            {
                int age = 0;
                var iPtr = new IntPtr(self.ToInt32() + i * size);
                age = (int)Marshal.PtrToStructure(iPtr, typeof(int));
                ageArray.Add(age);
            }
            //for(var i=0;i<length;i++)
            //{
            //    int age = 0;
            //    age = (int)Marshal.PtrToStructure(self, typeof(int));
            //    ageArray.Add(age);
            //}
            return ageArray;
        }
    }
}
