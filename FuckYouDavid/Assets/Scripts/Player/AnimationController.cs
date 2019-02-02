using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Sprite[] horizontalSprites;
    [SerializeField] private Sprite[] verticalSprites_Up;
    [SerializeField] private Sprite[] verticalSprites_Down;

    Movement mov => GetComponent<Movement>();

    SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>() ?? gameObject.AddComponent<SpriteRenderer>();

    float timeSpentOnSprite;

    int index;

    [SerializeField] float timeToSpendOnSprites = 0.5f;

    private void Update()
    {
        HandleSprites();   
    }

    private void HandleSprites()
    {
        Sprite spriteToUse = spriteRenderer.sprite;

        if (mov.input == Vector2.zero)
        {
            switch (mov.myFaceDirection)
            {
                case Movement.faceDirection.Down:
                    spriteToUse = verticalSprites_Down[0];
                    break;
                case Movement.faceDirection.Left:
                    spriteRenderer.flipX = true;

                    spriteToUse = horizontalSprites[0];
                    break;
                case Movement.faceDirection.Up:
                    spriteToUse = verticalSprites_Up[0];
                    break;
                case Movement.faceDirection.Right:
                    spriteRenderer.flipX = false;

                    spriteToUse = horizontalSprites[0];
                    break;
                default:
                    break;
            }

            spriteRenderer.sprite = spriteToUse;

            timeSpentOnSprite = Time.time;

            return;
        }

        if ((timeSpentOnSprite < Time.time))
        {
            if (mov.input.x != 0)
            {
                spriteRenderer.flipX = mov.myFaceDirection == Movement.faceDirection.Left;

                if (index < horizontalSprites.Length - 1)
                    index++;
                else index = 0;

                spriteToUse = horizontalSprites[index];

                timeSpentOnSprite = Time.time + timeToSpendOnSprites;
            }

            if (mov.input.y != 0)
            {
                bool movingUp = mov.myFaceDirection == Movement.faceDirection.Up;

                if (index < verticalSprites_Down.Length - 1 && index < verticalSprites_Up.Length - 1)
                    index++;
                else index = 0;


                spriteToUse = movingUp ? verticalSprites_Up[index] : verticalSprites_Down[index];

                timeSpentOnSprite = Time.time + timeToSpendOnSprites;
            }

            spriteRenderer.sprite = spriteToUse;
        }
    }
}