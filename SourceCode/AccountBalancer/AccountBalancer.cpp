//AUTHOR: BENJAMIN MORENO 
//LAST UPDATED: 1/11/16

/*TODO:
* 
*/

#include "Stack.h"	//contains the stack data structure
#include <iostream>
#include <string>
#include <cctype>
#include <vector>
#include <iomanip>
#include <Windows.h>

const double EPSILON = 0.001;

using namespace std;

//simple function to set the cursor position on the console window
void SetCursorPosition(HANDLE &hConsole, int xStart, int yStart)
{
	COORD cursorStartLocation = { xStart,yStart };
	SetConsoleCursorPosition(hConsole, cursorStartLocation);
}

//Clearing the console screen using WIN32 API
void ClearScreen(HANDLE &hConsole, int xStart = 0, int yStart = 0, int clearLength = 0)
{
	DWORD cCharsWritten;
	CONSOLE_SCREEN_BUFFER_INFO cConsoleBuffer;
	DWORD overwriteLength;
	COORD homeCoords = { 0,0 };
	COORD startLine = { xStart,yStart };
	COORD cursorStartLine = { 0,yStart + 1 };

	//Get the number of character cells in buffer
	if (!GetConsoleScreenBufferInfo(hConsole, &cConsoleBuffer))
		return;

	//no specification thus default
	if (clearLength == 0)
		overwriteLength = cConsoleBuffer.dwSize.X * cConsoleBuffer.dwSize.Y;
	else
		overwriteLength = clearLength;

	//Fill the entire screen with blanks
	if (!FillConsoleOutputCharacter(hConsole, (TCHAR) ' ', overwriteLength, startLine, &cCharsWritten))
		return;

	//Get current text attribute
	if (!GetConsoleScreenBufferInfo(hConsole, &cConsoleBuffer))
		return;

	//Set the buffer's attributes accordingly
	if (!FillConsoleOutputAttribute(hConsole, cConsoleBuffer.wAttributes, overwriteLength, homeCoords, &cCharsWritten))
		return;

	//Move the cursor after the where the clearing starts
	if (xStart != 0)
		SetCursorPosition(hConsole, cursorStartLine.X, cursorStartLine.Y);
	else
		SetCursorPosition(hConsole, 0, 0);
}

enum COLOR
{
	BLACK,
	DARK_BLUE,
	DARK_GREEN,
	DARK_TERQUIOSE,
	DARK_RED,
	DARK_PURPLE,
	DARK_YELLOW,
	DEFAULT,
	GREY,
	BLUE,
	GREEN,
	TERQUIOSE,
	RED,
	PURPLE,
	YELLOW,
	WHITE
};

//simple function to set color of text that will stay until changed again
inline void SetTextColor(HANDLE &hConsole, WORD wAttribute = DEFAULT)
{
	if (!SetConsoleTextAttribute(hConsole, wAttribute))
		return;
}

//handles going back to a previous step in the program through stack operations
void GoBack(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	cProgram.Pop();
	cProgram.Peek(hConsole,cProgram);
}

//added functionality of clearing the invalid input off the input line and allowing the user to re-enter data in that same spot and ability to recognize command codes
float GetInput(int startX, int line, HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	bool isBlank = false;
	bool isInvalid = false;
	float input;
	char firstChar = cin.get();

	//if the first char of the unformatted input is a newline then the enter key was just pressed and input is blank
	if (firstChar == '\n')
		isBlank = true;

	//check to see if there is a special command being entered, special commands begin with a '/' (47)
	else if (firstChar == 47)
	{
		char commandChar = cin.get();
		
		//using switch statement to make it easier to extend command library later
		switch (commandChar)
		{
			case 'b':
				//make sure that this step is not the first
				if (cProgram.GetUsedLength() == 1)
				{
					isInvalid = true;
					break;
				}

				else
					//function execution to this point is thrown out when current caller function is popped from the stack
					GoBack(hConsole,cProgram);
			default:
				isInvalid = true;
				break;
		}
	}

	//else the input is potentially valid, put the extracted first char back and perform extraction on operation
	else
	{
		cin.putback(firstChar);
		cin >> input;
		cin.ignore(255, '\n');
	}

	//if numbers were entered then stream is fine and failbit = false
	//if numbers followed by chars entered then failbit = false and only the numbers are extracted leaving behind the characters which are then extracted and ignored and thus gcount == other characters left in stream after extraction
	//if chars entered then failbit = true and nothing could be extracted from the input stream
	while (cin.fail() || cin.gcount() > 1 || isBlank || isInvalid || input < 0)
	{
		
		//wipe the old output if there is anything there to wipe
		if (!isBlank || !isInvalid)
			ClearScreen(hConsole, startX, line);

		//reset the isBlank flag
		isBlank = false;

		//reset the isInvalid flag
		isInvalid = false;

		SetTextColor(hConsole, RED);
		cerr << "ERROR INVALID INPUT! PLEASE TRY AGAIN!" << endl;
		SetTextColor(hConsole);

		//reposition cursor back to beginning of input field
		SetCursorPosition(hConsole, startX, line);

		if (cin.fail())
		{
			cin.clear();
			cin.ignore();
		}
		firstChar = cin.get();
		if (firstChar == '\n')
			isBlank = true;
		else
		{
			cin.putback(firstChar);
			cin >> input;
			cin.ignore(255, '\n');
		}
	}
	return input;
}

//InputArray() V2 which now has the added functionality of clearing the invalid input off the input line and allowing the user to re-enter data in that same spot
vector<float> InputArray(vector<float> vInput, string strInput, int line, HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	int endValue;
	bool errorFlag = false;
	bool singleValue = false;
	bool isEmpty = false;
	bool isInvalid = false;
	do
	{
		int numOfDecimals = 0;

		//if the string is being set for the first time or there is an issue with the old input then get new input
		if (strInput.length() == 0 || errorFlag)
		{
			//if there is an error then clear the rest of the line out and reseat the cursor at beginning of input
			//starting X value of any GetInputArray input fields is 48
			if (errorFlag)
				ClearScreen(hConsole, 48, line,32);

			if(errorFlag || isEmpty)
				SetCursorPosition(hConsole, 48, line);

			//reset the isEmpty flag after throwing the error
			isEmpty = false;

			//reset isInvalid flag
			isInvalid = false;

			//use getline in order to deal with extraneous newline chars in the input stream
			getline(cin, strInput);

			//check to see if input was special command, starts with '/' (47)
			if (strInput[0] == 47)
			{
				//make sure a command char follows, if not will use isEmpty flag as the error flag is reset after this block
				if (strInput.length() != 2)
					isInvalid = true;

				else
				{
					//using switch statement to make it easier to extend command library later
					switch (strInput[1])
					{
						case 'b':
							GoBack(hConsole,cProgram);
						default:
							isInvalid = true;
							break;
					}
				}
			}
			
			if (strInput.length() == 0)
				isEmpty = true;
		}

		//reset the error flag after dealing with bad input
		errorFlag = false;

		//make sure the comma is not the first char of the input and the first char is not a space
		if (!isEmpty && !isInvalid && strInput.find(',') != -1 && strInput.find(',') != 0)
		{
			//handles the values on the left of the comma
			//check to see whether the input are digits only if not then get new input
			for (int i = 0; i < strInput.find(','); i++)
			{
				//count the number of decimal points
				if (strInput[i] == '.')
				{
					numOfDecimals++;

					//only 1 decimal point is allowed otherwise the input is invalid
					if (numOfDecimals > 1)
					{
						errorFlag = true;
						break;
					}
					continue;
				}

				if (!isdigit(strInput[i]))
				{
					errorFlag = true;
					break;
				}
			}
			endValue = strInput.find(',');
		}
		//if no comma found assume only one value, must check that value then insert it into array then set the singleValue flag to true
		else
		{
			if (!isEmpty && !isInvalid)
			{
				for (int i = 0; i < strInput.length(); i++)
				{
					if (strInput[i] == '.')
					{
						numOfDecimals++;

						//only 1 decimal point is allowed otherwise the input is invalid
						if (numOfDecimals > 1)
						{
							errorFlag = true;
							break;
						}
						continue;
					}

					if (!isdigit(strInput[i]))
					{
						errorFlag = true;
						break;
					}
				}
				endValue = strInput.length();
				singleValue = true;
			}
		}
		if (errorFlag || isEmpty || isInvalid)
		{
			SetTextColor(hConsole, RED);
			cerr << "ERROR INVALID INPUT! PLEASE TRY AGAIN!" << endl;
			SetTextColor(hConsole);
		}

	} while (errorFlag || isEmpty || isInvalid);

	//if it is good then put the float value into the vector
	string currentVal = strInput.substr(0, endValue);
	vInput.push_back(atof(currentVal.c_str()));

	//if the value entered was a single value then return the vector as there is no more data
	if (singleValue)
		return vInput;

	//now remove the comma and anything left of it
	string newInput = strInput.substr(endValue + 1, string::npos);

	//repeat the process for the value right of the comma using recursion
	return InputArray(vInput, newInput,line,hConsole,cProgram);
}

//wrapper function for GetInputArray()
vector<float> GetInputArray(int line, HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	vector<float> vInput(0);
	string strInput;
	return InputArray(vInput, strInput, line, hConsole, cProgram);
}

//vector addition
template <typename T>
float VectorAdd(vector<T> vData)
{
	float nSum = 0.0;
	for (int i = 0; i < vData.size(); i++)
	{
		nSum += vData[i];
	}
	return nSum;
}

//simple function to wait for user to press enter before proceeding
void WaitForUser(HANDLE &hConsole, FunctionPointerStack &cProgram, int commandStartLine = 10)
{
	cout << "\n\n\n\n" << "\t\t" << "Once you are ready, press enter to continue." << "\n\n" << "**Remember that you can go back to a previous step at any time by entering \"/b\"!";

	SetCursorPosition(hConsole, 0, commandStartLine);
	cout << "\n\n" << "\t\t\t\t Commands: ";
	string input;
	getline(cin, input);
	if (input[0] == 47 && input.length() == 2)
	{
		switch (input[1])
		{
		case 'b':
			GoBack(hConsole, cProgram);
			break;
		default:
			break;
		}
	}
}

//forms can be represented as 
/*
*	- an array/vector of structs		<--------- SELECTION
*	- an array/vector of values stored by Hash function
*
*
*/
/*	process will:
*	1)prompt user for Date/Check # (identifier)(no input validation)
*	2)check to see if user entered NA, if true then break loop and wipe out text on screen
*	3)display the identifier where it was entered 
*	4)prompt user for amount (input validation)(use GetInput()) (add element to vector)
*	5)loop to step 1
*/

struct FormEntry
{
	string identifier;
	float amount;
	FormEntry(string id="", float value=0.0)
	{
		identifier = id;
		amount = value;
	}
};

//stack reference
//returns a vector containing FormEntry objects
vector<FormEntry> GetFormEntry(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	const int cursorStartLocationY = 9;
	const int amountFieldXStart = 65;
	int currentEntryY;
	int entryCount = 0;
	string id;
	float amountValue;
	vector<FormEntry> vForm(0);
	while (1)
	{
		currentEntryY = cursorStartLocationY + entryCount;
		cout << "Date/Check #/Identifier: ";

		//using unformatted data to allow for spaces 
		getline(cin, id);

		//check to see if terminating char sequence
		if (!id.compare("NA") || !id.compare("na"))
		{
			//will wipe the line that it is currently on then, relocate cursor to one line lower, and exit
			ClearScreen(hConsole, 0, currentEntryY);
			SetCursorPosition(hConsole, 0, currentEntryY+1);
			return vForm;
		}

		//check to see if command, commands are also check in the amount field by the GetInput() function
		if (id[0] == '/')
		{
			//if there is no command char then its just an identifier
			if (id.length() == 2)
			{
				//check to see which command
				switch (id[1])
				{
				case 'b':
					GoBack(hConsole,cProgram);
				default:
					break;
				}
			}
		}

		//move cursor back to the current line
		SetCursorPosition(hConsole, 60, currentEntryY);

		cout << "Amount: $";
		amountValue = GetInput(amountFieldXStart, currentEntryY, hConsole, cProgram);

		//add the collected data to a FormEntry object and place that into the vector NOTE: NO dynamic allocation here
		vForm.push_back(FormEntry(id, amountValue));

		//once valid input is entered remove any error messages and move cursor back to current line
		ClearScreen(hConsole, 0, currentEntryY + 1);
		SetCursorPosition(hConsole, 0, currentEntryY+1);

		//cout << '\n';
		entryCount++;
	}
}

//vector addition that deals with FormEntry objects
float VectorAdd(vector<FormEntry> vData)
{
	float nSum = 0.0;
	for (int i = 0; i < vData.size(); i++)
	{
		nSum += vData[i].amount;
	}
	return nSum;
}

//function that determines if two floating point values are equal
inline bool isEqual(float x, float y)
{
	return std::abs(x - y) <= EPSILON ? true : false;
}

//when user types /b the program will go back to the previous step

//Each piece of the program will need to be bundled in a function, each with the same function signature
//using global variables to keep the passing of the needed variables into the function each step and keep the return and arguments matching to the stack's array type
//issue with using ellipsis is that global variables will still need to be present for vars that are defined within each step function as their return type must be void
//possible global vars:

//remove var and use number in place
//max value of X enterable || the entire line's length
const int consoleWindowX = 79;

float initBalance;
float otherDeductionsTotal;
float otherCreditsTotal;
float accountRegisterBalance;
float statmentEndingBalance;
float otherDepositsTotal;
float subtotal;
float formDataSum;


//parameters for each step are: HANDLE &hConsole, FunctionPointerStack &cProgram
void Step9(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	ClearScreen(hConsole);

	//create overview screen where the standing text throughout the program are displayed for review
	cout << "1) Account Register/Checkbook Balance:";
	cout << setw(31) << right << "$" << initBalance << endl;
	cout << "2) Other service charges or deductions:";
	cout << setw(30) << right << "$" << otherDeductionsTotal << endl;
	cout << "3) Other credits:";
	cout << setw(52) << right << "$" << otherCreditsTotal << endl;
	cout << '\n';
	SetTextColor(hConsole, WHITE);
	cout << "NEW ACCOUNT REGISTER BALANCE:";
	cout << setw(40) << right << "$" << accountRegisterBalance << endl;
	SetTextColor(hConsole);
	cout << "---------------------------------------------------" << endl;
	cout << "\n";
	cout << "4) Statement Ending Balance:";
	cout << setw(41) << right << "$" << statmentEndingBalance << endl;
	cout << "5) Other deposits:";
	cout << setw(51) << right << "$" << otherDepositsTotal << endl;
	cout << '\n';
	SetTextColor(hConsole, WHITE);
	cout << "SUBTOTAL...................:";
	cout << setw(41) << right << "$" << subtotal;
	SetTextColor(hConsole);
	cout << '\n';
	cout << "---------------------------------------------------" << endl;
	cout << '\n';
	cout << "6) Outstanding checks, ATM, Check Card, other electronic withdrawls:    $" << formDataSum << endl;
	cout << "\n\n";

	float finalTotal = subtotal - formDataSum;
	SetTextColor(hConsole, WHITE);
	cout << "7) After your TOTAL outstanding checks, ATM, Check Card, and other \n electronic withdrawls were subtracted from SUBTOTAL, your balance is: \t$" << finalTotal;
	SetTextColor(hConsole);
	cout << "\n\n\n";

	if (isEqual(finalTotal,accountRegisterBalance))
	{
		SetTextColor(hConsole, GREEN);
		cout << "\tCONGRATULATIONS!! YOUR BANK ACCOUNT IS BALANCED CORRECTLY!!" << endl;
		SetTextColor(hConsole);
	}
	else
	{
		SetTextColor(hConsole, RED);
		cout << "\t\t\tUH-OH!! SOMETHING ISN'T RIGHT!!" << endl;
		cout << "     Your value from STEP 7 should match your new Account Register Balance!";
		SetTextColor(hConsole);
	}

	//DEBUG
	//cout << "STACK SIZE: " << cProgram.GetUsedLength() << endl;

	bool errorFlag = false;
	string commands;
	cout << "\n" << "Type \"exit\" to quit the program" << "\n\n" << "Commands: ";
	do 
	{
		if (errorFlag)
		{
			ClearScreen(hConsole, 10, 24);
			SetCursorPosition(hConsole, 10, 24);
		}
		errorFlag = false;
		cin >> commands;

		//if command is valid then do operation else wipe the input field for another try
		if (commands.length() == 2 && commands[0] == '/')
		{
			switch (commands[1])
			{
			case 'b':
				cin.ignore();
				GoBack(hConsole,cProgram);
				break;
			default:
				errorFlag = true;
				break;
			}
		}

		//use exit to quit program 
		else if (!commands.compare("exit") || !commands.compare("Exit"))
		{
			break;
		}

		else
			errorFlag = true;
	} while (errorFlag);
}

//ISSUE: when going back and then inserting an invalid amount breaks input validation loop -- seemingly random
void Step8(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	ClearScreen(hConsole);

	//Each withdrawl will be entered one by one where the previous will appear listed with the Data/Check # and Amount title, to indicate that there are no more values the user enters NA or na
	//user enters Date/Check # first then the program will prompt for a Amount, values entered are listed immediately
	cout << "6) List all outstanding checks, ATM, Check Card, other electronic withdrawls:" << "\n\n" << "*Once you have finished entering all your data, type \"NA\" or \"na\" into the \n Date/Check #/Identifier field to finish this step";
	cout << "\n\n" << "**Data will be in the form: \n Date/Check #/ID: item name (can be blank) \t Amount: $123456" << "\n\n\n";
	vector<FormEntry> formData = GetFormEntry(hConsole,cProgram);
	formDataSum = VectorAdd(formData);
	SetTextColor(hConsole, WHITE);
	cout << setw(69) << right << "TOTAL AMOUNT: $" << formDataSum << endl;
	SetTextColor(hConsole);
	WaitForUser(hConsole,cProgram,formData.size()+14);
	cProgram.Push(Step9);
	cProgram.Peek(hConsole,cProgram);
}

void Step7(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	ClearScreen(hConsole);
	cout << "4) Statement Ending Balance:";
	cout << setw(41) << right << "$" << statmentEndingBalance << endl;
	cout << "5) Other deposits:";
	cout << setw(51) << right << "$" << otherDepositsTotal << endl;
	cout << '\n';

	subtotal = statmentEndingBalance + otherDepositsTotal;
	SetTextColor(hConsole, WHITE);
	cout << "SUBTOTAL...................:";
	cout << setw(41) << right << "$" << subtotal;
	SetTextColor(hConsole);
	WaitForUser(hConsole,cProgram,9);
	cProgram.Push(Step8);
	cProgram.Peek(hConsole,cProgram);
}

void Step6(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	ClearScreen(hConsole);
	cout << "4) Statement Ending Balance:";
	cout << setw(41) << right << "$" << statmentEndingBalance << endl;
	cout << '\n';

	cout << "5) Add any deposits not shown on this statement \n   (AS A COMMA SEPARATED LIST WITH NO SPACES): $";
	vector<float> otherDeposits = GetInputArray(3, hConsole, cProgram);
	ClearScreen(hConsole, consoleWindowX, 0);
	otherDepositsTotal = VectorAdd(otherDeposits);
	cProgram.Push(Step7);
	cProgram.Peek(hConsole,cProgram);
}

void Step5(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	
	ClearScreen(hConsole);
	cout << "NOW, with your Account Statement:" << "\n\n";
	cout << "4) List your Statement Ending Balance here: $";
	statmentEndingBalance = GetInput(45, 2, hConsole, cProgram);
	cProgram.Push(Step6);
	cProgram.Peek(hConsole,cProgram);
}

void Step4(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	//approach to deal with the extra newline char that remains in the input stream after step 5 is popped and this step is called again
	static int callCount = 0;
	callCount++;
	if (callCount > 1)
	{
		cin.get();
	}

	ClearScreen(hConsole);
	cout << "1) Account Register/Checkbook Balance:";
	cout << setw(31) << right << "$" << initBalance << endl;
	cout << "2) Other service charges or deductions:";
	cout << setw(30) << right << "$" << otherDeductionsTotal << endl;
	cout << "3) Other credits:";
	cout << setw(52) << right << "$" << otherCreditsTotal << endl;
	cout << '\n';

	accountRegisterBalance = (initBalance - otherDeductionsTotal) + otherCreditsTotal;
	SetTextColor(hConsole, WHITE);
	cout << "This is your NEW ACCOUNT REGISTER BALANCE:";
	cout << setw(27) << right << "$" << accountRegisterBalance;
	SetTextColor(hConsole);
	WaitForUser(hConsole,cProgram);
	cProgram.Push(Step5);
	cProgram.Peek(hConsole,cProgram);
}

void Step3(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	ClearScreen(hConsole);
	cout << "1) Account Register/Checkbook Balance:";
	cout << setw(31) << right << "$" << initBalance << endl;
	cout << "2) Other service charges or deductions:";
	cout << setw(30) << right << "$" << otherDeductionsTotal << endl;
	cout << '\n';

	cout << "3) List credits not previoulsy recorded that are listed on this statement \n   such as interest \n   (AS A COMMA SEPARATED LIST WITH NO SPACES): $";
	vector<float> otherCredits = GetInputArray(5, hConsole, cProgram);
	ClearScreen(hConsole, consoleWindowX, 1);
	otherCreditsTotal = VectorAdd(otherCredits);
	cProgram.Push(Step4);
	cProgram.Peek(hConsole,cProgram);
}

void Step2(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	ClearScreen(hConsole);
	cout << "1) Account Register/Checkbook Balance:";

	//35 is ideal setting on line width, size of text before must be taken into account though to properly space values
	cout << setw(31) << right << "$" << initBalance << endl;
	cout << '\n';

	cout << "2) Enter any other other service charges or other deductions not previously \n   recorded that are listed on this statement \n   (AS A COMMA SEPARATED LIST WITH NO SPACES): $";
	vector<float> otherDeductions = GetInputArray(4, hConsole, cProgram);
	ClearScreen(hConsole, consoleWindowX, 0);
	otherDeductionsTotal = VectorAdd(otherDeductions);

	cProgram.Push(Step3);
	cProgram.Peek(hConsole,cProgram);
}

void Step1(HANDLE &hConsole, FunctionPointerStack &cProgram)
{
	ClearScreen(hConsole);
	cout << "FIRST, start with your Account Register/Checkbook:" << "\n\n";
	cout << "1) List your Account Register/Checkbook Balance here: $";
	initBalance = GetInput(55, 2, hConsole, cProgram);
	cProgram.Push(Step2);
	cProgram.Peek(hConsole,cProgram);
}

int main()
{
	HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

	//initialize the stack with inital length of 9 as program will max out with 9 entries pushed on the stack at once
	FunctionPointerStack cProgram(9);

	//intro screen
	SetTextColor(hConsole, WHITE);
	cout << "\t\tWelcome to the Account Balancing Program, for use \n\t\t\twith your Bank of America account!" << "\n\n\n\n";
	SetTextColor(hConsole, YELLOW);
	cout << "**At any time if you need to go back to a step to change the value enter \"/b\"\n  into the input field then press enter!" << "\n\n\n\n\n";
	SetTextColor(hConsole);
	cout << "\t\t   Program created by Benjamin Moreno C 2016";

	//simple exception handling for operations that will crash program
	try
	{
		//to prevent user from using /b on first screen
		WaitForUser(hConsole, cProgram, 12);

		//start the program by pushing and executing first function, from there each function has the push and peek calls for the next function
		cProgram.Push(Step1);
		cProgram.Peek(hConsole,cProgram);
	}
	catch (char* e)
	{
		cerr << "EXCEPTION CAUGHT: " << e << endl;
	}
	cin.clear();
	cin.ignore(255, '\n');
	cin.get();
    return 0;
}