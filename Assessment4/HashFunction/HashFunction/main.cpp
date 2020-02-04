//  main.cpp

#include "hash.h"
#include <iostream>
//#include <string>

int main() 
{
	//  Gives the hashmap a value of 99 at location "Terry".
	tHashmap<std::string, int> favoriteNumbersByName;
	favoriteNumbersByName["Terry"] = 99;

	//  Gives the key and values of the other hashmap.
	tHashmap<std::string, int> mapCpy(favoriteNumbersByName);

	//  Outputs the values at location "Terry" in both maps.
	std::cout << favoriteNumbersByName["Terry"] << std::endl;
	std::cout << mapCpy["Terry"] << std::endl;

	while (true) 
	{

	}
}
