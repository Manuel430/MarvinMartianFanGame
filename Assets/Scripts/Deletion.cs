using UnityEngine;

public class Deletion : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }

    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
