using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeController : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject Camera;
    public GameObject CenterBlock;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = this.GetComponent<Dropdown>();

    }
    private void Update()
    {
        if (GameObject.Find("0") != null)
        {
            CenterBlock = GameObject.Find("0");
            image = CenterBlock.GetComponent<Image>();
        }
        if (dropdown.value == 0)
        {
            if (image.color != new Color(0.86f, 0.003f, 0.05f, 1f))
            {
                image.color = new Color(0.86f, 0.003f, 0.05f, 1f);
            }
        }
        else if (dropdown.value == 1)
        {
            if (image.color != new Color(0.0039f, 0.137f, 0.87f, 1f))
            {
                image.color = new Color(0.0039f, 0.137f, 0.87f, 1f);
            }
        }
        else if (dropdown.value == 2)
        {
            if (image.color != new Color(0f, 0.121f, 0.9f, 0.526f))
            {
                image.color = new Color(0.59f, 0.77f, 0.96f, 1f);
            }
        }

    }

    // Update is called once per frame
    public void ChangeColor()
    {
        if (GameObject.Find("0") != null)
        {
            CenterBlock = GameObject.Find("0");
            if (dropdown.value == 0)
            {
                CenterBlock.GetComponent<Image>().color = new Color(0.86f, 0.003f, 0.05f, 1f);
                Camera.GetComponent<Camera>().backgroundColor = new Color(1f, 1f, 1f, 1f);
            }
            else if (dropdown.value == 1)
            {
                CenterBlock.GetComponent<Image>().color = new Color(0.0039f, 0.137f, 0.87f, 1f);
                Camera.GetComponent<Camera>().backgroundColor = new Color(1f, 1f, 1f, 1f);
            }
            else if (dropdown.value == 2)
            {
                CenterBlock.GetComponent<Image>().color = new Color(0f, 0.121f, 0.9f, 0.526f);
                Camera.GetComponent<Camera>().backgroundColor = new Color(0.59f, 0.77f, 0.96f, 1f);
            }
        }
    }
}
