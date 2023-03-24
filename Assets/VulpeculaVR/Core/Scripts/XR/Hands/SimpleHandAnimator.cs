using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



[RequireComponent(typeof(Animator))]
public class SimpleHandAnimator : MonoBehaviour
{
    private static readonly int Grip = Animator.StringToHash("Grip");
    private static readonly int Trigger = Animator.StringToHash("Trigger");

    [SerializeField] private float speed;
    private ActionBasedController _controller;

    private Animator _animator;
    private float _gripTarget, _triggerTarget, _gripCurrent, _triggerCurrent;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = transform.parent.gameObject.GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    private void Update()
    {
        _gripTarget = _controller.selectAction.action.ReadValue<float>();
        _triggerTarget = _controller.activateAction.action.ReadValue<float>();
        AnimateHand();
    }

    public void SetGrip(float v)
    {
        _gripTarget = v;
    }

    public void SetTrigger(float v)
    {
        _triggerTarget = v;
    }

    private void AnimateHand()
    {
        if (_gripCurrent != _gripTarget)
        {
            print("grip");
            _gripCurrent = Mathf.MoveTowards(_gripCurrent, _gripTarget, Time.deltaTime * speed);
            _animator.SetFloat(Grip, _gripCurrent);
        }

        if (_triggerCurrent != _triggerTarget)
        {
            _triggerCurrent = Mathf.MoveTowards(_triggerCurrent, _triggerTarget, Time.deltaTime * speed);
            _animator.SetFloat(Trigger, _triggerCurrent);
        }
    }
}
