using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Platform : MonoBehaviour
{
    [SerializeField]
    private float _speed = 12.0f;
    
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Vector3 initialPossition;

    void Awake()
    {
        initialPossition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        _rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * _speed, 0.0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("collision platform and " + col.gameObject.tag);

        if (GameManager.Instance.IsGameStarted && col.gameObject.tag == "Ball")
        {
            Ball ball = col.gameObject.GetComponent<Ball>();

            float x = FactorHorizontal(col.transform.position, 
                                transform.position,
                                _collider.bounds.size.x);

            float y = VerticalFactor(col.transform.position,
                                transform.position,
                                _collider.bounds.size.y);


            // Calculate direction, set length to 1
            ball.SetDirection(new Vector2(x, y).normalized);
        }
    }

    public void ResetState()
    {
        transform.position = initialPossition;
    }

    // Определяем на какой угол от платформы отбить мячик.
    // Чем левее от середины, тем на больший угол влево отклонится мячик
    // Чем правее от середины, тем на больший угол впрево отклоится мячик
    private float FactorHorizontal(Vector2 ballPos, Vector2 platformPos, float platformWidth)
    {
        //
        //- 1  -0.5  0  0.5   1  <- x value
        // ===================  <- platform
        //
        return (ballPos.x - platformPos.x) / platformWidth;
    }

    // определяем в какую чать ракетки по вертикали попал мячик, 
    // если он попал ниже серидины, то отбиваем его вниз, если выше - вверх
    // если всегда отбивать его вверх, то если вдруг успеем наехать 
    // низом платформы на мячик, он снова полетит вверх 
    private float VerticalFactor(Vector2 ballPos, Vector2 platformPos, float platformHeigth)
    {
        // platform
        // | 1  
        // | 
        // | 0
        // |
        // |-1
        var platformY = (ballPos.y - platformPos.y) / platformHeigth;
        return (platformY > 0) ? 1 : -1;
    }
}
