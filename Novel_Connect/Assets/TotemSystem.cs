using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemSystem : MonoBehaviour
{
    #region ½Ì±ÛÅæ ¹× DontDestroy
    private TotemSystem() { }
    public static TotemSystem Instance { get; private set; }

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
        inventory = FindObjectOfType<InventoryV2>();
    }

    #endregion
    InventoryV2 inventory;

    public bool CheckCanUse(int index)
    {
        switch(index)
        {
            case 0:
                return inventory.CheckHasItem(100);
            default:
                return false;
        }
    }

    public void TotemEvent(int index, Transform trans)
    {
        switch (index)
        {
            case 0:
                StartCoroutine(Event_0(trans));
                break;
        }
    }

    IEnumerator Event_0(Transform trans)
    {
        inventory.RemoveItem(100);
        GameManager.instance.player.ChangeState(PlayerState.Idle);
        GameManager.instance.player.playerInput.isCanControl = false;
        GameManager.instance.player.playerMovement.Stop();
        yield return new WaitForSeconds(1);
        GameManager.instance.player.anim.SetBool("isPort", true);
        yield return new WaitForSeconds(0.5f);
        PortalSystem.instance.PortOject(GameManager.instance.player.gameObject, "Cave_5");
        yield return new WaitForSeconds(1);
        GameManager.instance.player.anim.SetBool("isPort", false);
        GameManager.instance.player.playerInput.isCanControl = true;
    }
}
