using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    #region �̱��� �� DontDestroy
    private DialogSystem() { }
    public static DialogSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SetAllClose();
    }

    #endregion

    //�� ������ �ӵ�
    public float typingSpeed;

    [SerializeField] private SpeakerUI[] speakers;

    private int currentSpeakerUI_Index;
    private Dialog currentDialog;
    private bool isTypingEffect;

    private void Update()
    {
        //CheckSkip();
    }

    //Ŭ���� ������ ȿ�� ���, �� ���� ����
    void CheckSkip()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTypingEffect)
            {
                StopCoroutine("OnTypingText");
                isTypingEffect = false;
                speakers[currentSpeakerUI_Index].dialogueText.text = currentDialog.sentence;
                SettingButton(true);
            }
        }
    }
    //����Ŀ ��ü �ݱ�
    void SetAllClose()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObject(speakers[i], false);
            isTypingEffect = false;
        }
    }

    //���̾�α� ȣ�� �Լ�, �ε����� ���̾�α׸� �����Ͽ� ȣ��
    public void UpdateDialog(int dialogIndex)
    {
        currentDialog = DataBase.instance.GetDialog(dialogIndex);
        currentSpeakerUI_Index = currentDialog.speakerUIindex;

        speakers[currentSpeakerUI_Index].characterImage.sprite = Resources.Load<Sprite>(currentDialog.illustPath);
        speakers[currentSpeakerUI_Index].nameText.text = currentDialog.name;
        SettingButton(false);
        SetActiveObject(speakers[currentSpeakerUI_Index], true);
        StartCoroutine("OnTypingText");
    }

    public void NextDialogSetting()
    {
        //���� �� ���� nextIndex�� -100
        if (currentDialog.nextIndex == -100)
        {
            SetAllClose();
            return;
        }
        //Ư�� ����� ���� ��� -1
        if (currentDialog.nextIndex == -1)
        {
            OnClickBtn();
            return;
        }
        //�� �� �ƴ϶�� ���� �ε��� ȣ��
        UpdateDialog(currentDialog.nextIndex);
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        while (index < currentDialog.sentence.Length + 1)
        {
            speakers[currentSpeakerUI_Index].dialogueText.text = currentDialog.sentence.Substring(0, index);
            index++;

            yield return new WaitForSeconds(typingSpeed);
            if (!isTypingEffect)
                isTypingEffect = true;
        }

        isTypingEffect = false;
        SettingButton(true);
    }

    void SettingButton(bool visible)
    {
        switch (currentDialog.speakerUIindex)
        {
            case 0:
                if (visible)
                {
                    speakers[currentSpeakerUI_Index].selectBtn_1.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.nextBtnText;
                }
                speakers[currentSpeakerUI_Index].selectBtn_1.gameObject.SetActive(visible);
                break;

            case 1:
                if (visible)
                {
                    speakers[currentSpeakerUI_Index].selectBtn_1.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_1Text;
                    speakers[currentSpeakerUI_Index].selectBtn_2.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_2Text;
                }
                speakers[currentSpeakerUI_Index].selectBtn_1.gameObject.SetActive(visible);
                speakers[currentSpeakerUI_Index].selectBtn_2.gameObject.SetActive(visible);
                break;

            case 2:
                if (visible)
                {
                    speakers[currentSpeakerUI_Index].selectBtn_1.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_1Text;
                    speakers[currentSpeakerUI_Index].selectBtn_2.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_2Text;
                    speakers[currentSpeakerUI_Index].selectBtn_3.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.selectBtn_3Text;
                }
                speakers[currentSpeakerUI_Index].selectBtn_1.gameObject.SetActive(visible);
                speakers[currentSpeakerUI_Index].selectBtn_2.gameObject.SetActive(visible);
                speakers[currentSpeakerUI_Index].selectBtn_3.gameObject.SetActive(visible);
                break;

        }
    }

    void SetActiveObject(SpeakerUI speakerUI, bool visible)
    {
        speakerUI.characterImage.gameObject.SetActive(visible);
        speakerUI.dialoguePanel.gameObject.SetActive(visible);
        speakerUI.nameText.transform.parent.gameObject.SetActive(visible);
        speakerUI.header.SetActive(visible);
    }

    public void PlayInteractionSound()
    {
        AudioSystem.Instance.PlayOneShotButton("DialogueBtn");
    }
    #region �ε��� �� Ư�� ó��
    //���� ��ư�� �־��� ���
    public void OnClickBtn()
    {
        PlayInteractionSound();
        SetAllClose();
        switch (currentDialog.index)
        {
            case 0:
                return;
        }
        if (currentDialog.nextIndex == -1)
        {
            SetAllClose();
            return;
        }
        UpdateDialog(currentDialog.nextIndex);
    }

    //������ ��ư �� ù ��°�� ����� ���
    public void SelectBtn_1()
    {
        PlayInteractionSound();
        SetAllClose();
        //���� �������� �ε����� �� �´� Ư�� �ڵ� ȣ��
        switch (currentDialog.index)
        {
            case 0:
                break;
        }
    }
    //������ ��ư �� �� ��°�� ����� ���
    public void SelectBtn_2()
    {
        PlayInteractionSound();
        //���� �������� �ε����� �� �´� Ư�� �ڵ� ȣ��
        SetAllClose();
        switch (currentDialog.index)
        {
            case 0:
                break;
        }
    }

    //������ ��ư �� �� ��°�� ����� ���
    public void SelectBtn_3()
    {
        PlayInteractionSound();
        //���� �������� �ε����� �� �´� Ư�� �ڵ� ȣ��
        switch (currentDialog.index)
        {
            case 0:
                break;
        }
    }
    #endregion

}

