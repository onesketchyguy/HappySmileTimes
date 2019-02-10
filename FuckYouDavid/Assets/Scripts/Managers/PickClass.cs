using UnityEngine;
using UnityEngine.UI;
public class PickClass : MonoBehaviour
{
    Animator anim => GetComponent<Animator>() ?? null;

    public Text helpText;

    int channel = 0;
    int lastChannel = 0;

    bool chanelChanged = false;

    float timeOnScreen;

    private void Start()
    {
        timeOnScreen = Time.time;
    }

    void Update()
    {
        anim.SetInteger("Channel" , channel);

        if (Input.GetAxis("Vertical") > 0.9f && channel == lastChannel)
        {
            if (channel < 2)
                channel += 1;
            else channel = 0;

            chanelChanged = true;

            timeOnScreen = Time.time;
        }
        else
        if (Input.GetAxis("Vertical") < -0.9f && channel == lastChannel)
        {
            if (channel > 0)
                channel -= 1;
            else channel = 2;

            chanelChanged = true;

            timeOnScreen = Time.time;
        }
        else
        {
            lastChannel = channel;
        }

        if (chanelChanged == false)
        {
            if (Time.time > timeOnScreen + 3)
            {
                helpText.text = "Press up or down to change the channel!";
            }
            else
            {
                helpText.text = "";
            }
        }
        else
        {
            if (Time.time > timeOnScreen + 5)
            {
                helpText.text = "Press enter to pick this channel!";
            }
            else
            {
                helpText.text = "";
            }
        }

        if (Input.GetButtonDown("Submit"))
        {
            switch (channel)
            {
                case 1:
                    GameManager.playerClass = CombatUniversals.CLASSTYPE.Runner;
                    break;
                case 2:
                    GameManager.playerClass = CombatUniversals.CLASSTYPE.Tank;
                    break;
                case 0:
                    GameManager.playerClass = CombatUniversals.CLASSTYPE.Puncher;
                    break;
                default:
                    break;
            }

            GameManager.instance.LoadScene("World_1");
        }
    }
}
