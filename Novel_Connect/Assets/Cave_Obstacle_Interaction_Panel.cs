using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave_Obstacle_Interaction_Panel : MonoBehaviour
{
    Cave_Interaction_DragUI[] rotation_Image;
    bool isComplete = false;

    private void Awake()
    {
        rotation_Image = GetComponentsInChildren<Cave_Interaction_DragUI>();
    }

    private void FixedUpdate()
    {
        if (isComplete) return;
        for (int i = 0; i < rotation_Image.Length; i++)
        {
            if (rotation_Image[i].isComplete != true)
                return;
        }

        Complete();
    }

    public void Complete()
    {
        isComplete = true;
        StartCoroutine(CompleteCoroutine());
    }

    IEnumerator CompleteCoroutine()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
