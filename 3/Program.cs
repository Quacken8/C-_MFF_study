char[] paragraphEnderChars = {'\n'};
InputOutputHandler Ioh = new InputOutputHandler( args , paragraphEnderChars );

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
            num += word.Length;
        }
        return num;
    }

    bool fitsOnLine(List<string> lineWords, string wordToAppend, int symbolsPerLine){
        if (paragraphEnderChars.Contains(wordToAppend.Last())) {
            return getNumOfSymbols(lineWords) + lineWords.Count-1 + wordToAppend.Length -1 < symbolsPerLine;
        }
        return getNumOfSymbols(lineWords) + lineWords.Count-1 + wordToAppend.Length < symbolsPerLine;
    }

    public void appendNewWord(string wordToAppend) {
        if (paragraphEnderChars.Contains(wordToAppend.Last()) && fitsOnLine(lineWords, wordToAppend, symbolsPerLine)) { // that means we at the end of the paragraph, so just print it reguralrly
            lineWords.Add(wordToAppend);
            // write the line using iohandler
            var text = lineWords;
            int normalSpaces = 1;
            int lastSpace = 1;
            iohandler.writeFormattedOutput(text, normalSpaces, lastSpace);
            lineWords = new List<string>();
            return;
        }
        if (fitsOnLine(lineWords, wordToAppend, symbolsPerLine)){ // check if u have space on the line for this new word
            lineWords.Add(wordToAppend);
            return;
        }
        if (lineWords.Count == 0 && wordToAppend.Length>=symbolsPerLine){ // so we already have a line of a single word. Its too long but hey, we gotta do it
            var text = new List<string> {wordToAppend};
            int normalSpaces = 1;
            int lastSpace = 1;
            iohandler.writeFormattedOutput(text, normalSpaces, lastSpace);
            return;
        }
        else { // ok so the line woud overflow but it aint a massive piece of word, lets write it down
            calculateLineSpaceSizes();
            IOHandlerWriteArguments paramsForWriting = prepareOutputLine();
            // write the line using iohandler
            var text = paramsForWriting.text;
            int normalSpaces = (int)paramsForWriting.sizeOfMostSpaces;
            int lastSpace = (int)paramsForWriting.sizeOfLastSpace;
            iohandler.writeFormattedOutput(text, normalSpaces, lastSpace);
            lineWords = new List<string>();
            this.appendNewWord(wordToAppend);
        }
    }

    public void calculateLineSpaceSizes(){
        /// from words to be put on a line calculates the spaces for that line
        int numOfWords = lineWords.Count;
        if (numOfWords == 1){ // a single word being written down
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

    public InputOutputHandler(string[] input, char[] paragraphEnderChars){
        /// reads the input from Console, which is expected in such a format: "input.txt" "output.txt" "#symbols/line"
        this.paragraphEnderChars = paragraphEnderChars;
        try {
            this.inputFile = new StreamReader(input[0]); 
            this.outputFile = new StreamWriter(input[1]); 
            this.symbolsPerLine = Convert.ToInt32(input[2]);   // by immediatelly acessing the last argument I check for too few input arguments
            if (input.Length != 3){throw new IndexOutOfRangeException();}
            }
        catch (IndexOutOfRangeException) {
            Console.WriteLine("Argument error");
            return;
            }
        catch(FileNotFoundException){
            Console.WriteLine("File error");
            return;
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
        if (wordsToWrite.Count > 1){
            foreach (string word in wordsToWrite.Take(wordsToWrite.Count-2)){  // first take all but last two words
                writeWordCharByChar(word);
                writeSpace(sizeOfMostSpaces);
            }
            writeWordCharByChar(wordsToWrite[wordsToWrite.Count-2]); // the last two words will be separated by different space length
            writeSpace(sizeOfLastSpace);
        }
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