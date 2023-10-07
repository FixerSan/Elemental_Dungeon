using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public BossController skill;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            float x = Random.Range(-2, 2);
            Managers.Object.SpawnMonster(new Vector3(x, 0, 0), 0);
        }

        if (Input.GetKeyDown(KeyCode.F1))
            Managers.Dialog.Call(0);

        if (Input.GetKeyDown(KeyCode.F2))
            Managers.Screen.SetCameraTarget(Managers.Object.Player.trans);

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Managers.Line.SetLine("Test", new Vector3(0, 0, 0), new Vector3(0, 1, 0), 1);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            Managers.Line.SetLine("Test", new Vector3(0, 0, 0), new Vector3(0, 1, 0), 3);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            CentipedeController centipede = Managers.Resource.Instantiate("Ghost_Centipede",Managers.Object.MonsterTransform).GetOrAddComponent<CentipedeController>();
            centipede.SetPosition(new Vector3(0, 0.5f, 0)); 
            centipede.Init();
        }

        if(Input.GetKeyDown(KeyCode.F6))
        {
            Managers.Object.SpawnItem(1,new Vector3(5,0,0));
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            Managers.Object.Player.inventory.RemoveItem(1);
        }

        if(Input.GetKeyDown(KeyCode.F8))
        {
            Managers.Object.Player.inventory.AddGold(1);
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            Managers.Quest.AddQuest(Define.QuestType.GET,0);
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            Managers.Quest.AddQuest(Define.QuestType.KILL, 0);
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            Managers.Quest.AddQuest(Define.QuestType.KILL, 1);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            skill.trans.position = new Vector2(0, skill.trans.position.y);
            skill.ChangeState(BossState.SKILL_2CAST);
        }
    }
}
