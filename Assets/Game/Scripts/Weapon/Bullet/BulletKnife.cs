using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnife : BulletBase
{
    [SerializeField] private Rigidbody rb;
    public override void CreateBullet(Transform tfGun, Vector3 target, float speed)
    {
        base.CreateBullet(tfGun, target, speed);
        // Tính toán hướng từ đối tượng bắn đạn tới mục tiêu
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 euler = targetRotation.eulerAngles;
        euler.x = 90; // Giữ nguyên góc x
        transform.rotation = Quaternion.Euler(euler);
       
        rb.velocity = transform.up * speed;
    }
}
