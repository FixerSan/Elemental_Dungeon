using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBosSkill_1 : MonoBehaviour
{
    public List<IceSpear> list = new List<IceSpear>();
    public float shotDuration;
    public Actor actor;
    public Direction direction;

    public void Setup(Actor actor_)
    {
        actor = actor_;
        transform.position = actor.transform.position;
        transform.eulerAngles = actor.transform.eulerAngles;
        direction = actor.direction;
        StartCoroutine(Shot());
    }

    public IEnumerator Shot()
    {
        while(list.Count >0)
        {
            yield return new WaitForSeconds(shotDuration);
            list[list.Count - 1].gameObject.SetActive(true);
            list[list.Count - 1].SkillSetup(this);
            list.Remove(list[list.Count - 1]);
        }

        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
