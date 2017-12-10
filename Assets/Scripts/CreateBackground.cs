using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CreateBackground : MonoBehaviour
{
    public Sprite Background;
    public Camera MainCamera;
    public int Z;
    // Use this for initialization
    void Start()
    {
        var test = new GameObject();
        var render = test.AddComponent<SpriteRenderer>();
        render.sprite = Background;
        var size = new Vector2(Background.bounds.size.x / transform.localScale.x, Background.bounds.size.y / transform.localScale.y);

        test.transform.position = transform.position;


        GameObject child;
        for (int ix = 1; ix < 100; ++ix)
        {
            for (int iy = 1; iy < 100; ++iy)
            {


                child = Instantiate(test);
                child.transform.position = new Vector3(size.x * ix, size.y * iy,Z);
                child.transform.parent = transform;
                if (iy == 1)
                {
                    var z = MainCamera.transform.position.z;
                    //MainCamera.transform.position = new Vector3(size.x * ix, size.y * iy,z);
                }
            }
        }


        render.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
