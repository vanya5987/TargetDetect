namespace TestTask
{
    partial class TestTask
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestTask));
            OriginalPictures = new PictureBox();
            menuStrip2 = new MenuStrip();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStrip1 = new ToolStrip();
            ChangeCameraLanel = new ToolStripLabel();
            CameraChoice = new ToolStripComboBox();
            toolStripSeparator1 = new ToolStripSeparator();
            VievVideo = new ToolStripButton();
            CoordinatesLabel = new ToolStripLabel();
            PointCoordinateLabel = new ToolStripLabel();
            CircleRipers = new Button();
            SquareRipers = new Button();
            CrossRipers = new Button();
            ChooseRipers = new Label();
            ((System.ComponentModel.ISupportInitialize)OriginalPictures).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // OriginalPictures
            // 
            OriginalPictures.Dock = DockStyle.Fill;
            OriginalPictures.Enabled = false;
            OriginalPictures.Location = new Point(0, 24);
            OriginalPictures.Name = "OriginalPictures";
            OriginalPictures.Size = new Size(620, 477);
            OriginalPictures.TabIndex = 4;
            OriginalPictures.TabStop = false;
            // 
            // menuStrip2
            // 
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(620, 24);
            menuStrip2.TabIndex = 5;
            menuStrip2.Text = "menuStrip2";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // toolStrip1
            // 
            toolStrip1.Enabled = false;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { ChangeCameraLanel, CameraChoice, toolStripSeparator1, VievVideo, CoordinatesLabel, PointCoordinateLabel });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(620, 28);
            toolStrip1.TabIndex = 7;
            toolStrip1.Text = "toolStrip1";
            // 
            // ChangeCameraLanel
            // 
            ChangeCameraLanel.Name = "ChangeCameraLanel";
            ChangeCameraLanel.Size = new Size(68, 25);
            ChangeCameraLanel.Text = "Камеры:";
            // 
            // CameraChoice
            // 
            CameraChoice.Name = "CameraChoice";
            CameraChoice.Size = new Size(121, 28);
            CameraChoice.SelectedIndexChanged += CameraChoiceSelectedIndexChange;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 28);
            // 
            // VievVideo
            // 
            VievVideo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            VievVideo.Image = (System.Drawing.Image)resources.GetObject("VievVideo.Image");
            VievVideo.ImageTransparentColor = Color.Magenta;
            VievVideo.Name = "VievVideo";
            VievVideo.Size = new Size(79, 25);
            VievVideo.Text = "Смотреть";
            VievVideo.Click += VievVideoClick;
            // 
            // CoordinatesLabel
            // 
            CoordinatesLabel.Name = "CoordinatesLabel";
            CoordinatesLabel.Size = new Size(96, 25);
            CoordinatesLabel.Text = "Координаты";
            // 
            // PointCoordinateLabel
            // 
            PointCoordinateLabel.Name = "PointCoordinateLabel";
            PointCoordinateLabel.Size = new Size(0, 25);
            // 
            // CircleRipers
            // 
            CircleRipers.Location = new Point(166, 242);
            CircleRipers.Name = "CircleRipers";
            CircleRipers.Size = new Size(94, 29);
            CircleRipers.TabIndex = 9;
            CircleRipers.Text = "Круг";
            CircleRipers.UseVisualStyleBackColor = true;
            CircleRipers.Click += CircleRipers_Click;
            // 
            // SquareRipers
            // 
            SquareRipers.Location = new Point(266, 242);
            SquareRipers.Name = "SquareRipers";
            SquareRipers.Size = new Size(94, 29);
            SquareRipers.TabIndex = 10;
            SquareRipers.Text = "Квадрат";
            SquareRipers.UseVisualStyleBackColor = true;
            SquareRipers.Click += SquareRipers_Click;
            // 
            // CrossRipers
            // 
            CrossRipers.Location = new Point(366, 242);
            CrossRipers.Name = "CrossRipers";
            CrossRipers.Size = new Size(94, 29);
            CrossRipers.TabIndex = 11;
            CrossRipers.Text = "Крест";
            CrossRipers.UseVisualStyleBackColor = true;
            CrossRipers.Click += CrossRiper_Click;
            // 
            // ChooseRipers
            // 
            ChooseRipers.AutoSize = true;
            ChooseRipers.Location = new Point(212, 206);
            ChooseRipers.Name = "ChooseRipers";
            ChooseRipers.Size = new Size(200, 20);
            ChooseRipers.TabIndex = 12;
            ChooseRipers.Text = "Выберите форму риперов :";
            // 
            // TestTask
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 501);
            Controls.Add(ChooseRipers);
            Controls.Add(CrossRipers);
            Controls.Add(SquareRipers);
            Controls.Add(CircleRipers);
            Controls.Add(toolStrip1);
            Controls.Add(OriginalPictures);
            Controls.Add(menuStrip2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "TestTask";
            Text = "TestTask";
            Load += TestTaskLoad;
            ((System.ComponentModel.ISupportInitialize)OriginalPictures).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStrip toolStrip1;
        private ToolStripLabel ChangeCameraLanel;
        private ToolStripComboBox CameraChoice;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton VievVideo;
        private ToolStripLabel PointCoordinateLabel;
        private PictureBox OriginalPictures;
        private ToolStripLabel CoordinatesLabel;
        private Button CircleRipers;
        private Button SquareRipers;
        private Button CrossRipers;
        private Label ChooseRipers;
    }
}
