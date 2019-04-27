using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

[TestClass()]
public class ProgramTests
{
    //テストケース
    [TestMethod()]
    public void MainTest1() { TestInOut("Input1.txt", "Output1.txt"); } //変更する
    [TestMethod()]
    public void MainTest2() { TestInOut("Input2.txt", "Output2.txt"); } //変更する
    [TestMethod()]
    public void MainTest3() { TestInOut("Input3.txt", "Output3.txt"); } //変更する
    [TestMethod()]
    public void MainTest4() { TestInOut("Input4.txt", "Output4.txt"); } //変更する
    [TestMethod()]
    public void MainTest5() { TestInOut("Input5.txt", "Output5.txt"); } //変更する

    public void TestInOut(string inputFileName, string outputFileName)
    {
        using (var input = new StreamReader(inputFileName))
        using (var output = new StringWriter())
        {
            Console.SetOut(output);
            Console.SetIn(input);
            //Program.Main(new string[0]);

            Assert.AreEqual(File.ReadAllText(outputFileName), output.ToString());
        }
    }
}