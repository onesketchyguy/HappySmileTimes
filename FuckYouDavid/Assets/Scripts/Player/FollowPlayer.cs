using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player => GameObject.Find("Player");

    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + new Vector3(0, 0, -10);
        }
    }
}
