using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    StartComDataScriptObject startComDataScriptObject;

    private SerialController serialController;
    public float Degree_Thumb;
    public float Degree_Index;
    public float Degree_Middle;
    public float Degree_Ring;
    public float Degree_Pinky;
    public bool whetherNewVersion = false;
    public float Accel_x;
    public float Accel_y;
    public float Accel_z;
    public float Gyro_x;
    public float Gyro_y;
    public float Gyro_z;
    public float Allnumbers;
    //移动加速度和旋转加速度
    public Vector3 Acceleration;
    public Vector3 Gyroscope;
    public float AccelerSensitivity=1.0f;

    // Initialization
    void Start()
    {
        serialController=GetComponent<SerialController>();
    }

    // Executed each frame
    void Update()
    {
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
        {
            string[] m = message.Split(",");
            Degree_Thumb = float.Parse(m[0]);
            Degree_Index = float.Parse(m[1]);
            Degree_Middle = float.Parse(m[2]);
            Degree_Ring= float.Parse(m[3]);
            Degree_Pinky= float.Parse(m[4]);
            // other input
            if(whetherNewVersion)
            {
                Accel_x = float.Parse(m[5]);
                Accel_y = float.Parse(m[6]);
                Accel_z = float.Parse(m[7]);
                Gyro_x = float.Parse(m[8]);
                Gyro_y = float.Parse(m[9]);
                Gyro_z = float.Parse(m[10]);
                 //移动和旋转向量赋值
            //Acceleration = new Vector3(Mathf.Abs(float.Parse(m[5])) > AccelerSensitivity ? float.Parse(m[5]) :0,Mathf.Abs(float.Parse(m[7])) > AccelerSensitivity ? float.Parse(m[7]) : 0 ,Mathf.Abs(float.Parse(m[6])) > AccelerSensitivity ? float.Parse(m[6]) : 0);
                        Acceleration = new Vector3(Mathf.Abs(float.Parse(m[5])) > AccelerSensitivity ? float.Parse(m[5]) :0,Mathf.Abs(float.Parse(m[6])) > AccelerSensitivity ? float.Parse(m[6]) : 0 , Mathf.Abs(float.Parse(m[7])) > AccelerSensitivity ? float.Parse(m[7]) : 0);

            Gyroscope = new Vector3(float.Parse(m[8]), -float.Parse(m[9]), float.Parse(m[10]));
            }
        }
        Allnumbers = Degree_Thumb + Degree_Index + Degree_Middle + Degree_Pinky + Degree_Ring;
       
        startComDataScriptObject.FingertotalNumber = Allnumbers;
    }
}
