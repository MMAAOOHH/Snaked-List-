using System.Collections;
using System.Collections.Generic;

public class LLinkedList<T> 
{
    private const int INITIAL_CAPACITY = 8;
    private const int MAXIMUM_GROWTH = 16;
    
    private LListNode<T> head;
    
    private T[] _items;
    private int _count;
    private int _capacity;
    
    public LLinkedList(int capacity = INITIAL_CAPACITY)
    {
        head = null;
        _items = new T[capacity];
    }

    public void AddLast(T item)
    {
        manageSize();
        _items[_count++] = item;
        LListNode<T> _newItem = new LListNode<T>(item);
        _newItem.next = null;
        
        if (head == null)
        {
            head = new LListNode<T>(item);
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
    
    public int IndexOf(T item)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (object.Equals(item, _items[i]))
            {
                return i;
            }
        }
        return -1;
    }
    
    public bool Contains(T item)
    {
        return IndexOf(item) != -1;
    }
    
    private void manageSize()
    {
        if (_count == _items.Length)
        {
            _capacity = _items.Length < MAXIMUM_GROWTH ? _items.Length * 2 : _items.Length + MAXIMUM_GROWTH;
            T[] expanded = new T[_capacity];
            _items.CopyTo(expanded, 0);
            _items = expanded;
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
