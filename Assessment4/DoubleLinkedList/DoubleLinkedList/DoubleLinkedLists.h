#pragma once

// Templated doubly-linked list
template<typename T>
class tList
{
	struct Node
	{
		T data;                     // data for the element stored
		Node * prev;                // pointer to node following this node
		Node * next;                // pointer to node following this node
	};

	Node * head;                    // pointer to head of linked list
	Node * tail;                    // pointer to tail of linked list

	int listSize;

public:
	tList();                              // initializes head to null
	tList(const tList& other);            // copy-constructor
	tList& operator=(const tList &rhs);   // copy-assignment
	~tList();                             // delete all nodes upon destruction

	void push_front(const T& val);  // adds element to front (i.e. head)
	void pop_front();               // removes element from front
	void push_back(const T& val);   // adds element to back (i.e. before back)
	void pop_back();                // removes element from back

	T& front();                     // returns the element at the head
	const T& front() const;         // returns the element at the head (const)
	T& back();                      // returns the element at the tail
	const T& back() const;          // returns the element at the tail (const)

	void remove(const T& val);      // removes all elements equal to the given value

	bool empty() const;             // Returns true if there are no elements
	void clear();                   // Destroys every single node in the linked list
	void resize(size_t newSize);    // Resizes the linked list to contain the given number of elements
									// New elements are default-initialized

	class iterator
	{
		Node * cur;                                 // current node being operated upon

	public:
		iterator();                                 // initializes an empty iterator pointing to null
		iterator(Node * startNode);                 // initializes an iterator pointing to the given node

		bool operator==(const iterator& rhs) const; // returns true if the iterator points to the same node
		bool operator!=(const iterator& rhs) const; // returns false if the iterator does not point to the same node
		T& operator*();                             // returns a reference to the element pointed to by the current node
		const T& operator*() const;                 // returns a reference to the element pointed to by the current node
		iterator& operator++();                     // pre-increment (returns a reference to this iterator after it is incremented)
		iterator operator++(int);                   // post-increment (returns an iterator as it was before it was incremented)
		iterator& operator--();                     // pre-decrement (returns a reference to this iterator after it is decremented)
		iterator operator--(int);                   // post-decrement (returns an iterator as it was before it was decremented)
	};

	iterator begin();                               // returns an iterator pointing to the first element
	const iterator begin() const;                   // returns a const iterator pointing to the first element
	iterator end();                                 // returns an iterator pointing to one past the last element
	const iterator end() const;                     // returns a const iterator pointing to one past the last element
};

//  Standard constructor for doubly-linked lists.
template<typename T>
inline tList<T>::tList()
{
	head = nullptr;
	tail = nullptr;
	listSize = 0;
}

//  Creates a list based on the other list given.
template<typename T>
inline tList<T>::tList(const tList & other)
{
	if (other.head != nullptr)
	{
		Node * otherVal = other.head;
		iterator itera = other.head;
		while (itera != end())
		{
			itera++;
			push_back(otherVal->data);
			otherVal = otherVal->next;
		}
		listSize = other.listSize;
	}
}

//  An overloaded operator that sets the values of the list to the input list.
template<typename T>
inline tList<T> & tList<T>::operator=(const tList & rhs)
{
	if (rhs.head != nullptr)
	{
		clear();
		Node * otherVal = rhs.head;
		iterator itera = rhs.head;
		while (itera != end())
		{
			itera++;
			push_back(otherVal->data);
			otherVal = otherVal->next;
		}
		listSize = rhs.listSize;
	}

	return *this;
}

//  A destructor that deletes all of the data.
template<typename T>
inline tList<T>::~tList()
{
	clear();
}

//  A function that adds the values to the front of the list.
template<typename T>
inline void tList<T>::push_front(const T & val)
{
	Node* newVal = new Node();
	newVal->data = val;
	newVal->next = this->head;
	if(head != nullptr)
	{
		this->head->prev = newVal;
	}
	this->head = newVal;
	if (tail == nullptr) 
	{
		tail = head;
	}
	listSize++;
}

//  Pops the front of the list off and resets the head.
template<typename T>
inline void tList<T>::pop_front()
{
	if (head != nullptr)
	{
		Node * N = head;
		head = head->next;
		if (head != nullptr)
		{
			head->prev = nullptr;
		}
		delete(N);

		listSize--;
	}
}

//  Pushes the value to the back of the list and changes the tail.
template<typename T>
inline void tList<T>::push_back(const T & val)
{
	if (head == nullptr)
	{
	push_front(val);
	}
	else if (tail != nullptr)
	{
		Node * newVal = new Node;
		newVal->data = val;
		tail->next = newVal;
		tail->next->prev = tail;
		tail = tail->next;
		tail->next = nullptr;
		listSize++;
	}
	else
	{
		Node * newVal = new Node;
		newVal->data = val;
		head->next = newVal;
		tail->prev = head;
		tail = newVal;
		tail->next = nullptr;
		listSize++;
	}
}

//  Pops the back of the list and changes the tail.
template<typename T>
inline void tList<T>::pop_back()
{
	if (tail->prev != nullptr) 
	{
		tail = tail->prev;
		Node * delVal = tail->next;
		delete(delVal);
		tail->next = nullptr;
		listSize--;
	}	
	else 
	{
		tail = nullptr;
	}
}

//  Returns the data at the head.
template<typename T>
inline T & tList<T>::front()
{
	return head->data;
}

//  Returns a const version of the data at the head.
template<typename T>
inline const T & tList<T>::front() const
{
	return head->data;
}

//  Returns the data at the tail.
template<typename T>
inline T & tList<T>::back()
{
	return tail->data;
}

//  Returns a const version of the data at the tail.
template<typename T>
inline const T & tList<T>::back() const
{
	return tail->data;
}

//  Removes all of the Nodes and fixes the rest of the list.
template<typename T>
inline void tList<T>::remove(const T & val)
{
	while (head->data == val && head != nullptr)
	{
		pop_front();
	}
	while (tail->data == val && tail != nullptr)
	{
		pop_back();
	}
	Node * iter = head;
	while (iter->next != nullptr || head != nullptr || iter != nullptr)
	{
		if (iter->data == val)
		{
			Node *deleted = iter;
			iter = iter->next;
			iter->prev = deleted->prev;
			if (deleted == tail) 
			{
				tail = tail->prev;
			}
			delete(deleted);
			listSize--;
		}
		else if (iter->next != nullptr && iter->next->data == val)
		{
			Node *deleted = iter->next;
			iter->next = iter->next->next;
			if (iter->next != nullptr && iter->next->next != nullptr)
			{
				deleted->next->prev = iter;
			}
			if (deleted == tail)
			{
				tail = tail->prev;
			}
			delete(deleted);
			listSize--;
		}
		else if (iter == nullptr || iter->next == nullptr)
		{
			break;
		}
		else
		{
			iter = iter->next;
		}

		if (tail->next != nullptr) 
		{
			tail->next = nullptr;
		}

	}
}

//  Checks to see if the list is empty.
template<typename T>
inline bool tList<T>::empty() const
{
	if (head != nullptr)
	{
		return false;
	}
	return true;
}

//  Clears the list of all values and sets the tail value to null.
template<typename T>
inline void tList<T>::clear()
{
	while (head != nullptr)
	{
		pop_front();
	}
	tail = nullptr;
}

//  Resizes the list by poping the back of the array until the size is right.
template<typename T>
inline void tList<T>::resize(size_t newSize)
{
	while (listSize > newSize)
	{
		pop_back();
	}
}

//  Returns an iterator pointing at the head of the list.
template<typename T>
inline typename tList<T>::iterator tList<T>::begin()
{
	return iterator(head);
}

//  Returns a const iterator pointing at the head of the list.
template<typename T>
inline typename const tList<T>::iterator tList<T>::begin() const
{
	return iterator(head);
}

//  Returns an iterator pointing at the end of the list.
template<typename T>
inline typename tList<T>::iterator tList<T>::end()
{
	return iterator(nullptr);
}

//  Returns a const iterator pointing at the end of the list.
template<typename T>
inline typename const tList<T>::iterator tList<T>::end() const
{
	return iterator(nullptr);
}

//  Default constructor that sets the cur pointer to null.
template<typename T>
inline tList<T>::iterator::iterator()
{
	cur = nullptr;
}

//  A constructor that starts the iterator at the specified node.
template<typename T>
inline tList<T>::iterator::iterator(Node * startNode)
{
	cur = startNode;
}

//  An overloaded operator that returns a boolean on whether the two values are the same.
template<typename T>
inline bool tList<T>::iterator::operator==(const iterator & rhs) const
{
	if (rhs.cur == cur)
	{
		return true;
	}
	return false;
}

//  An overloaded operator that returns a boolean on whether the two values are not the same.
template<typename T>
inline bool tList<T>::iterator::operator!=(const iterator & rhs) const
{
	if (rhs.cur == cur)
	{
		return false;
	}
	return true;
}

//  An overloaded operator that returns the pointer to the data.
template<typename T>
inline T & tList<T>::iterator::operator*()
{
	return cur->data;
}

//  An overloaded operator that returns a const version of the pointer to the data.
template<typename T>
inline const T & tList<T>::iterator::operator*() const
{
	return cur -> data;
}

//  An overloaded operator that moves the iterator down to the next value in the list before incrementation.
template<typename T>
inline typename tList<T>::iterator & tList<T>::iterator::operator++()
{
	cur = cur->next;
	return *this;
}

//  An overloaded operator that moves the iterator down to the next value in the list after incrementation.
template<typename T>
inline typename tList<T>::iterator tList<T>::iterator::operator++(int)
{
	cur = cur->next;
	return *this;
}

//  An overloaded operator that moves the iterator up the list to the previous value in the list before incrementation.
template<typename T>
inline typename tList<T>::iterator & tList<T>::iterator::operator--()
{
	cur = cur->prev;
	return *this;
}

//  An overloaded operator that moves the iterator up the list to the previous value in the list after incrementation.
template<typename T>
inline typename tList<T>::iterator tList<T>::iterator::operator--(int)
{
	cur = cur->prev;
	return *this;
}
