namespace GenericsHomeWork;

public class Node<T>
{
    public T Value { get; set; }
    public Node<T> Next { get; private set; }

    public Node(T value)
    {
       
        Value = value;
        Next = this;
    }

    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

    private void SetNext(Node<T> nextNode)
    {
        Next = nextNode;
    }

    public void Append(T value)
    {
        if (Exists(value))
        {
            throw new InvalidOperationException($"Value '{value}' already exists in the list.");
        }
        Node<T> newNode = new Node<T>(value);
        newNode.Next = this.Next;
        this.Next = newNode;
    }

    public bool Exists(T value)
    {
        Node<T> currentNode = this;
        do
        {
            if (EqualityComparer<T>.Default.Equals(currentNode.Value, value))
            {
                return true;
            }
            currentNode = currentNode.Next;
        } while (currentNode != this);
        return false;
    }

    public void Clear()
    {
        if (this.Next == this)
        {
            return;
        }

        Node<T> firstRemoved = this.Next;
        this.SetNext(this);
        Node<T> currentNode = firstRemoved;
        while (currentNode.Next != this.Next)
        {
            if (currentNode.Next == this)
            {
                break;
            }
            currentNode = currentNode.Next;
        }
        currentNode.SetNext(firstRemoved);
    }
}



