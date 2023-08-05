using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSystem : MonoBehaviour
{
    public Dictionary<string, AnimationScriptableObject> animations = new Dictionary<string, AnimationScriptableObject>();
    public List<AnimationScriptableObject> animationList = new List<AnimationScriptableObject>();
    public float cycleTime = 0.1f;

    private AnimationScriptableObject currentAnimation;
    private int currentKeyCount;
    private bool isPlaying = false;
    private bool isCanChange = false;
    private float checkTime;
    [SerializeField]public Image spriteRenderer;

    public void Awake()
    {
        foreach (var item in animationList)
        {
            animations.Add(item.animationName, item);
        }
    }

    private void Update()
    {
        if(isPlaying)
            Play();
    }
    public void PlayAnimation(string animName)
    {
        if (animations.ContainsKey(animName))
        {
            checkTime = 0;
            currentKeyCount = 0;
            isCanChange = true;
            
            currentAnimation = animations[animName];
            isPlaying = true;
        }
    }

    public void StopAnimation()
    {
        isPlaying = false;
        isCanChange = false;
        currentAnimation = null;
        currentKeyCount = 0;
        checkTime = 0;
    }

    private void Play()
    {
        if (currentAnimation == null)
            return;

        checkTime += Time.deltaTime;

        if(currentAnimation.isLoop)
        {
            if(isCanChange)
            {
                currentKeyCount = currentKeyCount % currentAnimation.sprites.Count;
                spriteRenderer.sprite = currentAnimation.sprites[currentKeyCount];
                isCanChange = false;
            }
            if (checkTime >= cycleTime)
            {
                checkTime = 0;
                currentKeyCount++;
                isCanChange = true;
            }
        }

        else
        {
            if (isCanChange)
            {
                spriteRenderer.sprite = currentAnimation.sprites[currentKeyCount];
                isCanChange = false;
            }
            if (checkTime >= cycleTime)
            {
                checkTime = 0;
                currentKeyCount++;
                isCanChange = true;
                if (currentKeyCount == currentAnimation.sprites.Count - 1)
                    StopAnimation();
            }
        }
    }

    public float GetAnimationPlayTime(string animName)
    {
        if(animations.ContainsKey(animName))
            return animations[animName].sprites.Count * cycleTime;
        return 0;
    }
    public float GetCurrentAnimationPlayTime()
    {
        if (currentAnimation != null)
            return currentAnimation.sprites.Count* cycleTime;
        return 0;
    }
}
