using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWall : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().rect.size;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        bool isBall = col.gameObject.TryGetComponent(out Ball ball);

        if (isBall)
        {
            ball.ReversHorizontalMove();
        }   
    }
}
