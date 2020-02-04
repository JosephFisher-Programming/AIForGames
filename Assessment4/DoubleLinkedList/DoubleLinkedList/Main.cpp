//  Main.cpp

#include <iostream>
#include "DoubleLinkedLists.h"

int main() 
{

	//  Creates a list with values pushed to the back.
	tList<int> list;
	list.push_back(3);
	list.push_back(6);
	list.push_back(6);
	list.push_back(3);
	list.push_back(3);
	list.push_back(3);
	list.push_back(3);
	list.push_back(7);
	list.push_back(6);

	//  Resizes the list to a length of three.
	list.resize(3);

	//  Creates a new list where the values get pushed to the front.
	tList<int> backList;
	backList.push_front(100);
	backList.push_front(12);
	backList.push_front(130);
	backList.push_front(110);

	//  Pops the back of list off and resets the tail.
	backList.pop_back();

	//  Pops the front of list off and resets the head.
	backList.pop_front();

	//  Clears the rest of the list.
	backList.clear();

	//  Checks if either list are empty and outputs a boolean.
	std::cout << backList.empty() << std::endl;
	std::cout << list.empty() << std::endl;

	//  Returns the front and back of the list.
	std::cout << list.front() << std::endl;
	std::cout << list.back() << std::endl;

	//  Creates a copy of the main list and puts it into the new one.
	tList<int> cpyList(list);

	//  Removes all threes in the array, then pops the front and back.
	list.remove(3);
	list.pop_back();
	list.pop_front();

	//  Iterates trough the whole list and writes Joe to the console each time.
	for (auto it = cpyList.begin(); it != cpyList.end(); ++it)
	{
		std::cout << "Joe" << std::endl;
	}

	//  A loop to keep the console open.
	while (true) 
	{

	}
	return 0;
}