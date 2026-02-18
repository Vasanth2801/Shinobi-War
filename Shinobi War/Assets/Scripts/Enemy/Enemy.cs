using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float bulletForce = 8f;
    [SerializeField] private float lineOfSite = 8f;
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private int facingDirection = 1;

    [Header("References")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject firePoint;
    [SerializeField] Transform player;
    [SerializeField] Animator animator;

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
            animator.SetBool("isRunning", true);
            if(player.position.x > transform.position.x && facingDirection == -1 || player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }
        }
        else if(distanceFromPlayer <= shootingRange && nextFireRate < Time.time)
        {
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Attack");
            GameObject bullet = Instantiate(bulletPrefab,firePoint.transform.position,Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(-firePoint.transform.right * bulletForce, ForceMode2D.Impulse);
            nextFireRate = Time.time + fireRate;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}