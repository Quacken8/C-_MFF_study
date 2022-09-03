namespace _6_tests;


[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TreeAdd_tryRegularAddingOfOkBytes(){
        // Arange
        Tree tree = new Tree();
        byte[] mockupBytesToAdd = new byte[]{1, 2, 2, 10, 1, 3};

        // Act
        foreach (byte b in mockupBytesToAdd){
            Node newnode = new Node(b);
            tree.addNode(newnode);
        }

        // Assert
        Assert.AreEqual(tree.layers, 3);
    }

    [TestMethod]
    public void Input_WorksByteByByte()
    {
        /*
        // Arange
        var mockupByteInput[]{bajtybajtybajtybajty};
        InputHandler ih = new InputHandler();

        // Act
        readBytes = ih.read(mockupByteInput);
        

        // Assert

        Assert.AreEqual([bajtyy], readBytes);
        */
    }

    class ByteReaderMockup: IByteReader {
        public List<Byte> inputBytes = new List<byte>();

        public int ReadByte(){
            int toReturn = inputBytes[0];
            inputBytes.RemoveAt(0);
            return toReturn;
        }
    }
}