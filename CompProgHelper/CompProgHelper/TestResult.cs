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
        public TestResult(Probrem probrem, bool isSample)
        {
            this.probrem = probrem;
            this.isSample = isSample;
            InitializeComponent();
        }

        #region Field

        private bool isSample;
        private Probrem probrem;
        private TestcaseHeader testcaseHeader;
        private Testcase[] testcases;
        private Sample[] samples;
        private Result[] results;

        private const string FIND_BY_PROBLEM_ID_TESTCASE_HEADER = "https://judgedat.u-aizu.ac.jp/testcases/{0}/header";
        private const string FIND_BY_PROBLEM_ID_TESTCASE = "https://judgedat.u-aizu.ac.jp/testcases/{0}/{1}";
        private const string FIND_BY_PROBLEM_ID_TESTCASE_ALT_IN = "https://judgedat.u-aizu.ac.jp/testcases/{0}/{1}/in";
        private const string FIND_BY_PROBLEM_ID_TESTCASE_ALT_OUT = "https://judgedat.u-aizu.ac.jp/testcases/{0}/{1}/out";
        private const string FIND_BY_PROBLEM_ID_SAMPLES = "https://judgedat.u-aizu.ac.jp/testcases/samples/{0}";
        private const string TEST_TARGET_FILENAME = "test.exe";

        private enum DgvResultRow
        {
            Result,
            JudgeCnt,
            ProcessTime,
            UsedMemory,
        }

        private enum DgvDetailRow
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

        private void TestResult_ShownAsync(object sender, EventArgs e)
        {
            Execute();
        }

        private void btnReTest_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private async void Execute()
        {

            //初期化
            InitForm();

            //ヘッダー情報取得 
            if (testcaseHeader == null) testcaseHeader = await GetTestcaseHeaderAsync();
            PrintTestCaseHeader(testcaseHeader);
            results = new Result[testcaseHeader.Headers.Length];

            //コンパイル
            if (!CompileTarget()) return;

            var startInfo = new ProcessStartInfo(TEST_TARGET_FILENAME)
            {
                WorkingDirectory = Environment.CurrentDirectory,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            testcases = new Testcase[testcaseHeader.Headers.Length];
            for (int i = 0; i < testcaseHeader.Headers.Length; i++)
            {
                testcases[i] = await GetTestcaseAsync(i, testcaseHeader.Headers[i].Serial);
                results[i] = ExecTest(startInfo, testcases[i]);
                PrintDgvDetail(i, results[i]);
                if (results[i].Status != "AC") break;
            }

            //結果表示
            PrintDgvResult();

        }

        private void InitForm()
        {
            txtNo.Text = probrem.id;
            txtTimeLimit.Text = probrem.problemtimelimit.ToString();
            txtMemoryLimit.Text = probrem.problemmemorylimit.ToString();

            dgvResult.Rows.Clear();
            dgvDetail.Rows.Clear();

            dgvResult.Rows.Add();
            dgvResult.CurrentCell = null;
        }

        private async Task<TestcaseHeader> GetTestcaseHeaderAsync()
        {
            testcaseHeader = new TestcaseHeader();
            if (isSample)
            {
                samples = await HttpClientManager.ExecuteGetJsonAsync<Sample[]>(string.Format(FIND_BY_PROBLEM_ID_SAMPLES, probrem.id));
                return ConvertSamplesToTestcaseHeader();
            }
            else
            {
                return await HttpClientManager.ExecuteGetJsonAsync<TestcaseHeader>(string.Format(FIND_BY_PROBLEM_ID_TESTCASE_HEADER, probrem.id));
            }
        }

        private TestcaseHeader ConvertSamplesToTestcaseHeader()
        {
            TestcaseHeader tmp = new TestcaseHeader
            {
                ProblemId = samples[0].ProblemId,
                Headers = new TestcaseHeader.Info[samples.Length]
            };
            for (int i = 0; i < samples.Length; i++)
            {
                tmp.Headers[i] = new TestcaseHeader.Info
                {
                    Serial = samples[i].Serial,
                    Name = string.Format("samples_{0}", samples[i].Serial.ToString("00")),
                    InputSize = Encoding.GetEncoding("Shift_JIS").GetByteCount(samples[i].In),
                    OutputSize = Encoding.GetEncoding("Shift_JIS").GetByteCount(samples[i].Out),
                    Score = -1
                };
            }
            return tmp;
        }

        private void PrintTestCaseHeader(TestcaseHeader testcaseHeader)
        {
            for (int i = 0; i < testcaseHeader.Headers.Length; i++)
            {
                dgvDetail.Rows.Add();
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.No].Value = testcaseHeader.Headers[i].Serial;
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.Detail].Value = "";
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.InputSize].Value = testcaseHeader.Headers[i].InputSize + " B";
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.OutputSize].Value = testcaseHeader.Headers[i].OutputSize + " B";
                dgvDetail.Rows[i].Cells[(int)DgvDetailRow.SampleName].Value = testcaseHeader.Headers[i].Name;
            }
            dgvDetail.CurrentCell = null;
        }

        private bool CompileTarget()
        {

            string[] dlls = new string[]
            {  "Microsoft.CSharp.dll","System.dll","System.Core.dll" ,"System.Data.dll" ,"System.Data.DataSetExtensions.dll","System.Xml.dll","System.Xml.Linq.dll"};

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters(dlls);
            parameters.OutputAssembly = TEST_TARGET_FILENAME;
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

        private async Task<Testcase> GetTestcaseAsync(int i, int serial)
        {
            Testcase testcase = new Testcase();
            if (isSample)
            {
                testcase.Serial = samples[i].Serial;
                testcase.ProblemId = samples[i].ProblemId;
                testcase.In = samples[i].In;
                testcase.Out = samples[i].Out;
            }
            else
            {
                testcase = await HttpClientManager.ExecuteGetJsonAsync<Testcase>(string.Format(FIND_BY_PROBLEM_ID_TESTCASE, probrem.id, serial));
                if (testcase.In.Contains("(terminated because of the limitation)"))
                {
                    testcase.In = await HttpClientManager.ExecuteGetStringAsync(string.Format(FIND_BY_PROBLEM_ID_TESTCASE_ALT_IN, probrem.id, serial));
                }
                if (testcase.Out.Contains("(terminated because of the limitation)"))
                {
                    testcase.Out = await HttpClientManager.ExecuteGetStringAsync(string.Format(FIND_BY_PROBLEM_ID_TESTCASE_ALT_OUT, probrem.id, serial));
                }
            }
            return testcase;
        }

        private Result ExecTest(ProcessStartInfo startInfo, Testcase testcase)
        {
            var result = new Result();
            var timeout = TimeSpan.FromSeconds(probrem.problemtimelimit * 4);

            bool isTimedOut = false;
            bool isMemoryExceeded = false;
            bool isError = false;
            long memory = 0;
            TimeSpan time = new TimeSpan();

            var stdout = new StringBuilder();
            var stderr = new StringBuilder();
            string text = testcase.In.Replace("\n", "\r\n");
            //string[] text = testcase.In.Replace("\n", "\r\n").Split(new string[] { "\r\n" }, StringSplitOptions.None);
            using (Process process = Process.Start(startInfo))
            {
                process.OutputDataReceived += (ss, ee) => { if (ee.Data != null) { stdout.AppendLine(ee.Data); } }; // 標準出力に書き込まれた文字列を取り出す
                process.ErrorDataReceived += (ss, ee) => { if (ee.Data != null) { stderr.AppendLine(ee.Data); } }; // 標準エラー出力に書き込まれた文字列を取り出す
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                //process.StandardInput.WriteLine(text); タイムアウトする処理の場合、受け側で読み込みが開始せず遅延するため、非同期処理に変更
                process.StandardInput.WriteLineAsync(text);
                DateTime startTime = DateTime.Now;

                do //プロセスが終了するまで監視
                {
                    try
                    {
                        process.Refresh();
                        time = DateTime.Now - startTime;
                        memory = Math.Max(memory, process.PeakWorkingSet64);
                    }
                    catch (InvalidOperationException)
                    { } //プロセスが終了している場合、何もしない
                    catch (ArgumentException)
                    { } //プロセスが開かれていない場合も、何もしない

                    if (!string.IsNullOrEmpty(stderr.ToString()))
                    {
                        isError = true;
                        process.WaitForExit();
                    }

                    if (memory > probrem.problemmemorylimit * 1024) isMemoryExceeded = true;
                    if (time.TotalMilliseconds > timeout.TotalMilliseconds) isTimedOut = true;
                    if (isMemoryExceeded || isTimedOut)
                    {
                        try
                        {
                            process.Kill();
                            process.WaitForExit();
                        }
                        catch (Win32Exception)
                        { } //プロセスが終了中の場合、何もしない
                        catch (InvalidOperationException)
                        { } //プロセスが終了している場合、何もしない
                    }

                } while (!process.HasExited);

                process.WaitForExit();
                process.CancelOutputRead();
                process.CancelErrorRead();

                result.ProcessingTime = time;
                result.UsedMemory = memory;
            }

            var resultString = stdout.ToString();
            result.Output = resultString;
            string res;
            if (isMemoryExceeded || isTimedOut)
            {
                if (isMemoryExceeded) res = "MLE";
                else res = "TLE";
            }
            else
            {
                if (isError)
                {
                    res = "RE";
                    txtError.Text = stderr.ToString();
                }
                else
                {
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
                item.Style.BackColor = status == "AC" ? CommonColor.OK_BKG : CommonColor.NG_BKG;
                item.Style.ForeColor = status == "AC" ? CommonColor.OK_FRT : CommonColor.NG_FRT;
            }

            dgvResult.Rows[0].Cells[(int)DgvResultRow.JudgeCnt].Value = cnt + " / " + testcaseHeader.Headers.Count();
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
