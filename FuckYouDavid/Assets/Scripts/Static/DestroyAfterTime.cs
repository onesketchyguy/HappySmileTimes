using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeBeforeDestroy = 1;

    void Start()
    {
        if (timeBeforeDestroy > 0)
        {
            Invoke("DestroySelf", timeBeforeDestroy);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
