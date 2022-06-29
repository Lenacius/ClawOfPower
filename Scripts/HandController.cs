using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public ArduinoListener ardListener;

    public GameObject hand;
    public GameObject camera;

    //public Vector3 gravity;
    //public Vector3 res;
    //public Vector3 resAccel;
    //public float g = 10.81f;
    private void Update()
    {
        if(ardListener.conected)
            if (!isCalibrated)
                Calibrate();
            else
            {
                camera.transform.Rotate((ardListener.rawGyro[0] - offAccel) * Time.deltaTime);
                this.transform.Rotate(new Vector3(0, ardListener.rawGyro[0].y - offAccel.y, 0) * Time.deltaTime);
                hand.transform.Rotate((ardListener.rawGyro[1] - offGyroHan) * Time.deltaTime);

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

    // Calibration configuration
    public float calibrationTime = 5.0f;
    public bool isCalibrated = false;
    public int ammountOfValues = 0;

    public Vector3 offGyroCam;
    public Vector3 offGyroHan;
    public Vector3 offAccel;

    void Calibrate()
    {
        Debug.Log("Cal");
        calibrationTime -= Time.deltaTime;
        if (calibrationTime > 0)
        {
            offGyroCam += ardListener.rawGyro[0];
            offGyroHan += ardListener.rawGyro[1];
            //offGyro += rawGyro[0];
            //offAccel += rawAccelerometer;
            //offAccel.y -= 10;
            ammountOfValues++;
        }
        else
        {
            offGyroCam /= ammountOfValues;
            offGyroHan /= ammountOfValues;
            //offAccel /= ammountOfValues;
            //offAccel.y = 0;
            isCalibrated = true;
        }
    }
}
