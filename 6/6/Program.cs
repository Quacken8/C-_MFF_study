[assembly:System.Runtime.CompilerServices.InternalsVisibleTo("UnitTest1")]

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

/*
Node combineNodes(Node leftNode, Node rightNode){
    if (leftNode.combined || rightNode.combined){
        throw new ArgumentException("One of the nodes was already combined before!");
    }
    leftNode.combined = true;
    rightNode.combined = true;
    Node newNode = new Node(leftNode.value+rightNode.value);
    return newNode;
}
*/

public class Tree{
    List<TreeLayer> layers = new List<TreeLayer>();

    void addLayer(TreeLayer layer){
        /// adds a layer into the tree such that the lowest of the node values is higher than of all the nodes below
        /// since this happens only when combining nodes and creating a new node of new highest value, no need to care about
        /// sorting --- new layer is always on top
        layers.Add(layer);
    }

    public void addNode(Node node){
        /// adds a node to one of tree's layer such that the new node will be in the highest layer that doesnt have higher value in it
        TreeLayer? lastLayer = null;
        bool found = false;
        foreach (TreeLayer layer in layers){
            if (layer.lowestValue > node.value){
                found = true;
                try {
                    lastLayer.addNode(node);
                }
                catch (NullReferenceException){
                    throw new Exception("You tried to add a node that would be below the lowest layer of the tree. You should go through the nodes from lowest values up!");
                }
                
                break;
            }
            lastLayer = layer;
        }
        if (!found) {   // oh so the new node is for a brand new top layer
            TreeLayer newHighestLayer = new TreeLayer();
            newHighestLayer.addNode(node);
            this.addLayer(newHighestLayer);
        }
    }
}

public class TreeLayer{
    List<Node> nodes = new List<Node>();
    public int highestValue = int.MinValue;
    public int lowestValue = int.MaxValue;
    public void addNode(Node newNode){
        nodes.Add(newNode);
        if (newNode.value > highestValue){
            highestValue = newNode.value;
        }
        if (newNode.value < lowestValue){
            lowestValue = newNode.value;
        }
    }

    void printTree(){

    }
}

public class Node{
    public int value;
    public bool combined = false;
    public Node(int value){
        this.value = value;
    }
}

public class Leaf : Node{
    public Leaf(int value):base(value){
        this.value = value;
    }
}