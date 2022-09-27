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
        int number = 25;

        string name = AssortedExcelFunctions.numberToColumnName(number);
        string expectedName = "AA";
        Console.WriteLine(name);

        Assert.AreEqual(expectedName, name);
    }
}