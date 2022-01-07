using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonController : MonoBehaviour
{
    public GameObject treeBuilder;
    public GameObject ObjInputField;
    public GameObject ObjButton;
    // Start is called before the first frame update

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
        treeBuilder.SendMessage("Save", ObjInputField.GetComponent<InputField>().text, SendMessageOptions.DontRequireReceiver);
        ObjInputField.SetActive(false);
        ObjButton.SetActive(false);
    }
}
