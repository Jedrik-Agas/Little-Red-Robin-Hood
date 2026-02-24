using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float BTime = 0f;
    public GameObject DropArrow;

    void Update()
    {
        if (BTime > 1.5f)
        {
            Destroy(gameObject);
            BTime = 0f;
            Instantiate(DropArrow, transform.position, transform.rotation);
        }
        else
        {
            BTime = BTime + Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(DropArrow, transform.position, transform.rotation);
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
