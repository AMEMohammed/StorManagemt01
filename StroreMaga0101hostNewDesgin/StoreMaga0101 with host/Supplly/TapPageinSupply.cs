using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supplly
{
   public class TapPageinSupply
    {
        //desigen
        #region

        private System.Windows.Forms.TabPage tabPageِAddSup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TextBox textPriceAddSup;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnExiteAddSup;
        private System.Windows.Forms.Button btnRefrshAddSup;
        private System.Windows.Forms.Button btnPrintAddSup;
        private System.Windows.Forms.Button btnAddSup;
        private System.Windows.Forms.CheckBox checkPrintExitAddSup;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox textNoteAddSupp;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox textNameSuppAddSupp;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox combDaananAddSup;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox comboMaddenAddSup;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox combCurrncyAddSup;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textQuntityAddSup;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboTypeAddSup;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ComboBox comboCateAddSupply;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.DataGridView dataGridView1;
     public   TabPage b()
        {

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabPageِAddSup = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.textPriceAddSup = new System.Windows.Forms.TextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnExiteAddSup = new System.Windows.Forms.Button();
            this.btnRefrshAddSup = new System.Windows.Forms.Button();
            this.btnPrintAddSup = new System.Windows.Forms.Button();
            this.btnAddSup = new System.Windows.Forms.Button();
            this.checkPrintExitAddSup = new System.Windows.Forms.CheckBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.textNoteAddSupp = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textNameSuppAddSupp = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.combDaananAddSup = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.comboMaddenAddSup = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.combCurrncyAddSup = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textQuntityAddSup = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboTypeAddSup = new System.Windows.Forms.ComboBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.comboCateAddSupply = new System.Windows.Forms.ComboBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
           
       
          
          
            this.tabPageِAddSup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            // tabPageِAddSup
            // 
            this.tabPageِAddSup.Controls.Add(this.groupBox2);
            this.tabPageِAddSup.Controls.Add(this.groupBox3);
            this.tabPageِAddSup.Controls.Add(this.groupBox14);
            this.tabPageِAddSup.Location = new System.Drawing.Point(4, 22);
            this.tabPageِAddSup.Name = "tabPageِAddSup";
            this.tabPageِAddSup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageِAddSup.Size = new System.Drawing.Size(1215, 666);
            this.tabPageِAddSup.TabIndex = 0;
            this.tabPageِAddSup.Text = "   اضافة طلب توريد       ";
            this.tabPageِAddSup.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(414, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(423, 46);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(158, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "أضافة طلب توريد";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.groupBox13);
            this.groupBox3.Controls.Add(this.groupBox11);
            this.groupBox3.Controls.Add(this.checkPrintExitAddSup);
            this.groupBox3.Controls.Add(this.groupBox10);
            this.groupBox3.Controls.Add(this.groupBox9);
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.groupBox12);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(212, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(817, 235);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.textPriceAddSup);
            this.groupBox13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox13.Location = new System.Drawing.Point(128, 9);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(132, 51);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "سعر الوحدة";
            // 
            // textPriceAddSup
            // 
            this.textPriceAddSup.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPriceAddSup.Location = new System.Drawing.Point(6, 18);
            this.textPriceAddSup.Name = "textPriceAddSup";
            this.textPriceAddSup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textPriceAddSup.Size = new System.Drawing.Size(120, 25);
            this.textPriceAddSup.TabIndex = 3;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnExiteAddSup);
            this.groupBox11.Controls.Add(this.btnRefrshAddSup);
            this.groupBox11.Controls.Add(this.btnPrintAddSup);
            this.groupBox11.Controls.Add(this.btnAddSup);
            this.groupBox11.Location = new System.Drawing.Point(29, 164);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(360, 64);
            this.groupBox11.TabIndex = 10;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "العمليات";
            // 
            // btnExiteAddSup
            // 
            this.btnExiteAddSup.Font = new System.Drawing.Font("Tahoma", 12F);
           // this.btnExiteAddSup.Image = global::StoreMaga0101.Properties.Resources.exit__3_;
            this.btnExiteAddSup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExiteAddSup.Location = new System.Drawing.Point(6, 19);
            this.btnExiteAddSup.Name = "btnExiteAddSup";
            this.btnExiteAddSup.Size = new System.Drawing.Size(81, 35);
            this.btnExiteAddSup.TabIndex = 13;
            this.btnExiteAddSup.Text = "خروج";
            this.btnExiteAddSup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExiteAddSup.UseVisualStyleBackColor = true;
            // 
            // btnRefrshAddSup
            // 
            this.btnRefrshAddSup.Font = new System.Drawing.Font("Tahoma", 12F);
          //  this.btnRefrshAddSup.Image = global::StoreMaga0101.Properties.Resources.rotate;
            this.btnRefrshAddSup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefrshAddSup.Location = new System.Drawing.Point(178, 19);
            this.btnRefrshAddSup.Name = "btnRefrshAddSup";
            this.btnRefrshAddSup.Size = new System.Drawing.Size(81, 35);
            this.btnRefrshAddSup.TabIndex = 11;
            this.btnRefrshAddSup.Text = "تحديث";
            this.btnRefrshAddSup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefrshAddSup.UseVisualStyleBackColor = true;
            // 
            // btnPrintAddSup
            // 
            this.btnPrintAddSup.Font = new System.Drawing.Font("Tahoma", 12F);
          //  this.btnPrintAddSup.Image = global::StoreMaga0101.Properties.Resources.printer__3_;
            this.btnPrintAddSup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintAddSup.Location = new System.Drawing.Point(91, 19);
            this.btnPrintAddSup.Name = "btnPrintAddSup";
            this.btnPrintAddSup.Size = new System.Drawing.Size(81, 35);
            this.btnPrintAddSup.TabIndex = 12;
            this.btnPrintAddSup.Text = "طباعة";
            this.btnPrintAddSup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintAddSup.UseVisualStyleBackColor = true;
            // 
            // btnAddSup
            // 
            this.btnAddSup.Font = new System.Drawing.Font("Tahoma", 12F);
           // this.btnAddSup.Image = global::StoreMaga0101.Properties.Resources.plus;
            this.btnAddSup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddSup.Location = new System.Drawing.Point(265, 19);
            this.btnAddSup.Name = "btnAddSup";
            this.btnAddSup.Size = new System.Drawing.Size(81, 35);
            this.btnAddSup.TabIndex = 10;
            this.btnAddSup.Text = "اضافة";
            this.btnAddSup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddSup.UseVisualStyleBackColor = true;
            // 
            // checkPrintExitAddSup
            // 
            this.checkPrintExitAddSup.AutoSize = true;
            this.checkPrintExitAddSup.Location = new System.Drawing.Point(695, 208);
            this.checkPrintExitAddSup.Name = "checkPrintExitAddSup";
            this.checkPrintExitAddSup.Size = new System.Drawing.Size(105, 20);
            this.checkPrintExitAddSup.TabIndex = 9;
            this.checkPrintExitAddSup.Text = "طباعة امر خروج";
            this.checkPrintExitAddSup.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.textNoteAddSupp);
            this.groupBox10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(29, 59);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(393, 51);
            this.groupBox10.TabIndex = 6;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "وصف الطلب";
            // 
            // textNoteAddSupp
            // 
            this.textNoteAddSupp.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNoteAddSupp.Location = new System.Drawing.Point(6, 20);
            this.textNoteAddSupp.Name = "textNoteAddSupp";
            this.textNoteAddSupp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textNoteAddSupp.Size = new System.Drawing.Size(381, 25);
            this.textNoteAddSupp.TabIndex = 6;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.textNameSuppAddSupp);
            this.groupBox9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(428, 59);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(372, 51);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "اسم  المورد";
            // 
            // textNameSuppAddSupp
            // 
            this.textNameSuppAddSupp.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNameSuppAddSupp.Location = new System.Drawing.Point(5, 20);
            this.textNameSuppAddSupp.Name = "textNameSuppAddSupp";
            this.textNameSuppAddSupp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textNameSuppAddSupp.Size = new System.Drawing.Size(361, 25);
            this.textNameSuppAddSupp.TabIndex = 5;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.combDaananAddSup);
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(29, 107);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(393, 51);
            this.groupBox8.TabIndex = 8;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "حساب الدائن";
            // 
            // combDaananAddSup
            // 
            this.combDaananAddSup.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combDaananAddSup.FormattingEnabled = true;
            this.combDaananAddSup.Location = new System.Drawing.Point(6, 20);
            this.combDaananAddSup.Name = "combDaananAddSup";
            this.combDaananAddSup.Size = new System.Drawing.Size(381, 25);
            this.combDaananAddSup.TabIndex = 8;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.comboMaddenAddSup);
            this.groupBox7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(428, 107);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(372, 51);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "حساب المدين";
            // 
            // comboMaddenAddSup
            // 
            this.comboMaddenAddSup.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMaddenAddSup.FormattingEnabled = true;
            this.comboMaddenAddSup.Location = new System.Drawing.Point(6, 20);
            this.comboMaddenAddSup.Name = "comboMaddenAddSup";
            this.comboMaddenAddSup.Size = new System.Drawing.Size(360, 25);
            this.comboMaddenAddSup.TabIndex = 7;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.combCurrncyAddSup);
            this.groupBox6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(29, 9);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(93, 51);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "العملة";
            // 
            // combCurrncyAddSup
            // 
            this.combCurrncyAddSup.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combCurrncyAddSup.FormattingEnabled = true;
            this.combCurrncyAddSup.Location = new System.Drawing.Point(6, 17);
            this.combCurrncyAddSup.Name = "combCurrncyAddSup";
            this.combCurrncyAddSup.Size = new System.Drawing.Size(81, 25);
            this.combCurrncyAddSup.TabIndex = 4;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textQuntityAddSup);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(266, 9);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(110, 51);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "الكمية";
            // 
            // textQuntityAddSup
            // 
            this.textQuntityAddSup.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textQuntityAddSup.Location = new System.Drawing.Point(6, 18);
            this.textQuntityAddSup.Name = "textQuntityAddSup";
            this.textQuntityAddSup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textQuntityAddSup.Size = new System.Drawing.Size(98, 25);
            this.textQuntityAddSup.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboTypeAddSup);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(382, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(123, 51);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "نوع الكمية";
            // 
            // comboTypeAddSup
            // 
            this.comboTypeAddSup.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTypeAddSup.FormattingEnabled = true;
            this.comboTypeAddSup.Location = new System.Drawing.Point(12, 17);
            this.comboTypeAddSup.Name = "comboTypeAddSup";
            this.comboTypeAddSup.Size = new System.Drawing.Size(105, 25);
            this.comboTypeAddSup.TabIndex = 1;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.comboCateAddSupply);
            this.groupBox12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox12.Location = new System.Drawing.Point(511, 9);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(289, 51);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "اسم الصنف";
            // 
            // comboCateAddSupply
            // 
            this.comboCateAddSupply.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCateAddSupply.FormattingEnabled = true;
            this.comboCateAddSupply.Location = new System.Drawing.Point(6, 17);
            this.comboCateAddSupply.Name = "comboCateAddSupply";
            this.comboCateAddSupply.Size = new System.Drawing.Size(277, 25);
            this.comboCateAddSupply.TabIndex = 0;
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.Controls.Add(this.dataGridView1);
            this.groupBox14.Location = new System.Drawing.Point(6, 297);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(1204, 363);
            this.groupBox14.TabIndex = 14;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "طلبات التوريد لليوم";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          
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
            this.dataGridView1.Size = new System.Drawing.Size(1192, 338);
            this.dataGridView1.TabIndex = 12;
            // 
            // FrmMain
            // 
            
           
        
            this.tabPageِAddSup.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();

           return  tabPageِAddSup;
        }
        #endregion
        //end Design

    }
}
