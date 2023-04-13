using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : class //참조 형식으로 제약
{
    //스테이트 시작 함수
    public abstract void EnterState(T entity);
    //스테이트 지속 함수
    public abstract void UpdateState(T entity);
    //스테이트 종료 함수
    public abstract void ExitState(T entity);
}
