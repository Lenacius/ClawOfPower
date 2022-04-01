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
            connected = true; // This improve calibration
            string[] components = msg.Split('|');
            if (components[0] == "FNG")
                ProcessFingers(components);
            else if (components[0] == "MPU")
                ProcessMPU(components);
        }
    }

    // Finger bend processing
    public float[] fingerBend = { 0, 0, 0, 0, 0 };
    void ProcessFingers(string[] rawValues) {
        for (int x = 1; x < 6; x++) { // First value is the identifier FNG
            fingerBend[x - 1] = float.Parse(rawValues[x], CultureInfo.InvariantCulture);
//            Debug.Log("Finger " + x + ':' + fingerBend[x - 1]);
        }
    }

    // MPU6050 processing
    public Vector3 rawGyro;
    void ProcessMPU(string[] rawValues)
    {
        rawGyro.x = float.Parse(rawValues[1], CultureInfo.InvariantCulture);
        rawGyro.y = float.Parse(rawValues[2], CultureInfo.InvariantCulture);
        rawGyro.z = float.Parse(rawValues[3], CultureInfo.InvariantCulture);
//        Debug.Log("Gyroscope values:" + rawGyro.x + '|' + rawGyro.y + '|' + rawGyro.z);
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

    private void Update()
    {
        if(connected)
            if (!isCalibrated)
                Calibrate();
            else
                this.transform.Rotate((rawGyro - offGyro) * Time.deltaTime * 50f);

        FingersAnimator();
    }

    public Animator thumbAnimator;
    public Animator indexAnimator;
    public Animator middleAnimator;
    public Animator ringAnimator;
    public Animator pinkyAnimator;
    void FingersAnimator()
    {
        thumbAnimator.speed = 0f;
        indexAnimator.speed = 0f;
        middleAnimator.speed = 0f;
        ringAnimator.speed = 0f;
        pinkyAnimator.speed = 0f;

        thumbAnimator.Play("ThumbFingerAnimation", 0, fingerBend[0] / 100.0f);
        indexAnimator.Play("IndexFingerAnimation", 0, fingerBend[1] / 100.0f);
        middleAnimator.Play("MiddleFingerAnimation", 0, fingerBend[2] / 100.0f);
        ringAnimator.Play("RingFingerAnimation", 0, fingerBend[3] / 100.0f);
        pinkyAnimator.Play("PinkyFingerAnimation", 0, fingerBend[4] / 100.0f);
    }

    // Connection check
    bool connected = false;
    // Calibration configuration
    float calibrationTime = 5.0f;
    bool isCalibrated = false;
    int ammountOfValues = 0;

    public Vector3 offGyro;

    void Calibrate()
    {
        calibrationTime -= Time.deltaTime;
        if (calibrationTime > 0)
        {
            offGyro += rawGyro;
            ammountOfValues++;
        }
        else
        {
            offGyro /= ammountOfValues;
            isCalibrated = true;
        }
    }
}
