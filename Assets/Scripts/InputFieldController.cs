using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldController : MonoBehaviour
{
    // Start is called before the first frame update
    public int iLevel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Drag()//鼠标拖动事件，变换位置。
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        transform.position = Input.mousePosition-new Vector3 (rect.rect.width/2 ,0,0);
    }



}
