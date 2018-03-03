namespace TextAnalyzer
{
    partial class frmAnalyzer
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnGetDistinctValues = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.chtReport = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.minCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.chtReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetDistinctValues
            // 
            this.btnGetDistinctValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetDistinctValues.Location = new System.Drawing.Point(470, 197);
            this.btnGetDistinctValues.Name = "btnGetDistinctValues";
            this.btnGetDistinctValues.Size = new System.Drawing.Size(179, 41);
            this.btnGetDistinctValues.TabIndex = 2;
            this.btnGetDistinctValues.Text = "Analyze Text";
            this.btnGetDistinctValues.UseVisualStyleBackColor = true;
            this.btnGetDistinctValues.Click += new System.EventHandler(this.btnGetDistinctValues_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Text To Analyze";
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(12, 45);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(441, 448);
            this.txtInput.TabIndex = 0;
            this.txtInput.Text = "";
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // chtReport
            // 
            chartArea2.AxisX.Interval = 1D;
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.Name = "ChartArea1";
            this.chtReport.ChartAreas.Add(chartArea2);
            this.chtReport.Location = new System.Drawing.Point(666, 12);
            this.chtReport.Name = "chtReport";
            series2.ChartArea = "ChartArea1";
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.IsValueShownAsLabel = true;
            series2.IsVisibleInLegend = false;
            series2.Name = "Series1";
            this.chtReport.Series.Add(series2);
            this.chtReport.Size = new System.Drawing.Size(492, 481);
            this.chtReport.TabIndex = 3;
            this.chtReport.Text = "chart1";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "Word Counts";
            this.chtReport.Titles.Add(title2);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(284, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Min Count";
            // 
            // minCount
            // 
            this.minCount.Location = new System.Drawing.Point(386, 17);
            this.minCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minCount.Name = "minCount";
            this.minCount.Size = new System.Drawing.Size(67, 20);
            this.minCount.TabIndex = 1;
            this.minCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.minCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minCount.ValueChanged += new System.EventHandler(this.minCount_ValueChanged);
            // 
            // frmAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 505);
            this.Controls.Add(this.minCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chtReport);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetDistinctValues);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAnalyzer";
            this.Text = "Text Analyzer";
            this.Load += new System.EventHandler(this.frmAnalyzer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chtReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetDistinctValues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtInput;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtReport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown minCount;
    }
}

