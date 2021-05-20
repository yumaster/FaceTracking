
namespace VPDemo
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_top = new System.Windows.Forms.Panel();
            this.btn_Play = new System.Windows.Forms.Button();
            this.txt_rtmp = new System.Windows.Forms.TextBox();
            this.panel_bottom_left = new System.Windows.Forms.Panel();
            this.pic_Video = new System.Windows.Forms.PictureBox();
            this.panel_bottom_right = new System.Windows.Forms.Panel();
            this.lbl_Capture = new System.Windows.Forms.Label();
            this.lbl_target = new System.Windows.Forms.Label();
            this.pic_cutImg = new System.Windows.Forms.PictureBox();
            this.pic_upImg = new System.Windows.Forms.PictureBox();
            this.lbl_similarity = new System.Windows.Forms.Label();
            this.lbl_simiValue = new System.Windows.Forms.Label();
            this.panel_top.SuspendLayout();
            this.panel_bottom_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Video)).BeginInit();
            this.panel_bottom_right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cutImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_upImg)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.btn_Play);
            this.panel_top.Controls.Add(this.txt_rtmp);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(1074, 51);
            this.panel_top.TabIndex = 0;
            // 
            // btn_Play
            // 
            this.btn_Play.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_Play.Location = new System.Drawing.Point(974, 9);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(88, 32);
            this.btn_Play.TabIndex = 1;
            this.btn_Play.Text = "播放";
            this.btn_Play.UseVisualStyleBackColor = false;
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // txt_rtmp
            // 
            this.txt_rtmp.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txt_rtmp.Font = new System.Drawing.Font("宋体", 16F);
            this.txt_rtmp.Location = new System.Drawing.Point(3, 8);
            this.txt_rtmp.Name = "txt_rtmp";
            this.txt_rtmp.Size = new System.Drawing.Size(965, 32);
            this.txt_rtmp.TabIndex = 0;
            // 
            // panel_bottom_left
            // 
            this.panel_bottom_left.Controls.Add(this.pic_Video);
            this.panel_bottom_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_bottom_left.Location = new System.Drawing.Point(0, 51);
            this.panel_bottom_left.Name = "panel_bottom_left";
            this.panel_bottom_left.Size = new System.Drawing.Size(788, 574);
            this.panel_bottom_left.TabIndex = 1;
            // 
            // pic_Video
            // 
            this.pic_Video.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Video.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_Video.Location = new System.Drawing.Point(0, 0);
            this.pic_Video.Name = "pic_Video";
            this.pic_Video.Size = new System.Drawing.Size(788, 574);
            this.pic_Video.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Video.TabIndex = 0;
            this.pic_Video.TabStop = false;
            this.pic_Video.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_Video_Paint);
            // 
            // panel_bottom_right
            // 
            this.panel_bottom_right.Controls.Add(this.lbl_simiValue);
            this.panel_bottom_right.Controls.Add(this.lbl_similarity);
            this.panel_bottom_right.Controls.Add(this.lbl_Capture);
            this.panel_bottom_right.Controls.Add(this.lbl_target);
            this.panel_bottom_right.Controls.Add(this.pic_cutImg);
            this.panel_bottom_right.Controls.Add(this.pic_upImg);
            this.panel_bottom_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_bottom_right.Location = new System.Drawing.Point(788, 51);
            this.panel_bottom_right.Name = "panel_bottom_right";
            this.panel_bottom_right.Size = new System.Drawing.Size(286, 574);
            this.panel_bottom_right.TabIndex = 2;
            // 
            // lbl_Capture
            // 
            this.lbl_Capture.AutoSize = true;
            this.lbl_Capture.Location = new System.Drawing.Point(15, 389);
            this.lbl_Capture.Name = "lbl_Capture";
            this.lbl_Capture.Size = new System.Drawing.Size(17, 84);
            this.lbl_Capture.TabIndex = 3;
            this.lbl_Capture.Text = "抓\r\n\r\n拍\r\n\r\n图\r\n\r\n像";
            // 
            // lbl_target
            // 
            this.lbl_target.AutoSize = true;
            this.lbl_target.Location = new System.Drawing.Point(15, 97);
            this.lbl_target.Name = "lbl_target";
            this.lbl_target.Size = new System.Drawing.Size(17, 84);
            this.lbl_target.TabIndex = 2;
            this.lbl_target.Text = "目\r\n\r\n标\r\n\r\n人\r\n\r\n物";
            // 
            // pic_cutImg
            // 
            this.pic_cutImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_cutImg.Location = new System.Drawing.Point(38, 310);
            this.pic_cutImg.Name = "pic_cutImg";
            this.pic_cutImg.Size = new System.Drawing.Size(219, 248);
            this.pic_cutImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_cutImg.TabIndex = 1;
            this.pic_cutImg.TabStop = false;
            // 
            // pic_upImg
            // 
            this.pic_upImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_upImg.Location = new System.Drawing.Point(38, 10);
            this.pic_upImg.Name = "pic_upImg";
            this.pic_upImg.Size = new System.Drawing.Size(219, 248);
            this.pic_upImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_upImg.TabIndex = 0;
            this.pic_upImg.TabStop = false;
            this.pic_upImg.Click += new System.EventHandler(this.pic_upImg_Click);
            // 
            // lbl_similarity
            // 
            this.lbl_similarity.AutoSize = true;
            this.lbl_similarity.Location = new System.Drawing.Point(38, 277);
            this.lbl_similarity.Name = "lbl_similarity";
            this.lbl_similarity.Size = new System.Drawing.Size(53, 12);
            this.lbl_similarity.TabIndex = 4;
            this.lbl_similarity.Text = "相似度：";
            // 
            // lbl_simiValue
            // 
            this.lbl_simiValue.AutoSize = true;
            this.lbl_simiValue.Location = new System.Drawing.Point(94, 277);
            this.lbl_simiValue.Name = "lbl_simiValue";
            this.lbl_simiValue.Size = new System.Drawing.Size(29, 12);
            this.lbl_simiValue.TabIndex = 5;
            this.lbl_simiValue.Text = "0.00";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1074, 625);
            this.Controls.Add(this.panel_bottom_right);
            this.Controls.Add(this.panel_bottom_left);
            this.Controls.Add(this.panel_top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视频流播放";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            this.panel_bottom_left.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Video)).EndInit();
            this.panel_bottom_right.ResumeLayout(false);
            this.panel_bottom_right.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cutImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_upImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Panel panel_bottom_left;
        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.TextBox txt_rtmp;
        private System.Windows.Forms.PictureBox pic_Video;
        private System.Windows.Forms.Panel panel_bottom_right;
        private System.Windows.Forms.PictureBox pic_upImg;
        private System.Windows.Forms.PictureBox pic_cutImg;
        private System.Windows.Forms.Label lbl_target;
        private System.Windows.Forms.Label lbl_Capture;
        private System.Windows.Forms.Label lbl_similarity;
        private System.Windows.Forms.Label lbl_simiValue;
    }
}

