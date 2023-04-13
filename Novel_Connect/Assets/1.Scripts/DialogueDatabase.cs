using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    #region Singleton
    public static DialogueDatabase instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public List<Dialogue> dialogues;
    

    public Dialogue GetDialogue(int index)
    {
        foreach(Dialogue dialogue in dialogues)
        {
            if(dialogue.index == index)
            {
                return dialogue;
            }
        }

        return null;
    }
    
}
