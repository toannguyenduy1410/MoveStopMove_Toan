using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : Character
{
    public static Player instance;

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpess;
    [SerializeField] private Material[] materialPlay;
    [SerializeField] MaterialWaeponSO materialWaeponSO;
    public TextLevelUp textLevelUpPrifab;
    private Vector3 moveDirection;
    private DynamicJoystick joystick;
    private int cointDesEnemy;
    private string nameBot;
    private bool isWin;
    
    public bool IsWin
    {
        get { return isWin; }
        set { isWin = value; }
    }

    public string NameBot
    {
        get { return nameBot; }
        set { nameBot = value; }
    }
    public int CointDesEnemy
    {
        get { return cointDesEnemy; }
        set { cointDesEnemy = value; }
    }
    public override void OnAwake()
    {
        base.OnAwake();
        instance = this;
        joystick = JoySticks.Instance.joysticks;
    }
    LayerMask ko;
    private void OnEnable()
    {
        if (curentAnim != null)
        {
            anim.SetTrigger(curentAnim);            
        }
    }
    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            Move();
        }
    }
    public void UpdateAnim()
    {
        if (GameManager.IsState(GameState.MainMenu))
        {
            ChangAnim(Anim.ANIM_IDLE);
        }
        else if (GameManager.IsState(GameState.SetSkin))
        {
            ChangAnim(Anim.ANIM_DANCE);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        cointDesEnemy = 0;
        curentlevel = 0;

        SetWeapon();
        SetSkin();
        UpScale(curentlevel, this);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    private void SetWeapon()
    {
        DataManager.Instance.Load();
        WaeponType EquippedWeaponType = DataManager.Instance.useData.currentEquippedWeaponType;
        int Equipping = DataManager.Instance.useData.curentEquipping;
        if (Equipping == 0)
        {
            List<Material> materialList = DataManager.Instance.useData
                .colorList[(int)EquippedWeaponType].materials.ToList();
            ChangWaepon(EquippedWeaponType
          , materialList);
        }
        else
        {
            ChangWaepon(EquippedWeaponType
          , materialWaeponSO.listmateriaWaepon[(int)EquippedWeaponType].listmaterials[Equipping].materials);
        }
    }

    public void SetSkin()
    {
        DataManager.Instance.LoadSkin();
        if (DataManager.Instance.skinData.isEnppui)
        {
            PageSkinType pageSkinType = DataManager.Instance.skinData.DataCurrentPageSkin;
            int curentEnppui = DataManager.Instance.skinData.dataCurrentEnppui;
            if (pageSkinType == PageSkinType.SkinHair)
            {
                ChangHair(skinSO.listSkinHair[curentEnppui].listObjSkin[0].objSkin
                    , skinSO.listSkinHair[curentEnppui].listObjSkin[0].parent
                    , skinSO.listSkinHair[curentEnppui].matPlayer);
                colorCharactor = skinSO.listSkinHair[curentEnppui].matPlayer[0].color;
            }
            else if (pageSkinType == PageSkinType.SkinPant)
            {
                Changpants(skinSO.listSkinPant[curentEnppui].material
                    , skinSO.listSkinPant[curentEnppui].matPlayer);
                colorCharactor = skinSO.listSkinHair[curentEnppui].matPlayer[0].color;
            }
            else if (pageSkinType == PageSkinType.SkinShield)
            {
                ChangShield(skinSO.listSkinShield[curentEnppui].objSkin.objSkin
                     , skinSO.listSkinShield[curentEnppui].objSkin.parent
                     , skinSO.listSkinShield[curentEnppui].matPlayer);
                colorCharactor = skinSO.listSkinHair[curentEnppui].matPlayer[0].color;
            }
            else if (pageSkinType == PageSkinType.SkinSet)
            {
                ChangSetSkin(skinSO.listSkinSet[curentEnppui].listObjSkin
                    , skinSO.listSkinSet[curentEnppui].material);
                colorCharactor = skinSO.listSkinSet[curentEnppui].material[0].color;
            }
        }
        else
        {
            SetNoSkin();
        }

        UISetName();
    }
    private void Move()
    {
        if (!isDead)
        {
            moveDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
            if (moveDirection.magnitude > 0.01)
            {
                ChangAnim(Anim.ANIM_RUN);
                transform.position = transform.position + moveDirection * speed * Time.deltaTime;
                var targetDirection = Vector3.RotateTowards(transform.forward, moveDirection, rotationSpess * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(targetDirection);
                isMove = true;
                if (!curentGun.activeSelf)
                {
                    curentGun.gameObject.SetActive(true);
                }

            }
            else if (!isDead && Curenttarget != null)
            {

                ChangAnim(Anim.ANIM_ATTACK);
            }
            else
            {
                ChangAnim(Anim.ANIM_IDLE);
                isMove = false;
                if (!curentGun.activeSelf)
                {
                    curentGun.gameObject.SetActive(true);
                }
            }
        }
    }
    //public override void Attack(Transform tagert)
    //{
    //    base.Attack(tagert);
    //    Debug.Log("đã chạy");
    //}
    public void IncreaseCamera(float floatFacrter)
    {
        CameraFollower.Instance.UpdateBaseDistance(floatFacrter);
    }
    public void TextUpLevel()
    {
        TextLevelUp textLevelUp = Instantiate(textLevelUpPrifab);
        textLevelUp.OnInit(transform.localScale.x);
    }
    public override Transform Tagert()
    {
        Transform tagertTranfrom = base.Tagert();
        Enemy enemytranfrom = tagertTranfrom != null ? tagertTranfrom.GetComponent<Enemy>() : null;
        if (tagertTranfrom != null && enemytranfrom != null)
        {
            GameObject imageEnemy = enemytranfrom.ImaTagert;

            {
                imageEnemy.SetActive(true);
            }
        }
        return tagertTranfrom;
    }
    public override void RemoveTagert(Character character)
    {
        base.RemoveTagert(character);
        if (character is Enemy)
        {
            Enemy enemy = (Enemy)character;
            enemy.ImaTagert.gameObject.SetActive(false);
        }
    }
    public void UISetName()
    {
        DataManager.Instance.Load();
        namePlay = DataManager.Instance.useData.name;
        uiScores.TextName(namePlay, colorCharactor);
    }
    public override void UISetLevel(int level, Color colorIM)
    {
        base.UISetLevel(level, colorIM);
        uiScores.TextLevel(level, colorIM);
    }
    public override void ChangHair(GameObject objSkin, ParentSkin parentSkin, Material[] matPlayer)
    {
        base.ChangHair(objSkin, parentSkin, matPlayer);
        Color color = matPlayer[0].color;
        UISetLevel(curentlevel, color);
    }
    public override void Changpants(Material[] matSkin, Material[] matPlayer)
    {
        base.Changpants(matSkin, matPlayer);
        Color color = matPlayer[0].color;
        UISetLevel(curentlevel, color);
    }
    public override void ChangSetSkin(List<ObjSkin> objSkins, Material[] matPlayer)
    {
        base.ChangSetSkin(objSkins, matPlayer);
        Color color = matPlayer[0].color;
        UISetLevel(curentlevel, color);
    }
    public override void ChangShield(GameObject objSkin, ParentSkin parentSkin, Material[] matPlayer)
    {
        base.ChangShield(objSkin, parentSkin, matPlayer);
        Color color = matPlayer[0].color;
        UISetLevel(curentlevel, color);
    }
    public void SetNoSkin()
    {

        foreach (var obj in listObjSkin)
        {
            Destroy(obj);
        }
        listObjSkin.Clear();
        SkinnedMeshRenderer skinnedMesh = curentPants.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh.materials = new Material[0];
        SkinnedMeshRenderer skinnedMeshPlay = curentMatPlayer.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshPlay.materials = materialPlay;

        Color color = materialPlay[0].color;
        UISetLevel(curentlevel, color);
        colorCharactor = color;
    }
    public void WinGame()
    {
        //IsWin = true;
        ChangAnim(Anim.ANIM_Win);
    }
}
