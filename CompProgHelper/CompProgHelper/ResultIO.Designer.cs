namespace CreateBlogContents
{
    partial class ResultIO
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.cInput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvOutput = new System.Windows.Forms.DataGridView();
            this.cOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cExpect = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "入力";
            // 
            // dgvInput
            // 
            this.dgvInput.AllowUserToAddRows = false;
            this.dgvInput.AllowUserToDeleteRows = false;
            this.dgvInput.AllowUserToResizeRows = false;
            this.dgvInput.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInput.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cInput});
            this.dgvInput.Location = new System.Drawing.Point(14, 25);
            this.dgvInput.Margin = new System.Windows.Forms.Padding(0);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.ReadOnly = true;
            this.dgvInput.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInput.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvInput.RowTemplate.Height = 18;
            this.dgvInput.Size = new System.Drawing.Size(250, 413);
            this.dgvInput.TabIndex = 4;
            // 
            // cInput
            // 
            this.cInput.HeaderText = "入力";
            this.cInput.MaxInputLength = 0;
            this.cInput.Name = "cInput";
            this.cInput.ReadOnly = true;
            this.cInput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cInput.Width = 200;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "出力";
            // 
            // dgvOutput
            // 
            this.dgvOutput.AllowUserToAddRows = false;
            this.dgvOutput.AllowUserToDeleteRows = false;
            this.dgvOutput.AllowUserToResizeRows = false;
            this.dgvOutput.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOutput.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cOutput,
            this.cExpect});
            this.dgvOutput.Location = new System.Drawing.Point(283, 25);
            this.dgvOutput.Margin = new System.Windows.Forms.Padding(0);
            this.dgvOutput.Name = "dgvOutput";
            this.dgvOutput.ReadOnly = true;
            this.dgvOutput.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOutput.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOutput.RowTemplate.Height = 18;
            this.dgvOutput.Size = new System.Drawing.Size(508, 413);
            this.dgvOutput.TabIndex = 6;
            // 
            // cOutput
            // 
            this.cOutput.HeaderText = "出力";
            this.cOutput.MaxInputLength = 0;
            this.cOutput.Name = "cOutput";
            this.cOutput.ReadOnly = true;
            this.cOutput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cOutput.Width = 200;
            // 
            // cExpect
            // 
            this.cExpect.HeaderText = "期待値";
            this.cExpect.MaxInputLength = 0;
            this.cExpect.Name = "cExpect";
            this.cExpect.ReadOnly = true;
            this.cExpect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cExpect.Width = 200;
            // 
            // ResultIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvInput);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "ResultIO";
            this.Text = "ResultIO";
            this.Shown += new System.EventHandler(this.ResultIO_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn cInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn cExpect;
    }
}