//AUTHOR: BENJAMIN MORENO
//LAST UPDATE: 1/10/2016

#ifndef STACK_H
#define STACK_H

//function signature should have no return type and no parameters
typedef void(*fPtr)();

//for use with function pointers
class FunctionPointerStack
{
private:
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
	void Peek();

	//DEBUG OPERATIONS
	int GetArrayLength();
	int GetUsedLength();
};
#endif
