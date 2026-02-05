using UnityEngine;

public class BallPhysics : MonoBehaviour
{

    void Start()
    {
        Rigidbody2D ballRB = GetComponent<Rigidbody2D>();
        ballRB.AddForce(new Vector2(1, 1)*100f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<BlockState>(out BlockState state))
        {
            state.Damage();
        }
    }
}
