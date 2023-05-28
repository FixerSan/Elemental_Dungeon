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
    }
    #endregion
    private Stack<UIPopup> popupStack = new Stack<UIPopup>();
    public List<UIPopup> uIPopups;//0 = Inventory

    public void ExitLastPopup()
    {
        if(popupStack.Count>0)
        {
            popupStack.Pop().gameObject.SetActive(false);
        }
    }

    public void EnterPopup(UIPopup popup)
    {
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
            if (uIPopups[0].gameObject.activeSelf)
            {
                EnterPopup(uIPopups[0]);
                ExitLastPopup();
            }
            else
                EnterPopup(uIPopups[0]);
        }
    }
}
