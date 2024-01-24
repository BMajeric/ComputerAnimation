using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenCaptureManager : MonoBehaviour
{
    public string outputPath = Application.dataPath + "/Output";
    
    int frameCounter = 0;

    // Update is called once per frame
    void Update()
    {
        ScreenCapture.CaptureScreenshot(outputPath + "/Image" + frameCounter.ToString() + ".png");
        frameCounter++;
    }
}
