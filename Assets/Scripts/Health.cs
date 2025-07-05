using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Amount")]
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    [Header("Player or Enemy")]
    [SerializeField] bool isPlayer;

    [Header("Visualizer")]
    [SerializeField] GameObject visual;
    [SerializeField] Color defaultColor;
    [SerializeField] Color damageColor;
    [SerializeField] float lerpSpeed = 10f;
    [SerializeField] string colorAttributeName = "_Color";
    Color currentColor;

    List<Material> materials = new List<Material>();

    [Header("Death")]
    [SerializeField] GameObject enemyExplosion;
    [SerializeField] Transform enemyCenter;

    #region Public
    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    #endregion

    private void Awake()
    {
        health = maxHealth;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = new Material(renderer.material);
            materials.Add(renderer.material);
        }
        currentColor = defaultColor;
    }

    private void Update()
    {
        currentColor = Color.Lerp(currentColor, defaultColor, Time.deltaTime * lerpSpeed);
        SetMaterialColor(currentColor);
    }

    public void TakeDamage(int damage)
    {
        if(Mathf.Abs((currentColor - defaultColor).grayscale) < 0.1)
        {
            currentColor = damageColor;
            SetMaterialColor(currentColor);
        }

        health -= damage;
        
        if(isPlayer)
        {
            PlayerUI playerUI = GetComponent<PlayerUI>();
            if(playerUI != null)
            {
                playerUI.UpdateHealth();
            }
        }

        if(health <= 0)
        {
            health = 0;
            if(isPlayer)
            {
                PlayerMovement playerMovement = GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    PlayerUI playerUI = GetComponent<PlayerUI>();
                    if (playerUI != null)
                    {
                        playerUI.CannotPause();
                    }
                    playerMovement.GameOver();
                }
                return;
            }
            {
                Death();
                return;
            }
        }

    }

    void SetMaterialColor(Color color)
    {
        foreach(Material material in materials)
        {
            material.SetColor(colorAttributeName, color);
        }
    }

    void Death()
    {
        EnemyPatrol stopEnemy = GetComponent<EnemyPatrol>();
        if(stopEnemy != null)
        {
            stopEnemy.Stop();
        }
        if(enemyCenter != null)
        {
            Instantiate(enemyExplosion, enemyCenter.position, enemyCenter.rotation);
        }

        Destroy(gameObject);
    }
}
