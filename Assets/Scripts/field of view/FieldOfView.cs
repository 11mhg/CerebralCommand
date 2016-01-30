using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range (0,360)]
	public float viewAngle;
	public bool enableOnDrawGizmos;
    //Interesting numbers to fiddle with
    //will give different looking meshes
	public float meshResolution;
	public int edgeResolveIteration;
	public float edgeDstThreshold;
	public float mskCutAwayDst;
    //Layer Masks
	public LayerMask meshObstacleMask;
	public LayerMask obstaclesMask;
	public LayerMask targetMask;

    //List that can be accessed for its gameObjects
	[HideInInspector]
	public List<GameObject> visibleTargets = new List<GameObject>();

	void Start(){
		StartCoroutine ("FindTargetsWithDelay",.2f);
	}

	IEnumerator FindTargetsWithDelay(float delay){
		while(true){
			yield return new WaitForSeconds(delay);
		}
	}

    
}
