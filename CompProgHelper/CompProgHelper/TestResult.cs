using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CreateBlogContents
{
    public partial class TestResult : Form
    {
        public TestResult(Probrem probrem)
        {
            this.probrem = probrem;
            InitializeComponent();
        }

        #region Field

        private Probrem probrem;
        private TestcaseHeaders testcaseHeaders;
        private Testcase[] testcases;
        private Result[] results;

        private const string FIND_BY_PROBLEM_ID_TESTCASE_HEADER = "https://judgedat.u-aizu.ac.jp/testcases/{0}/header";
        private const string FIND_BY_PROBLEM_ID_TESTCASE = "https://judgedat.u-aizu.ac.jp/testcases/{0}/{1}";
        private const string FIND_BY_PROBLEM_ID_TESTCASE_ALT = "https://judgedat.u-aizu.ac.jp/testcases/{0}/{1}/in";

        string[] dlls = new string[]
                            {
                                 "Microsoft.CSharp.dll"
                                ,"System.dll"
                                ,"System.Core.dll"
                                ,"System.Data.dll"
                                ,"System.Data.DataSetExtensions.dll"
                                ,"System.Xml.dll"
                                ,"System.Xml.Linq.dll"
                            };

        Process process;


        enum DgvResultRow
        {
            Result,
            JudgeCnt,
            ProcessTime,
            UsedMemory,
        }

        enum DgvDetailRow
        {
            No,
            Detail,
            DetailStatus,
            ProcessTime,
            UsedMemory,
            InputSize,
            OutputSize,
            SampleName
        }

        #endregion

        private static bool CustomContains(string source, string toCheck)
        {
            CompareInfo ci = new CultureInfo("en-US").CompareInfo;
            CompareOptions co = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace;
            return ci.IndexOf(source, toCheck, co) != -1;
        }

        private async void TestResult_ShownAsync(object sender, EventArgs e)
        {
            txtNo.Text = probrem.id;
            txtTimeLimit.Text = probrem.problemtimelimit.ToString();
            txtMemoryLimit.Text = probrem.problemmemorylimit.ToString();

            dgvResult.Rows.Clear();
            dgvDetail.Rows.Clear();

            dgvResult.Rows.Add();
            dgvResult.CurrentCell = null;

            testcaseHeaders = await HttpClientManager.ExecuteGetJsonAsync<TestcaseHeaders>(string.Format(FIND_BY_PROBLEM_ID_TESTCASE_HEADER, probrem.id));
            testcases = new Testcase[testcaseHeaders.Headers.Length];
            results = new Result[testcaseHeaders.Headers.Length];
            for (int i = 0; i < testcaseHeaders.Headers.Length; i++)
            {
                dgvDetail.Rows.Add();
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.No].Value = testcaseHeaders.Headers[i].Serial;
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.Detail].Value = "";
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.InputSize].Value = testcaseHeaders.Headers[i].InputSize + " B";
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.OutputSize].Value = testcaseHeaders.Headers[i].OutputSize + " B";
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.SampleName].Value = testcaseHeaders.Headers[i].Name;
            }
            dgvDetail.CurrentCell = null;

            //コンパイル
            if (!CompileTarget()) return;

            var startInfo = new ProcessStartInfo("test.exe")
            {
                WorkingDirectory = Environment.CurrentDirectory,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            for (int i = 0; i < testcaseHeaders.Headers.Length; i++)
            {
                testcases[i] = await GetTestcaseAsync(testcaseHeaders.Headers[i].Serial);
                results[i] = ExecTest(startInfo, testcases[i]);
                PrintDgvDetail(i, results[i]);
                if (results[i].Status != "AC") break;
            }

            //結果表示
            PrintDgvResult();

        }

        private bool CompileTarget()
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters(dlls);
            parameters.OutputAssembly = "test.exe";
            parameters.GenerateExecutable = true;

            string source = "";
            using (StreamReader sr = new StreamReader(string.Format("D:/GitHub/Competitive-Programming/AOJ/PCK/{0}/{0}/Program.cs", probrem.id), Encoding.GetEncoding("UTF-8")))
            {
                source = sr.ReadToEnd();
            }
            var CompilerResults = provider.CompileAssemblyFromSource(parameters, source);
            if (CompilerResults.NativeCompilerReturnValue != 0)
            {
                dgvResult.Rows[0].Cells[0].Value = "CE";
                txtError.Text = CompilerResults.Errors[0].ErrorNumber + " : " + CompilerResults.Errors[0].ErrorText;
                return false;
            }
            return true;
        }

        private async Task<Testcase> GetTestcaseAsync(int serial)
        {
            Testcase testcase = new Testcase();
            testcase = await HttpClientManager.ExecuteGetJsonAsync<Testcase>(string.Format(FIND_BY_PROBLEM_ID_TESTCASE, probrem.id, serial));
            if (testcase.In.Contains("(terminated because of the limitation)"))
            {
                testcase.In = await HttpClientManager.ExecuteGetStringAsync(string.Format(FIND_BY_PROBLEM_ID_TESTCASE_ALT, probrem.id, serial));
            }
            return testcase;
        }

        private Result ExecTest(ProcessStartInfo startInfo, Testcase testcase)
        {
            var result = new Result();
            var timeout = TimeSpan.FromSeconds(probrem.problemtimelimit * 2);

            bool isTimedOut = false;
            bool isMemoryExceeded = false;
            long memory = 0;
            TimeSpan time = new TimeSpan();

            var stdout = new StringBuilder();
            var stderr = new StringBuilder();

            using (process = Process.Start(startInfo))
            {
                process.OutputDataReceived += (ss, ee) => { if (ee.Data != null) { stdout.AppendLine(ee.Data); } }; // 標準出力に書き込まれた文字列を取り出す
                process.ErrorDataReceived += (ss, ee) => { if (ee.Data != null) { stderr.AppendLine(ee.Data); } }; // 標準エラー出力に書き込まれた文字列を取り出す
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.StandardInput.WriteLine(testcase.In.Replace("\n", "\r\n"));
                do
                {
                    try
                    {
                        process.Refresh();
                        time = DateTime.Now - process.StartTime;
                        memory = Math.Max(memory, process.PeakWorkingSet64);
                    }
                    catch (InvalidOperationException)
                    { } //プロセスが終了している場合、何もしない
                    catch (ArgumentException)
                    { } //プロセスが開かれていない場合も、何もしない

                    if (memory > probrem.problemmemorylimit * 1024) isMemoryExceeded = true;
                    if (time.TotalMilliseconds > timeout.TotalMilliseconds) isTimedOut = true;

                    if (isMemoryExceeded || isTimedOut)
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch (Win32Exception)
                        { } //プロセスが終了中の場合、何もしない
                    }

                } while (!process.HasExited);

                process.WaitForExit();
                process.CancelOutputRead();
                process.CancelErrorRead();

                result.ProcessingTime = time;
                result.UsedMemory = memory;
            }

            string res;
            if (isMemoryExceeded || isTimedOut)
            {
                if (isMemoryExceeded) res = "MLE";
                else res = "TLE";
            }
            else
            {
                var errString = stderr.ToString();
                if (!string.IsNullOrEmpty(errString))
                {
                    res = "RE";
                    txtError.Text = errString;
                }
                else
                {
                    var resultString = stdout.ToString();
                    result.Output = resultString;
                    if (resultString == testcase.Out.Replace("\n", "\r\n"))//改行の統一
                    {
                        res = "AC";
                    }
                    else
                    {
                        res = "WA";
                    }
                }
            }
            result.Status = res;
            return result;
        }

        private void PrintDgvDetail(int i, Result result)
        {
            dgvDetail.Rows[i].Cells[(int)DgvDetailRow.ProcessTime].Value = result.ProcessingTime.TotalSeconds.ToString("F2") + " Sec";
            dgvDetail.Rows[i].Cells[(int)DgvDetailRow.UsedMemory].Value = result.UsedMemory / 1024 + " KB";
            dgvDetail.Rows[i].Cells[(int)DgvDetailRow.DetailStatus].Value = result.Status;
            dgvDetail.Rows[i].Cells[(int)DgvDetailRow.DetailStatus].Style.BackColor = result.Status == "AC" ? CommonColor.OK_BKG : CommonColor.NG_BKG;
            dgvDetail.Rows[i].Cells[(int)DgvDetailRow.DetailStatus].Style.ForeColor = result.Status == "AC" ? CommonColor.OK_FRT : CommonColor.NG_FRT;

        }

        private void PrintDgvResult()
        {
            double maxProcessingTime = 0;
            int maxUsedMemory = 0;
            string status = "";
            int cnt = 0;

            for (int i = dgvDetail.RowCount - 1; i >= 0; i--)
            {
                if (!(dgvDetail.Rows[i].Cells[(int)DgvDetailRow.DetailStatus].Value is null))
                {
                    cnt++;
                    if (status == string.Empty) status = dgvDetail.Rows[i].Cells[(int)DgvDetailRow.DetailStatus].Value.ToString();
                    maxProcessingTime = Math.Max(maxProcessingTime, double.Parse(dgvDetail.Rows[i].Cells[(int)DgvDetailRow.ProcessTime].Value.ToString().Replace(" Sec", "")));
                    maxUsedMemory = Math.Max(maxUsedMemory, int.Parse(dgvDetail.Rows[i].Cells[(int)DgvDetailRow.UsedMemory].Value.ToString().Replace(" KB", "")));
                }
            }
            dgvResult.Rows[0].Cells[(int)DgvResultRow.Result].Value = status;
            foreach (DataGridViewCell item in dgvResult.Rows[0].Cells)
            {
                item.Style.BackColor = status == "AC" ? CommonColor.OK_BKG : CommonColor.NG_FRT;
                item.Style.ForeColor = status == "AC" ? CommonColor.OK_FRT : CommonColor.NG_FRT;
            }

            dgvResult.Rows[0].Cells[(int)DgvResultRow.JudgeCnt].Value = cnt + " / " + testcaseHeaders.Headers.Count();
            dgvResult.Rows[0].Cells[(int)DgvResultRow.ProcessTime].Value = maxProcessingTime + " Sec";
            dgvResult.Rows[0].Cells[(int)DgvResultRow.UsedMemory].Value = maxUsedMemory + " KB";
        }

        private void DgvResult_SelectionChanged(object sender, EventArgs e)
        {
            dgvResult.Rows[dgvResult.CurrentCell.RowIndex].Selected = false;
        }

        private void DgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)DgvDetailRow.Detail)
            {
                Form form = new ResultIO(testcases[e.RowIndex].In, results[e.RowIndex].Output, testcases[e.RowIndex].Out);
                form.ShowDialog();
            }
        }

        private class Result
        {
            public int Serial { get; set; }
            public string Status { get; set; }
            public string Output { get; set; }
            public TimeSpan ProcessingTime { get; set; }
            public long UsedMemory { get; set; }
        }

    }
}
