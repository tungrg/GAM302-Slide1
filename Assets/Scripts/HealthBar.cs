using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthFill;
    private Camera mainCamera;
    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
    
        if (healthFill != null)
        {
            healthFill.fillAmount = healthPercentage;
        }
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
}
