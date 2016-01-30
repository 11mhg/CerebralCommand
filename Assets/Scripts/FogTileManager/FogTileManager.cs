using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FogTileManager : MonoBehaviour {
    public int height=10;
    public int width=10;
    public GameObject toInstance;
    public bool FogisRevealing;
    public Material matSeen;
    public Material matDiscovered;
    public Material matUnseen;
    public LayerMask meshObstacleMask;
    private FogRevealer[] refRevealers;
    public FogStates[,] Tiles;
    private GameObject[,] TilesObject;
    public List<Vector2> Revealers = new List<Vector2>();

    public enum FogStates
    {
        Unseen,
        Discovered,
        Seen
    }

	// Use this for initialization
	void Start () {
        Tiles = new FogStates[width, height];
        TilesObject = new GameObject[width, height];
        Instantiating();
        refRevealers = FindObjectsOfType<FogRevealer>();
        FogisRevealing = true;
    }
	
    void Update()
    {
        ResetStates();
        RetrieveRevealers(refRevealers);
        RevealFog();
    }
	
    void RevealFog()
    {
        RevealCircle();
        for (int i = 0; i < refRevealers.Length; i++)
        {
            RevealCone(refRevealers[i]);
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Tiles[i, j] == FogStates.Seen && FogisRevealing)
                {
                    TilesObject[i, j].GetComponent<SpriteRenderer>().material = matSeen;
                }else if (Tiles[i,j] == FogStates.Seen && !FogisRevealing)
                {
                    TilesObject[i, j].GetComponent<SpriteRenderer>().material = matDiscovered;
                }
                else if (Tiles[i,j] == FogStates.Discovered)
                {
                    TilesObject[i, j].GetComponent<SpriteRenderer>().material = matDiscovered;
                }
            }
        }
    }

    void RevealCone(FogRevealer rev)
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i <= 90; i++)
        {
            float Angle = -rev.gameObject.transform.eulerAngles.z - 45 +  i;
            
            for (int j = 0; j < rev.FOVRadius;j++) {
                Vector3 dir = DirFromAngle(Angle, true, rev);
                RaycastHit2D hit;
                hit = Physics2D.Raycast(rev.gameObject.transform.position, dir,j*10,meshObstacleMask);
                if (hit.collider!= null)
                {
                    points.Add(hit.point);
                    break;
                }
                else
                {
                    points.Add(rev.gameObject.transform.position + dir * j * 10);
                }
            }
        }
        foreach (Vector2 p in points)
        {
            int[] seen = Vector2Tile(p);
            if (seen[0]>=0 && seen[1] >= 0 && seen[0] < width-1 && seen[1] < height-1) { 
                Tiles[seen[0], seen[1]] = FogStates.Seen;
            }
        }
    }

    public int[] Vector2Tile (Vector2 vec)
    {
        int[] toReturn = new int[2];
        toReturn[0] = Mathf.RoundToInt(vec.x/10);
        toReturn[1] = Mathf.RoundToInt(vec.y/10);
        return toReturn;
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal, FogRevealer rev)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= rev.gameObject.transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void RevealCircle()
    {
        for (int i = 0; i < Revealers.Count; i++)
        {
            int MidX = Mathf.RoundToInt(Revealers[i].x);
            int MidY = Mathf.RoundToInt(Revealers[i].y);
            if (MidX > 0 && MidY > 0 && MidX < width-1 && MidY < height-1)
            {
                for (int x = MidX - 1; x <= MidX + 1; x++)
                {
                    for (int y = MidY - 1; y <= MidY + 1; y++)
                    {
                        Tiles[x, y] = FogStates.Seen;
                    }
                }
            }
        }
    }

    void ResetStates()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Tiles[i,j] == FogStates.Seen)
                {
                    Tiles[i, j] = FogStates.Discovered;
                }
            }
        }
    }

    void RetrieveRevealers(FogRevealer[] toReveal)
    {
        Revealers.Clear();
        for (int i = 0; i < toReveal.Length; i++)
        {
            Revealers.Add(new Vector2(toReveal[i].gameObject.transform.position.x / 10, toReveal[i].gameObject.transform.position.y / 10));
        }
    } 

    void Instantiating()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject instance = Instantiate(toInstance, new Vector3(x*10, y * 10, -2), Quaternion.identity) as GameObject;
                instance.transform.SetParent(gameObject.transform);
                TilesObject[x, y] = instance;
                Tiles[x, y] = FogStates.Unseen;
            }
        }
    }

}
