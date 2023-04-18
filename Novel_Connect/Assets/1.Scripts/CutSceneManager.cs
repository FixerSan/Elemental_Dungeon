using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    #region Singleton, DontDestoryOnLoad;
    private static CutSceneManager Instance;
    public static CutSceneManager instance
    {
        get
        {
            if (Instance != null)
                return Instance;
            else
                return null;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
    #endregion
    private CameraScript m_Camera;
    private PlayerController player;
    private DialogSystem dialogSystem;
    private void Setup()
    {
        m_Camera = FindObjectOfType<CameraScript>();
        player = PlayerController.instance;
        dialogSystem = DialogSystem.instance;
    }

    public IEnumerator Tutorial_1()
    {

        Setup();

        player.transform.position = new Vector3(36.5f, 5.5f);
        StartCoroutine(GameManager.instance.FadeOut());
        yield return new WaitForSeconds(1f);

        m_Camera.ChangeState(CameraState.cutscene);
        player.ChangeState(PlayerState.Idle);

        player.ChangeDirection(Direction.Left);
        bool isWalking = true;

        while (isWalking)
        {
            if (player.playerState != PlayerState.Walk)
                player.ChangeState(PlayerState.Walk);
            isWalking = player.transform.position.x > 26f;
            yield return new WaitForFixedUpdate();
        }
        player.ChangeState(PlayerState.Idle);
        player.Stop();

        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(0);
    }
}
