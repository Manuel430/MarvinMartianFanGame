using UnityEngine;

public class WorldWarp : MonoBehaviour
{
    [Header("Outputs")]
    [SerializeField] LightToDark changeWorld;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        changeWorld.SwitchWorld();
    }
}
