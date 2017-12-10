using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CreateWalls : MonoBehaviour
{

    public Sprite HorizontalWall;
    public float SpriteSize;
    public int Z;
    
    // Use this for initialization
    void Start()
    {
        float halfSpriteSize = SpriteSize / 2;
        GameObject wall = new GameObject();

        var renderer = wall.AddComponent<SpriteRenderer>();
        renderer.sprite = HorizontalWall;


        GameObject child;
        var ran = new System.Random();
        for (int i = 0; i < 1000; ++i)
        {
            
            var posx = ran.Next(0, 100);
            var posy = ran.Next(0, 100);

            child = Instantiate(wall);
            var x = (posx * SpriteSize) / transform.localScale.x;
            var y = (posy * SpriteSize) / transform.localScale.y;
            child.transform.position = new Vector3(x, y, Z);
            child.transform.parent = transform;
        }


        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
