using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomerang : BulletBase
{
    [SerializeField] private Rigidbody rb;
    private Vector3 targetWithoutHeight;
    private Vector3 moveDirection;
    private float speed;
    private bool returningToPlayer;
    private void Update()
    {

        Rotate();
        if (!returningToPlayer)
        {
            rb.velocity = moveDirection * speed;
            if (Vector3.Distance(transform.position, targetWithoutHeight) < 0.1f)
            {
                returningToPlayer = true;
            }
        }
        else
        {
            
            moveDirection = Player.instance.transform.position;
           // transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);
           rb.velocity = moveDirection * speed;
        }

    }
    public override void CreateBullet(Transform tfGun, Vector3 target, float speed)
    {
        base.CreateBullet(tfGun, target, speed);        
        // B? ?i ?? cao c?a m?c tiêu
        targetWithoutHeight = new Vector3(target.x, transform.position.y, target.z);       
        // Tính toán h??ng di chuy?n
        moveDirection = (targetWithoutHeight - transform.position).normalized;
        this.speed = speed;
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            GameUnit gameUnit = other.GetComponent<GameUnit>(); // Lấy đối tượng GameUnit từ GameObject
            if (gameUnit != null)
            {
                SimplePoll.Desspawn(gameUnit);
            }
        }
    }
}
