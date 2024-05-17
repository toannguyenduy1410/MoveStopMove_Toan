using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunKnife : GunBase
{
    // [SerializeField] private BulletSword bulletHamer;
    private BulletKnife bulletKnife;
      
    private void GetMaterialBullet(List<Material> materials)
    {        
        Renderer gunRender = bulletKnife.renderers;
        if (gunRender != null)
        {
            Material[] renderMat = gunRender.materials;
            for (int i = 0; i < renderMat.Length; i++)
            {
                renderMat[i] = materials[i];
            }
            gunRender.materials = renderMat;
        }
    }
    public override void Shoot(Vector3 target, float speed, Character character, Action<Character, Character> onHit, List<Material> materials)
    {
        base.Shoot(target, speed, character, onHit, materials);
        bulletKnife = SimplePoll.Spawn<BulletKnife>(PoolType.BulletKnife, gun, quaterBullet);
        bulletKnife.CreateBullet(transform, target, speed);
        GetMaterialBullet(materials);

        bulletKnife.SetSize(character.transform.localScale);
        bulletKnife.OnInit(character, onHit);
    }
}
