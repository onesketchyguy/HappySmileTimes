using UnityEngine;

public class MainManager : MonoBehaviour
{
    public delegate void Option_a();

    public static Option_a option_A;

    public delegate void Option_b();

    public static Option_b option_B;

    public static GameObject OptionsBOX,ButtonA,ButtonB;

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
        Debug.Log("On pressed");

        ButtonA.SetActive(true);
        ButtonB.SetActive(true);

    }

    public void OFF()
    {
        Debug.Log("Off pressed");
        ButtonA.SetActive(false);
        ButtonB.SetActive(true);
    }


}