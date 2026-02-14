using UnityEngine;

public class BlockState : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer blockSR;
    [SerializeField] private int state = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockSR.sprite = sprites[state];
    }

    public void Damage()
    {
        state--;
        if(state < 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            blockSR.sprite = sprites[state];
        }
    }
}
