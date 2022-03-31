using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);
        ProcessMessage(msg);
    }

    void ProcessMessage(string msg) {
        if (msg != "Failed to find MPU6050 chip" && msg != "MPU6050 Found!")
        {
            string[] components = msg.Split('|');
            if (components[0] == "FNG")
                ProcessFingers(components);
            else if (components[0] == "MPU")
                ProcessMPU(components);
        }
    }

    void ProcessFingers(string[] rawValues) { 
        
    }

    void ProcessMPU(string[] rawValues)
    {

    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
