//AUTHOR: BENJAMIN MORENO
//LAST UPDATE: 1/12/2016

#ifndef STACK_H
#define STACK_H

#include <Windows.h>

//for use with function pointers
class FunctionPointerStack
{
private:
	//function signature should have the console window handle, program stack as arguments and no return type
	typedef void(*fPtr)(HANDLE &hConsole, FunctionPointerStack &cProgram);

	int m_length;
	int m_currentLength;
	fPtr *m_fptrArray;

public:
	FunctionPointerStack(int length);
	~FunctionPointerStack();

	bool IsEmpty();
	void Resize();
	void Push(fPtr funct);
	void Pop();
	void Peek(HANDLE &hConsole, FunctionPointerStack &cProgram);

	//DEBUG OPERATIONS
	int GetArrayLength();
	int GetUsedLength();
};
#endif
