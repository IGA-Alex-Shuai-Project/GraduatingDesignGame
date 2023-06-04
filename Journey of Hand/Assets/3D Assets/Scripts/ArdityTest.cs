using UnityEngine;
using System.IO.Ports;

public class ArdityTest : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM8", 9600); // ���������ӵ�COM3�˿ڣ�������Ϊ9600
    void Start()
    {
        stream.Open(); // �򿪴���
    }
    void Update()
    {
        if (stream.IsOpen)
        {
            string value = stream.ReadLine(); // ��ȡ��������
            Debug.Log(value); // ������ݵ�����̨
        }
    }
    void OnApplicationQuit()
    {
        stream.Close(); // �رմ���
    }
}
