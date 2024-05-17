using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class Character : Singleton<Character>
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform tagertWaepon;
    [SerializeField] protected UpLevelDataSO upLevelDataSO;
    
    [SerializeField] protected CharacterSight characterSight;
    [SerializeField] public WaeponSO waeponSO;
    [SerializeField] public SkinSO skinSO;

    public UIscores uiScores;
    public AnimationClip timeAtackAnim;
    protected List<Transform> listTagert = new List<Transform>();
    protected List<GameObject> listObjSkin = new List<GameObject>();
    protected List<Material> curentMatGun;
    protected GameObject curentGun;
    protected Color colorCharactor;
    public GameObject curentPants;
    public GameObject curentMatPlayer;
    public Transform[] curentParent;//các vị trí spawn
    protected Transform curenttarget;
   
   
    public int curentlevel = 0;
    protected float speedButllet;

    protected string curentAnim;
    protected string namePlay = "";
    public bool isAttack = false;
    protected bool isMove = false;
    public bool isDead;
    protected bool[] isLevel;
    protected float timerAttack;
    protected float timerAttackDelay = 2f;

    public Transform Curenttarget
    {
        get { return curenttarget; }
        set { curenttarget = value; }
    }
    private void Awake()
    {
        OnAwake();
    }
   
    void Start()
    {
        OnInit();
        OnDespawn();
    }
    public virtual void OnAwake()
    {
        CharactorManager.Instance.AddCharacter(this);
    }
    public virtual void OnInit()
    {
        if (GameManager.IsState(GameState.MainMenu))
        {
            ChangAnim(Anim.ANIM_IDLE);
        }
        isDead = false;
        QuantityIsLevel(10);
        ActiveLevel();
    }
    public virtual void OnDespawn()
    {

    }
    public void QuantityIsLevel(int size)
    {
        isLevel = new bool[size];
        for (int i = 0; i < size; i++)
        {
            isLevel[i] = false;
        }
    }

    public void ChangWaepon(WaeponType waeponType, List<Material> materials)//lấy waepon
    {

        GameObject newCurentWaepon = waeponSO.listWaeponData[(int)waeponType].WaeponPrifab;
        if (this.curentGun != null)
        {
            Destroy(this.curentGun);
        }
        this.curentGun = Instantiate(newCurentWaepon, tagertWaepon);
        speedButllet = waeponSO.listWaeponData[(int)waeponType].speed;

        Renderer GunRender = this.curentGun.GetComponent<Renderer>();

        if (GunRender != null)
        {
            curentMatGun = materials;
            Material[] renderMate = GunRender.materials;
            for (int i = 0; i < renderMate.Length; i++)
            {
                renderMate[i] = materials[i];
            }
            GunRender.materials = renderMate;
        }
    }
    
    public virtual void ChangHair(GameObject objSkin, ParentSkin parentSkin, Material[] matPlayer)
    {
        SkinnedMeshRenderer skinnedMesh = curentPants.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh.materials = new Material[0];
        foreach (var obj in listObjSkin)
        {
            Destroy(obj);
        }
        listObjSkin.Clear();
        GameObject objPrifab = Instantiate(objSkin, curentParent[(int)parentSkin]);
        listObjSkin.Add(objPrifab);
        SkinnedMeshRenderer skinnedMeshPlay = curentMatPlayer.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshPlay.materials = matPlayer;
    }
    public virtual void ChangShield(GameObject objSkin, ParentSkin parentSkin, Material[] matPlayer)
    {
        SkinnedMeshRenderer skinnedMesh = curentPants.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh.materials = new Material[0];
        foreach (var obj in listObjSkin)
        {
            Destroy(obj);
        }
        listObjSkin.Clear();
        GameObject objPrifab = Instantiate(objSkin, curentParent[(int)parentSkin]);
        listObjSkin.Add(objPrifab);
        SkinnedMeshRenderer skinnedMeshPlay = curentMatPlayer.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshPlay.materials = matPlayer;
    }
    public virtual void ChangSetSkin(List<ObjSkin> objSkins, Material[] matPlayer)
    {
        //xoa skin quan
        SkinnedMeshRenderer skinnedMesh = curentPants.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh.materials = new Material[0];
        //xoa skin khac
        foreach (var obj in listObjSkin)
        {
            Destroy(obj);
        }
        listObjSkin.Clear();
        for (int i = 0; i < objSkins.Count; i++)
        {
            int parentIndex = (int)objSkins[i].parent;

            if (parentIndex >= 0 && parentIndex < curentParent.Length)
            {
                GameObject objPrifab = Instantiate(objSkins[i].objSkin, curentParent[parentIndex]);
                listObjSkin.Add(objPrifab);
            }
        }
        SkinnedMeshRenderer skinnedMeshPlay = curentMatPlayer.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshPlay.materials = matPlayer;
    }
    public virtual void Changpants(Material[] matSkin, Material[] matPlayer)
    {
        foreach (var obj in listObjSkin)
        {
            Destroy(obj);
        }
        listObjSkin.Clear();
        SkinnedMeshRenderer skinnedMesh = curentPants.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh.materials = matSkin;
        SkinnedMeshRenderer skinnedMeshPlay = curentMatPlayer.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshPlay.materials = matPlayer;
    }
    public void DirectionTagertAnim()
    {
        if (Curenttarget != null)
        {
            Vector3 directionEnemy = Curenttarget.position - transform.position;
            Quaternion targetDirection = Quaternion.LookRotation(directionEnemy);
            transform.rotation = targetDirection;
        }        
    }
    public void AtackAnim()
    {
        if (Curenttarget != null)
        {
            Vector3 directionEnemy = Curenttarget.position - transform.position;
            Quaternion targetDirection = Quaternion.LookRotation(directionEnemy);
            transform.rotation = targetDirection;
            Attack(Curenttarget);
            curentGun.gameObject.SetActive(false);
        }        
    }
    public void OnActiveWeaponAnim()
    {
        curentGun.gameObject.SetActive(true);
    }
    public IEnumerator IEAttackCoroutine()
    {
        // Lấy mục tiêu một lần trước khi bắt đầu vòng lặp
        Transform target = Tagert();
        if (!isMove && !isDead && isAttack == false && target != null)
        {
            if (timerAttack > timerAttackDelay)
            {
                timerAttack = 0;
                //chờ mới ném
                yield return new WaitForSeconds(0.3f);
                Attack(target);
                isAttack = true;
                if (!isDead)
                {
                    ChangAnim(Anim.ANIM_ATTACK);
                }
                // curentWaepon.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                isAttack = false;
                //curentWaepon.SetActive(true);
            }
        }
    }
    protected void Attack(Transform tagert)//tấn công
    {
        if (tagert != null && curentGun != null)
        {
            GunBase gunBase = curentGun.GetComponent<GunBase>();
            gunBase.Shoot(tagert.position, speedButllet, this, OnHit, curentMatGun);
        }
    }
    public virtual Transform Tagert()//lấy được mục tiêu
    {
        if (listTagert.Count > 0)
        {
            // Kiểm tra và loại bỏ các đối tượng đã bị destroy khỏi danh sách
            for (int i = listTagert.Count - 1; i >= 0; i--)
            {
                if (listTagert[i] == null || listTagert[i].GetComponent<Character>().isDead)
                {
                    listTagert.RemoveAt(i);
                }
            }
            if (listTagert.Count > 0)
            {
                // Trả về transform của đối tượng đầu tiên trong danh sách sau khi loại bỏ
                return listTagert[0].transform;
            }
        }
        return null;
    }
    //logic khi viên đạn trúng nạn nhân
    protected void OnHit(Character attacker, Character victim)
    {
        if (attacker != victim && !victim.isDead)
        {
            victim.isDead = true;
            int creatLevel = victim.QuantityScale();
            victim.ChangAnim(Anim.ANIM_DAED);                       
            attacker.UpScale(creatLevel, attacker);
            attacker.Curenttarget = Tagert();                     
            //lay so luong coint dc cong khi hit
            if (attacker is Player)
            {
                Player player = (Player)attacker;
                int slcoint = victim.QuantityCoint();
                player.CointDesEnemy += slcoint;
            }
            //lấy name cua bot da hit player
            if (victim is Player)
            {
                Player player = (Player)victim;
                player.NameBot = attacker.namePlay;
            }

            victim.StartCoroutine(IEDead(victim));
        }
    }
    public void UpScale(int level, Character attacker)//Tăng Scale
    {
        curentlevel += level;
        UISetLevel(curentlevel, colorCharactor);
        for (int i = 0; i < upLevelDataSO.UplevelData.Count; i++)
        {
            if (curentlevel >= upLevelDataSO.UplevelData[i].minLevel
                && curentlevel < upLevelDataSO.UplevelData[i].maxLevel)
            {
                if (!isLevel[i])
                {
                    //tăng level
                    IncreaseScale(upLevelDataSO.UplevelData[i].UpScale);

                    if (attacker is Player)//điều kiện
                    {
                        Player playerAttacker = attacker as Player;//ép kiểu                        
                        playerAttacker.IncreaseCamera(upLevelDataSO.UplevelData[i].UpCamera);
                        playerAttacker.TextUpLevel();
                    }
                    isLevel[i] = true;
                }
                break;
            }
        }
    }
    public int QuantityScale()//số lượng level được công them khi tieu diệt
    {
        for (int i = 0; i < upLevelDataSO.UplevelData.Count; i++)
        {
            if (curentlevel >= upLevelDataSO.UplevelData[i].minLevel
                && curentlevel <= upLevelDataSO.UplevelData[i].maxLevel)
            {
                return upLevelDataSO.UplevelData[i].quantityScale;
            }
        }
        return 0;
    }
    public int QuantityCoint()//số lượng level được công them khi tieu diệt
    {
        for (int i = 0; i < upLevelDataSO.UplevelData.Count; i++)
        {
            if (curentlevel >= upLevelDataSO.UplevelData[i].minLevel
                && curentlevel <= upLevelDataSO.UplevelData[i].maxLevel)
            {
                return upLevelDataSO.UplevelData[i].quantityCoint;
            }
        }
        return 0;
    }
    public void IncreaseScale(float scaleFactor)
    {
        if (this != null)
        {
            transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
    }
    public void AddTagert(Character character)
    {
        if (character != this)
        {
            listTagert.Add(character.transform);
        }

        Curenttarget = Tagert();

    }

    public virtual void RemoveTagert(Character character)
    {
        listTagert.Remove(character.transform);

        Curenttarget = Tagert();

    }
    public virtual IEnumerator IEDead(Character victim)
    {
        if (victim is Enemy)
        {
            Enemy enemy = (Enemy)victim;
            enemy.ImaTagert.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        if (victim != null)
        {
            if (victim is Player)
            {
                GameManager.ChangeState(GameState.Fall);
                UIManager.Instance.CloseUIAll();
                UIManager.Instance.OpenUI<CanvasFall>();
                CharactorManager.Instance.CharactorActiveLevel();
                CharactorManager.Instance.RemoveCharacter(victim);
            }
            else
            {
                Destroy(victim.gameObject);
                CharactorManager.Instance.RemoveCharacter(victim);
            }
        }
    }
    //cập nhật UI Level
    public virtual void UISetLevel(int level, Color colorIM)
    {

    }
    //cập nhật Name   
    public void ActiveLevel()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (uiScores != null)
            {
                uiScores.gameObject.SetActive(true);
            }
        }
        else
        {
            if (uiScores != null)
            {
                uiScores.gameObject.SetActive(false);
            }
        }
    }
    public void ChangAnim(string animName)
    {
        if (curentAnim != animName)
        {
            anim.ResetTrigger(animName);
            curentAnim = animName;
            anim.SetTrigger(curentAnim);
        }
    }

}
