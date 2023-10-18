using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatController : InteractableObject
{
    [SerializeField] private Transform leftGettingPos;
    [SerializeField] private Transform rightGettingPos;
    private Define.Direction direction;
    private PlayerController player;
    private AudioSource audioSource;
    private bool isFirst;
    private bool isUsing;

    protected override void Awake()
    {
        base.Awake();
        direction = Define.Direction.Right;
        leftGettingPos.SetParent(null);
        rightGettingPos.SetParent(null);
        isFirst = true;
        isUsing = false;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected override void CheckUse()
    {
        if (!isCanUse) return;
        if (isUsing) return;
        Managers.Input.CheckInput(Managers.Input.interactionKey, (_inputType) =>
        {
            if (_inputType != InputType.PRESS) return;
            if(isFirst)
            {
                if (Managers.Object.Monsters.Count == 0)
                {
                    Use();
                    isFirst = false;
                }
                else
                    Managers.UI.ShowToast("필드 내 몬스터를 전부 처치해 주세요!").SetColor(Color.red);
                return;
            }
            Use();
        });
    }

    protected override void Use()
    {
        isUsing = true;
        audioSource.Play();
        Managers.Input.isCanControl = false;
        player.ChangeState(PlayerState.IDLE);
        player.Stop();
        player.SetPosition(transform.position);
        player.trans.SetParent(transform);
        player.ChangeDirection(direction);
        transform.DOMoveX(transform.position.x + ((int)direction * 5.7f), 4).SetEase(Ease.Linear).onComplete += () => 
        {
            player.trans.SetParent(null);
            Managers.Input.isCanControl = true;
            if (direction == Define.Direction.Right) player.SetPosition(rightGettingPos.position);
            else player.SetPosition(leftGettingPos.position);
            direction = (Define.Direction)((int)direction * -1);
            if (direction == Define.Direction.Right)
                transform.eulerAngles = new Vector3(0, 0, 0);
            if (direction == Define.Direction.Left)
                transform.eulerAngles = new Vector3(0, 180, 0);
            isUsing = false;
            audioSource.Stop();
        };
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCanUse = true;
            player = collision.GetComponent<PlayerController>();
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCanUse = false;
            player = null;
        }
    }
}
