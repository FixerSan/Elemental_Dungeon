using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : class //���� �������� ����
{
    //������Ʈ ���� �Լ�
    public abstract void EnterState(T entity);
    //������Ʈ ���� �Լ�
    public abstract void UpdateState(T entity);
    //������Ʈ ���� �Լ�
    public abstract void ExitState(T entity);
}
