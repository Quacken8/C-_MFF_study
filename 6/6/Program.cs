[assembly:System.Runtime.CompilerServices.InternalsVisibleTo("6_tests")]    // to make testing *not* hell

Console.WriteLine("Hello");

static Tree treeMaker(IInputReader byteReader) {
    /// function that creates a tree using data provided by byteReader

    Dictionary<int, int> byteOccurances = Occurances.makeDictionary(byteReader);

    // turn them into a tree

    int age = 0;
    List<Tree> trees = new List<Tree>(); // use index as age
    
    while (true){
        
    }
}

public static class Occurances{
    public static Dictionary<int, int> makeDictionary(IInputReader reader){
    /// returns a dictionary of symbols (ints) and number of their occurances sorted by those occurances in descending order

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
    ///reads the next byte in file and returns it as int or returns -1 if end of file
    public int Read();
}

public class Node {
    /// Node in Huffman binary tree. Node's symbol is the piece of information it represents and its weight is the number of occurances of the information in input 
    public int weight;
    public byte? symbol = null;
    public Node[]? childern = null;
    public Node? parent = null;
    public bool combined = false;
    public int age;
    public bool leaf;

    public Node(int weight, byte symbol, int age){
        /// for leaf init
        this.weight = weight;
        this.age = age;
        this.leaf = true;
        this.symbol = symbol;
    }

    public Node(Node leftNode, Node rightNode, int age){
        /// for nonleaf init from combined nodes
        this.weight = leftNode.weight + rightNode.weight;
        this.age = age;

        this.childern = new Node[2]{leftNode, rightNode};

        leftNode.combined = true;
        leftNode.parent = this;
        
        rightNode.parent = this;
        rightNode.combined = true;

        this.leaf = false;
    }
}


public class Tree {
    public Node? highestNode;

    public Tree(){}

    public Tree(Node highestNode){
        this.highestNode = highestNode;
    }

    static bool isFirstNodeLeft(Node firstNode, Node secondNode){
    /// determines if firstNode should be added to the left in a tree
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
            return firstNode.age < secondNode.age;
        }
        return true;
}

    public void addNode(Node node, int age){
        /// adds a node to the tree in such a way to accomodate the left-right rules
        if (isFirstNodeLeft(this.highestNode, node)){
            this.highestNode = new Node(this.highestNode, node, age);
        }
        else {
            this.highestNode = new Node(node, this.highestNode, age);
        }
    }

    public override string ToString(){
        /// returns string representation of the tree
        List<Node> unexploredIntersections = new List<Node>{this.highestNode};
 
        string toPrint = "";

        Node? workingNode = null;

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