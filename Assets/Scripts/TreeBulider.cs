using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeBulider : MonoBehaviour
{
    private string Name;
    private int iChildrenCount;
    private int iLevel;

    [Serializable]
    public struct Zhuti
    {
        public  GameObject  Parent;
        public GameObject[]  Child ;
    };
    public Zhuti[] zhuti;


}
