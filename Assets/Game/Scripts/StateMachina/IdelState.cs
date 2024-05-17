using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : IState
{
    float time = 0;
    float randomtime;

    public void OnEnter(Enemy enemy)
    {
        randomtime = Random.Range(1f, 3f);
    }
    public void OnExcute(Enemy enemy)
    {
        if (!enemy.isDead )
        {
            time += Time.deltaTime;
            //enemy.curenttarget = enemy.Tagert();
            if (enemy.Curenttarget != null)
            {
                enemy.ChangState(new AttackState());
            }
            else if (time > randomtime)
            {

                enemy.ChangState(new MoveState());
            }
            else
            {
                enemy.StopMove();
            }
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
