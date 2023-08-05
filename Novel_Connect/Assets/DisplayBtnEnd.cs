using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBtnEnd : MonoBehaviour
{
    public int index;
    public void OnClick()
    {
        switch (index)
        {
            case 0:
                SceneManager.instance.LoadScene("Cave");
                break;
            case 1:
                SceneManager.instance.LoadScene("StartScene");
                break;
        }
    }
}
