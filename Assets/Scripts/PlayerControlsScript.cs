using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsScript : MonoBehaviour
{
    private float MoveSpeed = 10f;
    private const float BaseFireRate = 0.1f;
    private float FireRate = BaseFireRate;
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

        if (Input.GetMouseButton(0) && FireRate > 1f)
        {
            Bow.Fire();
            FireRate = BaseFireRate;
        }
        else
        {
            FireRate = FireRate + Time.deltaTime;
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
}
