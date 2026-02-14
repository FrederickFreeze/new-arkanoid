using UnityEngine;
using static GameManager;

public class BallPhysics : MonoBehaviour
{
    private Rigidbody2D ballRB;
    void Start()
    {
        ballRB = GetComponent<Rigidbody2D>();
        OnGameStateChanged += StateChange;
    }

    private void OnDestroy()
    {
        OnGameStateChanged -= StateChange;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<BlockState>(out BlockState state))
        {
            state.Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LoseTrigger"))
        {
            instance.SetState(GameState.Lose);
        }
    }

    private void StateChange(GameState state)
    {
        switch (state)
        {
            case GameState.AwaitingStart:
                GetComponent<FixedJoint2D>().enabled = true;
                break;
            case GameState.Lose:
                ballRB.gravityScale = 1;
                ballRB.linearVelocity = Vector2.zero;
                break;
            case GameState.Playing:
                GetComponent<FixedJoint2D>().enabled = false;
                ballRB.gravityScale = 0;
                ballRB.AddForce(Vector2.up * 100f);
                break;
        }
    }
}
