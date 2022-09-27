Console.WriteLine("hello world");

public class TableRow {
    List<string> cells = new List<string>();
    List<string> columnNames = new List<string>();
}

public class Sheet {
    const string suffix = ".sheet";
    string name { set; get; }
}
/// <summary>
/// A bunch of functions used to turn user input to program-friendly data
/// </summary>
public static class AssortedExcelFunctions{
    /// <summary>
    /// used to converting from column number to letter representation
    /// </summary>
    /// <param name="number">The number of the column</param>
    /// <returns>column name in letter representation</returns>
    public static string numberToColumnName(int number){
        const int padding = 65; // number after which in chars the alphabet starts
        const int lettersInAlphabet = 26;
        string name = "";
        do
        {
            int modulo = (number % lettersInAlphabet);
            number -= modulo;
            number /= lettersInAlphabet;
            name += (char)(modulo+padding);
        } while (number > 0);

        return name;
    }
}