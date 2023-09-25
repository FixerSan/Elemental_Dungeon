using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary> ��� �巡�� �� ��ӿ� ���� UI �̵� </summary>
public class DraggableImage : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Transform _targetTr; // �̵��� UI

    private Vector2 _startingPoint;
    private Vector2 _moveBegin;
    private Vector2 _moveOffset;

    private void Awake()
    {
        // �̵� ��� UI�� �������� ���� ���, �ڵ����� �θ�� �ʱ�ȭ
        if (_targetTr == null)
            _targetTr = transform.parent;
    }

    // �巡�� ���� ��ġ ����
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _startingPoint = _targetTr.position;
        _moveBegin = eventData.position;
    }

    // �巡�� : ���콺 Ŀ�� ��ġ�� �̵�
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _moveOffset = eventData.position - _moveBegin;
        _targetTr.position = _startingPoint + _moveOffset;
    }
}
