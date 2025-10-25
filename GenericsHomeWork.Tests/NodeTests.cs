namespace GenericsHomeWork.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericsHomeWork;
using System;


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
    }
    
