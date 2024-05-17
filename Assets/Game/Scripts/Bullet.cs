using System;

using UnityEngine;


public class Bullet : GameUnit
{   
    public Renderer renderers;
    protected Character attacker;
    protected Action<Character , Character > onHit;

    // set bullet data for bullet       
    public virtual void OnInit(Character attacker, Action<Character , Character > onHit)
    {
        this.attacker = attacker;
        this.onHit = onHit;        
    }
    // Set kích thước cho viên đạn
    public virtual void SetSize(Vector3 size)
    {
        transform.localScale = size;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character victim = other.GetComponent<Character>();
           
            if (attacker != victim)
            {               
                onHit?.Invoke(attacker, victim);
                SimplePoll.Desspawn(this);
            }

        }
    }
}
