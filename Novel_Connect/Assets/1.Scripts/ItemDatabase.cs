using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;
    public Vector2[] pos;
    private void Start()
    {
        for(int i = 0; i< pos.Length; i++)
        {
            GameObject item = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            item.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, itemDB.Count)]);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            for(int i = 0; i < 300; i++)
            {
                GameObject item = Instantiate(fieldItemPrefab, pos[0], Quaternion.identity);
                item.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, itemDB.Count)]);
            }

        }
    }
}
