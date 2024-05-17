using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    protected Vector3 gun;
    protected Quaternion quaterBullet;
    public virtual void Shoot(Vector3 target, float speed
        , Character character, Action<Character, Character> onHit, List<Material> materials)
    {
        gun = transform.position;
        gun = new Vector3(gun.x, target.y, gun.z);//chi lay y cua bot
        quaterBullet = Quaternion.Euler(90, 0, 0);
    }
}
