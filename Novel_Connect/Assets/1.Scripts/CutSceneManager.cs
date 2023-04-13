using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    public bool isClearTutorial = false;
    private CameraScript m_Camera;
    private PlayerController player;
    private void Start()
    {
        m_Camera = FindObjectOfType<CameraScript>();
        player = FindObjectOfType<PlayerController>();
        if (!isClearTutorial)
        {
            //StartCoroutine(Tutorial());
        }
    }

    //IEnumerator Tutorial()
    //{
    //    DialogSystem dialogueSystem = GetComponent<DialogSystem>();

    //    player.transform.position = new Vector3(39, 5.5f);
    //    StartCoroutine(GameManager.instance.FadeOut());
    //    yield return new WaitForSeconds(1f);

    //    m_Camera.ChangeState(CameraState.cutscene);
    //    player.ChangeState(PlayerState.idle, false);

    //    player.ChangeDirection(Direction.Left);
    //    bool isWalking = true;

    //    while (isWalking)
    //    {
    //        if (player.playerState != PlayerState.walk)
    //            player.ChangeState(PlayerState.walk, false);
    //        isWalking = player.transform.position.x > 26f;
    //        yield return new WaitForFixedUpdate();
    //    }
    //    player.ChangeState(PlayerState.idle, true);
    //    player.rb.velocity = Vector3.zero;

    //    yield return new WaitForSeconds(1f);
    //    dialogueSystem.UpdateDialog(0);
    //}
}
