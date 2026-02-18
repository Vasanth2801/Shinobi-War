using UnityEngine;

public class Bullet : MonoBehaviour
{

    PlayerHealth health;

    void Start()
    {
        health = FindObjectOfType<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            health.TakeDamage(10);
            Destroy(this.gameObject);
            Debug.Log("Attacking the Player");
        }
        Destroy(this.gameObject);
    }
}
