using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using System.IO;
using UnityEditor;


public class Block
{
    public string Contents { get; set; }
    public int Pos { get; set; }//在数组中的位置
    public int PartentPos { get; set; }//父节点在数组中的位置


}
public class BlockList
{
    public List<Block> list  { get; set; }
}

public class TreeBulider : MonoBehaviour
{
    public BlockList blockList = null;
    void Start()
    {
        Block block = new Block();
        block.Contents = "伟大的";
        block.Pos = 1;
        block.PartentPos = 0;

        Block block1 = new Block();
        block1.Contents = "嘟嘟嘟";
        block1.Pos = 0;
        block1.PartentPos = -1;

        //List<Block> list = new List<Block> { block, block1 };
        //File.WriteAllText(Application.dataPath + "/Data1.json", JsonMapper.ToJson(list));
        Save(block);
        Save (block1);

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
        sw.WriteLine(json);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();
    }


}
