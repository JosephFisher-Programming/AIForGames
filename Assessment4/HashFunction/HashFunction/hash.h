//  hash.h
#pragma once

#include <limits>
#include <string>

template<typename T>
size_t hash(const T& val)
{
	//  Unelegant way of telling users to specialize this template.
	T::please_specialize_this_template_for_your_type;
}

//  Template specialization for int.
template<>
size_t hash<int>(const int& val)
{
	// Knuth's hash function
	return val * 2654435761 % std::numeric_limits<size_t>::max();
}

//  Template specialization for char.
template<>
size_t hash<char>(const char& val)
{
	return (int)val * 2654435761 % std::numeric_limits<size_t>::max();
}

//  Template specialization for C-string.
template<>
size_t hash<char*>(char * const& val)
{
	int count = 0;
	int sum = 0;
	while (val[count] != '\0')
	{
		sum += (int)val[count];
		count++;
	}
	return sum * 2654435761 % std::numeric_limits<size_t>::max();
}

//  Template specialization for strings.
template<>
size_t hash<std::string>(const std::string& val)
{
	int count = 0;
	int sum = 0;
	while (val.c_str()[count] != '\0')
	{
		sum += (int)val.c_str()[count];
		count++;
	}
	return sum * 2654435761 % std::numeric_limits<size_t>::max();
}


//  Class used to create hashmaps that stores values with a key input.
template<typename K, typename V>
class tHashmap
{
	V * data;                       //  Buffer holding all potential pairs in the hashmap.
	size_t dataCapacity;            //  Size of the above buffers.

public:
	tHashmap();                     //  Constructs the hashmap with a specific size.
	tHashmap(size_t);               //  Constructs the hashmap with an unspecific size.
	~tHashmap();                    //  Cleans-up any underlying data.
	tHashmap(const tHashmap&);
	tHashmap& operator=(const tHashmap&);

	V& operator[] (const K& key);   //  Returns the object at the given key.
};

// Gives the hashmap a capacity, and sets the data to that length.
template<typename K, typename V>
inline tHashmap<K, V>::tHashmap()
{
	dataCapacity = 10;
	data = new V[dataCapacity];
}

//  Gives the hashmap a player input length, and sets the data to that length.
template<typename K, typename V>
inline tHashmap<K, V>::tHashmap(size_t size)
{
	dataCapacity = size;
	data = new V[dataCapacity];
}

//  A standard destructor for a hashmap.
template<typename K, typename V>
inline tHashmap<K, V>::~tHashmap()
{
	delete(data);
}

//  Sets the hashmap to the values of the other hashmap.
template<typename K, typename V>
inline tHashmap<K, V>::tHashmap(const tHashmap & otherMap)
{
	data = new V[otherMap.dataCapacity];
	dataCapacity = otherMap.dataCapacity;
	for (int i = 0; i < otherMap.dataCapacity; i++) 
	{
		data[i] = otherMap.data[i];
	}
}

//  An overloaded operator to set the values equal to the other map.
template<typename K, typename V>
inline tHashmap<K, V> & tHashmap<K, V>::operator=(const tHashmap & otherMap)
{
	this = new tHashmap(otherMap);

	return this;
}

//  An overloaded operator that checks a specific point in the array based on the key.
template<typename K, typename V>
inline V & tHashmap<K, V>::operator[](const K & key)
{
	auto keyVal = hash(key) % dataCapacity;
	return data[keyVal];
}
