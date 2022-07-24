using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private HelloWorldShaderRendererFeature _Feature;

    void Start()
    {
        _Feature.color = Color.cyan;
    }
    // Update is called once per frame
    void Update()
    {
        _Feature.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
        
    }
}
