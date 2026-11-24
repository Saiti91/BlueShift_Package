using UnityEngine;

public class BallNoPhysicsScript : MonoBehaviour
{
    public float timeBeforeDestroy = 2.0f;
    public float moveSpeed = 10f;

    [Header("Étirement")]
    public float stretchFactor = 0.05f;
    public float maxStretch = 2.0f;

    [Header("Moyenne de vitesse")]
    public int sampleCount = 5;     // nombre de frames pour la moyenne

    Vector3 _baseScale;
    Vector3 _lastPosition;
    Vector3 _velocity;

    // Buffers circulaires
    Vector3[] _displacements;
    float[] _deltaTimes;
    int _index;     // case où écrire la prochaine valeur
    int _filled;    // nombre de cases remplies (< sampleCount au début)

    void Start()
    {
        _baseScale = transform.localScale;
        _lastPosition = transform.position;

        _displacements = new Vector3[sampleCount];
        _deltaTimes = new float[sampleCount];

        Destroy(gameObject, timeBeforeDestroy);
    }

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        // 1. Enregistrer le déplacement de cette frame
        if (Time.deltaTime > 0f)
        {
            _displacements[_index] = transform.position - _lastPosition;
            _deltaTimes[_index] = Time.deltaTime;

            _index = (_index + 1) % sampleCount;           // on boucle sur le tableau
            _filled = Mathf.Min(_filled + 1, sampleCount);
        }
        _lastPosition = transform.position;

        // 2. Moyenne sur les frames enregistrées
        Vector3 totalDisplacement = Vector3.zero;
        float totalTime = 0f;
        for (int i = 0; i < _filled; i++)
        {
            totalDisplacement += _displacements[i];
            totalTime += _deltaTimes[i];
        }

        if (totalTime > 0f)
            _velocity = totalDisplacement / totalTime;

        // 3. Étirement
        float speed = _velocity.magnitude;
        float stretch = Mathf.Min(1f + speed * stretchFactor, maxStretch);

        transform.localScale = new Vector3(
            _baseScale.x,
            _baseScale.y,
            _baseScale.z * stretch
        );
    }
}