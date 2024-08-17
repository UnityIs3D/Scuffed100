using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthDamage : MonoBehaviour
{
    

    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI heartNumberText;
    public RawImage happyFace;
    public Texture originalTexture;
    public Texture sadFace;

    

    public void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damageAmount)
    {
        //Debug.Log($"Health was: {currentHealth}");
        currentHealth -= damageAmount;
        //Debug.Log($"Health is: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthText();
    }

    public void Die()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Heart"))
        {
            if (currentHealth < maxHealth)
            {
                
                Destroy(other.gameObject);
                UpdateHealthText();
            }
        }
    }

    public void UpdateHealthText()
    {
        heartNumberText.text = currentHealth.ToString();
    }

   

    public IEnumerator SadHeartDuration()
    {
        happyFace.texture = sadFace;
        yield return new WaitForSeconds(2f);
        happyFace.texture = originalTexture;
    }
}
