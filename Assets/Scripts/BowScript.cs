using UnityEngine;

public class BowScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePoint;
    public float BulletForce = 20f;

    public void Fire()
    {
        GameObject Bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.up * BulletForce, ForceMode2D.Impulse);
    }
}
