using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int qusetID;
    public float pay;
    public float servicePoint;
    public string m_name;
    public string content;
    public Sprite questSprite;
    public Sprite iconSprite;
    public QuestState state = QuestState.before;
    public QuestType type;
    public int killAmount;
    public int currentKillAmount;
    public int killMonsterID;
    public int itemAmount;
    public int currentItemAmount;
    public int itemID;

    public Quest(int qusetID_, float pay_, float servicePoint_, string m_name_, string content_, Sprite questSprite_, 
                 Sprite iconSprite_, QuestState state_, QuestType type_, int killAmount_, int currentKillAmount_, int killMonsterID_,int itemAmount_,int currentItemAmount_, int itemID_)
    {
        qusetID = qusetID_;
        pay = pay_;
        servicePoint = servicePoint_;
        m_name = m_name_;
        content = content_;
        questSprite = questSprite_;
        iconSprite = iconSprite_;
        state = state_;
        type = type_;
        killAmount = killAmount_;
        currentKillAmount = currentKillAmount_;
        killMonsterID = killMonsterID_;
        itemAmount = itemAmount_;
        currentItemAmount = currentItemAmount_;
        itemID = itemID_;
    }
}


public enum QuestState
{
    before, Proceeding, after
}

public enum QuestType
{
    kill, talk ,get
}
