﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace ArcFaceSharp.Util
{
    public class MemoryUtil
    {
        /// <summary>
        /// 申请内存
        /// </summary>
        /// <param name="len">内存长度(单位:字节)</param>
        /// <returns>内存首地址</returns>
        public static IntPtr Malloc(int len)
        {
            return Marshal.AllocHGlobal(len);
        }

        /// <summary>
        /// 释放ptr托管的内存
        /// </summary>
        /// <param name="ptr">托管指针</param>
        public static void Free(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// 将字节数组的内容拷贝到托管内存中
        /// </summary>
        /// <param name="source">元数据</param>
        /// <param name="startIndex">元数据拷贝起始位置</param>
        /// <param name="destination">托管内存</param>
        /// <param name="length">拷贝长度</param>
        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 将托管内存的内容拷贝到字节数组中
        /// </summary>
        /// <param name="source">托管内存</param>
        /// <param name="destination">目标字节数组</param>
        /// <param name="startIndex">拷贝起始位置</param>
        /// <param name="length">拷贝长度</param>
        public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 将ptr托管的内存转化为结构体对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="ptr">托管指针</param>
        /// <returns>转化后的对象</returns>
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            //return Marshal.PtrToStructure<T>(ptr);//.net 4.0 不支持
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }

        /// <summary>
        /// 将结构体对象复制到ptr托管的内存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="ptr"></param>
        public static void StructureToPtr<T>(T t, IntPtr ptr)
        {
            Marshal.StructureToPtr(t, ptr, false);
        }

        /// <summary>
        /// 获取类型的大小
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>类型的大小</returns>
        public static int SizeOf<T>()
        {
            //return Marshal.SizeOf<T>();//.net 4.0 不支持
            return Marshal.SizeOf(typeof(T));
        }

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            //获得当前工作进程
            Process proc = Process.GetCurrentProcess();
            long usedMemory = proc.PrivateMemorySize64;
            if (usedMemory > 1024 * 1024 * 200)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
                }
            }
        }
    }
}
