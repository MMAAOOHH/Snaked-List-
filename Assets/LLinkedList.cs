using System.Collections;
using System.Collections.Generic;

public class LLinkedList<T> 
{
    private T[] _items;
    private int _count;
    private LListNode<T> head;
    
    public LLinkedList()
    {
        head = null;
    }

    public void AddLast(T newItem)
    {
        LListNode<T> _newItem = new LListNode<T>(newItem);
        _items[_count++] = newItem;
        _newItem.next = null;
        
        if (head == null)
        {
            head = new LListNode<T>(newItem);
            return;
        }

        LListNode<T> temp = head;
        while (temp.next != null)
        {
            temp = temp.next;
        }
        temp.next = _newItem;
    }

    public T this[int index]
    {
        get {
            return _items[index];
        }
        set {
            _items[index] = value;
        }
    }
    
    
    public int Count {
        get {
            return _count;
        }
    }
}

public class LListNode<T>
{
    private T _item;
    public LListNode<T> next;

    public LListNode(T item)
    {
        _item = item;
        next = null;
    }
}
