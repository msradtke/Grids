using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            var script = collision.gameObject.GetComponent<MovePlayer>();
            script.Collided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var script = collision.gameObject.GetComponent<MovePlayer>();
            script.Collided = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
