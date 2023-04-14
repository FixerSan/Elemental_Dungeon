using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    //�����ͺ��̽����� ���� ���
    #region �̱��� �� DontDestroy
    private static DialogSystem Instance;

    public static DialogSystem instance
    {
        get
        {
            if (Instance != null)
                return Instance;
            else
                return null;
        }

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    #endregion

    public SpeakerUI[] speakers;
    public int currentSpeakerUI_Index;
    public Dialog currentDialog;
    public float typingSpeed;
    bool isTypingEffect;

    private void Start()
    {
        SetAllClose();
    }

    private void Update()
    {
        CheckSkip();
    }

    void CheckSkip()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (isTypingEffect)
            {
                StopCoroutine("OnTypingText");
                isTypingEffect = false;
                speakers[currentSpeakerUI_Index].textDialogue.text = currentDialog.sentence;
                SettingButton();
            }
        }
    }
    void SetAllClose()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObject(speakers[i],false);
            isTypingEffect = false;
        }
    }

    public void UpdateDialog(int dialogIndex)
    {
        currentDialog = DataBase.instance.GetDialog(dialogIndex);
        currentSpeakerUI_Index = (int)currentDialog.speakerUI_Index;

        speakers[currentSpeakerUI_Index].imgCharacter.sprite = Resources.Load<Sprite>(currentDialog.illustPath);
        speakers[currentSpeakerUI_Index].textName.text = currentDialog.name;
        speakers[currentSpeakerUI_Index].nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.nextBtnText;


        SetActiveObject(speakers[currentSpeakerUI_Index], true);
        StartCoroutine("OnTypingText");
    }

    public void NextDialogSetting()
    {
        if(currentDialog.nextIndex == -1)
        {
            OnClickBtn();
            return;
        }
        UpdateDialog(currentDialog.nextIndex);
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        while (index < currentDialog.sentence.Length + 1)
        {
            speakers[currentSpeakerUI_Index].textDialogue.text = currentDialog.sentence.Substring(0, index);
            index++;

            yield return new WaitForSeconds(typingSpeed);
            if(!isTypingEffect)
                isTypingEffect = true;
        }

        isTypingEffect = false;
        SettingButton();
    }

    void SettingButton()
    {
        switch (currentSpeakerUI_Index)
        {
            case 0:
                speakers[0].nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.nextBtnText;
                speakers[0].nextBtn.SetActive(true);
                break;
            case 1:
                speakers[1].nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_1Text;
                speakers[1].selectBtn_1.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_2Text;
                speakers[1].nextBtn.SetActive(true);
                speakers[1].selectBtn_1.SetActive(true);
                break;
            case 2:
                speakers[2].nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_1Text;
                speakers[2].selectBtn_1.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_2Text;
                speakers[2].selectBtn_2.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_3Text;
                speakers[2].nextBtn.SetActive(true);
                speakers[2].selectBtn_1.SetActive(true);
                speakers[2].selectBtn_2.SetActive(true);
                break;
        }
    }

    void SetActiveObject(SpeakerUI speakerUI,bool visible)
    {
        speakerUI.imgCharacter.gameObject.SetActive(visible);
        speakerUI.imageDialogue.gameObject.SetActive(visible);
        speakerUI.textName.transform.parent.gameObject.SetActive(visible);
        speakerUI.header.SetActive(visible);
        SettingButton();
    }
    public void OnClickBtn()
    {
        switch(currentDialog.index)
        {
            case 1001:
                SetAllClose();
                CanvasScript.instance.transform.Find("RosyStore").gameObject.SetActive(true);
                break;
        }
    }
}
