using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartScene : UIScene
{
    public override bool Init()
    {
        if (!base.Init())
            return false;
        BindButton(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.Button_Start).gameObject, () => 
        {
            Managers.Scene.LoadScene(Define.Scene.Guild);
        });
        return true;
    }

    private enum Buttons { Button_Start }

}
