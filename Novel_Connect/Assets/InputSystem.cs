using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    #region Singleton
    private static InputSystem Instance;
    public static InputSystem instance
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
    public KeyCode upArrow_;
    public bool upArrow;

    public KeyCode leftArrow_;
    public bool leftArrow;

    public KeyCode downArrow_;
    public bool downArrow;

    public KeyCode rightArrow_;
    public bool rightArrow;

    public KeyCode alpha1_;
    public bool alpha1;

    public KeyCode alpha2_;
    public bool alpha2;

    public KeyCode alpha3_;
    public bool alpha3;

    public KeyCode alpha4_;
    public bool alpha4;

    public KeyCode escape_;
    public bool escape;

    public KeyCode q_;
    public bool q;

    public KeyCode w_;
    public bool w;

    public KeyCode e_;
    public bool e;

    public KeyCode r_;
    public bool r;

    private void Update()
    {
        upArrow = Input.GetKeyDown(upArrow_);
        leftArrow = Input.GetKeyDown(leftArrow_);
        downArrow = Input.GetKeyDown(downArrow_);
        rightArrow = Input.GetKeyDown(rightArrow_);
        alpha1 = Input.GetKeyDown(alpha1_);
        alpha2 = Input.GetKeyDown(alpha2_);
        alpha3 = Input.GetKeyDown(alpha3_);
        alpha4 = Input.GetKeyDown(alpha4_);
        escape = Input.GetKeyDown(escape_);
        q = Input.GetKeyDown(q_);
        w = Input.GetKeyDown(w_);
        e = Input.GetKeyDown(e_);
        r = Input.GetKeyDown(r_);
    }
    
}
