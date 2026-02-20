using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsScript : MonoBehaviour
{
    private float MoveSpeed = 8f;
    private float BowCharge = 0f;
    private bool HasArrow = true;
    private Rigidbody2D RB;
    public BowScript Bow;

    private Vector2 MoveDirection;
    private Vector2 MouseDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.linearVelocity = MoveDirection * MoveSpeed;
        MouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButton(1) && HasArrow == true)
        {
            BowCharge = BowCharge + Time.deltaTime;
            print(BowCharge);
        }
        if (Input.GetMouseButton(0) && BowCharge > 1.5f)
        {
            Bow.Fire();
            BowCharge = 0f;
            HasArrow = false;
        }
    }

    public void OnMove(InputValue Value)
    {
        MoveDirection = Value.Get<Vector2>().normalized;
    }
    private void FixedUpdate()
    {
        Vector2 Aim = MouseDirection - RB.position;
        float Angle = Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg - 90f;
        RB.rotation = Angle;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            HasArrow = true;
            Destroy(collision.gameObject);
        }
    }
}

