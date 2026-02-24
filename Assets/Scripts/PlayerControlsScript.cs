using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControlsScript : MonoBehaviour
{
    private float MoveSpeed = 6f;
    private float BowCharge = 0f;
    private float MaxBowCharge = 1.5f;
    private bool BowCharged = false;
    private bool HasArrow = true;
    private Rigidbody2D RB;
    public BowScript Bow;

    [SerializeField] private Slider Slider;

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
            if(BowCharged == false)
            {
                BowCharge = BowCharge + Time.deltaTime;
                Slider.value = BowCharge;
            }
            if (BowCharge >= MaxBowCharge)
            {
                BowCharged = true;
            }
        }
        if (Input.GetMouseButtonUp(1) && BowCharged == false)
        {
            BowCharge = 0f;
            Slider.value = BowCharge;
        }

        if (Input.GetMouseButton(0) && BowCharged == true)
        {
            Bow.Fire();
            BowCharge = 0f;
            HasArrow = false;
            BowCharged = false;
            Slider.value = BowCharge;
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
        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}

