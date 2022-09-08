[assembly:System.Runtime.CompilerServices.InternalsVisibleTo("6_tests")]    // to make testing *not* hell

string filename;
try {
    filename = args[0];
}
catch (IndexOutOfRangeException){
    throw new ArgumentException();
}
if (args.Count() > 1){
    throw new ArgumentException();
}


myReader reader = new myReader(filename);

Tree tree = treeMaker(reader);
Console.WriteLine(tree.ToString());

static Tree treeMaker(IInputReader byteReader) {

    /// function that creates a tree using data provided by byteReader

    Dictionary<int, int> byteOccurances = Occurances.makeDictionary(byteReader);

    // turn them into trees

    List<Tree> trees = new List<Tree>();

    int age = 0;
    foreach (KeyValuePair<int, int> occurance in byteOccurances){
        Node newnode = new Node(occurance.Value, (byte)occurance.Key);
        newnode.leaf = true;
        Tree newTree = new Tree(newnode);
        newTree.age = age;
        age++;
        trees.Add(newTree);
    }

    // combine lowest two trees

    while (trees.Count > 1){
        Tree lowestTree = trees.First();
        lowestTree.age++;
        trees.RemoveAt(0);

        Tree secondLowestTree = trees.First();
        secondLowestTree.age++;
        trees.RemoveAt(0);

        secondLowestTree.addTree(lowestTree);

        TreeAgeSearch tas = new TreeAgeSearch(secondLowestTree.age);
        int indexToInsertAt = trees.FindIndex(0, tas.isOlderThan);
        if (indexToInsertAt == -1){
            trees.Add(secondLowestTree);
        }
        else {
            trees.Insert(indexToInsertAt, secondLowestTree);
        }

    }

    return trees[0];
}

/// <summary>
/// class to determine whether a tree is older than a specific age
/// </summary>
public class TreeAgeSearch{
    public int age;
    public TreeAgeSearch(int age){
        this.age = age;
    }

    public bool isOlderThan(Tree tree){
        return tree.age > this.age;
    }
}

public static class Occurances{
    /// <summary>
    /// returns a dictionary of symbols (ints) and number of their occurances sorted by those occurances in descending order
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static Dictionary<int, int> makeDictionary(IInputReader reader){

        Dictionary<int, int> occurances = new Dictionary<int, int>();
        bool inputExhausted = false;
    
        int symbol;
        while (!inputExhausted) {
            symbol = reader.Read();
            if (symbol == -1) {
                inputExhausted = true;  // redundancy for clarity
                break;
            }
            try {
                occurances[symbol]++;
            }
            catch (KeyNotFoundException){
                occurances.Add(symbol, 1);
            }
        }
        var orderedOccurances = occurances.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    
        return orderedOccurances;
    }
}

public interface IInputReader {
    ///reads the next byte in input and returns it as int or returns -1 if end of file
    public int Read();
}
public class myReader : IInputReader {
    Stream stream;
    public myReader(string filename){
        this.stream = File.OpenRead(filename);
    }

    public int Read(){
        return stream.ReadByte();
    }
}

public class Node {
    /// Node in Huffman binary tree. Node's symbol is the piece of information it represents and its weight is the number of occurances of the information in input 
    public int weight;
    public byte? symbol = null;
    public Node[]? childern = null;
    public Node? parent = null;
    public bool combined = false;
    public bool leaf;

    public Node(int weight, byte symbol){
        /// for leaf init
        this.weight = weight;
        this.leaf = true;
        this.symbol = symbol;
    }

    public Node(Node leftNode, Node rightNode){
        /// for nonleaf init from combined nodes
        this.weight = leftNode.weight + rightNode.weight;

        this.childern = new Node[2]{leftNode, rightNode};

        leftNode.combined = true;
        leftNode.parent = this;
        
        rightNode.parent = this;
        rightNode.combined = true;

        this.leaf = false;
    }
}


public class Tree {
    public Node highestNode;

    public int age;

    public Tree(Node highestNode){
        this.highestNode = highestNode;
    }

    static bool isFirstTreeLeft(Tree firstTree, Tree secondTree){
    /// determines if firstNode should be added to the left in a tree
        Node firstNode = firstTree.highestNode;
        Node secondNode = firstTree.highestNode;

        if (firstNode.weight < secondNode.weight){
            return true;
        }
        else if (firstNode.weight > secondNode.weight){
            return false;
        }
        else if (firstNode.leaf && !secondNode.leaf){
            return true;
        }
        else if (!firstNode.leaf && secondNode.leaf){
            return false;
        }
        else if (firstNode.leaf && secondNode.leaf){
            return firstNode.symbol < secondNode.symbol;
        }
        else if (!firstNode.leaf && !secondNode.leaf){
            return firstTree.age < secondTree.age;
        }
        return true;
}

    public void addTree(Tree tree){
        /// adds a node to the tree in such a way to accomodate the left-right rules
        if (isFirstTreeLeft(this, tree)){
            this.highestNode = new Node(this.highestNode, tree.highestNode);
        }
        else {
            this.highestNode = new Node(tree.highestNode, this.highestNode);
        }
    }

    public override string ToString(){
        /// returns string representation of the tree
        List<Node> unexploredIntersections = new List<Node>{this.highestNode};
 
        string toPrint = "";

        Node workingNode = unexploredIntersections.Last();

        while(unexploredIntersections.Count != 0){
            workingNode = unexploredIntersections.Last();
            unexploredIntersections.RemoveAt(unexploredIntersections.Count - 1);
            if (workingNode.leaf){
                // TBD CHANGE TO STRINGG BUILDER THAT LIVES AROIND THE WHOLE WHILE LOOP!
                toPrint += workingNode.symbol + ":" + workingNode.weight + " ";
            }
            else {
                unexploredIntersections.Add(workingNode.childern[1]);
                unexploredIntersections.Add(workingNode.childern[0]);
            }
        }

        return toPrint;
    }
}