using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldController : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    public int iLevel;
    public string contents;
    
    private  InputField inputfield;

    public  Vector2 offsetPos;
    void Start()
    {
        inputfield = GetComponent<InputField>(); 
        if (inputfield.text != contents)
        {
            inputfield.text = contents;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

      //��ʱ��¼�������UI�����λ��

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position - offsetPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offsetPos = eventData.position - (Vector2)transform.position;

    }

}
