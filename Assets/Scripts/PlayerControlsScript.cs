using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControlsScript : MonoBehaviour
{
    private float MoveSpeed = 6f;
    private float BowCharge = 0f;
    private float MaxBowCharge = 1.5f;
    private bool BowCharged = false;
    private bool HasArrow = true;
    public bool CanPlay = true;
    private Rigidbody2D RB;
    public BowScript Bow;

    [SerializeField] private Slider Slider;
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private GameObject MainSprite;

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
        if (CanPlay == true)
        {
            RB.linearVelocity = MoveDirection * MoveSpeed;

            if (Input.GetMouseButton(1) && HasArrow == true)
            {
                if (BowCharged == false)
                {
                    BowCharge = BowCharge + Time.deltaTime;
                    Slider.value = BowCharge;
                }
                if (BowCharge >= MaxBowCharge)
                {
                    BowCharged = true;
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                BowCharge = 0f;
                Slider.value = BowCharge;
                BowCharged = false;
            }

            if (Input.GetMouseButton(0) && BowCharged == true)
            {
                Bow.Fire();
                BowCharge = 0f;
                HasArrow = false;
                BowCharged = false;
                Slider.value = BowCharge;
                CanvasGroup.alpha = 0f;
            }
        }
            MouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MainSprite.transform.position = RB.transform.position;

    }

    public void OnMove(InputValue Value)
    {
            MoveDirection = Value.Get<Vector2>().normalized;
    }
    private void FixedUpdate()
    {
        if (CanPlay == true)
        {
            Vector2 Aim = MouseDirection - RB.position;
            float Angle = Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg - 90f;
            RB.rotation = Angle;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            HasArrow = true;
            CanvasGroup.alpha = 1f;
            Destroy(collision.gameObject);
        }
    }
}

