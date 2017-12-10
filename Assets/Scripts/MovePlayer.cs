using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using HelperMethods;
public class MovePlayer : MonoBehaviour
{
    public CreateStructure CreateStructures;
    public GameObject Player;
    public Camera Camera;
    public float Distance;
    public float Speed = 100.0f; 
    public float timeBetweenMoves = .3333f;
    public float SpriteSize;
    void Start()
    {
        Camera.transform.position = Player.transform.position;
    }

    // Update is called once per frame
    float _timeSinceUpdated = 0;
    float _timestamp;
    Vector3 desiredLocation;
    void Update()
    {
        KeyMovement();
        Player.transform.position = Vector3.Lerp(Player.transform.position, desiredLocation, Speed * Time.deltaTime);
    }
    void KeyMovement()
    {
        _timeSinceUpdated += Time.deltaTime;

        if (Time.time >= _timestamp)
        {
            if (Input.GetKey(KeyCode.W))
            {
                var p = Player.transform.position;
                var newLoc = new Vector3(p.x, p.y + Distance, p.z);
                if (!CollidesWithWall(newLoc))
                    desiredLocation.y += Distance;
                _timestamp = Time.time + timeBetweenMoves;
            }

            if (Input.GetKey(KeyCode.A))
            {
                var p = Player.transform.position;
                Player.transform.position = new Vector3(p.x - Distance, p.y, p.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                var p = Player.transform.position;
                Player.transform.position = new Vector3(p.x, p.y - Distance, p.z);
            }
            if (Input.GetKey(KeyCode.D))
            {
                var p = Player.transform.position;
                var newLoc = new Vector3(p.x + Distance, p.y, p.z);
                if (!CollidesWithWall(newLoc))
                    desiredLocation.x += Distance;
                _timestamp = Time.time + timeBetweenMoves;
            }
        }
        if (false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                var p = Player.transform.position;
                var newLoc = new Vector3(p.x, p.y + Distance, p.z);
                if (!CollidesWithWall(newLoc))
                    Player.transform.position = newLoc;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                var p = Player.transform.position;
                Player.transform.position = new Vector3(p.x - Distance, p.y, p.z);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                var p = Player.transform.position;
                Player.transform.position = new Vector3(p.x, p.y - Distance, p.z);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                var p = Player.transform.position;
                Player.transform.position = new Vector3(p.x + Distance, p.y, p.z);
            }

        }
        Camera.transform.position = Player.transform.position;
    }

    bool CollidesWithWall(Vector3 newLocation)
    {
        foreach(var wall in CreateStructures._walls)
        {
            if (Helpers.Collides( wall, newLocation, SpriteSize, SpriteSize))
                return true;
        }

        return false;
    }
}
