[assembly:System.Runtime.CompilerServices.InternalsVisibleTo("_6_tests")]   // why the fuck are you not working


Console.WriteLine("Hello");

bool fileEnd = false;

while (!fileEnd){
    //byte readByte = IByteReader.ReadByte();
}
/*
interface IByteReader {
    ///reads the next byte in file and returns it as int or returns null if end of file
    byte ReadByte();
}

byte readByte(){
    var reader = new StreamReader(filename);
}
*/


bool isFirstNodeLeft(Node firstNode, Node secondNode){
    /// determines if firstNode should be added to the left in a tree
    bool? firstNodeIsLeft = null;
        if (firstNode.weight < secondNode.weight){
            firstNodeIsLeft = true;
        }
        else if (firstNode.weight > secondNode.weight){
            firstNodeIsLeft = false;
        }
        else if (firstNode.leaf && !secondNode.leaf){
            firstNodeIsLeft = true;
        }
        else if (!firstNode.leaf && secondNode.leaf){
            firstNodeIsLeft = false;
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
    Node highestNode;

    public Tree(Node highestNode){
        this.highestNode = highestNode;
    }

    public void addNode(Node node){
        if (isFirstNodeLeft(this.highestNode, node){
            this.highestNode = new Node(this.highestNode, node);
        }
        else {
            this.highestNode = new Node(node, this.highestNode);
        }
    }

    public void printTree(){
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
    }
}