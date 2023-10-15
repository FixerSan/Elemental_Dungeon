using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class UIRetry : UIPopup
{
    private bool isOpen;
    private float currentTimeCount;
    private CanvasGroup canvasGroup;
    public int timeCount;
    public override bool Init()
    {
        if (!base.Init())
            return false;

        isOpen = false;
        currentTimeCount = timeCount;
        canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        BindEvent(GetButton((int)Buttons.Button_Yes).gameObject, _callback:OnClick_RetryGame);
        BindEvent(GetButton((int)Buttons.Button_No).gameObject, _callback: OnClick_ResetGame);
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, 1).SetEase(Ease.Linear).onComplete += () => { isOpen = true; };

        return true;
    }

    public void OnClick_RetryGame()
    {
        Debug.Log("Y");
    }

    public void OnClick_ResetGame()
    {
        Debug.Log("N");
        Managers.Game.RestartGame();
    }

    public void CheckTimeCount()
    {
        if (currentTimeCount > 0)
        {
            currentTimeCount -= Time.deltaTime;
            if (currentTimeCount <= 0)
            {
                currentTimeCount = 0;
                OnClick_ResetGame();
            }
        }

        GetText((int)Texts.Text_Count).text = $"{(int)currentTimeCount}";
    }

    protected override void Update()
    {
        if (!isOpen) return;
        CheckTimeCount();
    }

    private enum Buttons
    {
        Button_Yes, Button_No
    }

    private enum Texts
    {
        Text_Count
    }
}
