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
    private void SetLayout()
    {

    }


    public void Drag()//����϶��¼����任λ�á�
    {
        transform.position = Input.mousePosition;
    }



}
