namespace SystemConfiguration
{
    partial class frmConnectionPlaceWithAccounts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnRefrish = new System.Windows.Forms.Button();
            this.btnDele = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefrsh = new System.Windows.Forms.Button();
            this.btnAddSup = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.combAccountIDMAdden = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.combAccountIDDaan = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.combPalce = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.تصديرالىاكسلToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(187, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 46);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "ربط الحسابات بالجهات في طلب الصرف";
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.btnRefrish);
            this.groupBox11.Controls.Add(this.btnDele);
            this.groupBox11.Controls.Add(this.btnExit);
            this.groupBox11.Controls.Add(this.btnRefrsh);
            this.groupBox11.Controls.Add(this.btnAddSup);
            this.groupBox11.Location = new System.Drawing.Point(12, 130);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(454, 64);
            this.groupBox11.TabIndex = 3;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "العمليات";
            // 
            // btnRefrish
            // 
            this.btnRefrish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefrish.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnRefrish.Image = global::SystemConfiguration.Properties.Resources.refresh__1_;
            this.btnRefrish.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefrish.Location = new System.Drawing.Point(93, 19);
            this.btnRefrish.Name = "btnRefrish";
            this.btnRefrish.Size = new System.Drawing.Size(93, 35);
            this.btnRefrish.TabIndex = 6;
            this.btnRefrish.Text = "تحديث ";
            this.btnRefrish.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefrish.UseVisualStyleBackColor = true;
            this.btnRefrish.Click += new System.EventHandler(this.btnRefrish_Click);
            // 
            // btnDele
            // 
            this.btnDele.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDele.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnDele.Image = global::SystemConfiguration.Properties.Resources.delete;
            this.btnDele.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDele.Location = new System.Drawing.Point(193, 19);
            this.btnDele.Name = "btnDele";
            this.btnDele.Size = new System.Drawing.Size(81, 35);
            this.btnDele.TabIndex = 5;
            this.btnDele.Text = "حذف";
            this.btnDele.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDele.UseVisualStyleBackColor = true;
            this.btnDele.Click += new System.EventHandler(this.btnDele_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnExit.Image = global::SystemConfiguration.Properties.Resources.exit__3_;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(6, 19);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(81, 35);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "خروج";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnRefrsh
            // 
            this.btnRefrsh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefrsh.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnRefrsh.Image = global::SystemConfiguration.Properties.Resources.update;
            this.btnRefrsh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefrsh.Location = new System.Drawing.Point(280, 19);
            this.btnRefrsh.Name = "btnRefrsh";
            this.btnRefrsh.Size = new System.Drawing.Size(81, 35);
            this.btnRefrsh.TabIndex = 4;
            this.btnRefrsh.Text = "تعديل";
            this.btnRefrsh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefrsh.UseVisualStyleBackColor = true;
            this.btnRefrsh.Click += new System.EventHandler(this.btnRefrsh_Click);
            // 
            // btnAddSup
            // 
            this.btnAddSup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSup.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnAddSup.Image = global::SystemConfiguration.Properties.Resources.plus;
            this.btnAddSup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddSup.Location = new System.Drawing.Point(367, 19);
            this.btnAddSup.Name = "btnAddSup";
            this.btnAddSup.Size = new System.Drawing.Size(81, 35);
            this.btnAddSup.TabIndex = 3;
            this.btnAddSup.Text = "اضافة";
            this.btnAddSup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddSup.UseVisualStyleBackColor = true;
            this.btnAddSup.Click += new System.EventHandler(this.btnAddSup_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.combAccountIDMAdden);
            this.groupBox10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(401, 73);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(410, 51);
            this.groupBox10.TabIndex = 1;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "الحساب المدين ";
            // 
            // combAccountIDMAdden
            // 
            this.combAccountIDMAdden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.combAccountIDMAdden.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combAccountIDMAdden.FormattingEnabled = true;
            this.combAccountIDMAdden.Location = new System.Drawing.Point(6, 20);
            this.combAccountIDMAdden.Name = "combAccountIDMAdden";
            this.combAccountIDMAdden.Size = new System.Drawing.Size(398, 23);
            this.combAccountIDMAdden.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox11);
            this.groupBox2.Controls.Add(this.groupBox10);
            this.groupBox2.Controls.Add(this.groupBox9);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(817, 199);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtNumber);
            this.groupBox4.Location = new System.Drawing.Point(622, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(189, 50);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "الرقم";
            // 
            // txtNumber
            // 
            this.txtNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumber.Enabled = false;
            this.txtNumber.Location = new System.Drawing.Point(6, 19);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(177, 22);
            this.txtNumber.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.combAccountIDDaan);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(389, 51);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "الحساب الدائن";
            // 
            // combAccountIDDaan
            // 
            this.combAccountIDDaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.combAccountIDDaan.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combAccountIDDaan.FormattingEnabled = true;
            this.combAccountIDDaan.Location = new System.Drawing.Point(6, 20);
            this.combAccountIDDaan.Name = "combAccountIDDaan";
            this.combAccountIDDaan.Size = new System.Drawing.Size(377, 23);
            this.combAccountIDDaan.TabIndex = 2;
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.combPalce);
            this.groupBox9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(227, 16);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(389, 51);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "اسم الجهة";
            // 
            // combPalce
            // 
            this.combPalce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.combPalce.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combPalce.FormattingEnabled = true;
            this.combPalce.Location = new System.Drawing.Point(6, 20);
            this.combPalce.Name = "combPalce";
            this.combPalce.Size = new System.Drawing.Size(377, 23);
            this.combPalce.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1063, 437);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.تصديرالىاكسلToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 26);
            // 
            // تصديرالىاكسلToolStripMenuItem
            // 
            this.تصديرالىاكسلToolStripMenuItem.Name = "تصديرالىاكسلToolStripMenuItem";
            this.تصديرالىاكسلToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.تصديرالىاكسلToolStripMenuItem.Text = "تصدير الى اكسل";
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.dataGridView1);
            this.groupBox12.Location = new System.Drawing.Point(12, 262);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(1075, 462);
            this.groupBox12.TabIndex = 17;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "قائمة الربط";
            // 
            // frmConnectionPlaceWithAccounts
            // 
            this.AcceptButton = this.btnAddSup;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 736);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox12);
            this.Name = "frmConnectionPlaceWithAccounts";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "ربط الحسابات بالجهات";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmConnectionPlaceWithAccounts_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnDele;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefrsh;
        private System.Windows.Forms.Button btnAddSup;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem تصديرالىاكسلToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ComboBox combAccountIDMAdden;
        private System.Windows.Forms.ComboBox combAccountIDDaan;
        private System.Windows.Forms.ComboBox combPalce;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnRefrish;
    }
}