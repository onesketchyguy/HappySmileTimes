using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public void ToggleActive()
    {
        bool Active = !gameObject.activeSelf;

        gameObject.SetActive(Active);
    }
}