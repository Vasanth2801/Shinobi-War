using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float bulletForce = 8f;
    [SerializeField] private float lineOfSite = 8f;
    [SerializeField] private float shootingRange = 10f;

    [Header("References")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject firePoint;
    [SerializeField] Transform player;

    [Header("Shooting Settings")]
    [SerializeField] float fireRate = 1f;
    [SerializeField] float nextFireRate;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position,transform.position);
        if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position,player.position,speed * Time.deltaTime);
        }
        else if(distanceFromPlayer <= shootingRange && nextFireRate < Time.time)
        {
            Instantiate(bulletPrefab,firePoint.transform.position,Quaternion.identity);
            nextFireRate = Time.time + fireRate;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}