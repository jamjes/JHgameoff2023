using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour, IAnimatable
{
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Airbourne = Animator.StringToHash("airbourne");
    private static readonly int Backflip = Animator.StringToHash("backflip");
    private static readonly int Wait = Animator.StringToHash("wait");
    [SerializeField] private Animator _anim;

    private PlayerMovementController _playerRef;

    private void Start()
    {
        _playerRef = new PlayerMovementController();
        _anim.CrossFade(Run,0,0);
    }

    public void SetAnim(string animName)
    {
        switch(animName)
        {
            case "run":
                _anim.CrossFade(Run, 0, 0);
                break;
            
            case "backflip":
                _anim.CrossFade(Backflip, 0, 0);
                break;
            
            case "wait":
                _anim.CrossFade(Wait, 0, 0);
                break;
            
            case "airbourne":
                _anim.CrossFade(Airbourne, 0, 0);
                break;
        }
    }
}
