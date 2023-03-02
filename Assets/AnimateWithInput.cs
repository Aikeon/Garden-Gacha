using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateWithInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty gripAnimation;

    [SerializeField] private Animator handAnimator;

    public bool trigger = false;
    
    
    void Update()
    {
        trigger = gripAnimation.action.ReadValue<bool>();
        handAnimator.SetFloat("Grip", trigger?0.6f:0f);
    }
}
