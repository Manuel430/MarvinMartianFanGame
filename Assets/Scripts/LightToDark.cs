using UnityEngine;

public class LightToDark : MonoBehaviour
{
    [Header("Alternate Worlds")]
    [SerializeField] GameObject DarkWorld;
    [SerializeField] GameObject LightWorld;
    [SerializeField] bool inDarkWorld;

    private void Awake()
    {
        DarkWorld.SetActive(true);
        LightWorld.SetActive(false);

        inDarkWorld = true;
    }

    public void SwitchWorld()
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
