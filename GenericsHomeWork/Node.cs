namespace GenericsHomeWork;

public class Node<T>
{
    //Any type of value <T> can be stored in this node
    public T Value { get; set; }
    public Node<T> Next { get; private set; } //Must make setter private

    public Node(T value)
    {
        //Takes in the value to be stored, sets it to the Value propert, 
        // then assignes the next property to point to 'this', pointing to itself in this case
        Value = value;
        Next = this;
    }

    //override the ToString() method to return the string representation of the value stored in the node. 
    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

    public void Append(T value)
    {
        //Takes value as parameter, creates a new node with passed value, 
        // adds it to the end of the list then sets it to this.Next which points to the first node in the list. 

        Node<T> newNode = new Node<T>(value);
        newNode.Next = this.Next;
        this.Next = newNode;
    }
}



