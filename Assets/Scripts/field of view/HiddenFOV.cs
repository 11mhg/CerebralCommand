using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HiddenFOV : MonoBehaviour {
    private FieldOfView[] FOVRef;
	// Use this for initialization
	void Start () {
        FOVRef = FindObjectsOfType<FieldOfView>();
        GetComponent<SpriteRenderer>().enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (FieldOfView FOV in FOVRef)
        {
            List<GameObject> visible = FOV.visibleTargets;
            foreach (GameObject obj in visible)
            {
                if (gameObject == obj)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }

	}
}
