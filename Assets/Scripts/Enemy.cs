using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public static int aliveEnemies = 0;

    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;

    public bool isStunned = false;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void OnEnable()
    {
        aliveEnemies++;
    }

    void OnDestroy()
    {
        aliveEnemies--;
    }

    void Update()
    {
        if (isStunned) return;

        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        rb.AddForce(dir * speed);
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        rb.isKinematic = true;

        yield return new WaitForSeconds(duration);

        isStunned = false;
        rb.isKinematic = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    // fallback: destroy if falls off map
    void LateUpdate()
    {
        if (transform.position.y < -3)
        {
            Die();
        }
    }
}