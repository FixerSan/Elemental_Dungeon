using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    TextMeshPro textMeshPro;
    RectTransform rect;
    public float fadeTime;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        rect = GetComponent<RectTransform>();
    }

    public void Setup(float damage)
    {
        textMeshPro.color = Color.white;
        textMeshPro.text = Mathf.Round(damage).ToString();
        StartCoroutine(FadeOut());
    }

    private void FixedUpdate()
    {
        rect.eulerAngles = Vector3.zero;
        transform.position += Vector3.up*Time.deltaTime;
    }

    IEnumerator FadeOut()
    {
        while(textMeshPro.color.a > 0)
        {
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, textMeshPro.color.a - Time.deltaTime / fadeTime);
            yield return null;
        }

        ObjectPool.instance.ReturnDamageText(this.gameObject);
    }
}
