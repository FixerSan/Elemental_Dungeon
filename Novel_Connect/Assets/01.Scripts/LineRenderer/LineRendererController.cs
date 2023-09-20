using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController
{
    private LineRenderer line;
    public LineRenderer Line 
    {
        get
        {
            if(line == null)
                Init();
            return line;
        }
    }

    public void Init()
    {
        GameObject go = Managers.Resource.Instantiate("LineRenderer",_parent:Managers.Line.LinesTras, _pooling: true);
        line = go.GetComponent<LineRenderer>();
        line.enabled = false;
    }

    public void SetLine(Vector3 _startPos, Vector3 _endPos, float _width)
    {
        Line.positionCount = 2;
        Vector3[] poses = new Vector3[2];
        poses[0] = _startPos;
        poses[1] = _endPos;
        Line.SetPositions(poses);
        Line.startWidth = _width;
        Line.endWidth = _width;
        Line.enabled = true;
    }

    public void ReleaseLine()
    {
        Managers.Resource.Destroy(Line.gameObject);
    }
}
