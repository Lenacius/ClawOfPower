using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public ArduinoListener ardListener;

    public GameObject hand;

    //public Vector3 gravity;
    //public Vector3 res;
    //public Vector3 resAccel;
    //public float g = 10.81f;
    private void Update()
    {
        if (connected)
            if (!isCalibrated)
                Calibrate();
            else
            {
                this.transform.Rotate((ardListener.rawAccelerometer[0] - offGyro) * Time.deltaTime * 50f);
                    
                //this.transform.position += (rawAccelerometer - (this.rawAccelerometer.normalized * g) - offAccel) * Time.deltaTime;]
                //rawAccelerometer.y = 0;
                //this.transform.position += (rawAccelerometer) * Time.deltaTime;
                //at.text = (rawAccelerometer - (this.rawAccelerometer.normalized * g)).ToString();
                //at.text = (rawAccelerometer).ToString();
                //gravity = (this.rawAccelerometer.normalized * g);
                //res = (rawAccelerometer - gravity);
                //resAccel = res - offAccel;

                //ignore gravity
                //this.transform.position += ((rawAccelerometer.normalized - offAccel.normalized) - Vector3.up) * Time.deltaTime;
            }

    }


    // Connection check
    bool connected = false;
    // Calibration configuration
    float calibrationTime = 5.0f;
    bool isCalibrated = false;
    int ammountOfValues = 0;

    public Vector3 offGyro;
    public Vector3 offAccel;

    void Calibrate()
    {
        calibrationTime -= Time.deltaTime;
        if (calibrationTime > 0)
        {
            //offGyro += rawGyro[0];
            //offAccel += rawAccelerometer;
            //offAccel.y -= 10;
            ammountOfValues++;
        }
        else
        {
            offGyro /= ammountOfValues;
            //offAccel /= ammountOfValues;
            //offAccel.y = 0;
            isCalibrated = true;
        }
    }
}
