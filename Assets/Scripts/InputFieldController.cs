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



    public void Drag()//����϶��¼����任λ�á�
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        transform.position = Input.mousePosition-new Vector3 (rect.rect.width/2 ,0,0);
    }



}
