using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private float timeAtack;
    private float numberState;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMove();
        timeAtack = 0;
        numberState = Random.Range(1, 101);
        
    }
    public void OnExcute(Enemy enemy)
    {
        if (!enemy.isDead)
        {
            if (enemy.Curenttarget != null)
            {
                enemy.Attacker();
            }            
            timeAtack += Time.deltaTime;
            if (enemy.timeAtackAnim && timeAtack >= enemy.timeAtackAnim.length)
            {
                MoveState(enemy);                
            }
        }

    }

    public void OnExit(Enemy enemy)
    {

    }
    private void MoveState(Enemy enemy)
    {
        if (numberState <= 20)
        {
            enemy.ChangState(new IdelState());
        }
        else
        {
            enemy.ChangState(new MoveState());
        }
    }


}
