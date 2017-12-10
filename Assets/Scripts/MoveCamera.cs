using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Camera Camera;
    public float Speed;
    public float ZoomScale;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var p = Camera.transform.position;
            Camera.transform.position = new Vector3(p.x, p.y + Speed, p.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            var p = Camera.transform.position;
            Camera.transform.position = new Vector3(p.x - Speed, p.y, p.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            var p = Camera.transform.position;
            Camera.transform.position = new Vector3(p.x, p.y - Speed, p.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            var p = Camera.transform.position;
            Camera.transform.position = new Vector3(p.x + Speed, p.y, p.z);
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Camera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * ZoomScale;
        }
    }
}
