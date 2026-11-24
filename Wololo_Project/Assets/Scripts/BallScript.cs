using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float timeBeforeDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }
}
