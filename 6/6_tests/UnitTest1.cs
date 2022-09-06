namespace _6_tests;


[TestClass]
public class UnitTest1
{
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