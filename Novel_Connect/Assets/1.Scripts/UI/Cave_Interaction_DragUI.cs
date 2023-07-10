using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cave_Interaction_DragUI : MonoBehaviour, IDragHandler
{
    public bool isComplete = false;

    public float rotateSpeed = 10;
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isComplete) return;

        //�� ���� ���ϱ�
        float x = eventData.delta.x;
        float y = eventData.delta.y;

        if (eventData.position.y > Screen.height / 2)
        {
            if (eventData.position.x > Screen.width / 2)
            {
                //1��и�
                x *= -1;
                y *= 1;
            }

            else
            {
                //2��и�
                x *= -1;
                y *= -1;
            }
        }

        else
        {
            if (eventData.position.x < Screen.width / 2)
            {
                //3��и�
                x *= 1;
                y *= -1;
            }

            else
            {
                //4��и�
                x *= 1;
                y *= 1;
            }
        }

        float magnitude = new Vector2(x, y).magnitude;
        x /= magnitude;
        y /= magnitude;

        float dir = (x + y) * Time.deltaTime * rotateSpeed;
        transform.Rotate(0, 0, dir);

        if (rect.eulerAngles.z % 360 < 4)
        {
            isComplete = true;
            rect.eulerAngles = new Vector3(0, 0, 0);
        }

    }
}
