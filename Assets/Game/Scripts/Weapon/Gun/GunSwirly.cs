using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwirly : GunBase
{
    private BulletSwirly bulletSwirly;

    private void GetMaterialBullet(List<Material> materials)
    {
        Renderer gunRender = bulletSwirly.renderers;
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

        bulletSwirly = SimplePoll.Spawn<BulletSwirly>(PoolType.BulletSwirly, gun, quaterBullet);
        bulletSwirly.CreateBullet(transform, target, speed);
        GetMaterialBullet(materials);

        bulletSwirly.SetSize(character.transform.localScale);
        bulletSwirly.OnInit(character, onHit);
    }
}