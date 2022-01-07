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
    public string fileName;
    public GameObject Level0;
    public GameObject Level1;
    public GameObject Level2;

    private bool isFinished;

    private bool isClipBoardExisted;
    private BlockList ClipBoardList = new BlockList();
    private int pastePos;



    void Start()
    {
        fileName = @"/Saves/Data1.json";
        isClipBoardExisted = false;
        clearJson(@"/Saves/ClipBoard.json");

        //List<Block> list = new List<Block> { block, block1 };
        //File.WriteAllText(Application.dataPath + "/Data1.json", JsonMapper.ToJson(list));

    }
    private void buildTree(string fileName)
    {
        BlockList blockList = readJson(fileName);
        for (int i = 0; i < blockList.list.Count; i++)
        {

            GameObject tempObject;
            if (blockList.list[i].PartentPos == -1)
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
            tempObject.GetComponent<InputFieldController>().Pos = blockList.list[i].Pos;
            tempObject.name = blockList.list[i].Pos.ToString();

            if (i == 0)
            {
                tempObject.transform.SetParent(GameObject.Find("Map").transform);
            }
            else
            {
                tempObject.transform.SetParent(GameObject.Find(blockList.list[i].PartentPos.ToString()).transform);
            }
        }

    }
    private void SetPos()
    {
        BlockList blockList = readJson(fileName);
        GameObject tempObject;

        int count = 0;
        int[] child = new int[blockList.list.Count];
        for (int i = 0; i < blockList.list.Count; i++)
        {
            tempObject = GameObject.Find(blockList.list[i].Pos.ToString());
            if (i == 0)
            {
                tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);//将中心主题置于平面中央
            }
            for (int j = i + 1; j < blockList.list.Count; j++)
            {
                if (blockList.list[j].PartentPos == blockList.list[i].Pos)
                {
                    child[count] = blockList.list[j].Pos;
                    count++;
                }
            }
            GameObject tempSubObject;
            if (count == 1)
            {
                tempSubObject = GameObject.Find(child[0].ToString());
                tempSubObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(tempSubObject.GetComponent<RectTransform>().sizeDelta.x / 2 + 100, 0);
                count = 0;
            }
            else
            {
                for (int j = 0; j < count; j++)
                {
                    tempSubObject = GameObject.Find(child[j].ToString());
                    tempSubObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(tempSubObject.GetComponent<RectTransform>().sizeDelta.x / 2 + 100, (100 * (count / 2 - j)) / 2);
                }
                count = 0;
            }
        }
    }
    public void NewBlock(int Pos)
    {
        Block block = new Block();
        BlockList blockList = readJson(fileName);
        DestroyTree(blockList);

        //为新块赋值
        block.PartentPos = Pos;
        block.Contents = "";
        block.Pos = blockList.list.Count;
        //添加到list并排序
        blockList.list.Add(block);

        for (int i = blockList.list.Count - 1; i > 1; i--)
        {
            if (blockList.list[i].PartentPos >= blockList.list[i - 1].PartentPos)
            {
                Pos = blockList.list[i].Pos;
                //为每个方块重新赋值
                for (int j = blockList.list.Count - 1; j > 1; j--)
                {
                    if (blockList.list[j].PartentPos >= Pos)
                    {
                        blockList.list[j].PartentPos++;
                    }
                    blockList.list[j].Pos = j;
                }
                break;
            }
            else
            {
                Block tempBlock = blockList.list[i];
                blockList.list[i] = blockList.list[i - 1];
                blockList.list[i - 1] = tempBlock;
            }
        }

        writeJson(blockList, fileName);
        buildTree(fileName);
        SetPos();
    }
    public void DelectBlock(int Pos)
    {
        bool isExisted = false;
        BlockList blockList = readJson(fileName);
        DestroyTree(blockList);
        int count = 0;
        int[] DelectFlag = new int[blockList.list.Count];
        DelectFlag[count] = Pos;
        blockList.list.RemoveAt(Pos);
        for (int i = 1; i < blockList.list.Count; i++)
        {
            for (int j = 0; j < blockList.list.Count; j++)
            {
                if (blockList.list[i].PartentPos == blockList.list[j].Pos)
                {
                    isExisted = true;
                }
            }
            if (isExisted == false)
            {
                count++;
                DelectFlag[count] = i;
                blockList.list.RemoveAt(i);
            }
            isExisted = false;
        }
        for (int j = 0; j < count; j++)
        {
            for (int i = 1; i < blockList.list.Count; i++)
            {

                if (blockList.list[i].PartentPos > DelectFlag[j])
                {
                    blockList.list[i].PartentPos--;
                }

                blockList.list[i].Pos = i;
            }
        }
        for (int i = 1; i < blockList.list.Count; i++)
        {
            blockList.list[i].Pos = i;
        }

        writeJson(blockList, fileName);
        buildTree(fileName);
        SetPos();
    }
    public void Copy(int Pos)
    {
        SaveClipBoard(Pos);
    }
    public void Cut(int Pos)
    {
        SaveClipBoard(Pos);
        DelectBlock(Pos);
    }
    public void Paste(int Pos)
    {
        if (isClipBoardExisted)
        {
            int TempPos = Pos;
            BlockList blockList = readJson(fileName);
            DestroyTree(blockList);
            BlockList ClipBoardList = readJson(@"/Saves/ClipBoard.json");
            for (int k = 0; k < ClipBoardList.list.Count; k++)
            {
                blockList.list.Add(ClipBoardList.list[k]);
                blockList.list[blockList.list.Count - 1].PartentPos = TempPos;
                blockList.list[blockList.list.Count - 1].Pos = blockList.list.Count - 1;
                for (int i = blockList.list.Count - 1; i > 1; i--)
                {
                    if (blockList.list[i].PartentPos >= blockList.list[i - 1].PartentPos)
                    {
                        Pos = blockList.list[i].Pos;
                        TempPos = blockList.list[i].Pos;
                        //为每个方块重新赋值
                        for (int j = blockList.list.Count - 1; j > 1; j--)
                        {
                            if (blockList.list[j].PartentPos >= Pos)
                            {
                                blockList.list[j].PartentPos++;
                            }
                            blockList.list[j].Pos = j;
                        }
                        break;
                    }
                    else
                    {
                        Block tempBlock = blockList.list[i];
                        blockList.list[i] = blockList.list[i - 1];
                        blockList.list[i - 1] = tempBlock;
                    }
                }
            }
            writeJson(blockList, fileName);
            buildTree(fileName);
            SetPos();
        }
    }
    private void SaveClipBoard(int Pos)
    {
        bool isExisted = false;
        clearJson(@"/Saves/ClipBoard.json");
        BlockList blockList = readJson(fileName);
        BlockList ClipBoardList = readJson(@"/Saves/ClipBoard.json");
        isClipBoardExisted = true;
        ClipBoardList.list.Add(blockList.list[Pos]);
        blockList.list.RemoveAt(Pos);
        for (int i = 1; i < blockList.list.Count; i++)
        {
            for (int j = 0; j < blockList.list.Count; j++)
            {
                if (blockList.list[i].PartentPos == blockList.list[j].Pos)
                {
                    isExisted = true;
                }
            }
            if (isExisted == false)
            {
                ClipBoardList.list.Add(blockList.list[i]);
                blockList.list.RemoveAt(i);
            }
            isExisted = false;
        }
        writeJson(ClipBoardList, @"/Saves/ClipBoard.json");
    }

    public void newMap()
    {
        fileName = @"/Saves/template.json";
        clearJson(fileName);
        buildTree(fileName);
        SetPos();
    }
    public void Save(string FileName)
    {
        fileName = FileName;
        BlockList blockList = readJson(@"/Saves/template.json");
        writeJson(blockList, fileName);
        clearJson(@"/Saves/template.json");
    }
    public void Open(string FileName)
    {
        fileName = FileName;
        if (!System.IO.File.Exists(Application.dataPath + FileName))
        {
            clearJson(FileName);
        }
        buildTree(fileName);
        SetPos();
    }
    private void writeJson(BlockList blockList, string fileName)
    {
        string filePath = Application.dataPath + fileName;
        StreamWriter sw = new StreamWriter(filePath);
        string json = JsonMapper.ToJson(blockList.list);
        json = "{ \"list\":" + json + "}";
        sw.WriteLine(json);
        sw.Close();
        sw.Dispose();
    }
    private BlockList readJson(string fileName)
    {
        string filePath = Application.dataPath + fileName;
        StreamReader sr = new StreamReader(filePath);
        JsonReader js = new JsonReader(sr);
        BlockList blockList = JsonMapper.ToObject<BlockList>(js);
        sr.Close();
        return blockList;
    }
    private void clearJson(string fileName)
    {
        string filePath = Application.dataPath + fileName;
        StreamWriter sw = new StreamWriter(filePath);
        sw.WriteLine("{ \"list\":[{\"Contents\":\"\",\"Pos\":0,\"PartentPos\":-1}]}");
        sw.Close();
        sw.Dispose();
    }
    private void DestroyTree(BlockList oldBlockList)
    {
        for (int i = oldBlockList.list.Count - 1; i > 0; i--)
        {
            DestroyImmediate(GameObject.Find(oldBlockList.list[i].Pos.ToString()));
        }
        DestroyImmediate(GameObject.Find(oldBlockList.list[0].Pos.ToString()));
    }
    public void Clean()
    {
        DestroyImmediate(GameObject.Find("0"));
    }

}
