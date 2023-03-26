using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGuyBehaviour : MonoBehaviour
{
    public static TheGuyBehaviour Instance;
    private float v;
    private Vector3 bEA;
    private Animator anim;
    private int latestState;

    [SerializeField] private Renderer rendererVAT;
    public Material materialVAT;
    public bool isAnim = false;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bEA = transform.eulerAngles;  
        anim = GetComponentInChildren<Animator>();
        materialVAT = rendererVAT.material;
    }


    public void PlaySatisfied()
    {
        if(isAnim) return;
        isAnim = true;
        anim.CrossFade("Satisfied",0.2f);
    }

    public void PlayOkay()
    {
        if(isAnim) return;
        isAnim = true;
        anim.CrossFade("Okay",0.2f);
    }
    
    public void PlayDisapointed()
    {
        if(isAnim) return;
        isAnim = true;
        anim.CrossFade("Disapointed",0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position) < 3)
        {
            var curRot = transform.eulerAngles.y;
            transform.LookAt(PlayerMovement.Instance.transform);
            transform.eulerAngles = new Vector3(0,Mathf.SmoothDamp(curRot,transform.eulerAngles.y,ref v,Time.deltaTime, 90f),0);
        }
        else 
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, bEA, 0.03f);
        }
        
        if(Input.GetKey(KeyCode.I)) PlayDisapointed();
        if(Input.GetKey(KeyCode.O)) PlayOkay();
        if(Input.GetKey(KeyCode.P)) PlaySatisfied();
    }
}
