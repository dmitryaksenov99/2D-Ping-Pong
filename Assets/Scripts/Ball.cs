using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] Image ballBg;
    [SerializeField] float randomProcent = 15;
    [SerializeField] float minRadiusValue = 5;
    [SerializeField] float maxRadiusValue = 50;
    [SerializeField] float speed = 10f;

    RectTransform ballRect; 
    CircleCollider2D ballColl;
    Score score;
    float xSpeed, ySpeed;
    float _xSpeedRnd, _ySpeedRnd;

    void OnEnable()
    {
        ballRect = GetComponent<RectTransform>();
        ballColl = GetComponent<CircleCollider2D>();
        score = FindObjectOfType<Score>();

        ChangeBallConfig();
    }

    public void ChangeBallConfig()
    {
        ballBg.color = score.GetBallColor();
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        ballRect.position = new Vector3(centerX, centerY, 0);
        float rndRadius = Random.Range(minRadiusValue, maxRadiusValue);
        ballRect.sizeDelta = new Vector2(rndRadius, rndRadius);
        ballColl.radius = rndRadius / 2;

        float randomX = Random.Range(float.MinValue, float.MaxValue);
        float randomY = Random.Range(float.MinValue, float.MaxValue);

        xSpeed = randomX / Mathf.Abs(randomX) * speed;
        ySpeed = randomY / Mathf.Abs(randomY) * speed;
    }
    void FixedUpdate()
    {
        _xSpeedRnd = Random.Range(xSpeed, xSpeed + xSpeed * randomProcent / 100f) - (xSpeed * randomProcent / 100f) / 2;
        _ySpeedRnd = Random.Range(ySpeed, ySpeed + ySpeed * randomProcent / 100f) - (ySpeed * randomProcent / 100f) / 2;

        transform.Translate(new Vector3(_xSpeedRnd, _ySpeedRnd, 0));
    }

    public void ReversVerticalMove()
    {
        ySpeed *= -1;
    }
    public void ReversHorizontalMove()
    {
        xSpeed *= -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isRocket = collision.gameObject.TryGetComponent(out Rocket rocket);

        if (isRocket)
        {
            score.UpdateScore(rocket.player);
        }
    }
}