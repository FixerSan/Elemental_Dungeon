using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave_Obstacle_Interaction_Panel : UIPopup
{
    Cave_Interaction_DragUI[] rotation_Image;
    [SerializeField] private GameObject completeImage;
    bool isComplete = false;

    private void Awake()
    {
        completeImage.SetActive(false);
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
        completeImage.SetActive(true);
        AnimationSystem imageAnimation = GetComponent<AnimationSystem>();
        imageAnimation.PlayAnimation("Cave_Interaction");
        StartCoroutine(CompleteCoroutine());
    }

    IEnumerator CompleteCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.instance.GetCurrentScene().TriggerEffect(25);
    }


}
