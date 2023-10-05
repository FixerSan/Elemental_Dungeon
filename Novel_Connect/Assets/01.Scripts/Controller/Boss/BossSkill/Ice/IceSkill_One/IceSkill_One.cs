using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill_One : MonoBehaviour
{
    public List<IceSkill_One_IceSpear> iceSpears = new List<IceSkill_One_IceSpear>();
    public List<Transform> iceSpearPoses = new List<Transform>();
    public float shootDelay;
    private Transform targetTrans;
    private Transform userTrans;

    public void Init(Transform _userTrans, Transform _targetTrans)
    {
        userTrans = _userTrans;
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

        yield return new WaitForSeconds(10);
        Managers.Resource.Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(userTrans.position.x, userTrans.position.y + 1.6f , userTrans.position.z);
    }
}