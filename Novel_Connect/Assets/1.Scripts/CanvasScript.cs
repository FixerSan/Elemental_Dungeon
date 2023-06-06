using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    #region Singleton
    public static CanvasScript instance;
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
    public List<GameObject> nomalUIs;
    public List<GameObject> clickableUIs;

    public LayerMask clickLayer;
    public GameObject canClickNPC;

    private void Start()
    {
        //AllClearUI();
    }

    private void Update()
    {
        if(CanClickCheck())
            HoverCheck();
        if (canClickNPC)
            ClickCheck();
    }
    private void HoverCheck()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0, clickLayer);


        if (hit.collider != null)
        {
            canClickNPC = hit.transform.gameObject;
            canClickNPC.GetComponent<SpriteRenderer>().enabled = true;
        }

        else
        {
            if (canClickNPC)
            {
                canClickNPC.GetComponent<SpriteRenderer>().enabled = false;
                canClickNPC = null;
            }
        }
    }
    private void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AllClearUI();

            if (canClickNPC.GetComponent<ClickableNPC>() != null)
            {
                canClickNPC.GetComponent<ClickableNPC>().Interaction();
                canClickNPC.GetComponent<SpriteRenderer>().enabled = false;
                canClickNPC = null;
            }
        }
    }

    bool CanClickCheck()
    {
        foreach (var item in nomalUIs)
        {
            if(item.activeSelf == true)
            {
                return false;
            }
        }
        return true;
    }

    void AllClearUI()
    {
        foreach (var item in nomalUIs)
        {
            item.SetActive(false);
        }
        foreach (var item in clickableUIs)
        {
            item.SetActive(false);
        }
    }
}
