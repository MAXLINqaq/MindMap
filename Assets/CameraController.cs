using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Slider HorizontalSlider;
    public Slider VerticalSlider;
    public int step;
    public bool isControlable;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (isControlable)
        {
            this.transform.position = new Vector3(1460, 540, 0) + new Vector3(HorizontalSlider.value * step, VerticalSlider.value * step, -10);
        }
    }

}
