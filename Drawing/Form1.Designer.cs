using System.Collections.Generic;
namespace BeehiveSelection
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.explorationMap = new System.Windows.Forms.PictureBox();
            this.beeNumber = new System.Windows.Forms.TextBox();
            this.generateBtn = new System.Windows.Forms.Button();
            this.siteNumber = new System.Windows.Forms.TextBox();
            this.migrateBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.maskBeesChkBox = new System.Windows.Forms.CheckBox();
            this.maskSitesChkBox = new System.Windows.Forms.CheckBox();
            this.activityPicture = new System.Windows.Forms.PictureBox();
            this.showExploration = new System.Windows.Forms.CheckBox();
            this.taskDistribution = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.beeDistributionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.qualityThreshold = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RestingBar = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.Rdot = new System.Windows.Forms.TextBox();
            this.Odot = new System.Windows.Forms.TextBox();
            this.Edot = new System.Windows.Forms.TextBox();
            this.Adot = new System.Windows.Forms.TextBox();
            this.Ddot = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.explorationMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskDistribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beeDistributionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RestingBar)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 653);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1012, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // explorationMap
            // 
            this.explorationMap.BackColor = System.Drawing.Color.DarkGray;
            this.explorationMap.Location = new System.Drawing.Point(12, 12);
            this.explorationMap.Name = "explorationMap";
            this.explorationMap.Size = new System.Drawing.Size(500, 500);
            this.explorationMap.TabIndex = 1;
            this.explorationMap.TabStop = false;
            this.explorationMap.Click += new System.EventHandler(this.map_click);
            this.explorationMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_down);
            this.explorationMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouse_move);
            this.explorationMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouse_up);
            // 
            // beeNumber
            // 
            this.beeNumber.Location = new System.Drawing.Point(110, 530);
            this.beeNumber.Name = "beeNumber";
            this.beeNumber.Size = new System.Drawing.Size(58, 20);
            this.beeNumber.TabIndex = 2;
            this.beeNumber.Text = "100";
            // 
            // generateBtn
            // 
            this.generateBtn.Location = new System.Drawing.Point(301, 529);
            this.generateBtn.Name = "generateBtn";
            this.generateBtn.Size = new System.Drawing.Size(102, 20);
            this.generateBtn.TabIndex = 3;
            this.generateBtn.Text = "Generate";
            this.generateBtn.UseVisualStyleBackColor = true;
            this.generateBtn.Click += new System.EventHandler(this.generateBtn_Click);
            // 
            // siteNumber
            // 
            this.siteNumber.Location = new System.Drawing.Point(110, 556);
            this.siteNumber.Name = "siteNumber";
            this.siteNumber.Size = new System.Drawing.Size(57, 20);
            this.siteNumber.TabIndex = 4;
            this.siteNumber.Text = "1";
            this.siteNumber.TextChanged += new System.EventHandler(this.siteNumber_TextChanged);
            // 
            // migrateBtn
            // 
            this.migrateBtn.Location = new System.Drawing.Point(409, 529);
            this.migrateBtn.Name = "migrateBtn";
            this.migrateBtn.Size = new System.Drawing.Size(103, 20);
            this.migrateBtn.TabIndex = 5;
            this.migrateBtn.Text = "Migrate!";
            this.migrateBtn.UseVisualStyleBackColor = true;
            this.migrateBtn.Click += new System.EventHandler(this.migrateBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Number of Bees:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 559);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Number of Sites:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(301, 559);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(211, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // maskBeesChkBox
            // 
            this.maskBeesChkBox.AutoSize = true;
            this.maskBeesChkBox.Location = new System.Drawing.Point(189, 532);
            this.maskBeesChkBox.Name = "maskBeesChkBox";
            this.maskBeesChkBox.Size = new System.Drawing.Size(79, 17);
            this.maskBeesChkBox.TabIndex = 9;
            this.maskBeesChkBox.Text = "Mask Bees";
            this.maskBeesChkBox.UseVisualStyleBackColor = true;
            // 
            // maskSitesChkBox
            // 
            this.maskSitesChkBox.AutoSize = true;
            this.maskSitesChkBox.Location = new System.Drawing.Point(189, 558);
            this.maskSitesChkBox.Name = "maskSitesChkBox";
            this.maskSitesChkBox.Size = new System.Drawing.Size(78, 17);
            this.maskSitesChkBox.TabIndex = 10;
            this.maskSitesChkBox.Text = "Mask Sites";
            this.maskSitesChkBox.UseVisualStyleBackColor = true;
            // 
            // activityPicture
            // 
            this.activityPicture.BackColor = System.Drawing.SystemColors.Window;
            this.activityPicture.Location = new System.Drawing.Point(12, 12);
            this.activityPicture.Name = "activityPicture";
            this.activityPicture.Size = new System.Drawing.Size(500, 500);
            this.activityPicture.TabIndex = 11;
            this.activityPicture.TabStop = false;
            // 
            // showExploration
            // 
            this.showExploration.AutoSize = true;
            this.showExploration.Checked = true;
            this.showExploration.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showExploration.Location = new System.Drawing.Point(189, 581);
            this.showExploration.Name = "showExploration";
            this.showExploration.Size = new System.Drawing.Size(122, 17);
            this.showExploration.TabIndex = 12;
            this.showExploration.Text = "Show Explored Area";
            this.showExploration.UseVisualStyleBackColor = true;
            this.showExploration.CheckedChanged += new System.EventHandler(this.showExploration_CheckedChanged);
            // 
            // taskDistribution
            // 
            chartArea3.Name = "ChartArea1";
            this.taskDistribution.ChartAreas.Add(chartArea3);
            this.taskDistribution.Cursor = System.Windows.Forms.Cursors.Arrow;
            legend3.Name = "Legend1";
            this.taskDistribution.Legends.Add(legend3);
            this.taskDistribution.Location = new System.Drawing.Point(544, 12);
            this.taskDistribution.Name = "taskDistribution";
            this.taskDistribution.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.taskDistribution.Series.Add(series3);
            this.taskDistribution.Size = new System.Drawing.Size(453, 500);
            this.taskDistribution.TabIndex = 13;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(189, 604);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Suppress Piping";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // qualityThreshold
            // 
            this.qualityThreshold.Location = new System.Drawing.Point(20, 602);
            this.qualityThreshold.Name = "qualityThreshold";
            this.qualityThreshold.Size = new System.Drawing.Size(106, 45);
            this.qualityThreshold.TabIndex = 17;
            this.qualityThreshold.ValueChanged += new System.EventHandler(this.qualityThreshold_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 585);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Dance Threshold";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(300, 602);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(106, 45);
            this.trackBar1.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(314, 585);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Observing Rate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(425, 585);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Fraction that Rest";
            // 
            // RestingBar
            // 
            this.RestingBar.AccessibleName = "RestingBar";
            this.RestingBar.Location = new System.Drawing.Point(412, 601);
            this.RestingBar.Name = "RestingBar";
            this.RestingBar.Size = new System.Drawing.Size(106, 45);
            this.RestingBar.TabIndex = 23;
            this.RestingBar.Scroll += new System.EventHandler(this.RestingBar_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(561, 519);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 18);
            this.label6.TabIndex = 24;
            this.label6.Text = "Ṙ";
            // 
            // Rdot
            // 
            this.Rdot.Location = new System.Drawing.Point(581, 518);
            this.Rdot.Name = "Rdot";
            this.Rdot.ReadOnly = true;
            this.Rdot.Size = new System.Drawing.Size(49, 20);
            this.Rdot.TabIndex = 29;
            // 
            // Odot
            // 
            this.Odot.Location = new System.Drawing.Point(581, 539);
            this.Odot.Name = "Odot";
            this.Odot.ReadOnly = true;
            this.Odot.Size = new System.Drawing.Size(49, 20);
            this.Odot.TabIndex = 30;
            // 
            // Edot
            // 
            this.Edot.Location = new System.Drawing.Point(581, 560);
            this.Edot.Name = "Edot";
            this.Edot.ReadOnly = true;
            this.Edot.Size = new System.Drawing.Size(49, 20);
            this.Edot.TabIndex = 31;
            // 
            // Adot
            // 
            this.Adot.Location = new System.Drawing.Point(581, 581);
            this.Adot.Name = "Adot";
            this.Adot.ReadOnly = true;
            this.Adot.Size = new System.Drawing.Size(49, 20);
            this.Adot.TabIndex = 32;
            // 
            // Ddot
            // 
            this.Ddot.Location = new System.Drawing.Point(581, 602);
            this.Ddot.Name = "Ddot";
            this.Ddot.ReadOnly = true;
            this.Ddot.Size = new System.Drawing.Size(49, 20);
            this.Ddot.TabIndex = 33;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(561, 603);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 18);
            this.label11.TabIndex = 34;
            this.label11.Text = "Ḋ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(562, 582);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 18);
            this.label12.TabIndex = 35;
            this.label12.Text = "Ȧ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(563, 561);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 18);
            this.label13.TabIndex = 36;
            this.label13.Text = "Ė";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(561, 540);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(21, 18);
            this.label14.TabIndex = 37;
            this.label14.Text = "Ȯ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 675);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Ddot);
            this.Controls.Add(this.Adot);
            this.Controls.Add(this.Edot);
            this.Controls.Add(this.Odot);
            this.Controls.Add(this.Rdot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RestingBar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.qualityThreshold);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.taskDistribution);
            this.Controls.Add(this.showExploration);
            this.Controls.Add(this.activityPicture);
            this.Controls.Add(this.maskSitesChkBox);
            this.Controls.Add(this.maskBeesChkBox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.migrateBtn);
            this.Controls.Add(this.siteNumber);
            this.Controls.Add(this.generateBtn);
            this.Controls.Add(this.beeNumber);
            this.Controls.Add(this.explorationMap);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.form1_load);
            ((System.ComponentModel.ISupportInitialize)(this.explorationMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskDistribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beeDistributionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RestingBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.PictureBox explorationMap;
        private System.Windows.Forms.TextBox beeNumber;
        private System.Windows.Forms.Button generateBtn;
        private System.Windows.Forms.TextBox siteNumber;
        private System.Windows.Forms.Button migrateBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox maskBeesChkBox;
        private System.Windows.Forms.CheckBox maskSitesChkBox;
        private System.Windows.Forms.PictureBox activityPicture;
        private System.Windows.Forms.CheckBox showExploration;
        private System.Windows.Forms.DataVisualization.Charting.Chart taskDistribution;
        private System.Windows.Forms.BindingSource beeDistributionBindingSource;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar qualityThreshold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar RestingBar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Rdot;
        private System.Windows.Forms.TextBox Odot;
        private System.Windows.Forms.TextBox Edot;
        private System.Windows.Forms.TextBox Adot;
        private System.Windows.Forms.TextBox Ddot;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}

