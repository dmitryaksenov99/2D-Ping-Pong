using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] Image background;
    public Player player;
    private bool up;
    RectTransform rocketRect;
    float xPos;
    private void OnEnable()
    {
        rocketRect = GetComponent<RectTransform>();
        ResetValues();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        bool isBall = col.gameObject.TryGetComponent(out Ball ball);

        if (isBall)
        {
            ball.ReversVerticalMove();
        }
    }

    private void FixedUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            up = touch.position.y >= Screen.height / 2;

            if (up && player == Player.Player1 || !up && player == Player.Player2)
            {
                xPos = touch.position.x;
            }

            rocketRect.position = new Vector2(xPos, rocketRect.position.y);
        }
    }

    public void ResetValues()
    {
        xPos = Screen.width / 2;
        up = false;
        rocketRect.position = new Vector2(xPos, rocketRect.position.y);
    }
}

public enum Player
{
    Player1,
    Player2
}