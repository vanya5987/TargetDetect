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
            toolStripSeparator2 = new ToolStripSeparator();
            RipersRemover = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            PointCoordinateLabel = new ToolStripLabel();
            ExitButton = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)OriginalPictures).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // OriginalPictures
            // 
            OriginalPictures.Dock = DockStyle.Fill;
            OriginalPictures.Location = new Point(0, 24);
            OriginalPictures.Name = "OriginalPictures";
            OriginalPictures.Size = new Size(946, 569);
            OriginalPictures.SizeMode = PictureBoxSizeMode.Zoom;
            OriginalPictures.TabIndex = 4;
            OriginalPictures.TabStop = false;
            // 
            // menuStrip2
            // 
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(946, 24);
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
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { ChangeCameraLanel, CameraChoice, toolStripSeparator1, VievVideo, toolStripSeparator2, RipersRemover, toolStripSeparator3, PointCoordinateLabel, ExitButton });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(946, 28);
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
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 28);
            // 
            // RipersRemover
            // 
            RipersRemover.DisplayStyle = ToolStripItemDisplayStyle.Text;
            RipersRemover.Image = (System.Drawing.Image)resources.GetObject("RipersRemover.Image");
            RipersRemover.ImageTransparentColor = Color.Magenta;
            RipersRemover.Name = "RipersRemover";
            RipersRemover.Size = new Size(136, 25);
            RipersRemover.Text = "Удалить маркеры";
            RipersRemover.Click += RipersRemoverClick;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 28);
            // 
            // PointCoordinateLabel
            // 
            PointCoordinateLabel.Name = "PointCoordinateLabel";
            PointCoordinateLabel.Size = new Size(0, 25);
            // 
            // ExitButton
            // 
            ExitButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ExitButton.Image = (System.Drawing.Image)resources.GetObject("ExitButton.Image");
            ExitButton.ImageTransparentColor = Color.Magenta;
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(57, 25);
            ExitButton.Text = "Выход";
            ExitButton.Click += ExitButtonClick;
            // 
            // TestTask
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(946, 593);
            Controls.Add(toolStrip1);
            Controls.Add(OriginalPictures);
            Controls.Add(menuStrip2);
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
        private PictureBox OriginalPictures;
        private MenuStrip menuStrip2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStrip toolStrip1;
        private ToolStripLabel ChangeCameraLanel;
        private ToolStripComboBox CameraChoice;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton VievVideo;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton RipersRemover;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel PointCoordinateLabel;
        private ToolStripButton ExitButton;
    }
}
