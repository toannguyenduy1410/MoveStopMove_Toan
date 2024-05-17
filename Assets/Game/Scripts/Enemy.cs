using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    public Target target;
    public GameObject ImaTagert;
    private IState currentIState;
    public Vector3 curentPosition;
    public List<Material> material;
    private void Update()
    {
        if (GameManager.IsState(GameState.GamePlay) || GameManager.IsState(GameState.Fall))
        {
            if (currentIState != null)
            {
                currentIState.OnExcute(this);
            }
            if (isDead)
            {
                agent.SetDestination(transform.position);
            }
        }
    }
    public override void OnAwake()
    {
        base.OnAwake();
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangState(new MoveState());

    }    
    //set color cho offscreen
    public void SetColorTarget(Color targetColor)
    {
        target.TargetColor = targetColor;
    }
    public override void UISetLevel(int level, Color colorIM)
    {
        base.UISetLevel(level, colorIM);
        colorCharactor = colorIM;        
        uiScores.TextLevel(curentlevel, colorIM);
    }
    public  void UISetName(string name, Color colorIM)
    {       
        this.namePlay = name;
        uiScores.TextName(name, colorIM);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public void Attacker()
    {
        ChangAnim(Anim.ANIM_ATTACK);
    }
    public void Move()
    {
        if (!isAttack && !isDead)
        {
            ChangAnim(Anim.ANIM_RUN);
            isMove = true;
            agent.SetDestination(curentPosition);
        }
    }
    public void StopMove()
    {
        if (!isAttack && !isDead)
        {
            isMove = false;

            if (agent != null)
            {
                ChangAnim(Anim.ANIM_IDLE);
                agent.SetDestination(transform.position);
            }
        }

    }
    public void RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = transform.position + UnityEngine.Random.insideUnitSphere * radius;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            curentPosition = hit.position;
        }
    }
    public void ChangState(IState newState)
    {
        if (currentIState != null)
        {
            currentIState.OnExit(this);
        }
        currentIState = newState;
        if (currentIState != null)
        {
            currentIState.OnEnter(this);
        }
    }
  
}
