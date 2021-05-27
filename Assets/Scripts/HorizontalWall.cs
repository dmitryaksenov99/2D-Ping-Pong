using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalWall : MonoBehaviour
{
    Rocket[] rockets;
    Ball ball;
    Score score;
    void FixedUpdate()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().rect.size;
    }
    private void OnEnable()
    {
        rockets = FindObjectsOfType<Rocket>();
        ball = FindObjectOfType<Ball>();
        score = FindObjectOfType<Score>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(Death());
    }
    void SetEnabledForRockets(bool value)
    {
        foreach (Rocket rocket in rockets)
        {
            rocket.enabled = value;
        }
    }
    void ResetRockets()
    {
        foreach (Rocket rocket in rockets)
        {
            rocket.ResetValues();
        }
    }
    IEnumerator Death()
    {
        score.SetMaxScore();
        score.DrawMaxScore();

        SetEnabledForRockets(false);
        yield return new WaitForSeconds(2f);

        ResetRockets();
        score.ResetScore();
        score.DrawScore();

        ball.ChangeBallConfig();
        SetEnabledForRockets(true);
    }
}
