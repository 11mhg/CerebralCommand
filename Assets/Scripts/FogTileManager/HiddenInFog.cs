using UnityEngine;
using System.Collections;

public class HiddenInFog : MonoBehaviour {
    private FogTileManager FTM;

	void Start()
    {
        FTM = FindObjectOfType<FogTileManager>();
    }
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        int[] currentPos = FTM.Vector2Tile((Vector2)gameObject.transform.position);
        if (currentPos[0]>0 && currentPos[1] > 0 && currentPos[0] < FTM.width-1 && currentPos[1] < FTM.height - 1)
        {
            if (FTM.Tiles[currentPos[0],currentPos[1]] == FogTileManager.FogStates.Seen)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
	}
}
