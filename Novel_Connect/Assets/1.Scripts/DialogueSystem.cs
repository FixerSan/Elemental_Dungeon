using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public GameObject DialogueUI;
    public Image txtIllust;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtSentence;
    public TextMeshProUGUI btn_1Text;
    public bool isUseBtn;
    public int useBtnCnt;
    public List<string> btnName = new List<string>();


    Queue<string> sentences = new Queue<string>();
    public void Begin(Dialogue info)
    {
        sentences.Clear();
        txtIllust.sprite = info.illust;
        txtName.text = info._name;
        isUseBtn = info.isUseBtn;
        useBtnCnt = info.useBtnCnt;
        btnName = info.btnName;
        DialogueUI.transform.Find("Button_1").gameObject.SetActive(false);


        foreach (var sentence in info._sentences)
        {
            sentences.Enqueue(sentence);
        }
            
        Next();
    }

    private void Next()
    {
        txtSentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
            
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach(var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        if(isUseBtn)
        {
            yield return new WaitForSeconds(0.5f);
            DialogueUI.transform.Find("Button_1").gameObject.SetActive(true);
            btn_1Text.text = btnName[0];

        }

    }

    private void End()
    {
        DialogueUI.SetActive(false);
        GameManager.instance.MouseLayCheckUse = true;
    }

    [System.Obsolete]
    private void Update()
    {
        if (DialogueUI.active && Input.GetKeyDown(KeyCode.Escape))
        {
            End();
        }
    }
}
