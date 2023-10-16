using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private new Camera camera;
    public Camera Camera 
    { 
        get
        {
            if (camera == null)
                camera = gameObject.GetOrAddComponent<Camera>();
            return camera;
        }
    }

    private Transform trans;
    public Transform Trans
    {
        get
        {
            if (trans == null)
                trans = gameObject.GetOrAddComponent<Transform>();
            return trans;
        }
    }

    public Transform target;

    public Transform listener;

    public Vector3 offset;
    public Vector2 min, max;
    public float delayTime;

    public bool isShake = false;
    public float shakeForce = 0;


    private Vector3 nextPos;

    private void Awake()
    {
        if (Managers.Screen.CameraController != this)
            Managers.Resource.Destroy(gameObject);
        listener = Util.FindChild<AudioListener>(gameObject).transform;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetOffset(Vector3 _offset)
    {
        offset = _offset;
    }

    public void FollowTarget()
    {
        if (target == null) return;
        nextPos = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, -10);
        nextPos = Vector3.Lerp(Trans.position, nextPos, delayTime * Time.deltaTime);
        nextPos = new Vector3(Mathf.Clamp(nextPos.x, min.x, max.x), Mathf.Clamp(nextPos.y, min.y, max.y), -10);
        if(isShake)
            nextPos = nextPos + (Vector3)UnityEngine.Random.insideUnitCircle * shakeForce * Time.deltaTime;
        Trans.position = nextPos;
        listener.transform.position = target.transform.position;
    }

    public void LinearMoveCamera(Vector3 _pos , float _moveTotalTime , Action _callback = null)
    {
        Trans.DOMove(_pos, _moveTotalTime).onComplete += () => 
        {
            _callback?.Invoke();
        };
    }

    public IEnumerator FadeIn(float _fadeTime, Action _callback = null)
    {
        Managers.UI.BlackPanel.gameObject.SetActive(true);
        Managers.UI.BlackPanel.alpha = 0;
        while (Managers.UI.BlackPanel.alpha < 1)
        {
            Managers.UI.BlackPanel.alpha = Managers.UI.BlackPanel.alpha + Time.deltaTime / _fadeTime;
            yield return null;
        }
        Managers.UI.BlackPanel.alpha = 1;
        _callback?.Invoke();
    }

    public IEnumerator FadeOut(float _fadeTime, Action _callback = null)
    {
        Managers.UI.BlackPanel.gameObject.SetActive(true);
        Managers.UI.BlackPanel.alpha = 1;
        while (Managers.UI.BlackPanel.alpha > 0)
        {
            Managers.UI.BlackPanel.alpha = Managers.UI.BlackPanel.alpha - Time.deltaTime / _fadeTime;
            yield return null;
        }
        Managers.UI.BlackPanel.alpha = 0;
        Managers.UI.BlackPanel.gameObject.SetActive(false);
        _callback?.Invoke();
    }

    public IEnumerator FadeInOut(float _totalTile, Action _callback = null)
    {
        yield return StartCoroutine(FadeIn(_totalTile * 0.5f));
        yield return StartCoroutine(FadeOut(_totalTile * 0.5f));
        _callback?.Invoke();
    }



    private void Update()
    {
        FollowTarget();
    }
}
