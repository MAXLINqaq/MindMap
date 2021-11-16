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
    public List<Block> list = new List<Block>();
}

public class TreeBulider : MonoBehaviour
{
    public BlockList blockList = null;
    void Start()
    {
        Block block = new Block();
        block.Contents = "1";
        block.Pos = 14;
        block.PartentPos = 231;
        
        Save(block);
        Block block1 = new Block();
        block1.Contents = "asdadwa";
        block1.Pos = 12;
        block1.PartentPos = 1;
        Save(block1);
    }
    public void Save(Block block)
    {
        string filePath = Application.dataPath + @"/Data1.json";

        blockList = new BlockList();
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

        FileInfo file = new FileInfo(filePath);
        StreamWriter sw = file.CreateText();
        string json = JsonMapper.ToJson(blockList.list);
        sw.WriteLine(json);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();
    }


}
