namespace _6_tests;


[TestClass]
public class UnitTest1
{
    /*
    [TestMethod]
    public void TreeAdd_tryTreeOfRegularBytes(){
        // Arange
        Tree tree = new Tree();
        byte[] mockupBytesToAdd = new byte[]{2, 1};

        // Act
        foreach (byte b in mockupBytesToAdd){
            Node newnode = new Node(1, b, 0);
            tree.addNode(newnode, newnode.age);
        }

        
        Console.WriteLine(tree.ToString());
        // Assert
        // Assert.AreEqual(tree.highestNode, firstNode);   // yes, dumb assert, but just lemme access the internals aaaa
    }
    */
    
    [TestMethod]
    public void TreeAgeSearch_regular(){
        // Arange
        List<int> mockupWeights = new List<int>{1, 2, 3, 3, 4, 10, 20, 50};
        int expectedIndex = 5;
        List<Tree> trees = new List<Tree>();

        foreach (int weight in mockupWeights){
            Node newnode = new Node(weight, 0);
            Tree newtree = new Tree(newnode);
            newtree.age = newtree.highestNode.weight;
            trees.Add(newtree);
        }

        // Act

        var tas = new TreeAgeSearch(5);

        int index = trees.FindIndex(0, tas.isOlderThan);

        // Assert

        Assert.AreEqual(expectedIndex, index);
    }

    [TestMethod]
    public void TreeAgeSearch_twoSameIndexes(){
        // Arange
        List<int> mockupWeights = new List<int>{1, 2, 3, 3, 4, 10, 20, 50};
        int expectedIndex = 4;
        List<Tree> trees = new List<Tree>();

        foreach (int weight in mockupWeights){
            Node newnode = new Node(weight, 0);
            Tree newtree = new Tree(newnode);
            newtree.age = newtree.highestNode.weight;
            trees.Add(newtree);
        }

        // Act

        var tas = new TreeAgeSearch(3);

        int index = trees.FindIndex(0, tas.isOlderThan);

        // Assert

        Assert.AreEqual(expectedIndex, index);
    }

    [TestMethod]
    public void occurancesDictionary_Test()
    {
        // Arange
        List<byte> mockupByteInput = new List<byte>{1, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5};
        ByteReaderMockup br = new ByteReaderMockup();
        br.inputBytes = mockupByteInput;

        Dictionary<int, int> expectedResult = new Dictionary<int, int>();
        expectedResult.Add(5, 5);
        expectedResult.Add(4, 4);
        expectedResult.Add(3, 3);
        expectedResult.Add(2, 2);
        expectedResult.Add(1, 1);

        // Act
        var orderedOccurances = Occurances.makeDictionary(br);
        

        // Assert

        CollectionAssert.AreEqual(expectedResult, orderedOccurances);
    }

    class ByteReaderMockup: IInputReader {
        public List<Byte> inputBytes = new List<byte>();

        public int Read(){
            int toReturn;
            try{
                toReturn = inputBytes[0];
            }
            catch (ArgumentOutOfRangeException){
                return -1;
            }

            inputBytes.RemoveAt(0);
            return toReturn;
        }
    }
}