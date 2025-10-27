using System.IO.Ports;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArduinoController : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM8", 9600);

    void Start()
    {
        try
        {
            sp.Open();
            sp.ReadTimeout = 50;
            Debug.Log("Serial port opened!");
        }
        catch
        {
            Debug.LogError("Could not open serial port!");
        }
    }

    void Update()
    {
        // send signal when pressing space
        if (Keyboard.current.spaceKey.wasPressedThisFrame && sp.IsOpen)
            sp.Write("1");
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && sp.IsOpen)
            sp.Write("0");

        // check for messages from Arduino
        if (sp.IsOpen)
        {
            try
            {
                string message = sp.ReadLine();  // read serial line
                if (message.Contains("TOUCHED"))
                {
                    Debug.Log("Arduino said wire touched!");
                }
            }
            catch (System.TimeoutException)
            {
                // no message yet
            }
        }
    }

    void OnApplicationQuit()
    {
        if (sp.IsOpen)
            sp.Close();
    }
}