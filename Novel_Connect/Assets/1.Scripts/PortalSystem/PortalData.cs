using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potal", menuName = "Containers/Portal", order = 2)]
public class PortalData : ScriptableObject
{
    public string portal_name;
    public string sceneName;
    public Vector2 portPos;
    public Direction direction;
}
