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
    public void PortOject(GameObject portObject, string portSpotName)
    {
        PortalData portalData = DataBase.instance.GetPortalSpot(portSpotName);

        SceneManager.instance.LoadScene(portalData.sceneName);
        portObject.transform.position = portalData.portPos;
        if(portalData.direction == Direction.Left)
        {
            portObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            portObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }
}
