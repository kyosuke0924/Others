using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public string Input { get; set; }
        public string OutPut { get; set; }
        public string Expected { get; set; }

        List<string> input;

        public MainWindow(string input, string output, string expected)
        {
            InitializeComponent();
            this.Input = input;
            this.OutPut = output;
            this.Expected = expected;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

            input = Input.Replace("\n", Environment.NewLine).Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();

          
            DataTable m_dt = new DataTable("DataGridTest");
            m_dt.Columns.Add(new DataColumn("test", typeof(string)));// 文字列

            DataRow newRowItem;
            for (int i = 0; i < input.Count; i++)
            {
                newRowItem = m_dt.NewRow();
                newRowItem["test"] = input[i];
                m_dt.Rows.Add(newRowItem);
            }

            StringFormat StrFormat = new StringFormat();
            StrFormat.Trimming = StringTrimming.EllipsisCharacter;
            StrFormat.FormatFlags = StrFormat.FormatFlags | StringFormatFlags.NoWrap;

            dataGrid.DataContext = m_dt;

      
            // 水平スクロールバー
            dataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            // 垂直スクロールバー
            dataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

        }
    }
}
