using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : Singleton<BackGroundController>
{
    public Sprite[] background;

    public SpriteRenderer control;

    public override void Awake()
    {
        MakeSingleton(false);
    }


    public override void Start()
    {
        changeSprite();
    }

    public void changeSprite()
    {
        if(control != null && background != null && background.Length > 0)
        {
            int random = Random.Range(0, background.Length);
            if (background[random] != null)
            {
                control.sprite = background[random];
            }
        }
    }
}
