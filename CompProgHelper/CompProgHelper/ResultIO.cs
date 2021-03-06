﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateBlogContents
{
    public partial class ResultIO : Form
    {

        public ResultIO(string input, string output, string expected)
        {
            InitializeComponent();
            this.input = input.Replace("\n", Environment.NewLine).Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            this.output = output is null ? new string[] { "" } : output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            this.expected = expected.Replace("\n", Environment.NewLine).Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            //データグリッドビューのデリゲート
            SetSelectAllToDataGridView(this);
        }

        private string[] input;
        private string[] output;
        private string[] expected;
        private const int MAX_COL_LEN = 2048;

        //コントロールがデータグリッドの場合の権限移譲
        public static void SetSelectAllToDataGridView(Control hParent)
        {
            foreach (Control control in hParent.Controls)
            {
                if (control.HasChildren == true) SetSelectAllToDataGridView(control);
                if (control is DataGridView)
                {
                    ((DataGridView)control).CellPainting += new DataGridViewCellPaintingEventHandler(Dgv_CellPainting);
                    ((DataGridView)control).ColumnWidthChanged += new DataGridViewColumnEventHandler(Dgv_ColumnWidthChanged);
                    EnableDoubleBuffering(control);
                    ((DataGridView)control).EnableHeadersVisualStyles = false;
                }
            }
        }

        private void ResultIO_Shown(object sender, EventArgs e)
        {
            DataGridViewRow[] rowsInput = new DataGridViewRow[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvInput, input[i]);
                row.HeaderCell.Value = (i + 1).ToString();
                row.HeaderCell.Style.BackColor = SystemColors.Control;
                row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Height = 18;
                rowsInput[i] = row;
            }
            dgvInput.Rows.AddRange(rowsInput);

            dgvInput.RowHeadersWidth = dgvInput.Rows[input.Length - 1].HeaderCell.ContentBounds.Width + 40;
            dgvInput.Columns[0].Width = Math.Min(MAX_COL_LEN, dgvInput.Columns[0].GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true)) + 50;

            int maxRow = Math.Max(output.Length, expected.Length);
            DataGridViewRow[] rowsOutput = new DataGridViewRow[maxRow];
            for (int i = 0; i < maxRow; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvOutput);
                if (i < output.Length) row.Cells[0].Value = output[i];
                if (i < expected.Length) row.Cells[1].Value = expected[i];

                if (i >= output.Length || i >= expected.Length || output[i] != expected[i])
                {
                    foreach (DataGridViewCell item in row.Cells) item.Style.BackColor = CommonColor.NG_BKG;
                }

                row.HeaderCell.Value = (i + 1).ToString();
                row.HeaderCell.Style.BackColor = SystemColors.Control;
                row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Height = 18;
                rowsOutput[i] = row;
            }
            dgvOutput.Rows.AddRange(rowsOutput);

            dgvOutput.RowHeadersWidth = dgvOutput.Rows[maxRow - 1].HeaderCell.ContentBounds.Width + 40;
            dgvOutput.Columns[0].Width = Math.Min(MAX_COL_LEN, dgvOutput.Columns[0].GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true)) + 50;
            dgvOutput.Columns[1].Width = Math.Min(MAX_COL_LEN, dgvOutput.Columns[1].GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true)) + 50;
        }

        public static void EnableDoubleBuffering(Control control)
        {
            control.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, control, new object[] { true });
        }

        private static void Dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.Value == null) return;
                if (e.ColumnIndex > -1) // セル
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                }
                else //ヘッダー
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground & ~DataGridViewPaintParts.ContentBackground);
                }

                Color foreColor = new Color();
                if ((e.State & DataGridViewElementStates.Selected) != 0) foreColor = e.CellStyle.SelectionForeColor;
                else foreColor = e.CellStyle.ForeColor;

                using (var foreColorBrush = new SolidBrush(foreColor))
                {
                    StringFormat StrFormat = new StringFormat { Trimming = StringTrimming.EllipsisCharacter };
                    StrFormat.FormatFlags = StrFormat.FormatFlags | StringFormatFlags.NoWrap;

                    switch (e.CellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.TopLeft:
                            StrFormat.Alignment = StringAlignment.Near;
                            StrFormat.LineAlignment = StringAlignment.Near;
                            break;
                        case DataGridViewContentAlignment.TopCenter:
                            StrFormat.Alignment = StringAlignment.Center;
                            StrFormat.LineAlignment = StringAlignment.Near;
                            break;
                        case DataGridViewContentAlignment.TopRight:
                            StrFormat.Alignment = StringAlignment.Far;
                            StrFormat.LineAlignment = StringAlignment.Near;
                            break;
                        case DataGridViewContentAlignment.MiddleLeft:
                            StrFormat.Alignment = StringAlignment.Near;
                            StrFormat.LineAlignment = StringAlignment.Center;
                            break;
                        case DataGridViewContentAlignment.MiddleCenter:
                            StrFormat.Alignment = StringAlignment.Center;
                            StrFormat.LineAlignment = StringAlignment.Center;
                            break;
                        case DataGridViewContentAlignment.MiddleRight:
                            StrFormat.Alignment = StringAlignment.Far;
                            StrFormat.LineAlignment = StringAlignment.Center;
                            break;
                        case DataGridViewContentAlignment.BottomLeft:
                            StrFormat.Alignment = StringAlignment.Near;
                            StrFormat.LineAlignment = StringAlignment.Far;
                            break;
                        case DataGridViewContentAlignment.BottomCenter:
                            StrFormat.Alignment = StringAlignment.Center;
                            StrFormat.LineAlignment = StringAlignment.Far;
                            break;
                        case DataGridViewContentAlignment.BottomRight:
                            StrFormat.Alignment = StringAlignment.Far;
                            StrFormat.LineAlignment = StringAlignment.Far;
                            break;
                        default:
                            break;
                    }

                    Rectangle newRect = new Rectangle(
                                            e.CellBounds.X + e.CellStyle.Padding.Left,
                                            e.CellBounds.Y + e.CellStyle.Padding.Top + 2,
                                            e.CellBounds.Width - e.CellStyle.Padding.Horizontal - 2,
                                            e.CellBounds.Height - e.CellStyle.Padding.Vertical - 3);

                    string renderText = System.Convert.ToString(e.Value);
                    int charCnt = Math.Min(30000, renderText.Length);

                    if (charCnt > 0) renderText = renderText.Substring(0, charCnt);
                    e.Graphics.DrawString(renderText, e.CellStyle.Font, foreColorBrush, newRect, StrFormat);
                }
                e.Handled = true;
            }
        }

        private static void Dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.Width > MAX_COL_LEN) e.Column.Width = MAX_COL_LEN;
        }

    }
}
