using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] Image healthBar;
    [SerializeField] private GameObject gameOverPanel;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }

    public void TakeDamage(int  damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(this.gameObject);
            gameOverPanel.SetActive(true);
        }
    }
}