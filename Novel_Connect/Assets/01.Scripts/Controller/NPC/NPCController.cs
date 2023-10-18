using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NPCController : MonoBehaviour
{
    public bool isHover;
    private SpriteRenderer spriteRenderer;
    private Color color = new Color(255,150,0,255);
    protected SpriteRenderer guideSprite;
    public int outlineSize = 1;
    private void Awake()
    {
        Init();
        guideSprite = Util.FindChild<SpriteRenderer>(gameObject, "GuideSprite");
        if (guideSprite != null)
        {
            guideSprite.transform.eulerAngles = Vector3.zero;
            guideSprite.gameObject.SetActive(false);
        }

    }

    protected virtual void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void EnterHover()
    {
        if (isHover) return;
        isHover = true;
        SetOutline();
    }

    public virtual void ExitHover()
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
