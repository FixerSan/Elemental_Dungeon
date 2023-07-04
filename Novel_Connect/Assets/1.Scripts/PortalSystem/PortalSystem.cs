using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    #region Singleton
    public static PortalSystem instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public Coroutine portCoroutine;
    public void PortOject(GameObject portObject, string portSpotName)
    {
        if (portCoroutine != null) return;
        portCoroutine = StartCoroutine(PortCoroutine(portObject, portSpotName));
    }

    IEnumerator PortCoroutine(GameObject portObject, string portSpotName)
    {
        GameManager.instance.player.anim.SetBool("isPort", true);
        GameManager.instance.player.playerInput.isCanControl = false;
        GameManager.instance.player.ChangeState(PlayerState.Idle);
        GameManager.instance.player.playerMovement.Stop();
        PortalData portalData = DataBase.instance.GetPortalSpot(portSpotName);

        yield return StartCoroutine(ScreenEffect.instance.FadeIn(1f));
        GameManager.instance.player.anim.SetBool("isPort", false);
        SceneManager.instance.LoadScene(portalData.sceneName);
        portObject.transform.position = portalData.portPos;
        if (portalData.direction == Direction.Left)
        {
            portObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            portObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        GameManager.instance.player.playerInput.isCanControl = true;
        yield return StartCoroutine(ScreenEffect.instance.FadeOut(1f));
        portCoroutine = null;
    }
}
