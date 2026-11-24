using System;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    
    Rigidbody _rb;
    
    public Material ballMat;
    public float maxSpeed;
    public Color baseColor;
    public Color highSpeedColor;
    public float currentSpeed;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        currentSpeed = _rb.linearVelocity.magnitude;
        ballMat.color = Color.Lerp(baseColor, highSpeedColor, currentSpeed/maxSpeed);
    }
}
