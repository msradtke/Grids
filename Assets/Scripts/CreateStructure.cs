using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;
using System;
using HelperMethods;
public class CreateStructure : MonoBehaviour
{

    // Use this for initialization
    public int MinArea;
    public int MaxArea;
    public int MinWidth;
    public int MaxWidth;
    public int WorldWidth;
    public List<RectangleBound> _structures;
    public List<Vector3> _walls;
    public Sprite HorizontalWall;
    public Sprite VerticalWall;
    public Sprite BLCornerWall;
    public Sprite BRCornerWall;
    public Sprite TLCornerWall;
    public Sprite TRCornerWall;
    public float SpriteSize;
    public int Z;

    GameObject hWall;
    GameObject vWall;
    GameObject corner;

    SpriteRenderer wRenderer;
    SpriteRenderer vRenderer;

    SpriteRenderer cornerRenderer;
    void Start()
    {
        _structures = new List<RectangleBound>();
        hWall = new GameObject();
        vWall = new GameObject();
        corner = new GameObject();

        wRenderer = hWall.AddComponent<SpriteRenderer>();
        wRenderer.sprite = HorizontalWall;

        vRenderer = vWall.AddComponent<SpriteRenderer>();
        vRenderer.sprite = VerticalWall;

        cornerRenderer = corner.AddComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateAStructure()
    {
        RectangleBound rect = new RectangleBound();
        SetSizeAndLocation(rect);

    }

    public void SetSizeAndLocation(RectangleBound rect)
    {
        var ran = new System.Random();
        var thisArea = ran.Next(MinArea, MaxArea + 1);
        var thisMaxWidth = thisArea / MinWidth;

        var width = ran.Next(MinWidth, thisMaxWidth + 1);

        var height = thisArea / width;

        rect.Width = width;
        rect.Height = height;

        var valid = FindValidLocation(rect);
        if (valid)
        {
            BuildWalls(rect);
            _structures.Add(rect);

        }
    }

    public bool FindValidLocation(RectangleBound rect)
    {
        var validLocations = new List<RectangleBound>();

        var sortedBottomLeft = _structures.OrderBy(x => x.X);

        var ran = new System.Random();

        var startLocation = new Location();
        startLocation.X = ran.Next(0, WorldWidth);
        startLocation.Y = ran.Next(0, WorldWidth);

        var blocked = IsStructureBlocked(rect, startLocation);
        if (blocked)
        {
            //all up to Right
            for (int x = startLocation.X; x < WorldWidth - rect.Width; ++x)
            {
                for (int y = startLocation.Y; y < WorldWidth - rect.Height; ++y)
                {

                    startLocation.X = x;
                    startLocation.Y = y;
                    blocked = IsStructureBlocked(rect, startLocation);
                    if (!blocked)
                        break;
                }
                if (!blocked)
                    break;
            }
        }
        if (!blocked)
        {
            rect.X = startLocation.X;
            rect.Y = startLocation.Y;
            return true;
        }
        return false;


    }

    public bool IsStructureBlocked(RectangleBound rect, Location loc)
    {
        var isBlocked = false;
        foreach(var struc in _structures)
        {
            var strucL = struc.X;
            var strucR = struc.X + struc.Width;

            var rectL = loc.X;
            var rectR = loc.X + rect.Width;


            // If one rectangle is on left side of other
            if (strucL > rectR || rectL > strucR)
                continue;

            // If one rectangle is above other
            if (struc.Y + struc.Height < loc.Y || loc.Y +rect.Height < struc.Y)
                continue;

            return true;
        }
        return isBlocked;
        /*
        var isBlocked =  _structures.Exists(x => x.X.Between(loc.X, rect.Width + loc.X, true) && x.Y.Between(loc.Y, rect.Height + loc.Y,true)); //BL
        if (isBlocked)
            return true;
        isBlocked = _structures.Exists(x => x.X.Between(loc.X, rect.Width + loc.X, true) && (x.Y + x.Height).Between(loc.Y, rect.Height + loc.Y, true)); 
        if (isBlocked)
            return true;
        isBlocked = _structures.Exists(x => (x.X + x.Width).Between(loc.X, rect.Width + loc.X, true) && (x.Y + x.Height).Between(loc.Y, rect.Height + loc.Y, true)); //TR
        if (isBlocked)
            return true;
        isBlocked = _structures.Exists(x => (x.X + x.Width).Between(loc.X, rect.Width + loc.X, true) && x.Y.Between(loc.Y, rect.Height + loc.Y, true));  //BR
        if (isBlocked)
            return true;
        return isBlocked;*/
    }

    public void BuildWalls(RectangleBound rect)
    {
        GameObject child;

        //child = Instantiate(vWall);
        var x = (rect.X * SpriteSize) / transform.localScale.x;
        var y = (rect.Y * SpriteSize) / transform.localScale.y;
        //child.transform.position = new Vector3(x, y, Z);

        //horizontalwalls
        for(int i = 1; i < rect.Width-1; ++i)
        {
            child = Instantiate(hWall);
            x = (rect.X * SpriteSize) / transform.localScale.x;
            y = (rect.Y * SpriteSize) / transform.localScale.y;
            var add = i * SpriteSize;
            child.transform.position = new Vector3(x + add, y, Z);
            _walls.Add(child.transform.position);

            child = Instantiate(hWall);
            x = (rect.X * SpriteSize) / transform.localScale.x;
            y = (rect.Y * SpriteSize) / transform.localScale.y;
            add = i * SpriteSize;
            child.transform.position = new Vector3(x + add, y + (rect.Height-1) * SpriteSize, Z);
            _walls.Add(child.transform.position);
        }
        //verticalwalls
        for (int i = 0; i < rect.Height; ++i)
        {
            if (i == 0)
            {
                cornerRenderer.sprite = BLCornerWall;
                child = Instantiate(corner);
            }
            else if (i == rect.Height - 1)
            {
                cornerRenderer.sprite = TLCornerWall;
                child = Instantiate(corner);
            }
            else
                child = Instantiate(vWall);

            x = (rect.X * SpriteSize) / transform.localScale.x;
            y = (rect.Y * SpriteSize) / transform.localScale.y;
            var add = i * SpriteSize;

            child.transform.position = new Vector3(x, y + add, Z);
            _walls.Add(child.transform.position);

            if (i == 0)
            {
                cornerRenderer.sprite = BRCornerWall;
                child = Instantiate(corner);
            }
            else if (i == rect.Height - 1)
            {
                cornerRenderer.sprite = TRCornerWall;
                child = Instantiate(corner);
            }
            else
                child = Instantiate(vWall);

            x = (rect.X * SpriteSize) / transform.localScale.x;
            y = (rect.Y * SpriteSize) / transform.localScale.y;
            add = i * SpriteSize;
            child.transform.position = new Vector3(x + (rect.Width-1) * SpriteSize, y + add, Z);
            _walls.Add(child.transform.position);
        }
        //child.transform.parent = transform;

            //Vrenderer.enabled = false;
            //wRenderer.enabled = false;
    }

}
