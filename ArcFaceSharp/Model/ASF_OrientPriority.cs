using System;

namespace ArcFaceSharp.Model
{
    /// <summary>
    /// 人脸检测优先角度结构体，推荐ASF_OP_0_HIGHER_EXT
    /// </summary>
    public struct ASF_OrientPriority
    {
        public const int ASF_OP_0_ONLY = 0x1;
        public const int ASF_OP_90_ONLY = 0x2;
        public const int ASF_OP_270_ONLY = 0x3;
        public const int ASF_OP_180_ONLY = 0x4;
        public const int ASF_OP_0_HIGHER_EXT = 0x5;
        //public const int ASF_OP_0_ONLY = 0x1;		// 常规预览下正方向
        //public const int ASF_OP_90_ONLY = 0x2;      // 基于0°逆时针旋转90°的方向
        //public const int ASF_OP_270_ONLY = 0x3;     // 基于0°逆时针旋转270°的方向
        //public const int ASF_OP_180_ONLY = 0x4;		// 基于0°旋转180°的方向（逆时针、顺时针效果一样）
        //public const int ASF_OP_ALL_OUT = 0x5;	// 全角度
    }
}
