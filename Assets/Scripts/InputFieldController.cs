using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerClickHandler
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
    void OnEndEdit(string text)
    {
        inputfield.interactable = false;
    }
    void Awake()
    {
        
    }


    
    // Update is called once per frame
    void Update()
    {
        if (inputfield.interactable == true)
        {
            inputfield.onEndEdit.AddListener(OnEndEdit);
        }
       
    }

      //临时记录点击点与UI的相对位置

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position - offsetPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offsetPos = eventData.position - (Vector2)transform.position;

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2 && eventData.button == PointerEventData.InputButton.Left)
        {
            InputField inputField = GetComponent<InputField>();
            inputField .interactable = true;
            inputField.Select();
        }
    }

}
