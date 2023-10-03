using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NPCController : MonoBehaviour
{
    public bool isHover;
    private SpriteRenderer spriteRenderer;
    public Color color = Color.white;
    public int outlineSize = 1;
    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void EnterHover()
    {
        if (isHover) return;
        isHover = true;
        SetOutline();
    }

    public void ExitHover()
    {
        if (!isHover) return;
        isHover = false;
        SetOutline();
    }

    public void SetOutline()
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", isHover ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }

    public abstract void Interaction();
}
