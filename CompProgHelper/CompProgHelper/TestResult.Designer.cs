namespace CreateBlogContents
{
    partial class TestResult
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DetailStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsedMemory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Input = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Output = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtError = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JudgeCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessingTimeMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsedMemoryMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMemoryLimit = new System.Windows.Forms.TextBox();
            this.txtTimeLimit = new System.Windows.Forms.TextBox();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dgvDetail);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 220);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 253);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "詳細";
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AllowUserToResizeRows = false;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Detail,
            this.DetailStatus,
            this.ProcessTime,
            this.UsedMemory,
            this.Input,
            this.Output,
            this.CaseName});
            this.dgvDetail.Location = new System.Drawing.Point(14, 18);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowTemplate.Height = 21;
            this.dgvDetail.Size = new System.Drawing.Size(683, 223);
            this.dgvDetail.TabIndex = 1;
            this.dgvDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDetail_CellContentClick);
            // 
            // No
            // 
            this.No.Frozen = true;
            this.No.HeaderText = "No.";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.No.Width = 50;
            // 
            // Detail
            // 
            this.Detail.Frozen = true;
            this.Detail.HeaderText = "詳細";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            this.Detail.Width = 50;
            // 
            // DetailStatus
            // 
            this.DetailStatus.Frozen = true;
            this.DetailStatus.HeaderText = "結果";
            this.DetailStatus.Name = "DetailStatus";
            this.DetailStatus.ReadOnly = true;
            this.DetailStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailStatus.Width = 60;
            // 
            // ProcessTime
            // 
            this.ProcessTime.Frozen = true;
            this.ProcessTime.HeaderText = "処理時間";
            this.ProcessTime.Name = "ProcessTime";
            this.ProcessTime.ReadOnly = true;
            this.ProcessTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProcessTime.Width = 80;
            // 
            // UsedMemory
            // 
            this.UsedMemory.Frozen = true;
            this.UsedMemory.HeaderText = "使用メモリ";
            this.UsedMemory.Name = "UsedMemory";
            this.UsedMemory.ReadOnly = true;
            this.UsedMemory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UsedMemory.Width = 80;
            // 
            // Input
            // 
            this.Input.Frozen = true;
            this.Input.HeaderText = "入力";
            this.Input.Name = "Input";
            this.Input.ReadOnly = true;
            this.Input.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Input.Width = 80;
            // 
            // Output
            // 
            this.Output.HeaderText = "出力";
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Output.Width = 80;
            // 
            // CaseName
            // 
            this.CaseName.HeaderText = "ケース名";
            this.CaseName.Name = "CaseName";
            this.CaseName.ReadOnly = true;
            this.CaseName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CaseName.Width = 200;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtError);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.dgvResult);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(709, 188);
            this.panel2.TabIndex = 4;
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(12, 77);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(672, 105);
            this.txtError.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "エラー";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.JudgeCnt,
            this.ProcessingTimeMax,
            this.UsedMemoryMax});
            this.dgvResult.Location = new System.Drawing.Point(12, 18);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowTemplate.Height = 21;
            this.dgvResult.Size = new System.Drawing.Size(313, 41);
            this.dgvResult.TabIndex = 7;
            this.dgvResult.SelectionChanged += new System.EventHandler(this.DgvResult_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "結果";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // JudgeCnt
            // 
            this.JudgeCnt.HeaderText = "判定数";
            this.JudgeCnt.Name = "JudgeCnt";
            this.JudgeCnt.ReadOnly = true;
            this.JudgeCnt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProcessingTimeMax
            // 
            this.ProcessingTimeMax.HeaderText = "処理時間";
            this.ProcessingTimeMax.Name = "ProcessingTimeMax";
            this.ProcessingTimeMax.ReadOnly = true;
            this.ProcessingTimeMax.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProcessingTimeMax.Width = 80;
            // 
            // UsedMemoryMax
            // 
            this.UsedMemoryMax.HeaderText = "使用メモリ";
            this.UsedMemoryMax.Name = "UsedMemoryMax";
            this.UsedMemoryMax.ReadOnly = true;
            this.UsedMemoryMax.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UsedMemoryMax.Width = 80;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "結果";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtMemoryLimit);
            this.panel3.Controls.Add(this.txtTimeLimit);
            this.panel3.Controls.Add(this.txtNo);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(709, 32);
            this.panel3.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(474, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "KB";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(260, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Sec";
            // 
            // txtMemoryLimit
            // 
            this.txtMemoryLimit.Location = new System.Drawing.Point(401, 6);
            this.txtMemoryLimit.Name = "txtMemoryLimit";
            this.txtMemoryLimit.ReadOnly = true;
            this.txtMemoryLimit.Size = new System.Drawing.Size(67, 19);
            this.txtMemoryLimit.TabIndex = 5;
            // 
            // txtTimeLimit
            // 
            this.txtTimeLimit.Location = new System.Drawing.Point(187, 6);
            this.txtTimeLimit.Name = "txtTimeLimit";
            this.txtTimeLimit.ReadOnly = true;
            this.txtTimeLimit.Size = new System.Drawing.Size(67, 19);
            this.txtTimeLimit.TabIndex = 4;
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(39, 6);
            this.txtNo.Name = "txtNo";
            this.txtNo.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(57, 19);
            this.txtNo.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(325, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "MemoryLimit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "TimeLimit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "No.";
            // 
            // TestResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.Name = "TestResult";
            this.Text = "テスト結果";
            this.Shown += new System.EventHandler(this.TestResult_ShownAsync);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMemoryLimit;
        private System.Windows.Forms.TextBox txtTimeLimit;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn JudgeCnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessingTimeMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsedMemoryMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsedMemory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Input;
        private System.Windows.Forms.DataGridViewTextBoxColumn Output;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaseName;
    }
}