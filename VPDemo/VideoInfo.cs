using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPDemo
{
    public class VideoInfo
    {
        public object Filename { get; internal set; }
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public int Fps { get; internal set; }
    }
}
