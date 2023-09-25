using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager
{
    private Transform linesTras; // 라인 렌더러들을 담을 부모 트랜스폼
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
    public Dictionary<string, LineRendererController> lines;    // 라인 렌더러 딕셔너리
    
    // 라인 렌더러 설정
    public void SetLine(string _key, Vector3 _startPos, Vector3 _endPos, float _width)
    {
        if (!lines.ContainsKey(_key))
        {
            LineRendererController line = new LineRendererController();
            lines.Add(_key, line);
        }
        lines[_key].SetLine(_startPos,_endPos, _width);
    }

    // 라인 렌더러 해제
    public void ReleaseLine(string _key)
    {
        if (lines.TryGetValue(_key, out LineRendererController _line))
        {
            _line.ReleaseLine();
            lines.Remove(_key);
        }
    }

    //라인 렌더러 초기화
    public LineRendererManager()
    {
        lines = new Dictionary<string, LineRendererController>();
    }
}
