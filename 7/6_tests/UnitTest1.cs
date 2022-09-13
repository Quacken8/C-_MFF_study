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

    /*
    [TestMethod]
    public void Tree_newToStringWithStack(){
        // Arange
        myReader reader = new myReader("dust_density.binp");
        Tree tree = treeMaker.MakeTree(reader);

        // Act
        string oldeResult = tree.OldToString();
        string newResult = tree.ToString();

        // Assert
        Assert.AreEqual(oldeResult, newResult);
    }
    */
    
    [TestMethod]
    public void TreeAgeSearch_regular(){
        // Arange
        List<long> mockupWeights = new List<long>{1, 2, 3, 3, 4, 10, 20, 50};
        int expectedIndex = 5;
        List<Tree> trees = new List<Tree>();

        foreach (int weight in mockupWeights){
            Node newnode = new Node(weight, 0);
            Tree newtree = new Tree(newnode);
            newtree.age = (int)newtree.highestNode.weight;
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
        List<long> mockupWeights = new List<long>{1, 2, 3, 3, 4, 10, 20, 50};
        int expectedIndex = 4;
        List<Tree> trees = new List<Tree>();

        foreach (int weight in mockupWeights){
            Node newnode = new Node(weight, 0);
            Tree newtree = new Tree(newnode);
            newtree.age = (int)newtree.highestNode.weight;
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

    [TestMethod]
    public void toBits_testRightTree(){
        // Arange

        Dictionary<byte, ulong> expectedTreeInBits = new Dictionary<byte, ulong>();
        expectedTreeInBits.Add(2, 0b_10);
        expectedTreeInBits.Add(0, 0b_110);
        expectedTreeInBits.Add(1, 0b_111);

        Tree mockupTree = MockupTrees.right012tree;

        // Act
        Dictionary<byte, ulong> mockupTreeInBits = mockupTree.ToBytes();

        // Assert
        Console.WriteLine("Expected tree \t mockup tree");
        foreach (KeyValuePair<byte, ulong> keyAndValue in expectedTreeInBits){
            Console.WriteLine(keyAndValue + "\t [" + keyAndValue.Key + ", " + mockupTreeInBits[keyAndValue.Key] + "]");
        }
        Console.WriteLine(mockupTreeInBits.Values);
        CollectionAssert.AreEqual(expectedTreeInBits, mockupTreeInBits);
    }
    [TestMethod]
    public void toBits_testLeftTree(){
        // Arange

        Dictionary<byte, ulong> expectedTreeInBits = new Dictionary<byte, ulong>();
        expectedTreeInBits.Add(2, 0b_11);
        expectedTreeInBits.Add(0, 0b_100);
        expectedTreeInBits.Add(1, 0b_101);

        Tree mockupTree = MockupTrees.left012tree;

        // Act
        Dictionary<byte, ulong> mockupTreeInBits = mockupTree.ToBytes();

        // Assert
        Console.WriteLine("Expected tree \t mockup tree");
        foreach (KeyValuePair<byte, ulong> keyAndValue in expectedTreeInBits){
            Console.WriteLine(keyAndValue + "\t [" + keyAndValue.Key + ", " + mockupTreeInBits[keyAndValue.Key] + "]");
        }
        Console.WriteLine(mockupTreeInBits.Values);
        CollectionAssert.AreEquivalent(expectedTreeInBits, mockupTreeInBits);
    }
    [TestMethod]
    public void toBits_testSymetricTree(){
        // Arange

        Dictionary<byte, ulong> expectedTreeInBits = new Dictionary<byte, ulong>();
        expectedTreeInBits.Add(3, 0b_111);
        expectedTreeInBits.Add(2, 0b_110);
        expectedTreeInBits.Add(0, 0b_100);
        expectedTreeInBits.Add(1, 0b_101);

        Tree mockupTree = MockupTrees.tree0123;

        // Act
        Dictionary<byte, ulong> mockupTreeInBits = mockupTree.ToBytes();

        // Assert
        Console.WriteLine("Expected tree \t mockup tree");
        foreach (KeyValuePair<byte, ulong> keyAndValue in expectedTreeInBits){
            Console.WriteLine(keyAndValue + "\t [" + keyAndValue.Key + ", " + mockupTreeInBits[keyAndValue.Key] + "]");
        }
        Console.WriteLine(mockupTreeInBits.Values);
        CollectionAssert.AreEquivalent(expectedTreeInBits, mockupTreeInBits);
    }

    public static class MockupTrees {
        static Node node0 = new Node(1, 0);
        static Node node1 = new Node(1, 1);
        static Node node01 = new Node(node0, node1);
        static Node node2 = new Node(1, 2);
        static Node node012 = new Node(node2, node01);
        /// <summary>
        /// such a tree:<br/>
        ///   │<br/>
        /// ┌─┴─┐<br/>
        ///┌┴┐  │<br/>
        ///│2│  │<br/>
        ///└─┘┌─┴─┐<br/>
        ///  ┌┴┐ ┌┴┐<br/>
        ///  │0│ │1│<br/>
        ///  └─┘ └─┘<br/>
        /// </summary>
        public static Tree right012tree = new Tree(node012);
        static Node node2heavy = new Node(5, 2);
        static Node node012left = new Node(node01, node2heavy);
        /// <summary>
        /// such a tree:
        ///     │
        ///   ┌─┴─┐
        ///   │  ┌┴┐
        ///   │  │2│
        /// ┌─┴─┐└─┘
        ///┌┴┐ ┌┴┐
        ///│0│ │1│
        ///└─┘ └─┘
        /// </summary>
        public static Tree left012tree = new Tree(node012left);
        static Node node3 = new Node(1, 3);
        static Node node23 = new Node(node2, node3);
        static Node node0123 = new Node(node01, node23);
        /// <summary>
        /// such a tree:
        ///      │
        ///   ┌──┴──┐
        ///   │     │
        /// ┌─┴─┐ ┌─┴─┐
        /// │   │ │   │
        ///┌┴┐ ┌┴┬┴┐ ┌┴┐
        ///│0│ │1│2│ │3│
        ///└─┘ └─┴─┘ └─┘
        /// </summary>
        public static Tree tree0123 = new Tree(node0123);
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