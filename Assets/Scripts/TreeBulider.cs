using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using System.IO;
using UnityEditor;
using UnityEngine.UI;


public class Block
{
    public string Contents { get; set; }
    public int Pos { get; set; }//自身在数组中的位置
    public int PartentPos { get; set; }//父节点的位置


}
public class BlockList
{
    public List<Block> list { get; set; }
}

public class TreeBulider : MonoBehaviour
{
    public BlockList blockList = null;

    public GameObject Level0;
    public GameObject Level1;
    public GameObject Level2;


    void Start()
    {
        buildTree();

        //List<Block> list = new List<Block> { block, block1 };
        //File.WriteAllText(Application.dataPath + "/Data1.json", JsonMapper.ToJson(list));

    }
    public void Save(Block block)
    {
        string filePath = Application.dataPath + @"/Data1.json";
        StreamReader sr = new StreamReader(filePath);
        JsonReader js = new JsonReader(sr);
        BlockList blockList = JsonMapper.ToObject<BlockList>(js);
        for (int i = 0; i < blockList.list.Count; i++)
        {
            Debug.Log(blockList.list[i].Contents);
        }
        sr.Close();

        if (!File.Exists(filePath))
        {
            blockList.list.Add(block);
        }
        else
        {
            bool bFind = false;
            for (int i = 0; i < blockList.list.Count; i++)
            {
                Block saveBlock = blockList.list[i];
                if (block.Contents == saveBlock.Contents)
                {
                    saveBlock.Pos = block.Pos;
                    saveBlock.PartentPos = block.PartentPos;

                    bFind = true;
                    break;
                }

            }
            if (!bFind)
            {
                blockList.list.Add(block);
            }
        }


        StreamWriter sw = new StreamWriter(filePath);
        string json = JsonMapper.ToJson(blockList.list);
        json = "{ \"list\":" + json + "}";
        sw.WriteLine(json);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();
    }
    private void buildTree()
    {
        string filePath = Application.dataPath + @"/Data1.json";
        StreamReader sr = new StreamReader(filePath);
        JsonReader js = new JsonReader(sr);
        BlockList blockList = JsonMapper.ToObject<BlockList>(js);
        sr.Close();
        for (int i = 0; i < blockList.list.Count; i++)
        {
            GameObject tempObject;
            if (i == 0)
            {
                tempObject = Instantiate(Level0);
            }
            else if (blockList.list[i].PartentPos == 0)
            {
                tempObject = Instantiate(Level1);
            }
            else
            {
                tempObject = Instantiate(Level2);
            }
            tempObject.GetComponent<InputFieldController>().contents = blockList.list[i].Contents;
            tempObject.name = blockList.list[i].Pos.ToString();

            if (i == 0)
            {
                tempObject.transform.parent = GameObject.Find("Map").gameObject.transform;
                tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            }
            else
            {
                tempObject.transform.parent = GameObject.Find(blockList.list[i].PartentPos.ToString()).gameObject.transform;
                tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            }
        }

    }

}
