using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public InputField inputField;
    private RectTransform parentRectTransform;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        parentRectTransform = inputField.GetComponent<RectTransform>();
        rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentRectTransform.sizeDelta != rectTransform.sizeDelta)
        {
            rectTransform.sizeDelta = parentRectTransform.sizeDelta;
        }
    }
}
