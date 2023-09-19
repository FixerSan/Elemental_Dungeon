using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager
{
    public ParticleSystem PlayParticle(string _key, Vector2 _position, Transform _parent = null, Define.Direction _direction = Define.Direction.Left)
    {
        GameObject go = Managers.Resource.Instantiate(_key,_pooling:true);
        if(_parent != null) go.transform.SetParent(_parent);
        go.transform.position = _position;
        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        ps.Play();
        return ps;
    }
}
