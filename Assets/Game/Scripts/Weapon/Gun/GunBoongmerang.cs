using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunBoongmerang : GunBase
{
    private BulletBoomerang bulletBoongmerang;

    private void GetMaterialBullet(List<Material> materials)
    {
        Renderer gunRender = bulletBoongmerang.renderers;
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
        
        bulletBoongmerang = SimplePoll.Spawn<BulletBoomerang>(PoolType.BulletBoomerang, gun, quaterBullet);
        bulletBoongmerang.CreateBullet(transform, target, speed);
        GetMaterialBullet(materials);

        bulletBoongmerang.SetSize(character.transform.localScale);
        bulletBoongmerang.OnInit(character, onHit);
    }
}
