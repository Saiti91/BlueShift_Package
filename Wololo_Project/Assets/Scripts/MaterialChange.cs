using System;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public Material ballMat;
    public float maxSpeed;
    public Color baseColor;
    public Color highSpeedColor;
    public float currentSpeed;

    private void Update()
    {
        ballMat.color = Color.Lerp(baseColor, highSpeedColor, currentSpeed/maxSpeed);
    }
}
