using UnityEngine;

public class Deletion : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
