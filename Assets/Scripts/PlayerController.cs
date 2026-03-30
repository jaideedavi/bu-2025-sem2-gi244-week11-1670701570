using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    public float speed = 5f;

    public Transform focalPoint;
    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");
    }

    void Update()
    {
        var move = moveAction.ReadValue<Vector2>();
        rb.AddForce(move.y * speed * focalPoint.forward);

        if (breakAction.IsPressed())
        {
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = collision.transform.position - transform.position;

            // push enemy
            enemyRb.AddForce(100 * dir.normalized, ForceMode.Impulse);

            // 🔥 kill enemy
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }
        }
    }
}