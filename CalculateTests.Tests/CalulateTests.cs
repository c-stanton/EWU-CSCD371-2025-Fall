using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculate;
using System.Transactions;

namespace CalculateTests.Tests;

[TestClass]
public sealed class CalulateTests
{
    [TestMethod]
    public void Properties_SetAtConstruction()
    {
        //Arrange
        string? output1 = null;
        string? output2 = null;

        string nameInput = "Messi";

        var program = new Program
        {
            WriteLine = r =>
            {
                if(output1 == null)
                {
                    output1 = r;
                } else {
                    output2 = r;
                }
            },
            ReadLine = () => nameInput
        };

        //Act
        program.WriteLine("Enter name:");
        string? input = program.ReadLine();
        program.WriteLine($"Hey There, {input}");

        //Assert
        Assert.AreEqual("Enter name:", output1);
        Assert.AreEqual("Hey There, Messi", output2);
        Assert.AreEqual("Messi", input);
    }

}
