using UnityEngine;

public class Mover : MonoBehaviour
{
    void Start() // called before the first frame update
    {
        float speed = 20;
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision) // called when this collider/rigidbody has begun touching another rigidbody/collider
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);   // destroys enemy
            Destroy(gameObject);             // destroys bullet
        }
        else
        {
            Destroy(gameObject);             // destroys bullet on wall/floor/etc.
        }
    }
}
