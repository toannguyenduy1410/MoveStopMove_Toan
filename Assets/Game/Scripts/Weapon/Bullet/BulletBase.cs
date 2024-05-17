using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBase : GameUnit
{
   
    public Renderer renderers;
    public GameObject objRender;
    protected Character attacker;
    protected Action<Character, Character> onHit;
    // Tốc độ quay của đối tượng (độ/giây)
    protected float rotationSpeed = 720;
    public virtual void CreateBullet(Transform tfGun, Vector3 target, float speed)
    {
        //con ke thua
        objRender.transform.position = tfGun.position;
    }
    // set bullet data for bullet       
    public void OnInit(Character attacker, Action<Character, Character> onHit)
    {
        this.attacker = attacker;
        this.onHit = onHit;
    }
    // Set kích thước cho viên đạn
    public void SetSize(Vector3 size)
    {
        transform.localScale = size;
    }
   //qoay tron
    protected void Rotate()
    {
        // Tính góc quay dựa trên thời gian và tốc độ quay
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // Quay đối tượng theo trục forward với góc tính được
        transform.Rotate(Vector3.forward, rotationAngle);
    }   
    public virtual void OnTriggerEnter(Collider other)
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
