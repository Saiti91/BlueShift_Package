using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawnScript : MonoBehaviour
{
    InputAction m_ShootAction;
    public GameObject m_BallPrefab;
    public int ShootingForce;

    void Awake()
    {
        // Find the action once, and store the reference.
        m_ShootAction = InputSystem.actions.FindAction("Shoot");
    }

    void OnEnable()
    {
        // Subscribe when the script is enabled.
        m_ShootAction.performed += OnShootPerformed;
    }

    void OnDisable()
    {
        // Unsubscribe when the script is disabled, so the action doesn't call a disabled script.
        m_ShootAction.performed -= OnShootPerformed;
    }

    // The Input System calls this method each time the Jump action is performed.
    void OnShootPerformed(InputAction.CallbackContext context)
    {
        GameObject ball = Instantiate(m_BallPrefab, transform.position, transform.rotation);

        if (ball.TryGetComponent<Rigidbody>(out var rb))
            rb.AddForce(transform.forward * -ShootingForce, ForceMode.Impulse);
    }
}
