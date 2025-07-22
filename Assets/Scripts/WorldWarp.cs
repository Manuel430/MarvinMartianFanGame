using UnityEngine;

public class WorldWarp : MonoBehaviour
{
    [Header("Alternate Worlds")]
    [SerializeField] GameObject DarkWorld;
    [SerializeField] GameObject LightWorld;
    bool inDarkWorld;

    private void Awake()
    {
        inDarkWorld = true;
        DarkWorld.SetActive(true);
        LightWorld.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchWorld();
        }
    }

    private void SwitchWorld()
    {
        if (inDarkWorld)
        {
            DarkWorld.SetActive(false);
            LightWorld.SetActive(true);
        }
        else
        {
            DarkWorld.SetActive(true);
            LightWorld.SetActive(false);
        }

        inDarkWorld = !inDarkWorld;
    }
}
