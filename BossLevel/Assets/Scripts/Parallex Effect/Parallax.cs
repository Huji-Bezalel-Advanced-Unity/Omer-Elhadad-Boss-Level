using System;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private Material _material;
    private float _distance;
    
    [Range(0,0.5f)] public float speed = 0.2f;

    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }


    void Update()
    {
        _distance += speed * Time.deltaTime;
        _material.SetTextureOffset(MainTex, Vector2.right * _distance);  
    }
}
