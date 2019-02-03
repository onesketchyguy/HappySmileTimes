using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public delegate void Option_a();

    public static Option_a option_A;

    public delegate void Option_b();

    public static Option_b option_B;

    public GameObject ButtonA, ButtonB;

    public void Yes()
    {
        option_A();
    }

    public void No()
    {
        option_B();
    }

    public void ON()
    {
        Debug.Log("Turning on buttons...");

        ButtonA.SetActive(true);
        ButtonB.SetActive(true);

    }

    public void OFF()
    {
        Debug.Log("Turning off buttons...");

        ButtonA.SetActive(false);
        ButtonB.SetActive(false);
    }
}