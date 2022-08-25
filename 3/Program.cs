//string userInput = args[0] + args[1];  // first get the user input; this may not be how its supposed to be used, cuz i aint waiting for a readline, its supposed to be called as an argument←

#define gaelol

char[] paragraphEnderChars = {'\n'};
string userInput = "file.txt formattedfile.txt 30";
InputOutputHandler Ioh = new InputOutputHandler( userInput , paragraphEnderChars );

LineHandler Lh = new LineHandler( Ioh , paragraphEnderChars );

while (true) {
    string nextWord = Ioh.readWord();
    if (nextWord == ""){ // only happens at the end of the file
        break;
    }
    Lh.appendNewWord(nextWord);
    // if its a paragraph end, tell line handler to end a paragraph
    // if its a word give it to line handler
    // it tries to append; if it fits, loop again
    // if not, rememeber it, write formatted words u have till now, then recreate the array with the remembered word
}

class IOHandlerWriteArguments {
    public List<string> text;
    public int sizeOfMostSpaces;
    public int sizeOfLastSpace;
    public IOHandlerWriteArguments(List<string> text, int sizeOfMostSpaces, int sizeOfLastSpace){
        this.text = text;
        this.sizeOfMostSpaces = sizeOfMostSpaces;
        this.sizeOfLastSpace = sizeOfLastSpace;
    }
}

class LineHandler {    
    List<string> lineWords = new List<string>();
    int sizeOfMostSpaces;
    int sizeOfLastSpace;
    //char[] paragraphEnderChars = {'\n'};
    InputOutputHandler iohandler;
    int symbolsPerLine;
    char[] paragraphEnderChars;
    public LineHandler(InputOutputHandler iohh, char[] paragraphEnderChars){
        this.iohandler = iohh;
        this.symbolsPerLine = iohandler.symbolsPerLine;
        this.paragraphEnderChars = paragraphEnderChars;
    }

    public int getNumOfSymbols(List<string> array) {
        /// returns the number of symbols in an array of strings that represent the words on this line
        int num = 0;
        foreach (string word in array){
            if (word.Last() == '\n') {
                num += word.Length-1;
            }
            else {
                num += word.Length;
            }
        }
        return num;
    }


    public void appendNewWord(string wordToAppend) {
        List<string> newLineWords = lineWords.Append(wordToAppend).ToList();
        if (getNumOfSymbols(newLineWords) + newLineWords.Count-1  < symbolsPerLine && !paragraphEnderChars.Contains(wordToAppend.Last())){ // check if u have space on the line for this new word
            lineWords.Add(wordToAppend);
            return;
        }
        if (paragraphEnderChars.Contains(wordToAppend.Last())) { // that means we at the end of the paragraph, so just print it reguralrly
            calculateLineSpaceSizes(justLeftJustify : true);
            IOHandlerWriteArguments paramsForWriting = prepareOutputLine();
            // write the line using iohandler
            var text = paramsForWriting.text;
            int normalSpaces = paramsForWriting.sizeOfMostSpaces;
            int lastSpace = paramsForWriting.sizeOfLastSpace;
            iohandler.writeFormattedOutput(text, normalSpaces, lastSpace);
            lineWords = new List<string>();
        }
        else { // ok so the line woud overflow, lets write it down
            calculateLineSpaceSizes();
            IOHandlerWriteArguments paramsForWriting = prepareOutputLine();
            // write the line using iohandler
            var text = paramsForWriting.text;
            int normalSpaces = (int)paramsForWriting.sizeOfMostSpaces;
            int lastSpace = (int)paramsForWriting.sizeOfLastSpace;
            iohandler.writeFormattedOutput(text, normalSpaces, lastSpace);
            lineWords = new List<string>();
            lineWords.Add(wordToAppend);
        }
    }

    public void calculateLineSpaceSizes(bool justLeftJustify = false){
        /// from words to be put on a line calculates the spaces for that line
        int numOfWords = lineWords.Count;
        if (numOfWords == 1 || justLeftJustify == true){ // a single word being written down
            this.sizeOfLastSpace = 1;
            this.sizeOfMostSpaces = 1;
        }
        else {
            int numOfSymbols = getNumOfSymbols(lineWords);
            this.sizeOfMostSpaces = (symbolsPerLine - numOfSymbols) / (numOfWords - 1);
            this.sizeOfLastSpace = sizeOfMostSpaces+(symbolsPerLine - numOfSymbols) % (numOfWords - 1);
        }
    }

    public IOHandlerWriteArguments prepareOutputLine(){
        /// prepares a line for the io handler 
        IOHandlerWriteArguments toReturn = new IOHandlerWriteArguments(this.lineWords, this.sizeOfMostSpaces, this.sizeOfLastSpace );
        return toReturn;
    }
}

class InputOutputHandler{
    char[] whiteChars = {' ', '\t'};
    StreamReader inputFile;
    StreamWriter outputFile;
    public int symbolsPerLine;
    char[] paragraphEnderChars;

    public InputOutputHandler(string input, char[] paragraphEnderChars){
        /// reads the input from Console, which is expected in such a format: "input.txt" "output.txt" "#symbols/line"
        this.paragraphEnderChars = paragraphEnderChars;
        try {
        string[] wholeInputArray = input.Split(" ", count: 3);    // by using count keyword I check for too many input arguments
        this.inputFile = new StreamReader(wholeInputArray[0]); 
        this.outputFile = new StreamWriter(wholeInputArray[1]); 
        this.symbolsPerLine = Convert.ToInt32(wholeInputArray[2]);   // by immediatelly acessing the last argument I check for too few input arguments
        }
        catch (Exception ex) {
            if (ex is IndexOutOfRangeException || ex is ArgumentException){
                Console.WriteLine("Argument error");
                return;
            }
        }
    }
    public int printSymbolsPerLine(){
        return symbolsPerLine;
    }

    public void writeWordCharByChar(string word){
        foreach (char chr in word){
            outputFile.Write(chr);
            #if gaelol
                Console.Write(chr);
            #endif
        }
    }

    public void writeSpace(int length){
        for (int i = 0; i < length; i++) {
            outputFile.Write(' ');
            #if gaelol
                Console.Write(' ');
            #endif
        }
    }

    public void writeFormattedOutput(List<string> wordsToWrite, int sizeOfMostSpaces, int sizeOfLastSpace){
        /// writes a formatted line and terminates it with \n 
        foreach (string word in wordsToWrite.Take(wordsToWrite.Count-2)){  // first take all but last two words
            writeWordCharByChar(word);
            writeSpace(sizeOfMostSpaces);
        }
        writeWordCharByChar(wordsToWrite[wordsToWrite.Count-2]); // the last two words will be separated by different space length
        writeSpace(sizeOfLastSpace);
        writeWordCharByChar(wordsToWrite.Last());
        writeWordCharByChar("\n");
    }

    public string readWord(){
        string word = "";
        while (true){
            int readVal = inputFile.Read();
            if (readVal == -1){ // end of file handling
                return word;
            }

            char readchr = (char)readVal;   // ok so file hasnt ended yet
            if (!whiteChars.Contains(readchr)){ // add the nonwhite
                if (paragraphEnderChars.Contains(readchr)){
                    if (word == ""){    // means the whole word would be just \n, we dont want that
                        continue;
                    }
                    word += readchr;    // ok so it is a \n but at the end of a word => its a proper paragraph ender
                    return word;
                }
                else { // ok, so its just a normal char
                    word += readchr;
                }

            }
            else if (word != ""){ // each word might have multiple whites before it, so only return if you already found a nonwhite
                return word;
            }
        }
    }
}