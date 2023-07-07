using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private const string IS_WALKING = "isWalking";
    private Animator _animator;
    
    private void Awake() {
        _animator = GetComponent<Animator>();    
    }
    
    private void Update()
    {
        _animator.SetBool("isWalking", _player.GetIsWalking());
    }
}
