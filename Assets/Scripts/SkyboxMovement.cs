using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private static readonly int Rotation = Shader.PropertyToID("_Rotation");

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat(Rotation,Time.time * speed);
    }
}
