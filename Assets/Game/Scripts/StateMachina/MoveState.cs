using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveState : IState
{
    private float time;
    private float timedelay;
    private bool CanAtack;
    public void OnEnter(Enemy enemy)
    {
        enemy.RandomNavmeshLocation(25);
        time = 0;
        timedelay = 1f;
    }
    public void OnExcute(Enemy enemy)
    {
        if ( !enemy.isDead)
        {

            time += Time.deltaTime;
            if (time > timedelay)
            {
                CanAtack = true;
            }
            if (Vector3.Distance(enemy.transform.position, enemy.curentPosition) < 0.1f)
            {
                enemy.ChangState(new IdelState());
            }
            else if (enemy.Curenttarget != null && CanAtack)
            {

                enemy.ChangState(new AttackState());
            }
            else if (!enemy.isDead)
            {
                enemy.Move();
            }

        }
    }

    public void OnExit(Enemy enemy)
    {
        CanAtack = false;
    }
}