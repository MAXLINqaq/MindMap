using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEditor;

public class Person
{
    public string Name { get; set; }
    public double HP { get; set; }
    public int Level { get; set; }
    public double Exp { get; set; }
    public int Attak { get; set; }

}
public class PersonList
{
    public Dictionary<string, string> dictionary = new Dictionary<string, string>();
}

public class WriteJson : MonoBehaviour
{
    /*定义一个Person对象（其属性包括，Name，HP,Level，Exp,Attak等），
     将其转会成json格式字符串并且写入到person.json的文本中，
     然后将person.json文本中的内容读取出来赋值给新的Person对象。
     */

    public PersonList personList = new PersonList();

    // Use this for initialization
    void Start()
    {
        //初始化人物信息
        Person person = new Person();
        person.Name = "MAXLIN";
        person.HP = 11;
        person.Level = 323;
        person.Exp = 919;
        person.Attak = 18;

        //调用保存方法
        Save(person);

    }
    /// <summary>
    /// 保存JSON数据到本地的方法
    /// </summary>
    /// <param name="player">要保存的对象</param>
    public void Save(Person player)
    {
        //打包后Resources文件夹不能存储文件，如需打包后使用自行更换目录
        string filePath = Application.dataPath + @"/JsonPerson.json";

        if (!File.Exists(filePath))  //不存在就创建键值对
        {
            personList.dictionary.Add("Name", player.Name);
            personList.dictionary.Add("HP", player.HP.ToString());
            personList.dictionary.Add("Level", player.Level.ToString());
            personList.dictionary.Add("Exp", player.Exp.ToString());
            personList.dictionary.Add("Attak", player.Attak.ToString());

        }
        else   //若存在就更新值
        {
            personList.dictionary["Name"] = player.Name;
            personList.dictionary["HP"] = player.HP.ToString();
            personList.dictionary["Level"] = player.Level.ToString();
            personList.dictionary["Exp"] = player.Exp.ToString();
            personList.dictionary["Attak"] = player.Attak.ToString();
        }

        //找到当前路径
        FileInfo file = new FileInfo(filePath);
        //判断有没有文件，有则打开文件，，没有创建后打开文件
        StreamWriter sw = file.CreateText();
        //ToJson接口将你的列表类传进去，，并自动转换为string类型
        string json = JsonMapper.ToJson(personList.dictionary);
        //将转换好的字符串存进文件，
        sw.WriteLine(json);
        //注意释放资源
        sw.Close();
        sw.Dispose();

        AssetDatabase.Refresh();

    }
}
