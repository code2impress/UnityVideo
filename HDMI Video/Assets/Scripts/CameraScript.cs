using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public RawImage rawImageLeft;     // Reference to the Left RawImage for displaying the camera feed
    public RawImage rawImageRight;    // Reference to the Right RawImage for displaying the camera feed
    public Button cameraOnButton;     // Reference to the CameraON button
    public Button cameraSwitchButton; // Reference to the CameraSwitch button

    private WebCamTexture webcamTexture;
    private int currentCameraIndex = 0;
    private WebCamDevice[] devices;

    void Start()
    {
        // Get the available camera devices
        devices = WebCamTexture.devices;
        
        if (devices.Length > 0)
        {
            // Assign button click events
            cameraOnButton.onClick.AddListener(StartCamera);
            cameraSwitchButton.onClick.AddListener(SwitchCamera);
        }
        else
        {
            Debug.LogError("No camera detected");
        }
    }

    // Method to start the camera
    void StartCamera()
    {
        if (devices.Length > 0)
        {
            // Initialize the WebCamTexture with the selected camera
            webcamTexture = new WebCamTexture(devices[currentCameraIndex].name);
            
            // Set both RawImages' textures to the webcam feed
            rawImageLeft.texture = webcamTexture;
            rawImageRight.texture = webcamTexture;
            
            // Start the webcam
            webcamTexture.Play();
        }
    }

    // Method to switch between available cameras
    void SwitchCamera()
    {
        if (webcamTexture != null)
        {
            // Stop the current camera feed
            webcamTexture.Stop();
        }

        // Switch to the next camera (if multiple cameras are available)
        currentCameraIndex = (currentCameraIndex + 1) % devices.Length;

        // Start the new camera
        StartCamera();
    }

    // Optional: Stop the camera when the application quits
    private void OnApplicationQuit()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}
