using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScene : BaseScene
{
    Centipede centipede => FindObjectOfType<Centipede>();

    protected override void Setup()
    {
        centipede.gameObject.SetActive(false);
        ObjectPool.instance.InitHpBar(5);
    }

    public override void TriggerEffect(int index)
    {
        switch (index)
        {
            case 0:
                ScreenEffect.instance.ShakeHorizontal(0.3f, 0.2f);
                
                break;
        }
    }
}
