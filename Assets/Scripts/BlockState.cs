using System;
using UnityEngine;

public class BlockState : MonoBehaviour
{
    public static event Action<int> OnBlockDestroyed;
    private static int blockCount;
    public Sprite[] sprites;
    private SpriteRenderer blockSR;
    [SerializeField] private int state = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockSR = GetComponent<SpriteRenderer>();
        blockSR.sprite = sprites[state];
    }

    private void OnEnable()
    {
        blockCount++;
    }

    private void OnDisable()
    {
        blockCount--;
    }
    public void Damage()
    {
        state--;
        if(state < 0)
        {
            
            gameObject.SetActive(false);
            OnBlockDestroyed?.Invoke(blockCount);
        }
        else
        {
            blockSR.sprite = sprites[state];
        }
    }
}
