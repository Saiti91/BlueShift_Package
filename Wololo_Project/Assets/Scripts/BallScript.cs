using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float timeBeforeDestroy = 2.0f;

    [Header("Étirement")]
    public float stretchFactor = 0.05f; // combien on étire par unité de vitesse
    public float maxStretch = 2.0f;     // étirement max (x2)

    Rigidbody _rb;
    Vector3 _baseScale;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _baseScale = transform.localScale;
        Destroy(gameObject, timeBeforeDestroy);
    }

    void Update()
    {
        float speed = _rb.linearVelocity.magnitude;
        float stretch = Mathf.Min(1f + speed * stretchFactor, maxStretch);

        transform.localScale = new Vector3(
            _baseScale.x ,
            _baseScale.y,
            _baseScale.z* stretch
        );
    }
}