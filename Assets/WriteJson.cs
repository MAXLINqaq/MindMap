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
    /*����һ��Person���������԰�����Name��HP,Level��Exp,Attak�ȣ���
     ����ת���json��ʽ�ַ�������д�뵽person.json���ı��У�
     Ȼ��person.json�ı��е����ݶ�ȡ������ֵ���µ�Person����
     */

    public PersonList personList = new PersonList();

    // Use this for initialization
    void Start()
    {
        //��ʼ��������Ϣ
        Person person = new Person();
        person.Name = "MAXLIN";
        person.HP = 11;
        person.Level = 323;
        person.Exp = 919;
        person.Attak = 18;

        //���ñ��淽��
        Save(person);

    }
    /// <summary>
    /// ����JSON���ݵ����صķ���
    /// </summary>
    /// <param name="player">Ҫ����Ķ���</param>
    public void Save(Person player)
    {
        //�����Resources�ļ��в��ܴ洢�ļ�����������ʹ�����и���Ŀ¼
        string filePath = Application.dataPath + @"/JsonPerson.json";

        if (!File.Exists(filePath))  //�����ھʹ�����ֵ��
        {
            personList.dictionary.Add("Name", player.Name);
            personList.dictionary.Add("HP", player.HP.ToString());
            personList.dictionary.Add("Level", player.Level.ToString());
            personList.dictionary.Add("Exp", player.Exp.ToString());
            personList.dictionary.Add("Attak", player.Attak.ToString());

        }
        else   //�����ھ͸���ֵ
        {
            personList.dictionary["Name"] = player.Name;
            personList.dictionary["HP"] = player.HP.ToString();
            personList.dictionary["Level"] = player.Level.ToString();
            personList.dictionary["Exp"] = player.Exp.ToString();
            personList.dictionary["Attak"] = player.Attak.ToString();
        }

        //�ҵ���ǰ·��
        FileInfo file = new FileInfo(filePath);
        //�ж���û���ļ���������ļ�����û�д�������ļ�
        StreamWriter sw = file.CreateText();
        //ToJson�ӿڽ�����б��ഫ��ȥ�������Զ�ת��Ϊstring����
        string json = JsonMapper.ToJson(personList.dictionary);
        //��ת���õ��ַ�������ļ���
        sw.WriteLine(json);
        //ע���ͷ���Դ
        sw.Close();
        sw.Dispose();

        AssetDatabase.Refresh();

    }
}
