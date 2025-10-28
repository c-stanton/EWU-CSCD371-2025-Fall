using System.Collections;
using System.Collections.Generic;

namespace GenericsHomeWork;

public class Node<T> : ICollection<T>
{
    public T Value { get; set; }
    public Node<T>? Next { get; private set; }

    public Node(T value)
    {

        Value = value;
        Next = this;
    }

    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

    private void SetNext(Node<T>? nextNode)
    {
        Next = nextNode;
    }

    public void Add(T item)
    {
        Append(item);
    }

    public bool Contains(T item)
    {
        return Exists(item);
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

    // The Clear method breaks the circular loop of the removed nodes by setting the last node's Next
    // to null and isolates the removed nodes for garbage collection

    public void Clear()
    {
        if (this.Next == this)
        {
            return;
        }

        Node<T> endNode = this.Next!;

        while (endNode.Next != this)
        {
            endNode = endNode.Next!;
        }

        endNode.SetNext(null);
        this.SetNext(this);
    }

    public int Count
    {
        get
        {
            int count = 0;
            Node<T>? currNode = this.Next;
            while (currNode != null && currNode != this)
            {
                count++;
                currNode = currNode.Next;
            }
            return count;
        }
    }

    public bool IsReadOnly => false;

    public bool Remove(T value)
    {
        Node<T>? prevValue = this;
        Node<T>? currValue = this.Next;

        while (currValue != null && currValue != this)
        {
            if (EqualityComparer<T>.Default.Equals(currValue.Value, value))
            {
                prevValue.SetNext(currValue.Next);
                currValue.SetNext(null);
                return true;
            }
            prevValue = currValue;
            currValue = currValue.Next;
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), "arrayIndex cannot be negative.");
        } 

        if (Count > array.Length - arrayIndex)
        {
            throw new ArgumentException("Destination array is too small.");
        }

        Node<T>? currentValue = this.Next;
        while (currentValue != null && currentValue != this)
        {
            array[arrayIndex++] = currentValue.Value;
            currentValue = currentValue.Next;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? currentValue = this.Next;
        while (currentValue != null && currentValue != this)
        {
            yield return currentValue.Value;
            currentValue = currentValue.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}