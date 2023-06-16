using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererSystem : MonoBehaviour
{
    private LineRenderer line => GetComponent<LineRenderer>();

    private void Awake()
    {
        line.positionCount = 2;
        line.enabled = false;
    }

    

    public void Play(Vector3 from, Vector3 to)
    {
        line.enabled = true;
        line.SetPosition(0, from);
        line.SetPosition(1, to);
    }

    public void Play(Vector3 from, Vector3 to, float time)
    {
        line.enabled = true;

        line.SetPosition(0, from);
        line.SetPosition(1, to);
        CheckStop(time);
    }

    IEnumerator CheckStop(float time)
    {
        yield return new WaitForSeconds(time);
        Stop();
    }

    public void Play(Vector3[] points)
    {
        line.positionCount = points.Length;
        line.enabled = true;

        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i]);
        }
    }

    public void Play(Vector3[] points,float time)
    {
        line.positionCount = points.Length;
        line.enabled = true;

        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i]);
        }
        CheckStop(time);
    }


    public void Stop()
    {
        line.enabled = false;
    }
}
