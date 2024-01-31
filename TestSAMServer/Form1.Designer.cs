namespace TestSAMServer {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.iv = new LotusAPI.Controls.FastImageView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bt_CONNECT = new Abeo.Controls.Roundable.RoundButton();
            this.flatLabel1 = new Abeo.Controls.FlatLabel();
            this.flatPanel1 = new Abeo.Controls.FlatPanel();
            this.bt_LOAD = new Abeo.Controls.Roundable.RoundButton();
            this.tb_IP = new Abeo.Controls.Roundable.RoundTextbox();
            this.logView1 = new LotusAPI.Controls.LogView();
            ((System.ComponentModel.ISupportInitialize)(this.iv)).BeginInit();
            this.flatPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // iv
            // 
            this.iv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iv.DrawFPS = false;
            this.iv.EnablePanZoom = true;
            this.iv.FrameRate = 60;
            this.iv.Location = new System.Drawing.Point(0, 38);
            this.iv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iv.Name = "iv";
            this.iv.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.iv.RenderContextType = SharpGL.RenderContextType.FBO;
            this.iv.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.iv.SelectionType = LotusAPI.Controls.FastImageView.SelectionMode.ROI;
            this.iv.ShowCrossHair = false;
            this.iv.ShowDebugInfo = false;
            this.iv.ShowImageBorder = true;
            this.iv.ShowPixelInfo = false;
            this.iv.Size = new System.Drawing.Size(970, 416);
            this.iv.TabIndex = 1;
            this.iv.UseCustomMouseEvent = false;
            this.iv.ZoomPanModifierKey = System.Windows.Forms.Keys.None;
            this.iv.ROISelectedEvent += new LotusAPI.Controls.FastImageView.ROISelectedEventHandler(this.iv_ROISelectedEvent);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // bt_CONNECT
            // 
            this.bt_CONNECT.BackColor = System.Drawing.Color.Transparent;
            this.bt_CONNECT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(110)))), ((int)(((byte)(118)))));
            this.bt_CONNECT.BorderCorners = Abeo.Controls.Roundable.Corners.All;
            this.bt_CONNECT.BorderRadius = 3;
            this.bt_CONNECT.BorderThickness = 0;
            this.bt_CONNECT.Checked = false;
            this.bt_CONNECT.CheckEnable = false;
            this.bt_CONNECT.ContentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.bt_CONNECT.ContentBackColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.bt_CONNECT.ContentBackColorMouseDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.bt_CONNECT.ContentBackColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(110)))));
            this.bt_CONNECT.ControlToBringToFront = null;
            this.bt_CONNECT.DebugMode = false;
            this.bt_CONNECT.Dock = System.Windows.Forms.DockStyle.Left;
            this.bt_CONNECT.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.bt_CONNECT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_CONNECT.ForeColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.bt_CONNECT.ForeColorMouseDown = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_CONNECT.ForeColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_CONNECT.Icon = null;
            this.bt_CONNECT.IconPortion = 0.3F;
            this.bt_CONNECT.IconSize = 24;
            this.bt_CONNECT.IconVisible = false;
            this.bt_CONNECT.Image = null;
            this.bt_CONNECT.ImageSizeMode = Abeo.Controls.Roundable.SizeMode.Stretch;
            this.bt_CONNECT.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.bt_CONNECT.IsExclusive = false;
            this.bt_CONNECT.Location = new System.Drawing.Point(225, 3);
            this.bt_CONNECT.Name = "bt_CONNECT";
            this.bt_CONNECT.Size = new System.Drawing.Size(109, 32);
            this.bt_CONNECT.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.bt_CONNECT.TabIndex = 3;
            this.bt_CONNECT.TagColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(66)))), ((int)(((byte)(129)))));
            this.bt_CONNECT.TagLocation = Abeo.Controls.Roundable.TagLocation.Left;
            this.bt_CONNECT.TagOffset = 5;
            this.bt_CONNECT.TagVisible = false;
            this.bt_CONNECT.TagWidth = 5;
            this.bt_CONNECT.Text = "CONNECT";
            this.bt_CONNECT.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bt_CONNECT.TextIconRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_CONNECT.Click += new System.EventHandler(this.bt_CONNECT_Click);
            // 
            // flatLabel1
            // 
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flatLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.flatLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.flatLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(123)))), ((int)(((byte)(131)))));
            this.flatLabel1.Location = new System.Drawing.Point(3, 3);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(62, 32);
            this.flatLabel1.TabIndex = 4;
            this.flatLabel1.Text = "Server";
            this.flatLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flatPanel1
            // 
            this.flatPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flatPanel1.Controls.Add(this.bt_LOAD);
            this.flatPanel1.Controls.Add(this.bt_CONNECT);
            this.flatPanel1.Controls.Add(this.tb_IP);
            this.flatPanel1.Controls.Add(this.flatLabel1);
            this.flatPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flatPanel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.flatPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(123)))), ((int)(((byte)(131)))));
            this.flatPanel1.Location = new System.Drawing.Point(0, 0);
            this.flatPanel1.Name = "flatPanel1";
            this.flatPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flatPanel1.Size = new System.Drawing.Size(970, 38);
            this.flatPanel1.TabIndex = 5;
            // 
            // bt_LOAD
            // 
            this.bt_LOAD.BackColor = System.Drawing.Color.Transparent;
            this.bt_LOAD.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(110)))), ((int)(((byte)(118)))));
            this.bt_LOAD.BorderCorners = Abeo.Controls.Roundable.Corners.All;
            this.bt_LOAD.BorderRadius = 3;
            this.bt_LOAD.BorderThickness = 0;
            this.bt_LOAD.Checked = false;
            this.bt_LOAD.CheckEnable = false;
            this.bt_LOAD.ContentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.bt_LOAD.ContentBackColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.bt_LOAD.ContentBackColorMouseDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.bt_LOAD.ContentBackColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(110)))));
            this.bt_LOAD.ControlToBringToFront = null;
            this.bt_LOAD.DebugMode = false;
            this.bt_LOAD.Dock = System.Windows.Forms.DockStyle.Left;
            this.bt_LOAD.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.bt_LOAD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_LOAD.ForeColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.bt_LOAD.ForeColorMouseDown = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_LOAD.ForeColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_LOAD.Icon = null;
            this.bt_LOAD.IconPortion = 0.3F;
            this.bt_LOAD.IconSize = 24;
            this.bt_LOAD.IconVisible = false;
            this.bt_LOAD.Image = null;
            this.bt_LOAD.ImageSizeMode = Abeo.Controls.Roundable.SizeMode.Stretch;
            this.bt_LOAD.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.bt_LOAD.IsExclusive = false;
            this.bt_LOAD.Location = new System.Drawing.Point(334, 3);
            this.bt_LOAD.Name = "bt_LOAD";
            this.bt_LOAD.Size = new System.Drawing.Size(141, 32);
            this.bt_LOAD.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.bt_LOAD.TabIndex = 6;
            this.bt_LOAD.TagColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(66)))), ((int)(((byte)(129)))));
            this.bt_LOAD.TagLocation = Abeo.Controls.Roundable.TagLocation.Left;
            this.bt_LOAD.TagOffset = 5;
            this.bt_LOAD.TagVisible = false;
            this.bt_LOAD.TagWidth = 5;
            this.bt_LOAD.Text = "LOAD IMAGE";
            this.bt_LOAD.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bt_LOAD.TextIconRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_LOAD.Click += new System.EventHandler(this.bt_LOAD_Click);
            // 
            // tb_IP
            // 
            this.tb_IP.BackColor = System.Drawing.Color.Transparent;
            this.tb_IP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(110)))), ((int)(((byte)(118)))));
            this.tb_IP.BorderCorners = Abeo.Controls.Roundable.Corners.All;
            this.tb_IP.BorderRadius = 3;
            this.tb_IP.BorderThickness = 1;
            this.tb_IP.ContentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.tb_IP.ContentBackColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.tb_IP.ContentBackColorMouseDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.tb_IP.ContentBackColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(110)))));
            this.tb_IP.DebugMode = false;
            this.tb_IP.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_IP.EnforceContent = Abeo.Controls.Roundable.RoundTextbox.EnforceType.IP;
            this.tb_IP.Font = new System.Drawing.Font("Consolas", 14F);
            this.tb_IP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.tb_IP.ForeColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.tb_IP.ForeColorMouseDown = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.tb_IP.ForeColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.tb_IP.Icon = null;
            this.tb_IP.IconPortion = 0.3F;
            this.tb_IP.IconSize = 24;
            this.tb_IP.IconVisible = false;
            this.tb_IP.Image = null;
            this.tb_IP.ImageSizeMode = Abeo.Controls.Roundable.SizeMode.Stretch;
            this.tb_IP.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.tb_IP.Location = new System.Drawing.Point(65, 3);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Padding = new System.Windows.Forms.Padding(6);
            this.tb_IP.PasswordChar = '\0';
            this.tb_IP.ReadOnly = false;
            this.tb_IP.Size = new System.Drawing.Size(160, 34);
            this.tb_IP.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tb_IP.TabIndex = 5;
            this.tb_IP.TagColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(66)))), ((int)(((byte)(129)))));
            this.tb_IP.TagLocation = Abeo.Controls.Roundable.TagLocation.Left;
            this.tb_IP.TagOffset = 5;
            this.tb_IP.TagVisible = false;
            this.tb_IP.TagWidth = 5;
            this.tb_IP.Text = "127.0.0.1";
            this.tb_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_IP.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.tb_IP.TextIconRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // logView1
            // 
            this.logView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
            this.logView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logView1.ColorDateTime = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(110)))), ((int)(((byte)(118)))));
            this.logView1.ColorDebug = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(123)))), ((int)(((byte)(131)))));
            this.logView1.ColorError = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(59)))), ((int)(((byte)(39)))));
            this.logView1.ColorFatal = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(66)))), ((int)(((byte)(129)))));
            this.logView1.ColorInfo = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(212)))));
            this.logView1.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(212)))));
            this.logView1.ColorTrace = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(123)))), ((int)(((byte)(131)))));
            this.logView1.ColorWarning = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(135)))), ((int)(((byte)(0)))));
            this.logView1.DateTimeFormat = LotusAPI.Controls.LogDateTimeFormat.Time;
            this.logView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logView1.Font = new System.Drawing.Font("Consolas", 9F);
            this.logView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(212)))));
            this.logView1.LineBuffer = 1000;
            this.logView1.Location = new System.Drawing.Point(0, 454);
            this.logView1.Name = "logView1";
            this.logView1.ReadOnly = true;
            this.logView1.ShowDateTime = true;
            this.logView1.Size = new System.Drawing.Size(970, 187);
            this.logView1.TabIndex = 6;
            this.logView1.Text = "";
            this.logView1.UpdateInterval = 100;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(970, 641);
            this.Controls.Add(this.iv);
            this.Controls.Add(this.logView1);
            this.Controls.Add(this.flatPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.iv)).EndInit();
            this.flatPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LotusAPI.Controls.FastImageView iv;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private Abeo.Controls.Roundable.RoundButton bt_CONNECT;
        private Abeo.Controls.FlatLabel flatLabel1;
        private Abeo.Controls.FlatPanel flatPanel1;
        private LotusAPI.Controls.LogView logView1;
        private Abeo.Controls.Roundable.RoundTextbox tb_IP;
        private Abeo.Controls.Roundable.RoundButton bt_LOAD;
    }
}

