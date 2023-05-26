using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AnimationScriptableObject : ScriptableObject
{
    public string animationName;
    public bool isLoop;
    public List<Sprite> sprites;
}
