using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float ammoSpeed;
    Rigidbody2D rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.linearVelocity = transform.right * ammoSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            Destroy(gameObject); 
            return;
        }
    }
}
