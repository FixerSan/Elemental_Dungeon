using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    public GameObject fieldItemPrefab;
    public Vector2[] pos;

    private void Start()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            GameObject item = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            item.GetComponent<FieldItems>().SetItem(DataBase.instance.datas.itemDatas[Random.Range(0, DataBase.instance.datas.itemDatas.Length)]);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            for (int i = 0; i < 300; i++)
            {
                GameObject item = Instantiate(fieldItemPrefab, pos[0], Quaternion.identity);
                item.GetComponent<FieldItems>().SetItem(DataBase.instance.datas.itemDatas[Random.Range(0, DataBase.instance.datas.itemDatas.Length)]);
            }

        }
    }
}
