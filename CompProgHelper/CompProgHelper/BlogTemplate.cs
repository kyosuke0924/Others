using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AngleSharp.Html.Parser;
using System.Security.Permissions;

namespace CreateBlogContents
{
    public partial class BlogTemplate : Form
    {
        #region Constructor

        public BlogTemplate()
        {
            InitializeComponent();
            SetSelectAllToTextBox(this);
        }

        #endregion

        #region Field

        string ID;
        Probrem probrem;
        private const string API_AOJ_OLD = "http://judge.u-aizu.ac.jp/onlinejudge/webservice/problem?id={0}&status=false";
        private const string API_AOJ_NEW = "https://judgeapi.u-aizu.ac.jp/resources/descriptions/jp/{0}";

        #endregion

        #region EventHandler

        [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Returnキーが押されているか調べる
            //Altキーが押されている時は、本来の動作をさせる
            if (((keyData & Keys.KeyCode) == Keys.Return) && ((keyData & Keys.Alt) != Keys.Alt))
            {
                bool isMultilineTextbox = (ActiveControl is TextBoxBase) && ((TextBoxBase)ActiveControl).Multiline && !((TextBoxBase)ActiveControl).ReadOnly;
                bool isButton = ActiveControl is ButtonBase;
                bool isMulTxtBoxOrButton = isMultilineTextbox || isButton;
                bool pressingCtrl = (keyData & Keys.Control) == Keys.Control;

                if (isMulTxtBoxOrButton == pressingCtrl)
                {
                    //Tabキーを押した時と同じ動作をさせる。Shiftキーが押されている時は、逆順にする
                    ProcessTabKey((keyData & Keys.Shift) != Keys.Shift);
                    //本来の処理はさせない
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private async void BtnConnect_ClickAsync(object sender, EventArgs e)
        {
            if (txtNo.Text == string.Empty) return;
            ClearDisplayArea();

            probrem = await HttpClientManager.ExecuteGetXMLAsync<Probrem>(string.Format(API_AOJ_OLD, ID));
            probrem.id = probrem.id.Replace("\n", string.Empty);
            probrem.name = probrem.name.Replace("\n", string.Empty);

            var responce2 = await HttpClientManager.ExecuteGetJsonAsync<Probrem2>(string.Format(API_AOJ_NEW, ID));

            var parser = new HtmlParser();
            var doc = parser.ParseDocument(responce2.html);
            txtTitle.Text = string.Format("AOJ {0} （{1} : {2}）", ID, probrem.name, doc.QuerySelectorAll("h1")[0].TextContent);

            string text = "";
            using (StreamReader sr = new StreamReader("ContentsTemplate.txt", Encoding.GetEncoding("UTF-8")))
            {
                text = sr.ReadToEnd();
            }
            txtContents.Text = string.Format(text, ID);
        }

        //数値とバックスペース以外の入力を認めない
        private void TxtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b') e.Handled = true;
        }

        private void txtNo_Leave(object sender, EventArgs e)
        {
             ID = txtNo.Text.PadLeft(4, '0');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNo.Text == string.Empty) return;

            Form form = new TestResult(probrem);
            form.ShowDialog();
        }

        #endregion

        #region Method

        //コントロールがテキストボックスの場合の権限移譲
        public static void SetSelectAllToTextBox(Control hParent)
        {
            foreach (Control control in hParent.Controls)
            {
                if (control.HasChildren == true) SetSelectAllToTextBox(control);
                if (control is TextBoxBase)
                {
                    control.Enter += new EventHandler(SelectAll);
                    control.MouseDown += new MouseEventHandler(SelectAll);
                    control.KeyDown += new KeyEventHandler(SelectAll);
                }             
            }
        }

        private static void SelectAll(object sender, EventArgs e) { ((TextBox)sender).SelectAll(); }
        private static void SelectAll(object sender, KeyEventArgs e) { if (e.Control && e.KeyCode == Keys.A) ((TextBox)sender).SelectAll(); }

        private void ClearDisplayArea()
        {
            txtTitle.Text = "";
            txtContents.Text = "";
        }

        #endregion

    }
}
