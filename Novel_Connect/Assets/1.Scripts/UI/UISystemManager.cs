using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystemManager : MonoBehaviour
{
    #region Singleton
    public static UISystemManager instance;
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
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        FindPopups();
    }
    #endregion
    private Stack<UIPopup> popupStack = new Stack<UIPopup>();
    private Dictionary<string, UIPopup> popups = new Dictionary<string, UIPopup>();

    private void FindPopups()
    {
        UIPopup[] popups_ = FindObjectsOfType<UIPopup>(true);
        for (int i = 0; i < popups_.Length; i++)
        {
            popups.Add(popups_[i].gameObject.name, popups_[i]);
        }
    }

    public UIPopup GetUIPopup(string popupName)
    {
        if (popups.ContainsKey(popupName))
            return popups[popupName];
        return null; 
    }

    public void ExitLastPopup()
    {
        if(popupStack.Count>0)
        {
            popupStack.Pop().gameObject.SetActive(false);
        }
    }

    public void ExitPopup(string popupName)
    {
        EnterPopup(GetUIPopup(popupName));
        ExitLastPopup();
    }

    public void EnterPopup(UIPopup popup)
    {
        popup.gameObject.SetActive(true);
        popupStack.Push(popup);
    }

    public void EnterPopup(string popupName)
    {
        UIPopup popup = GetUIPopup(popupName);
        popup.gameObject.SetActive(true);
        popupStack.Push(popup);
    }


    private void Update()
    {
        ChackExitInput();
        ChackInventoryInput();
    }

    void ChackExitInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLastPopup();
        }
    }

    void ChackInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (GetUIPopup("InventoryUI").gameObject.activeSelf)
            {
                EnterPopup(GetUIPopup("InventoryUI"));
                ExitLastPopup();
            }
            else
                EnterPopup(GetUIPopup("InventoryUI"));
        }
    }
}
