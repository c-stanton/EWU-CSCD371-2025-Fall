namespace GenericsHomeWork.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericsHomeWork;
using System;
using System.Globalization;

[TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void Node_SetsValue_PointsToSelf()
        {
            // Arrange
            int expectedValue = 5;

            // Act
            Node<int> node = new Node<int>(expectedValue);

            // Assert
            Assert.AreEqual(expectedValue, node.Value);
            Assert.AreEqual(node, node.Next); 
        }

    [TestMethod]
    public void Append_InsertsNewNode_AfterCurrentNode()
    {
        // Arrange
        var head = new Node<string>("first");
        // Act
        head.Append("second");
        // Assert
        Assert.AreEqual("second", head.Next.Value, "String 'second' should be inserted after 'first'");
        Assert.AreEqual(head, head.Next.Next, "Next of 'second' should point back to head node 'first'.");

        // Act
        head.Append("third");
        // Assert
        Assert.AreEqual("third", head.Next.Value, "String 'third' should be inserted after 'second'.");
        Assert.AreEqual("second", head.Next.Next.Value, "String 'second' should still be after 'first'.");
        Assert.AreEqual(head, head.Next.Next.Next, "Next of 'third' should point back to head node 'first'.");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Append_ThrowsException_OnDuplicateValue()
    {
        // Arrange
        var head = new Node<int>(10);
        head.Append(20);
        // Act
        head.Append(10);
    }

    [TestMethod]
    public void Exists_ReturnsTrue_IfValueExists()
    {
        // Arrange
        var head = new Node<double>(1.1);
        head.Append(2.2);
        head.Append(3.3);
        // Act & Assert
        Assert.IsTrue(head.Exists(1.1), "Value 1.1 should exist in the list.");
        Assert.IsTrue(head.Exists(2.2), "Value 2.2 should exist in the list.");
        Assert.IsTrue(head.Exists(3.3), "Value 3.3 should exist in the list.");
    }

    [TestMethod]
    public void Exists_ReturnsFalse_IfValueDoesNotExist()
    {
        // Arrange
        var head = new Node<char>('A');
        head.Append('B');
        head.Append('C');
        // Act & Assert
        Assert.IsFalse(head.Exists('D'), "Value 'D' should not exist in the list.");
        Assert.IsFalse(head.Exists('E'), "Value 'E' should not exist in the list.");
    }

    [TestMethod]
    public void Clear_RemovesAllNodes_ExceptHead()
    {
        // Arrange
        var head = new Node<string>("root");
        head.Append("child1");
        head.Append("child2");
        head.Append("child3");
        // Act
        head.Clear();
        // Assert
        Assert.AreEqual(head, head.Next, "After Clear(), head's Next should point to itself.");
        Assert.IsFalse(head.Exists("child1"), "Value 'child1' should not exist after Clear().");
        Assert.IsFalse(head.Exists("child2"), "Value 'child2' should not exist after Clear().");
        Assert.IsFalse(head.Exists("child3"), "Value 'child3' should not exist after Clear().");
    }

    [TestMethod]
    public void Clear_ClosesLoop_OfRemovedNodes()
    {
        // Arrange
        var head = new Node<int>(100);
        head.Append(200);
        head.Append(300);
        var removedStartNode = head.Next;
        var removedEndNode = head.Next.Next;
        // Act
        head.Clear();
        // Assert
        Assert.AreEqual(removedStartNode, removedEndNode.Next, "After Clear(), the last removed node should point to the first removed node, closing the loop.");
    }

    [TestMethod]
    public void Clear_OnSingleNodeList_DoesNothing()
    {
        // Arrange
        var head = new Node<int>(42);
        // Act
        head.Clear();
        // Assert
        Assert.AreEqual(head, head.Next, "After Clear() on single node list, head's Next should still point to itself.");
        Assert.IsTrue(head.Exists(42), "Value 42 should still exist after Clear() on single node list.");
    }

    [TestMethod]
    public void Clear_IsololatesCurrentNode()
    {
        // Arrange
        var head = new Node<int>(1);
        head.Append(2);
        head.Append(3);

        // Act
        head.Clear();
        // Assert
        Assert.AreEqual(head, head.Next, "After Clear(), head's Next should point to itself.");
        Assert.AreEqual(1, head.Value, "Head node's value should remain unchanged after Clear().");
    }

    [TestMethod]
    public void ToString_ReturnsValueStringRepresentation()
    {
        // Arrange
        string value = "TestNode";
        var node = new Node<string>(value);
        // Act
        string result = node.ToString();
        // Assert
        Assert.AreEqual(value, result, "ToString() should return the string representation of the node's value.");
    }

    [TestMethod]
    public void ToString_ReturnsValueIntegerRepresentation()
    {
        // Arrange
        int value = 12345;
        var node = new Node<int>(value);
        // Act
        string result = node.ToString();
        // Assert
        Assert.AreEqual(value.ToString(CultureInfo.InvariantCulture), result, "ToString() should return the string representation of the integer value.");
    }

    [TestMethod]
    public void ToString_ReturnsEmptyString_ForNullValue()
    {
        // Arrange
        string? value = null;
        var node = new Node<string>(value!);
        // Act
        string result = node.ToString();
        // Assert
        Assert.AreEqual(string.Empty, result, "ToString() should return an empty string for null value.");
    }
}