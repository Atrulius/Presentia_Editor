namespace Presentia_Editor {
    partial class Form_Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if( disposing && (components != null) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.lpImport = new System.Windows.Forms.FlowLayoutPanel();
            this.pEditing_Panel = new System.Windows.Forms.Panel();
            this.imgDisplay = new System.Windows.Forms.PictureBox();
            this.timer_Drag = new System.Windows.Forms.Timer(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.Progress_Bar = new System.Windows.Forms.ProgressBar();
            this.btnPause = new System.Windows.Forms.Button();
            this.timer_Progress_Bar = new System.Windows.Forms.Timer(this.components);
            this.pb_Drag = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Drag)).BeginInit();
            this.SuspendLayout();
            // 
            // lpImport
            // 
            this.lpImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lpImport.AutoScroll = true;
            this.lpImport.Location = new System.Drawing.Point(12, 12);
            this.lpImport.Name = "lpImport";
            this.lpImport.Size = new System.Drawing.Size(243, 334);
            this.lpImport.TabIndex = 0;
            this.lpImport.DoubleClick += new System.EventHandler(this.lpImport_DoubleClick);
            // 
            // pEditing_Panel
            // 
            this.pEditing_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pEditing_Panel.Location = new System.Drawing.Point(12, 352);
            this.pEditing_Panel.Name = "pEditing_Panel";
            this.pEditing_Panel.Size = new System.Drawing.Size(753, 200);
            this.pEditing_Panel.TabIndex = 1;
            // 
            // imgDisplay
            // 
            this.imgDisplay.Location = new System.Drawing.Point(261, 12);
            this.imgDisplay.Name = "imgDisplay";
            this.imgDisplay.Size = new System.Drawing.Size(504, 334);
            this.imgDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgDisplay.TabIndex = 2;
            this.imgDisplay.TabStop = false;
            // 
            // timer_Drag
            // 
            this.timer_Drag.Interval = 15;
            this.timer_Drag.Tick += new System.EventHandler(this.timer_Drag_Tick);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExport.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnExport.Location = new System.Drawing.Point(673, 518);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(104, 46);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // Progress_Bar
            // 
            this.Progress_Bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Progress_Bar.BackColor = System.Drawing.Color.Black;
            this.Progress_Bar.ForeColor = System.Drawing.Color.Red;
            this.Progress_Bar.Location = new System.Drawing.Point(72, 362);
            this.Progress_Bar.MarqueeAnimationSpeed = 20;
            this.Progress_Bar.Maximum = 1000;
            this.Progress_Bar.Name = "Progress_Bar";
            this.Progress_Bar.Size = new System.Drawing.Size(683, 10);
            this.Progress_Bar.Step = 1;
            this.Progress_Bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Progress_Bar.TabIndex = 4;
            this.Progress_Bar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Progress_Bar_MouseDown);
            this.Progress_Bar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Progress_Bar_MouseUp);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPause.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPause.Font = new System.Drawing.Font("Bahnschrift", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(17, 357);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(45, 20);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Play";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // timer_Progress_Bar
            // 
            this.timer_Progress_Bar.Interval = 16;
            this.timer_Progress_Bar.Tick += new System.EventHandler(this.timer_Progress_Bar_Tick);
            // 
            // pb_Drag
            // 
            this.pb_Drag.BackColor = System.Drawing.Color.Black;
            this.pb_Drag.Location = new System.Drawing.Point(216, 301);
            this.pb_Drag.Name = "pb_Drag";
            this.pb_Drag.Size = new System.Drawing.Size(75, 75);
            this.pb_Drag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Drag.TabIndex = 0;
            this.pb_Drag.TabStop = false;
            this.pb_Drag.Visible = false;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(777, 564);
            this.Controls.Add(this.pb_Drag);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.Progress_Bar);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.imgDisplay);
            this.Controls.Add(this.pEditing_Panel);
            this.Controls.Add(this.lpImport);
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "Form_Main";
            this.Text = "Presentia Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_Main_MouseUp);
            this.Resize += new System.EventHandler(this.Form_Main_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.imgDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Drag)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel lpImport;
        private System.Windows.Forms.Panel pEditing_Panel;
        private System.Windows.Forms.PictureBox imgDisplay;
        private System.Windows.Forms.Timer timer_Drag;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ProgressBar Progress_Bar;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Timer timer_Progress_Bar;
        private System.Windows.Forms.PictureBox pb_Drag;
    }
}

