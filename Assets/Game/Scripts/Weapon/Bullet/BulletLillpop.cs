using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLillpop : BulletBase
{
    [SerializeField] private Rigidbody rb;
    private void Update()
    {
        Rotate();
    }

    public override void CreateBullet(Transform tfGun, Vector3 target, float speed)
    {
        base.CreateBullet(tfGun, target, speed);
        // Bỏ đi độ cao của mục tiêu
        Vector3 targetWithoutHeight = new Vector3(target.x, transform.position.y, target.z);

        // Tính toán hướng di chuyển
        Vector3 moveDirection = (targetWithoutHeight - transform.position).normalized;

        rb.velocity = moveDirection * speed;
    }
}

