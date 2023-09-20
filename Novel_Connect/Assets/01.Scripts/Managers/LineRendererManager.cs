using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager
{
    private Transform linesTras;
    public Transform LinesTras
    {
        get
        {
            if(linesTras == null)
            {
                GameObject go = GameObject.Find("@LineRenderers");
                if (go == null)
                    go = new GameObject(name: "@LineRenderers");
                linesTras = go.transform;
                UnityEngine.Object.DontDestroyOnLoad(go);
            }
            return linesTras;
        }
    }
    public Dictionary<string, LineRendererController> lines;
    public void SetLine(string _key, Vector3 _startPos, Vector3 _endPos, float _width)
    {
        if (!lines.ContainsKey(_key))
        {
            LineRendererController line = new LineRendererController();
            lines.Add(_key, line);
        }
        lines[_key].SetLine(_startPos,_endPos, _width);
    }

    public void ReleaseLine(string _key)
    {
        if (lines.TryGetValue(_key, out LineRendererController _line))
        {
            _line.ReleaseLine();
            lines.Remove(_key);
        }
    }

    public LineRendererManager()
    {
        lines = new Dictionary<string, LineRendererController>();
    }
}
