using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCandy : GunBase
{
    private BulletCandy bulletCandy;

    private void GetMaterialBullet(List<Material> materials)
    {
        Renderer gunRender = bulletCandy.renderers;
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
    public override void Shoot(Vector3 target, float speed
        , Character character, Action<Character, Character> onHit, List<Material> materials)
    {
        base.Shoot(target, speed, character, onHit, materials);

        bulletCandy = SimplePoll.Spawn<BulletCandy>(PoolType.BulletCandy, gun, quaterBullet);
        bulletCandy.CreateBullet(transform, target, speed);
        GetMaterialBullet(materials);

        bulletCandy.SetSize(character.transform.localScale);
        bulletCandy.OnInit(character, onHit);
    }
}
