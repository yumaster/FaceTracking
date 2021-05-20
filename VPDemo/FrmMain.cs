using ArcFaceSharp.Model;
using ArcFaceSharp.SDKAPI;
using ArcFaceSharp.Util;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPDemo
{
    public partial class FrmMain : Form
    {
        private IntPtr pImageEngine = IntPtr.Zero;//==========图片引擎Handle
        private float threshold = 0.8f;//相似度  阈值设置为0.8
        private FaceTrackUnit trackUnit = new FaceTrackUnit();//视频检测缓存实体类
        private Font font = new Font(System.Drawing.FontFamily.GenericSerif, 10f);//定义特定的文本格式，包括字体、字号和样式特性。
        private SolidBrush brush = new SolidBrush(Color.Red);// 定义一种颜色的画笔。 画笔用于填充图形形状，如矩形、 椭圆、 饼、 多边形和路径。
        private Pen pen = new Pen(Color.Red);//定义用于绘制直线和曲线的对象。
        private bool isLock = false;//下面用于同步的锁变量
        string rtmp = string.Empty;//rtmp地址
        private IntPtr imageTemp = IntPtr.Zero;//比对图片指针
        public bool IsStartPlay { get; private set; }
        VideoCapture videoCapture;
        VideoInfo videoInfo = null;
        private object LockHelper = new object();
        System.Timers.Timer myTimer = new System.Timers.Timer();
        public FrmMain()
        {
            InitializeComponent();
            myTimer.Elapsed += MyTimer_Elapsed;
            ActiveAndInitEngines();//激活并初始化引擎
        }

        private void MyTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (IsStartPlay)
                {
                    lock (LockHelper)
                    {
                        var frame = videoCapture.RetrieveMat();
                        if (frame != null)
                        {
                            if (frame.Width == videoInfo.Width && frame.Height == videoInfo.Height)
                                this.SetVideoCapture(frame);
                            else
                                LogHelper.Log($"bad frame");
                        }
                    }
                }
            }catch(Exception ex)
            {
                LogHelper.Log(ex.Message);
            }
        }
        Bitmap btm = null;
        private void SetVideoCapture(Mat frame)//设置视频捕获
        {
            try
            {
                btm = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                pic_Video.Image = btm;
            }catch(Exception ex)
            {
                LogHelper.Log(ex.Message);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            rtmp = ConfigurationManager.AppSettings["videoStreamUrl"];
            txt_rtmp.Text = rtmp;
        }

        private void PlayVideo()
        {
            videoCapture = new VideoCapture(rtmp);
            if (videoCapture.IsOpened())
            {
                videoInfo.Filename = rtmp;
                videoInfo.Width = (int)videoCapture.FrameWidth;
                videoInfo.Height = (int)videoCapture.FrameHeight;
                videoInfo.Fps = (int)videoCapture.Fps;

                myTimer.Interval = videoInfo.Fps == 0 ? 300 : 1000 / videoInfo.Fps;
                IsStartPlay = true;
                myTimer.Start();
            }
            else
            {
                MessageBox.Show("视频源异常");
            }
        }

        private void pic_Video_Paint(object sender, PaintEventArgs e)
        {
            CompareImgWithIDImg(btm, e);

            MemoryUtil.ClearMemory();
        }

        /// <summary>
        /// 比对函数，将每一帧抓拍的照片和目标人物照片进行比对
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private void CompareImgWithIDImg(Bitmap bitmap, PaintEventArgs e)
        {
            if (bitmap != null)
            {
                //保证只检测一帧，防止页面卡顿以及出现其他内存被占用情况
                if (isLock == false)
                {
                    isLock = true;
                    Graphics g = e.Graphics;
                    float offsetX = (pic_Video.Width * 1f / bitmap.Width);
                    float offsetY = (pic_Video.Height * 1f / bitmap.Height);
                    //根据Bitmap 获取人脸信息列表
                    List<FaceInfoModel> list = FaceUtil.GetFaceInfos(pImageEngine, bitmap);
                    foreach (FaceInfoModel sface in list)
                    {
                        //异步处理提取特征值和比对，不然页面会比较卡
                        ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                        {
                            try
                            {
                                //提取人脸特征
                                float similarity = CompareTwoFeatures(sface.feature, imageTemp);
                                if (similarity > threshold)
                                {
                                    this.pic_cutImg.Image = bitmap;

                                    this.Invoke((Action)(() =>
                                    {
                                        this.lbl_simiValue.Text = similarity.ToString();
                                    }));
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }));


                        MRECT rect = sface.faceRect;
                        float x = rect.left * offsetX;
                        float width = rect.right * offsetX - x;
                        float y = rect.top * offsetY;
                        float height = rect.bottom * offsetY - y;
                        //根据Rect进行画框
                        g.DrawRectangle(pen, x, y, width, height);
                        trackUnit.message = "年龄：" + sface.age.ToString() + "\r\n" + "性别：" + (sface.gender == 0 ? "男" : "女");
                        g.DrawString(trackUnit.message, font, brush, x, y + 5);
                    }
                    isLock = false;
                }
            }
        }

        /// <summary>
        /// 比较两个特征值的相似度,返回相似度
        /// </summary>
        /// <param name="feature1"></param>
        /// <param name="feature2"></param>
        /// <returns></returns>
        private float CompareTwoFeatures(IntPtr feature1, IntPtr feature2)
        {
            float similarity = 0.0f;
            //调用人脸匹配方法，进行匹配
            ASFFunctions.ASFFaceFeatureCompare(pImageEngine, feature1, feature2, ref similarity);
            return similarity;
        }

        /// <summary>
        /// 激活并初始化引擎
        /// </summary>
        private void ActiveAndInitEngines()
        {
            //读取配置文件中的 APP_ID 和 SDKKEY
            AppSettingsReader reader = new AppSettingsReader();
            string appId = (string)reader.GetValue("APP_ID", typeof(string));
            string sdkKey = (string)reader.GetValue("SDKKEY", typeof(string));
            int retCode = 0;
            //激活引擎    
            try
            {
                retCode = ASFFunctions.ASFActivation(appId, sdkKey);
            }
            catch (Exception ex)
            {
                //异常处理
                return;
            }
            #region 图片引擎pImageEngine初始化
            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //检测脸部的角度优先值
            int detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            if (retCode == 0)
                this.Text = ("图片引擎初始化成功!\n");
            else
                this.Text = (string.Format("图片引擎初始化失败!错误码为:{0}\n", retCode));
            #endregion
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (btn_Play.Text == "播放")
            {
                rtmp = txt_rtmp.Text.Trim();
                videoInfo = new VideoInfo();
                Task.Run(PlayVideo);
                this.btn_Play.Text = "暂停";
            }
            else if (btn_Play.Text == "继续")
            {
                myTimer.Start();
                this.btn_Play.Text = "暂停";
            }
            else
            {
                myTimer.Stop();
                this.btn_Play.Text = "继续";
            }
        }

        /// <summary>
        /// 目标图片添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_upImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "All Image Files|*.bmp;*.jpeg;*.jpg;*.png";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                //第一步，打开图片文件，获得比特流，生成字节数组。
                byte[] picbinary = ImageHelper.getImageByte(openfile.FileName);
                //第二步，将比特流存在内存工作流中
                MemoryStream mempicstream = new MemoryStream(picbinary);
                //加载内存流到图片控件
                this.pic_upImg.Image = Image.FromStream(mempicstream);
                mempicstream.Dispose();
                mempicstream.Close();
                imageTemp = ImageHelper.GetFeatureFromImage(pImageEngine, this.pic_upImg.Image);
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //销毁引擎
            int retCode = ASFFunctions.ASFUninitEngine(pImageEngine);
            Console.WriteLine("UninitEngine pImageEngine Result:" + retCode);
            this.Dispose();
            this.Close();
            MemoryUtil.ClearMemory();
        }
    }
}
