using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleLinkedList<T> : IEnumerable<T>
{
    private class Node
    {
        public T Value { get; }
        public Node Next { get; }

        public Node(T value, Node next)
        {
            Value = value;
            Next = next;
        }
    }

    private Node _head;

    public SimpleLinkedList() { }

    public SimpleLinkedList(T value) => Push(value);

    public SimpleLinkedList(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            Push(value);
        }
    }

    public int Count { get; private set; }

    public void Push(T value)
    {
        _head = new Node(value, _head);
        Count++;
    }

    public T Pop()
    {
        if (_head == null)
            throw new InvalidOperationException();

        var value = _head.Value;
        _head = _head.Next;
        Count--;
        return value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}