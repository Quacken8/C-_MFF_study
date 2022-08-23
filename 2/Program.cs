//Console.WriteLine("Ohayoo C: uwu whats ur file name?");

//var filename = Console.ReadLine();
string filename = "file.txt";
if (filename is null) {
    Console.WriteLine("Error: filename is null! What the hell did you input?");
}

StreamReader reader = new StreamReader(filename);


string wordICareAbout = "";
char[] delimiters = {'.', ',', ' ', '\n', '\t'};

Dictionary<string, int> cetnosti = new Dictionary<string, int>();

while (true) {
    Int32 readValue = reader.Read();
    if (readValue == -1){
        break;
    }
    char chr = (char)readValue;
    if (delimiters.Contains(chr)){
        try {
            cetnosti[wordICareAbout]++;
        }
        catch (KeyNotFoundException){
            if (wordICareAbout != ""){
                cetnosti.Add(wordICareAbout, 1);
            }
        }
        // add it to the dick
        wordICareAbout = "";
        continue;
    }
    wordICareAbout+=chr;
}

var orderedCetnosti = cetnosti.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

foreach(KeyValuePair<string, int> word in orderedCetnosti){
    Console.WriteLine("{0} \t\t {1}", word.Key, word.Value);
}