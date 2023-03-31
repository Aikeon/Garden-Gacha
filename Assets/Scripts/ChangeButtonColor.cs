using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonColor : MonoBehaviour
{
    MeshRenderer rend;
    int destroySaveCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) Change();
    }
    
    public void Change()
    {
        destroySaveCount++;
        if (destroySaveCount == 1) rend.material.SetColor("_EmissionColor",new Color(0.6f,0f,0f,1f));
        if (destroySaveCount == 2) rend.material.SetColor("_EmissionColor",new Color(0.3f,0f,0f,1f));
        if (destroySaveCount >= 3) Destroy(gameObject);
    }
}
