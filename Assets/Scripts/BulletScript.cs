using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float BTime = 0f;
    void Update()
    {
        if (BTime > 3f)
        {
            Destroy(gameObject);
            BTime = 0f;
        }
        else
        {
            BTime = BTime + Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
