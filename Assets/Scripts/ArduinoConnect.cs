using System;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.InputSystem;
public class ArduinoConnector : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM8", 9600); // change COM3 to your Arduino port
    
    InputAction Action;

    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 100;
        Action = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        // Check if Arduino sent message
        try
        {
            string message = serialPort.ReadLine();
            if (message.Contains("BUTTON_PRESSED"))
            {
                Debug.Log("Arduino button pressed!");
            }
        }
        catch (System.Exception) { }

        // If press spacebar in Unity
        
        if (Action.IsPressed())
        {
            Debug.Log("Sending LED_ON");
            serialPort.WriteLine("LED_ON");
        }
    }

    void OnApplicationQuit()
    {
        serialPort.Close();
    }
}
