using UnityEngine;

public class TestPhysics : MonoBehaviour
{
    public Rigidbody2D ballRB;

    void Start()
    {
        ballRB.AddForce(new Vector2(1, 1)*100f);
    }
}
