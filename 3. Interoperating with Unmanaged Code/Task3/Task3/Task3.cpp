#include "stdafx.h"
#include <iostream>
#import "..\..\Task1\Task1\bin\Debug\DevicePowerManager.tlb" 

using namespace DevicePowerManager;
using namespace std;

int main()
{
	CoInitialize(NULL);

	IDevicePowerPtr devicePower;

	HRESULT hRes = devicePower.CreateInstance(__uuidof(DevicePower));

	if (hRes == S_OK)
	{
		// Call .net function as com object.
		__int64 time = devicePower->GetLastSleepTime();

		std::cout << "Last sleep time = " << time << endl;
	}

	CoUninitialize();

	return 0;
}

