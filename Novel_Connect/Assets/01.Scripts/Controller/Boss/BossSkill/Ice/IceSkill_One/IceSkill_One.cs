using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill_One : MonoBehaviour
{
    public List<IceSkill_One_IceSpear> iceSpears = new List<IceSkill_One_IceSpear>();
    public List<Transform> iceSpearPoses = new List<Transform>();
    public float shootDelay;
    private Transform targetTrans;

    public void Init(Transform _targetTrans)
    {
        targetTrans = _targetTrans;
        for (int i = 0; i < iceSpears.Count; i++)
        {
            iceSpears[i].transform.position = iceSpearPoses[i].position;
            iceSpears[i].gameObject.SetActive(true);
        }
        Managers.Routine.StartCoroutine(ShotSpear());
    }

    private IEnumerator ShotSpear()
    {
        for (int i = iceSpears.Count - 1; i > -1; i--)
        {
            yield return new WaitForSeconds(shootDelay);
            iceSpears[i].Shot(targetTrans.position);
        }
    }
}
