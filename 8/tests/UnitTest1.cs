namespace excel_tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void numberToColumnName_test0A()
    {
        int number = 0;

        string name = AssortedExcelFunctions.numberToColumnName(number);
        string expectedName = "A";
        Console.WriteLine(name);

        Assert.AreEqual(expectedName, name);
    }    
    [TestMethod]
    public void numberToColumnName_test25Z()
    {
        int number = 25;

        string name = AssortedExcelFunctions.numberToColumnName(number);
        string expectedName = "Z";
        Console.WriteLine(name);

        Assert.AreEqual(expectedName, name);
    }

    [TestMethod]
    public void numberToColumnName_test26AA()
    {
        int number = 26;

        string name = AssortedExcelFunctions.numberToColumnName(number);
        string expectedName = "AA";
        Console.WriteLine(name);

        Assert.AreEqual(expectedName, name);
    }
    [TestMethod]
    public void numberToColumnName_test27AB()
    {
        int number = 27;

        string name = AssortedExcelFunctions.numberToColumnName(number);
        string expectedName = "AB";
        Console.WriteLine(name);

        Assert.AreEqual(expectedName, name);
    }    
    [TestMethod]
    public void numberToColumnName_testAAA()
    {
        int number = 26+26*26;

        string name = AssortedExcelFunctions.numberToColumnName(number);
        string expectedName = "AAA";
        Console.WriteLine(name);

        Assert.AreEqual(expectedName, name);
    }

    [TestMethod]
    public void columnNameToNumber_test0A(){
        string name = "A";
        int number = AssortedExcelFunctions.columnNameToNumber(name);

        int expectedNumber = 0;

        Console.WriteLine(number);

        Assert.AreEqual(expectedNumber, number);
    }

    [TestMethod]
    public void columnNameToNumber_test1B(){
        string name = "B";
        int number = AssortedExcelFunctions.columnNameToNumber(name);

        int expectedNumber = 1;

        Console.WriteLine(number);

        Assert.AreEqual(expectedNumber, number);
    }    
    [TestMethod]
    public void columnNameToNumber_test25Z(){
        string name = "Z";
        int number = AssortedExcelFunctions.columnNameToNumber(name);

        int expectedNumber = 25;

        Console.WriteLine(number);

        Assert.AreEqual(expectedNumber, number);
    }
    [TestMethod]
    public void columnNameToNumber_test26AA(){
        string name = "AA";
        int number = AssortedExcelFunctions.columnNameToNumber(name);

        int expectedNumber = 26;

        Console.WriteLine(number);

        Assert.AreEqual(expectedNumber, number);
    }
}