using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill_One : MonoBehaviour
{
    public List<IceSkill_One_IceSpear> iceSpears = new List<IceSkill_One_IceSpear>();
    public List<Transform> iceSpearPoses = new List<Transform>();
    public float shootDelay;
    private Transform targetTrans;
    private BaseController user;
    private Coroutine ShotSpearCoroutine;

    public void Init(BaseController _user, Transform _targetTrans)
    {
        user = _user;
        targetTrans = _targetTrans;
       
        for (int i = 0; i < iceSpears.Count; i++)
        {
            iceSpears[i].transform.position = iceSpearPoses[i].position;
            iceSpears[i].gameObject.SetActive(true);
        }
        ShotSpearCoroutine = Managers.Routine.StartCoroutine(ShotSpear());
        Managers.Sound.PlaySoundEffect(Define.AudioClip_Effect.Skill_Ice_One_Create);
    }

    private IEnumerator ShotSpear()
    {
        for (int i = iceSpears.Count - 1; i > -1; i--)
        {
            yield return new WaitForSeconds(shootDelay);
            iceSpears[i].Shot(targetTrans.position, user);
            Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Skill_Ice_One, i);
        }

        yield return new WaitForSeconds(10);
        Managers.Resource.Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Managers.Routine.StopCoroutine(ShotSpearCoroutine);
    }

    private void FixedUpdate()
    {
        if(user != null)
            transform.position = new Vector3(user.trans.position.x, user.trans.position.y + 1.6f , user.trans.position.z);
    }
}