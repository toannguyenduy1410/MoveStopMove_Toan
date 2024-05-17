using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLillpop : GunBase
{
    private BulletLillpop bulletLillpop;

    private void GetMaterialBullet(List<Material> materials)
    {
        Renderer gunRender = bulletLillpop.renderers;
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

        bulletLillpop = SimplePoll.Spawn<BulletLillpop>(PoolType.BulletLillpop, gun, quaterBullet);
        bulletLillpop.CreateBullet(transform, target, speed);
        GetMaterialBullet(materials);

        bulletLillpop.SetSize(character.transform.localScale);
        bulletLillpop.OnInit(character, onHit);
    }
}
