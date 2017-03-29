namespace ChromaKey_GUI
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.previewPicture = new System.Windows.Forms.PictureBox();
            this.preview = new System.Windows.Forms.Button();
            this.apply = new System.Windows.Forms.Button();
            this.baseColor = new System.Windows.Forms.PictureBox();
            this.fileOpen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.border = new System.Windows.Forms.TrackBar();
            this.fileList = new System.Windows.Forms.ListBox();
            this.redDrop = new System.Windows.Forms.NumericUpDown();
            this.greenDrop = new System.Windows.Forms.NumericUpDown();
            this.blueDrop = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.previewPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.border)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redDrop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenDrop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueDrop)).BeginInit();
            this.SuspendLayout();
            // 
            // previewPicture
            // 
            this.previewPicture.BackColor = System.Drawing.SystemColors.Control;
            this.previewPicture.Location = new System.Drawing.Point(309, 36);
            this.previewPicture.Name = "previewPicture";
            this.previewPicture.Size = new System.Drawing.Size(380, 500);
            this.previewPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewPicture.TabIndex = 0;
            this.previewPicture.TabStop = false;
            this.previewPicture.Click += new System.EventHandler(this.previewPicture_Click);
            // 
            // preview
            // 
            this.preview.Location = new System.Drawing.Point(50, 449);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(214, 35);
            this.preview.TabIndex = 1;
            this.preview.Text = "プレビュー";
            this.preview.UseVisualStyleBackColor = true;
            this.preview.Click += new System.EventHandler(this.preview_Click);
            // 
            // apply
            // 
            this.apply.Location = new System.Drawing.Point(50, 501);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(214, 35);
            this.apply.TabIndex = 2;
            this.apply.Text = "実行";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // baseColor
            // 
            this.baseColor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.baseColor.Location = new System.Drawing.Point(89, 207);
            this.baseColor.Name = "baseColor";
            this.baseColor.Size = new System.Drawing.Size(123, 121);
            this.baseColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.baseColor.TabIndex = 3;
            this.baseColor.TabStop = false;
            // 
            // fileOpen
            // 
            this.fileOpen.Location = new System.Drawing.Point(50, 36);
            this.fileOpen.Name = "fileOpen";
            this.fileOpen.Size = new System.Drawing.Size(214, 35);
            this.fileOpen.TabIndex = 4;
            this.fileOpen.Text = "ファイルを選択";
            this.fileOpen.UseVisualStyleBackColor = true;
            this.fileOpen.Click += new System.EventHandler(this.fileOpen_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*";
            this.openFileDialog1.Filter = "画像ファイル|*.jpg;*.jpeg;*.png|すべてのファイル|*.*";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // border
            // 
            this.border.Location = new System.Drawing.Point(69, 346);
            this.border.Maximum = 100;
            this.border.Name = "border";
            this.border.Size = new System.Drawing.Size(152, 45);
            this.border.TabIndex = 13;
            this.border.Value = 30;
            // 
            // fileList
            // 
            this.fileList.FormattingEnabled = true;
            this.fileList.HorizontalScrollbar = true;
            this.fileList.ItemHeight = 12;
            this.fileList.Location = new System.Drawing.Point(50, 86);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(214, 100);
            this.fileList.TabIndex = 14;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.fileList_SelectedIndexChanged);
            // 
            // redDrop
            // 
            this.redDrop.Location = new System.Drawing.Point(67, 397);
            this.redDrop.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.redDrop.Name = "redDrop";
            this.redDrop.Size = new System.Drawing.Size(40, 19);
            this.redDrop.TabIndex = 15;
            this.redDrop.ValueChanged += new System.EventHandler(this.redDrop_ValueChanged);
            // 
            // greenDrop
            // 
            this.greenDrop.Location = new System.Drawing.Point(139, 397);
            this.greenDrop.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.greenDrop.Name = "greenDrop";
            this.greenDrop.Size = new System.Drawing.Size(40, 19);
            this.greenDrop.TabIndex = 16;
            this.greenDrop.ValueChanged += new System.EventHandler(this.greenDrop_ValueChanged);
            // 
            // blueDrop
            // 
            this.blueDrop.Location = new System.Drawing.Point(212, 397);
            this.blueDrop.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.blueDrop.Name = "blueDrop";
            this.blueDrop.Size = new System.Drawing.Size(40, 19);
            this.blueDrop.TabIndex = 17;
            this.blueDrop.ValueChanged += new System.EventHandler(this.blueDrop_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 399);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 399);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "G";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 399);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "B";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "閾値 0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(210, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "100";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(744, 581);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blueDrop);
            this.Controls.Add(this.greenDrop);
            this.Controls.Add(this.redDrop);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.border);
            this.Controls.Add(this.fileOpen);
            this.Controls.Add(this.baseColor);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.previewPicture);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.border)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redDrop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenDrop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueDrop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewPicture;
        private System.Windows.Forms.Button preview;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.PictureBox baseColor;
        private System.Windows.Forms.Button fileOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TrackBar border;
        private System.Windows.Forms.ListBox fileList;
        private System.Windows.Forms.NumericUpDown redDrop;
        private System.Windows.Forms.NumericUpDown greenDrop;
        private System.Windows.Forms.NumericUpDown blueDrop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

