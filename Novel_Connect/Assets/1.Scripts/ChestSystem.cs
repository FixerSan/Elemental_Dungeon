using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSystem : MonoBehaviour
{
    #region ½Ì±ÛÅæ ¹× DontDestroy
    private ChestSystem() { }
    public static ChestSystem Instance { get; private set; }

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
    }

    #endregion

    InventoryV2 inventory => FindObjectOfType<InventoryV2>();

    public void ChestEvent(int chestIndex,Transform chestTrans)
    {
        switch(chestIndex)
        {
            case 0:
                inventory.AddItem(100);
                break;

            case 1:
                inventory.AddItem(101);
                break;
            case 2:
                GameObject slime = Instantiate(Resources.Load<GameObject>("Prefabs/Monster/Slime"));
                slime.transform.position = new Vector2(chestTrans.transform.position.x, chestTrans.transform.position.y+2);
                break;
            case 3:
                GameObject bat = Instantiate(Resources.Load<GameObject>("Prefabs/Monster/Bat"));
                bat.transform.position = new Vector2(chestTrans.transform.position.x, chestTrans.transform.position.y + 2);
                break;

            case 4:

                break;
        }
    }
}
