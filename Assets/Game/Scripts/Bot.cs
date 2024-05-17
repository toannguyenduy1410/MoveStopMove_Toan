using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class Bot : Singleton<Bot>
{
    [SerializeField] private Player playerPrifab;
    [SerializeField] private Enemy enemyPrifab;
    [SerializeField] private Transform enemyParen;
    [SerializeField] private MaterialWaeponSO materialWaeponSO;
    [SerializeField] private SkinSO skinSO;
    [SerializeField] private UpLevelDataSO upLevelDataSO;
    [SerializeField] private List<MaterialSkin> listMaterials;
    [SerializeField] private List<Transform> pos;//vi tri enemy dc spawn
    private List<Transform> usedPositions;
    private List<Enemy> listEnemySpawn = new List<Enemy>();
    private Player playerSpawn;
    public int aliveEnemy = 50;//sl tren map
    public int maxEnemy = 50;//sl dc phep spawn
    private int aliveEnemyMap = 9;
    private Transform curenpos;
    private Color ColorBot;
    public Player PlayerSpawn
    {
        get { return playerSpawn; }
    }
    void Start()
    {       
        usedPositions = new List<Transform>(pos); // Sao chép tất cả các phần tử từ pos vào usedPositions
        SpawnPlayer();
        SpawnEnemy(aliveEnemyMap);

        InvokeRepeating(nameof(RemoveBot), 0f, 1f);
    }
    private void RemoveBot()
    {
        for (int i = 0; i < listEnemySpawn.Count; i++)
        {
            if (listEnemySpawn[i] == null)
            {
                listEnemySpawn.RemoveAt(i);
                aliveEnemy--;
            }
        }
        if (maxEnemy > 0) // Thêm điều kiện kiểm tra maxEnemy
        {
            int missingEnemies = aliveEnemyMap - listEnemySpawn.Count;
            if (missingEnemies > 0)
            {
                SpawnEnemy(missingEnemies);
            }
        }
    }



    public Transform RandomNavmeshLocation()
    {
        // Kiểm tra nếu danh sách các vị trí đã sử dụng trống hoặc không tồn tại
        if (usedPositions.Count <= 0)
        {
            // Sao chép danh sách vị trí ban đầu vào danh sách đã sử dụng
            usedPositions = new List<Transform>(pos);
        }

        // Lưu trữ vị trí trước đó
        Transform previousPos = curenpos;
        while (usedPositions.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, usedPositions.Count);
            Transform randomPos = usedPositions[randomIndex];
            if (randomPos != previousPos)
            {
                // Nếu có, gán vị trí mới vào curenpos và loại bỏ khỏi danh sách
                curenpos = randomPos;
                usedPositions.RemoveAt(randomIndex);
                return curenpos;
            }
        }
        return curenpos.transform;
    }

    public void SpawnEnemy(int AmoutEnemy)
    {
        int levelplay = GetLevelCountByType(Player.instance.curentlevel);
        for (int i = 0; i < AmoutEnemy; i++)
        {
            if (maxEnemy > 0)
            {
                int levelEnemy = UnityEngine.Random.Range(upLevelDataSO.UplevelData[levelplay].minLevel
               , upLevelDataSO.UplevelData[levelplay].maxLevel + 1);
                string Name = GenerateRandomName();
                Transform transform = RandomNavmeshLocation();
                Enemy enemy = Instantiate(enemyPrifab, transform.position, Quaternion.identity, enemyParen);
                SetWeapon(enemy);
                SetSkin(enemy);
                enemy.curentlevel = levelEnemy;
                enemy.SetColorTarget(ColorBot);

                enemy.IncreaseScale(upLevelDataSO.UplevelData[levelplay].UpScale);
                enemy.UISetLevel(levelplay, ColorBot);

                enemy.UISetName(Name, ColorBot);
                listEnemySpawn.Add(enemy);
                maxEnemy--;
            }
        }
    }
    private void SpawnPlayer()
    {
        Transform tfPlayer = RandomNavmeshLocation();
        playerSpawn = Instantiate(playerPrifab, tfPlayer.position, Quaternion.identity, enemyParen);
        maxEnemy--;
       
    }
    public void SetWeapon(Enemy enemy)
    {
        WaeponType waepon = (WaeponType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(WaeponType)).Length);
        int curentMtaWeapon = UnityEngine.Random.Range(1, 5);
        enemy.ChangWaepon(waepon
          , materialWaeponSO.listmateriaWaepon[(int)waepon].listmaterials[curentMtaWeapon].materials);
    }
    public void SetSkin(Enemy enemy)
    {
        // Lấy ngẫu nhiên một giá trị cho PageSkinType
        PageSkinType pageSkinType = (PageSkinType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(PageSkinType)).Length);
        int itemCount = GetSkinCountByType(pageSkinType);
        // Lấy ngẫu nhiên một giá trị cho curentEnppui
        int curentEnppui = UnityEngine.Random.Range(0, itemCount);
        int mateSkin = UnityEngine.Random.Range(0, listMaterials.Count);

        if (pageSkinType == PageSkinType.SkinHair)
        {
            enemy.ChangHair(skinSO.listSkinHair[curentEnppui].listObjSkin[0].objSkin
                , skinSO.listSkinHair[curentEnppui].listObjSkin[0].parent
                , listMaterials[mateSkin].mateSkin);
            ColorBot = listMaterials[mateSkin].mateSkin[0].color;

        }
        else if (pageSkinType == PageSkinType.SkinPant)
        {
            enemy.Changpants(skinSO.listSkinPant[curentEnppui].material
                , listMaterials[mateSkin].mateSkin);
            ColorBot = listMaterials[mateSkin].mateSkin[0].color;

        }
        else if (pageSkinType == PageSkinType.SkinShield)
        {
            enemy.ChangShield(skinSO.listSkinShield[curentEnppui].objSkin.objSkin
                 , skinSO.listSkinShield[curentEnppui].objSkin.parent
                 , listMaterials[mateSkin].mateSkin);
            ColorBot = listMaterials[mateSkin].mateSkin[0].color;

        }
        else if (pageSkinType == PageSkinType.SkinSet)
        {
            enemy.ChangSetSkin(skinSO.listSkinSet[curentEnppui].listObjSkin
                , skinSO.listSkinSet[curentEnppui].material);
            ColorBot = skinSO.listSkinSet[curentEnppui].material[0].color;

        }
    }

    private string GenerateRandomName()
    {
        StringBuilder builder = new StringBuilder();
        System.Random random = new System.Random();

        // Số ký tự trong tên
        int nameLength = random.Next(5, 8);

        // Tạo một chuỗi ngẫu nhiên từ các ký tự trong bảng chữ cái
        for (int i = 0; i < nameLength; i++)
        {
            // Chọn một ký tự ngẫu nhiên từ bảng chữ cái (a-z)
            char randomChar = (char)('a' + random.Next(0, 26));

            builder.Append(randomChar);
        }

        return builder.ToString();
    }

    //so luong item
    private int GetSkinCountByType(PageSkinType skinType)
    {
        switch (skinType)
        {
            case PageSkinType.SkinHair:
                return skinSO.listSkinHair.Count;
            case PageSkinType.SkinPant:
                return skinSO.listSkinPant.Count;
            case PageSkinType.SkinShield:
                return skinSO.listSkinShield.Count;
            case PageSkinType.SkinSet:
                return skinSO.listSkinSet.Count;
            default:
                return 0;
        }
    }

    private int GetLevelCountByType(int level)
    {
        if (level <= upLevelDataSO.UplevelData[0].maxLevel)
        {
            return 0;
        }
        else if (level <= upLevelDataSO.UplevelData[1].maxLevel)
        {
            return 1;
        }
        else if (level <= upLevelDataSO.UplevelData[2].maxLevel)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
[Serializable]
public class MaterialSkin
{
    public Material[] mateSkin;
}
