using UnityEngine;

public class MainManager : MonoBehaviour
{
    public delegate void Option_a();

    public static Option_a option_A;

    public delegate void Option_b();

    public static Option_b option_B;

    public void Yes()
    {
        option_A();
    }

    public void No()
    {
        option_B();
    }
}