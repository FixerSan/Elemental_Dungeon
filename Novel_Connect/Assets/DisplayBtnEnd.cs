using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBtnEnd : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.instance.LoadScene("Cave");
    }
}
