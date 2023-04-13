using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int index;
    public string _name;
    public List<string> _sentences;
    public Sprite illust;
    public bool isUseBtn = true;
    public int useBtnCnt = 3;
    public List<string> btnName = new List<string>(); 
}
