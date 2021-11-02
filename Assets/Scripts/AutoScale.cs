using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AutoScale : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform RectTrans;
    private Text SubText;

    private void Awake()
    {
        RectTrans = transform.GetComponent<RectTransform>();
        SubText = transform.GetChild(2).GetComponent<Text>();
    }
    private void AutoScaleRect()
    { 

    }
}
