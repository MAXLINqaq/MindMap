using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LitJson;
using System.IO;

public class InputFieldController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    // Start is called before the first frame update
    public int iLevel;
    public int Pos;
    public string contents;
    public GameObject treeBuilder;

    public Image Select;
    private InputField inputfield;
    private RectTransform rectTransform;
    private RectTransform selectRectTransform;
    private bool isSelected;
    private bool isOut;
    private bool isDragable;

    public Vector2 offsetPos;


    void Start()
    {
        treeBuilder = GameObject.Find("TreeBuilder");
        inputfield = GetComponent<InputField>();
        if (inputfield.text != contents)
        {
            inputfield.text = contents;
        }
        inputfield.lineType = InputField.LineType.MultiLineSubmit;

        rectTransform = this.GetComponent<RectTransform>();
        selectRectTransform = Select.GetComponent<RectTransform>();

        isDragable = true;

    }

    void Awake()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (inputfield.interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                inputfield.interactable = false;
            }
        }
        if (inputfield.text != contents)
        {
            ChangeContents();
        }

        OnPointerExitAndClick();

        if (isSelected)
        {
            selectRectTransform.sizeDelta = rectTransform.sizeDelta + new Vector2(5, 5);
            SendMessageUpwards("ClearSelected");
            isSelected = true;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                NewBlock();
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                InputField inputField = GetComponent<InputField>();
                if (inputField.interactable == false)
                {
                    DelectBlock();
                }
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("1");
                    Copy();
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    Paste();
                    Debug.Log("2");
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Cut();
                    Debug.Log("3");
                }
            }
        }
        else
        {
            selectRectTransform.sizeDelta = new Vector2(0, 0);
        }

    }




    public void OnDrag(PointerEventData eventData)
    {
        if (isDragable)
        {
            transform.position = eventData.position - offsetPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offsetPos = eventData.position - (Vector2)transform.position;

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1 && eventData.button == PointerEventData.InputButton.Left)
        {
            isSelected = true;
            isDragable = true;
        }
        else if (eventData.clickCount == 2 && eventData.button == PointerEventData.InputButton.Left)
        {
            InputField inputField = GetComponent<InputField>();
            inputField.interactable = true;
            inputField.Select();
            isDragable = false;

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isOut = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isOut = true;
    }
    private void OnPointerExitAndClick()
    {
        if (isOut && Input.GetMouseButtonDown(0))
        {
            isSelected = false;
            InputField inputField = GetComponent<InputField>();
            inputField.interactable = false;
            isDragable = true;
        }
    }
    private void NewBlock()
    {
        treeBuilder.SendMessage("NewBlock", Pos, SendMessageOptions.DontRequireReceiver);
        isSelected = false;
    }
    private void DelectBlock()
    {
        treeBuilder.SendMessage("DelectBlock", Pos, SendMessageOptions.DontRequireReceiver);
        isSelected = false;
    }
    private void ChangeContents()
    {
        contents = inputfield.text;
        BlockList blockList = readJson();
        blockList.list[Pos].Contents = contents;
        writeJson(blockList);
    }
    public void SetIsSelected()
    {
        isSelected = true;
    }
    public void ClearSelected()
    {
        isSelected = false;
    }
    private void Copy()
    {
        treeBuilder.SendMessage("Copy", Pos, SendMessageOptions.DontRequireReceiver);
        isSelected = false;
    }
    private void Paste()
    {
        treeBuilder.SendMessage("Paste", Pos, SendMessageOptions.DontRequireReceiver);
        isSelected = false;
    }
    private void Cut()
    {
        treeBuilder.SendMessage("Cut", Pos, SendMessageOptions.DontRequireReceiver);
        isSelected = false;
    }
    private void writeJson(BlockList blockList)
    {
        string filePath = Application.dataPath + treeBuilder.GetComponent<TreeBulider>().fileName;
        StreamWriter sw = new StreamWriter(filePath);
        string json = JsonMapper.ToJson(blockList.list);
        json = "{ \"list\":" + json + "}";
        sw.WriteLine(json);
        sw.Close();
        sw.Dispose();
    }
    private BlockList readJson()
    {
        string filePath = Application.dataPath + treeBuilder.GetComponent<TreeBulider>().fileName;
        StreamReader sr = new StreamReader(filePath);
        JsonReader js = new JsonReader(sr);
        BlockList blockList = JsonMapper.ToObject<BlockList>(js);
        sr.Close();
        return blockList;
    }


}
