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
    /// used to converting from column number to letter representation indexing from 0
    /// </summary>
    /// <param name="number">The number of the column</param>
    /// <returns>column name in letter representation</returns>
    public static string numberToColumnName(int number){
        const int padding = 65; // number at which in chars the alphabet starts
        const int lettersInAlphabet = 26;
        Stack<char> nameStack = new Stack<char>();
        do
        {
            int modulo = (number % lettersInAlphabet);
            number /= lettersInAlphabet;
            number--;
            nameStack.Push((char)(modulo + padding));

        } while (number >= 0);
        string name = "";
        
        while (nameStack.Count > 0){
            name += nameStack.Pop();
        }
        return name;
    }

    /// <summary>
    /// quickly-ish makes a baseInt**power
    /// </summary>
    /// <param name="baseInt"></param>
    /// <param name="power"></param>
    /// <returns></returns>
    public static int intPow(int baseInt, int power){
        int toReturn = 1;
        while (power > 1){
            toReturn *= baseInt;
            power--;
        }
        return toReturn;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns>number of column represented by 'name'</returns>
    public static int columnNameToNumber(string name){
        const int padding = 65; // number at which in chars the alphabet starts
        const int lettersInAlphabet = 26;
        int colNumber = 0;
        char[] nameArr = name.ToCharArray();
        for (int i = 0; i < nameArr.Length; i++)
        {
            int power = nameArr.Length - 1 - i;
            int digit = (int)nameArr[i] - padding;
            colNumber += digit * intPow(lettersInAlphabet, power);
        }

        return colNumber;
    }
}