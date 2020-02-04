//  main.cpp

#include "tVector.h"
#include <iostream>

int main() 
{
	//  Creates a vector with values pushed to the back.
	tVector<int> vec;
	vec.push_back(6);
	vec.push_back(1);
	vec.push_back(2);
	vec.push_back(3);
	vec.push_back(3);
	vec.push_back(4);
	vec.push_back(7);
	vec.push_back(626);
	vec.push_back(11);
	vec.push_back(1);

	//  Outputs the values of the vector.
	for (int i = 0; i < vec.size(); i++)
	{
		std::cout << vec.at(i) << std::endl;
	}

	//  Creates a small space, then sorts the vector.
	std::cout << std::endl;
	vec.bubbleSort();
	std::cout << std::endl;

	//  Outputs the values of the vector again.
	for (int i = 0; i < vec.size(); i++)
	{
		std::cout << vec.at(i) << std::endl;
	}

	while(true)
	{

	}
	return 0;
}