
InputOutputHandler ioh = new InputOutputHandler(Console.ReadLine());

while (true){
    // ioh read

    // line calculate

    // ioh write
    Line line = new Line(ioh.symbolsPerLine);
}

public class Line {
    public Line(int symbolsOLine, string[] arrayOfWords){
        symbolsPerLine = symbolsOLine;
        lineWords = arrayOfWords;
    }

    int symbolsPerLine;
    string[] lineWords;

    int sizeOfMostSpaces;
    int sizeOfLastSpace;

    int getNumOfSymbols(string[] array) {
        /// returns the number of symbols in an array of nonwhite strings that represent the words on this line
        int num = 0;
        foreach (string word in array){
            num += word.Length;
        }
        return num;
    }

    void calculateLineSpaceSizes(){
        int numOfWords = lineWords.Length;
        int numOfSymbols = getNumOfSymbols(lineWords);
        sizeOfMostSpaces = (symbolsPerLine - numOfSymbols) / (numOfWords - 1);
        sizeOfLastSpace = (symbolsPerLine - numOfSymbols) % (numOfWords - 1);
    }
}

public class InputOutputHandler{
    char[] whiteChars = {'\n', ' ', '\t'};
    StreamReader inputFile;
    StreamWriter outputFile;
    int symbolsPerLine;

    public InputOutputHandler(string input){
        /// reads the input from Console, which is expected in such a format: "input.txt" "output.txt" "#symbols/line"
        try {
        string[] wholeInputArray = input.Split(" ", count: 3);    // by using count keyword I check for too many input arguments
        inputFile = new StreamReader(wholeInputArray[0]); 
        outputFile = new StreamWriter(wholeInputArray[1]); 
        int symbolsPerLine = Convert.ToInt32(wholeInputArray[2]);   // by immediatelly acessing the last argument I check for too few input arguments
        }
        catch (Exception ex) {
            if (ex is IndexOutOfRangeException || ex is ArgumentException){
                Console.WriteLine("Argument error");
                return;
            }
        }
    }

    void writeWordCharByChar(string word){
        foreach (char chr in word){
            outputFile.Write(chr);
        }
    }

    void writeSpace(int length){
        for (int i = 0; i < length; i++) {
            outputFile.Write(' ');
        }
    }

    void writeFormattedOutput(string[] wordsToWrite, int sizeOfMostSpaces, int sizeOfLastSpace){
  
        foreach (string word in wordsToWrite.Take(wordsToWrite.Length-2)){  // first take all but last two words
            writeWordCharByChar(word);
            writeSpace(sizeOfMostSpaces);
        }
        writeWordCharByChar(wordsToWrite[wordsToWrite.Length-2]); // the last two words will be separated by different space length
        writeSpace(sizeOfLastSpace);
        writeWordCharByChar(wordsToWrite[wordsToWrite.Length-1]);
    }

    string readWord(){
        string word = "";
        while (true){
            int readVal = inputFile.Read();
            if (readVal == -1){ // end of file handling
                return word;
            }

            char readchr = (char)readVal;   // ok so file hasnt ended yet

            if (!whiteChars.Contains(readchr)){ // add the nonwhite
                word += readchr;
            }
            else if (word != ""){ // each word might have multiple whites before it, so only return if you already found a nonwhite
                return word;
            }
        }
    }

    string[] readWords(){
        string[] arrayOfWords;
        int numOfChars = 0;
        string lastReadWord;
        while (true){
            lastReadWord = readWord();
            numOfChars += lastReadWord.Length;
            
            if (numOfChars > symbolsPerLine){   // last word would be too long
                break;
            }

            arrayOfWords.Append(lastReadWord);
        }
        return arrayOfWords;
    }
}