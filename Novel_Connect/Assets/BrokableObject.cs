using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BrokableObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BrokeTrigger"))
        {
            StartCoroutine(FadeOut(collision));
        }
    }

    public IEnumerator FadeOut(Collider2D collision)
    {
        Rigidbody2D collRB = collision.GetComponent<Rigidbody2D>();
        collRB.gravityScale = 0f;
        collRB.velocity = Vector2.zero;

        StartCoroutine(SpriteEffect.instance.FadeOutCoroutine(collision.GetComponent<SpriteRenderer>(), 0.25f));
        ScreenEffect.instance.ShakeHorizontal(0.3f, 0.5f);
        yield return StartCoroutine(SpriteEffect.instance.FadeOutCoroutine(GetComponent<Tilemap>(), 0.25f));
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
