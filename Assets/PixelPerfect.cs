using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour
{
    protected Resolution last_resolution;
    public uint desired_ppu = 32;
    public uint ppu_scale = 4;

    // Start is called before the first frame update
    void Start()
    {
        CheckAndUpdateOrthographicSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckAndUpdateOrthographicSize()
    {
        Resolution resolution = Screen.currentResolution;
        if (resolution.height != last_resolution.height ||
                resolution.width != last_resolution.width)
        {
            UpdateOrthographicSize(resolution, desired_ppu);
        }
        last_resolution = resolution;
    }

    void UpdateOrthographicSize(Resolution resolution, uint desired_ppu)
    {
        float orthosize = resolution.height/(2*desired_ppu*ppu_scale);
        print("Updating orthographic size to: " + orthosize);
        Camera camera = GetComponent<Camera>();
        camera.orthographicSize = orthosize;
    }
}
