using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGuyBehaviour : MonoBehaviour
{
    public static TheGuyBehaviour Instance;
    public int state;
    private float v;
    private Vector3 bEA;
    private Animator anim;
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
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("State",state);
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
    }
}
