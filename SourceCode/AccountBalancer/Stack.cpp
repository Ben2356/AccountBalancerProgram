//AUTHOR: BENJAMIN MORENO
//LAST UPDATE: 1/10/2016

#include "Stack.h"


//constructor initializes a stack of length size
FunctionPointerStack::FunctionPointerStack(int length)
	: m_length(length), m_currentLength(0)
{
	//initialize the entire array and set each of its elements to 0 (null pointer)
	m_fptrArray = new fPtr[m_length];
	for (int i = 0; i < length; i++)
	{
		m_fptrArray[i] = 0;
	}
}

//destructor for deallocating the dynamically allocated array
FunctionPointerStack::~FunctionPointerStack()
{
	delete[] m_fptrArray;
}

bool FunctionPointerStack::IsEmpty()
{
	return m_currentLength == 0 ? true : false;
}

//array resize operation
void FunctionPointerStack::Resize()
{
	//update the length with the new length
	m_length *= 2;

	//create the new array
	fPtr *tmp = new fPtr[m_length];

	//copy the old array elements into the new array
	for (int i = 0; i < m_length; i++)
	{
		//initialize the array with null pointers for non data elements elements
		if (i >= m_length)
			tmp[i] = 0;

		//otherwise assign the actual data to new array
		else
			tmp[i] = m_fptrArray[i];
	}

	//delete the old array
	delete[] m_fptrArray;

	//assign the stack array pointer to the new array
	m_fptrArray = tmp;
}

void FunctionPointerStack::Push(fPtr funct)
{
	//need to add another element to the array
	m_currentLength++;

	//check if the size of the array is large enough, if not then double the array
	if (m_currentLength >= m_length)
	{
		Resize();
	}

	//add the value to the array
	m_fptrArray[m_currentLength] = funct;
}

void FunctionPointerStack::Pop()
{
	//first check to make sure the stack is not empty if it is then throw an exception
	if (IsEmpty())
		throw "EXCEPTION - POP OPERATION ON EMPTY STACK";
	else
	{
		//remove the value from the array (value is set to 0)
		m_fptrArray[m_currentLength] = 0;

		//update the currentLength
		m_currentLength--;
	}
}

//calls the function at the top of the stack
void FunctionPointerStack::Peek()
{
	//if there is nothing on the stack then throw an exception
	return m_currentLength == 0 ? throw "EXCEPTION - PEEK OPERATION ON EMPTY STACK" : (*m_fptrArray[m_currentLength])();
}

//DEBUG OPERATIONS
int FunctionPointerStack::GetArrayLength() { return m_length; }
int FunctionPointerStack::GetUsedLength() { return m_currentLength; }
