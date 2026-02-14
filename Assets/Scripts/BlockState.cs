using System;
using UnityEngine;

public class BlockState : MonoBehaviour
{
    public static event Action<bool> OnBlockDestroyed;
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
            OnBlockDestroyed?.Invoke(true);
        }
        else
        {
            blockSR.sprite = sprites[state];
        }
    }
}
