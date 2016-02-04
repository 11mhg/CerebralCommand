using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TBSPlayerUnit : MonoBehaviour {

    public enum UnitState
    {
        Idle,
        Turn,
        Selected,
        Move,
        Moving,
        Attack,
        End
    }

    public UnitState unitState;
    public Class unitClass;
    private Text text;
    private bool hasMoved;
    private bool hasAttacked;
    private Pathfinding Pathfinder;
    private Vector2 targetPos;
    private Grid grid;
    private float speed = 1000f;
    private Rigidbody2D rb2d;
    private TBSMachine TurnStateMachine;
    void Start()
    {
        unitState = UnitState.Idle;
        Pathfinder = FindObjectOfType<Pathfinding>();
        grid = FindObjectOfType<Grid>();
        TurnStateMachine = FindObjectOfType<TBSMachine>();
        rb2d = GetComponent<Rigidbody2D>();
        text = FindObjectOfType<Text>();
    }

    public void InitiateUnit(Class _UnitClass)
    {
        unitClass = _UnitClass;
    }

    void Update()
    {
        //Debug.Log(unitState);
        //Debug.Log(TurnStateMachine.turn);
        if (TurnStateMachine.turn != TBSMachine.TurnState.Player)
        {
            unitState = UnitState.Idle;
        }
        switch (unitState)
        {
            
            case (UnitState.Idle):
                if (TurnStateMachine.turn == TBSMachine.TurnState.Player)
                {
                    hasMoved = false;
                    unitState = UnitState.Turn;
                }
                break;
            case (UnitState.Turn):
                break;
            case (UnitState.Selected):
                text.text = "Hello";
                PlayerClick();
                break;
            case (UnitState.Move):
                StartCoroutine("Moving");
                unitState = UnitState.Moving;
                break;
            case (UnitState.Moving):
                break;
            case (UnitState.Attack):
                break;
            case (UnitState.End):
                break;
        }
    }

    IEnumerator Moving()
    {
        List<Node> Path = Pathfinder.FindPath(transform.position, targetPos);
        List<Vector3> PathV = new List<Vector3>();
        foreach (Node n in Path)
        {
            PathV.Add(grid.WorldPointFromNode(n));
        }
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (Vector3.Distance(transform.position,PathV[0]) > 1)
            {
                rb2d.velocity = new Vector2(Mathf.Clamp((PathV[0].x - transform.position.x),-1,1) * speed * Time.deltaTime, Mathf.Clamp((PathV[0].y - transform.position.y),-1,1) * speed * Time.deltaTime);
            }else
            {
                
                PathV.RemoveAt(0);
            }
            if (PathV.Count == 0)
            {
                unitState = UnitState.Selected;
                hasMoved = true;
                break;
            }
        }
        rb2d.velocity = Vector2.zero;
    }

    void OnMouseDown()
    {
        if (unitState == UnitState.Turn)
        {
            unitState = UnitState.Selected;
        }
    }

    void PlayerClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos =Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Node tempNode = grid.NodeFromWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
            mousePos = grid.WorldPointFromNode(tempNode);
            Collider2D collhit = Physics2D.OverlapCircle(mousePos, grid.nodeRadius-0.1f);
            if (collhit != null)
            {
                if (collhit.CompareTag("Enemy"))
                {
                    unitState = UnitState.Attack;
                }
                else
                {
                    unitState = UnitState.Selected;
                }
            }
            else
            {
                if (!hasMoved)
                {
                    targetPos = new Vector3(mousePos.x, mousePos.y, gameObject.transform.position.z);
                    unitState = UnitState.Move;
                }
            }
        }
    }

	
}
