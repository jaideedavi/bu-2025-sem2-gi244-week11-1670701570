using UnityEngine;

public class PowerUpStunt : MonoBehaviour
{
   public float stunDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

            foreach (Enemy enemy in enemies)
            {
                enemy.Stun(stunDuration);
            }

            Destroy(gameObject);
        }
    }

}
