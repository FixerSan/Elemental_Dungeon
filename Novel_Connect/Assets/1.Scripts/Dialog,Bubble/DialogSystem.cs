using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    #region 싱글톤 및 DontDestroy
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

    //글 적히는 속도
    public float typingSpeed;

    [SerializeField] private SpeakerUI[] speakers;

    private int currentSpeakerUI_Index;
    private Dialog currentDialog;
    private bool isTypingEffect;

    public bool isOpen = false;

    private void Update()
    {
        CheckSkip();
    }

    //클릭시 적히는 효과 취소, 한 번에 적힘
    void CheckSkip()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
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

    //스피커 전체 닫기
    public void SetAllClose()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObject(speakers[i], false);
            isTypingEffect = false;
            isOpen = false;
        }
    }

    //다이얼로그 호출 함수, 인덱스로 다이얼로그를 구별하여 호출
    public void UpdateDialog(int dialogIndex)
    {
        isOpen = true;
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
        //종료 할 때는 nextIndex를 -100
        if (currentDialog.nextIndex == -100)
        {
            SetAllClose();
            return;
        }
        //특정 기능이 있을 경우 -1
        if (currentDialog.nextIndex == -1)
        {
            OnClickBtn();
            return;
        }
        //둘 다 아니라면 다음 인덱스 호출
        UpdateDialog(currentDialog.nextIndex);
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        while (index < currentDialog.sentence.Length + 1)
        {
            AudioSystem.Instance.PlayOneShotSoundProfile("Dialogue");
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
        AudioSystem.Instance.PlayOneShotSoundProfile("Dialogue_Click");
    }
    #region 인덱스 별 특수 처리
    //다음 버튼만 있었을 경우
    public void OnClickBtn()
    {
        PlayInteractionSound();
        SetAllClose();
        switch (currentDialog.index)
        {
            case 1045:
                SceneManager.instance.GetCurrentScene().SceneEvent(0);
                return;
            case 1046:
                SceneManager.instance.GetCurrentScene().SceneEvent(1);
                return;
            case 1054:
                SceneManager.instance.GetCurrentScene().SceneEvent(3);
                return;
            case 2002:
                SceneManager.instance.GetCurrentScene().TriggerEffect(14);
                return;
            case 2020:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;
            case 2022:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;
            case 2030:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;
            case 2031:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;
            case 2040:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;

            case 2041:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;

            case 2045:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;

            case 2046:
                SceneManager.instance.GetCurrentScene().TriggerEffect(26);
                return;

            case 2051:
                SceneManager.instance.GetCurrentScene().TriggerEffect(28);
                return;
            case 2061:
                SceneManager.instance.GetCurrentScene().TriggerEffect(29);
                return;
            case 2062:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;
            case 2064:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;
            case 2066:
                SceneManager.instance.GetCurrentScene().TriggerEffect(20);
                return;
            case 2072:
                SceneManager.instance.GetCurrentScene().TriggerEffect(16);
                return;
        }
        if (currentDialog.nextIndex == -100)
        {
            SetAllClose();
            return;
        }
        UpdateDialog(currentDialog.nextIndex);
    }

    //선택지 버튼 중 첫 번째를 골랐을 경우
    public void SelectBtn_1()
    {
        PlayInteractionSound();
        SetAllClose();
        //현재 실행중인 인덱스에 알 맞는 특수 코드 호출
        switch (currentDialog.index)
        {
            case 1048:
                UpdateDialog(1049);
                break;

            case 2057:
                UpdateDialog(2058);
                break;
        }
    }
    //선택지 버튼 중 두 번째를 골랐을 경우
    public void SelectBtn_2()
    {
        PlayInteractionSound();
        //현재 실행중인 인덱스에 알 맞는 특수 코드 호출
        SetAllClose();
        switch (currentDialog.index)
        {
            case 1048:
                UpdateDialog(1051);
                break;

            case 2057:
                UpdateDialog(2060);
                break;

        }
    }

    //선택지 버튼 중 세 번째를 골랐을 경우
    public void SelectBtn_3()
    {
        PlayInteractionSound();
        //현재 실행중인 인덱스에 알 맞는 특수 코드 호출
        SetAllClose();
        switch (currentDialog.index)
        {
            case 1048:
                UpdateDialog(1052);
                break;
        }
    }
    #endregion

}

