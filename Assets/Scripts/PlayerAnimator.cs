using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    
    [SerializeField] private Player _player;
    
    private Animator _animator;
    
    private void Awake() {
        _animator = GetComponent<Animator>();    
    }
    
    private void Update()
    {
        _animator.SetBool("isWalking", _player.GetIsWalking());
    }
}
