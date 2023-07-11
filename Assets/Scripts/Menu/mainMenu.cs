using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        Resolution[] resolution = Screen.resolutions;
        Screen.SetResolution(resolution[resolution.Length - 1].width, resolution[resolution.Length - 1].height, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


}
