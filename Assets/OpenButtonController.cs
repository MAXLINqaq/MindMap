using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenButtonController : MonoBehaviour
{
    public GameObject treeBuilder;
    public GameObject ObjInputField;
    public GameObject ObjButton;

    private void Start()
    {
        treeBuilder = GameObject.Find("TreeBuilder");
    }
    public void SetActive()
    {
        ObjInputField.SetActive(true);
        ObjButton.SetActive(true);
    }
    public void Confirm()
    {
        string text = @"/Saves/" + ObjInputField.GetComponent<InputField>().text + ".json";
        treeBuilder.SendMessage("Open", text, SendMessageOptions.DontRequireReceiver);
        ObjInputField.SetActive(false);
        ObjButton.SetActive(false);
    }
}
