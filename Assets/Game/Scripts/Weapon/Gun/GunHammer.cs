using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHammer : GunBase
{
    private BulletHamer bulletHamer;

    private void GetMaterialBullet(List<Material> materials)
    {
        Renderer gunRender = bulletHamer.renderers;
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
        bulletHamer = SimplePoll.Spawn<BulletHamer>(PoolType.BulletHammer, gun, quaterBullet);
        bulletHamer.CreateBullet(transform,target, speed);
        GetMaterialBullet(materials);

        bulletHamer.SetSize(character.transform.localScale);
        bulletHamer.OnInit(character, onHit);
    }
}
