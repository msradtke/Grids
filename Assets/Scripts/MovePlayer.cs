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
        Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Camera.transform.position.z);
    }

    // Update is called once per frame
    float _timeSinceUpdated = 0;
    float _timestamp;
    Vector3 desiredLocation;
    void Update()
    {
        KeyMovement();
        Player.transform.position = Vector3.Lerp(Player.transform.position, desiredLocation, Speed * Time.deltaTime);

        Camera.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,Camera.transform.position.z);
    }
    void KeyMovement()
    {
        _timeSinceUpdated += Time.deltaTime;

        if (Time.time >= _timestamp)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
            {
                var p = desiredLocation;
                var newLoc = new Vector3(p.x, p.y + Distance, p.z);
                if (!CollidesWithWall(newLoc))
                    desiredLocation.y += Distance;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                var p = desiredLocation;
                var newLoc = new Vector3(p.x - Distance, p.y, p.z);
                if (!CollidesWithWall(newLoc))
                    desiredLocation.x -= Distance;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                var p = desiredLocation;
                var newLoc = new Vector3(p.x, p.y - Distance, p.z);
                if (!CollidesWithWall(newLoc))
                    desiredLocation.y -= Distance;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                var p = desiredLocation;
                var newLoc = new Vector3(p.x + Distance, p.y, p.z);
                if (!CollidesWithWall(newLoc))
                    desiredLocation.x += Distance;
            }
            _timestamp = Time.time + timeBetweenMoves;
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
    }

    bool CollidesWithWall(Vector3 newLocation)
    {
        return false;

        foreach(var wall in CreateStructures._walls)
        {
            if (Helpers.Collides( wall, newLocation, SpriteSize, SpriteSize))
                return true;
        }

        return false;
    }
}
