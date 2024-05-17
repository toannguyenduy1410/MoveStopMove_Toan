using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class UIWeaponSelection : Singleton<UIWeaponSelection>
{
    [SerializeField] private RawImage rawImShoWaepon;
    [SerializeField] private Button btnext;
    [SerializeField] private Button btprevious;
    [SerializeField] private Button btSelectButton;
    [SerializeField] private Button btEquipButton;
    [SerializeField] private Button btUnlockButton;
    [SerializeField] private Button btCoint;
    [SerializeField] private TextMeshProUGUI txCoint;
    [SerializeField] private MaterialWaeponSO materialWaeponSO;
    [SerializeField] private UIMaterialWaepon uIMaterialWaepon;
    [SerializeField] private UIMaterialSelections uIMaterialSelections;
    [SerializeField] private Transform parentMateri;
    // Biến tạm thời để lưu trữ các giao diện người dùng đã tạo
    private List<UIMaterialWaepon> temporaryList = new List<UIMaterialWaepon>();

    //lưu trữ trạng thái mở khóa của mỗi màu súng cho từng loại súng
    private List<WeaponData> listGunData = new List<WeaponData>();
    private Image currentImagekhung;

    //luu weapon dang mac
    public WaeponType currentWaeponType;
    private int currenState;
    private void Awake()
    {
        uIMaterialSelections.SetData();
    }
    private void OnEnable()
    {
        //DataManager.Instance.Load();
        //listGunData = DataManager.Instance.useData.WaeponList;

        SetUIWeaponData();
        currentWaeponType = DataManager.Instance.useData.currentEquippedWeaponType;
        currenState = DataManager.Instance.useData.curentEquipping;
        //dat la dang mac khi vao game
        setEquipping();               
        ShowEquippingStart();
    }
    void Start()
    {
        // Gắn sự kiện cho các nút   
        btnext.onClick.AddListener(Next);
        btprevious.onClick.AddListener(Previous);
        btSelectButton.onClick.AddListener(OnSelectButtonClick);
        btEquipButton.onClick.AddListener(OnEquipButtonClick);
        btUnlockButton.onClick.AddListener(OnUnlockButtonClick);
        btCoint.onClick.AddListener(OnCointButtonClick);
    }
    private void UpdateBT()
    {
        bool bought = listGunData[(int)currentWaeponType].isBoght;

        // Tắt tất cả các nút
        btCoint.gameObject.SetActive(false);
        btSelectButton.gameObject.SetActive(false);
        btEquipButton.gameObject.SetActive(false);
        btUnlockButton.gameObject.SetActive(false);

        if (!bought)//chua mua
        {
            btCoint.gameObject.SetActive(true);
            int coint = materialWaeponSO.listmateriaWaepon[(int)currentWaeponType].price;
            txCoint.text = coint.ToString();
        }
        else
        {
            //dang bi khoa hay ko
            if (listGunData[(int)currentWaeponType].isLocked[currenState])
            {
                btUnlockButton.gameObject.SetActive(true);
                return;
            }
            //dang mac
            if (listGunData[(int)currentWaeponType].isEnppui[currenState])
            {
                btEquipButton.gameObject.SetActive(true);
            }
            else
            {
                btSelectButton.gameObject.SetActive(true);
            }
        }
    }
    public void ShowEquippingStart()
    {
        //show den item nao den item dang mac  
        for (int i = 0; i < listGunData[(int)currentWaeponType].isEnppui.Count; i++)
        {
            //show den item nao dang mac
            if (listGunData[(int)currentWaeponType].isEnppui[i])
            {               
                LoadMaterial();
                LoadIMWaepon(currentWaeponType);
                if (temporaryList != null && i >= 0 && i < temporaryList.Count)
                {
                    UIMaterialWaepon uiMate = temporaryList[i];
                    ShowItem(i, uiMate.ImageKhung, uiMate.renderTexture);
                }
                return;
            }
        }
    }
    // Hàm xử lý khi nút "Select" được nhấn
    public void OnSelectButtonClick()
    {
        setEquipping();
        if (currenState == 0)
        {
            // Đoạn mã để ép kiểu mảng thành danh sách
            List<Material> materialList = DataManager.Instance.useData.colorList[(int)currentWaeponType].materials.ToList();
            Player.instance.ChangWaepon(currentWaeponType
         , materialList);
        }
        else
        {
            Player.instance.ChangWaepon(currentWaeponType
           , materialWaeponSO.listmateriaWaepon[(int)currentWaeponType].listmaterials[currenState].materials);

        }
        UpdateBT();
        UIManager.Instance.CloseUI<CanvasWaepon>(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }
    // Hàm xử lý khi nút "Equip" được nhấn
    private void OnEquipButtonClick()
    {
        UIManager.Instance.CloseUI<CanvasWaepon>(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }
    // Hàm xử lý khi nút "Unlock" được nhấn
    private void OnUnlockButtonClick()
    {
        listGunData[(int)currentWaeponType].isLocked[currenState] = false;
        //chang vu khi
        Player.instance.ChangWaepon(currentWaeponType
           , materialWaeponSO.listmateriaWaepon[(int)currentWaeponType].listmaterials[currenState].materials);
        UIMaterialWaepon clickedWaepon = temporaryList[currenState];
        clickedWaepon.SetLocket(false);
        setEquipping();
        UpdateBT();
    }

    private void OnCointButtonClick()
    {
        int coint = materialWaeponSO.listmateriaWaepon[(int)currentWaeponType].price;
        if (Coint.Instance.Coints >= coint)
        {
            //mac dinh khi mua
            currenState = 2;
            Player.instance.ChangWaepon(currentWaeponType
          , materialWaeponSO.listmateriaWaepon[(int)currentWaeponType].listmaterials[2].materials);


            listGunData[(int)currentWaeponType].isBoght = true;//mua                                                   
            //dat la dang mac
            setEquipping();
            LoadMaterial();
            ShowLockStatus();
            UpdateBT();
            Coint.Instance.DesCoint(coint);
            Coint.Instance.LoadCoint();
        }
    }
    private void SetUIWeaponData()
    {
        DataManager.Instance.Load();
        listGunData = DataManager.Instance.useData.WaeponList;
        
        // Duyệt qua từng loại skin
        foreach (WaeponType Waepontype in Enum.GetValues(typeof(WaeponType)))
        {
            int weaponSOCount = materialWaeponSO.listmateriaWaepon[(int)Waepontype].listmaterials.Count; // Số lượng item cho từng loại skin           
            if ((int)Waepontype >= listGunData.Count)
            {               
                // Tạo một đối tượng Skin mới cho mỗi loại skin
                WeaponData newWeapon = new WeaponData();
                if (Waepontype == 0)//weapon dau tien se mo khoa
                {
                    newWeapon.isBoght = true;                   
                }
                else
                {
                    newWeapon.isBoght = false;
                }

                // Khởi tạo trạng thái mặc định cho mỗi mục trong skin
                for (int i = 0; i < weaponSOCount; i++)
                {
                    if (i <= 2)//tu 0->2
                    {
                        newWeapon.isLocked.Add(false);
                    }
                    else
                    {
                        newWeapon.isLocked.Add(true);
                    }
                    newWeapon.isEnppui.Add(false);
                }
                // Thêm skin mới vào danh sách listSkin
                listGunData.Add(newWeapon);
            }
            else
            {
                //locket
                int slIsLocked = listGunData[(int)Waepontype].isLocked.Count;
                int slIsLockedMissing = weaponSOCount - slIsLocked;
                for (int i = 0; i < slIsLockedMissing; i++)
                {
                    if (i <= 2)//tu 0->2
                    {
                        listGunData[(int)Waepontype].isLocked.Add(false);
                    }
                    else
                    {
                        listGunData[(int)Waepontype].isLocked.Add(true);
                    }
                }
                //Enppui
                int slIsEnppui = listGunData[(int)Waepontype].isEnppui.Count;
                int slIsEnppuiMissing = weaponSOCount - slIsEnppui;
                for (int i = 0; i < slIsEnppuiMissing; i++)
                {
                    listGunData[(int)Waepontype].isEnppui.Add(false);
                }
            }
        }
        // Lưu dữ liệu vào DataManager
        DataManager.Instance.useData.WaeponList = listGunData;
        DataManager.Instance.Save();
    }


    //set lam dang mac
    private void setEquipping()
    {
        //lấy ra item nao dang mac
        WaeponType dataCurentWeapon = DataManager.Instance.useData.currentEquippedWeaponType;
        int dataCurrenWeapon = DataManager.Instance.useData.curentEquipping;

        // Kiểm tra nếu đã có một loại vũ khí khác được trang bị, đặt nó thành false
        listGunData[(int)dataCurentWeapon].isEnppui[dataCurrenWeapon] = false;

        // Đặt loại vũ khí mới và vị trí trang bị
        listGunData[(int)currentWaeponType].isEnppui[currenState] = true;
        DataManager.Instance.useData.WaeponList = listGunData;
        DataManager.Instance.useData.currentEquippedWeaponType = currentWaeponType;
        DataManager.Instance.useData.curentEquipping = currenState;

        // Lưu dữ liệu vào PlayerPrefs
        DataManager.Instance.Save();

    }
    private void Next()
    {
        if ((int)currentWaeponType < materialWaeponSO.listmateriaWaepon.Count - 1)
        {
            currentWaeponType++;
            LoadMaterial();
            ShowLockStatus();
            LoadIMWaepon(currentWaeponType);
            SpamCosTom();
        }
    }
    private void Previous()
    {
        if ((int)currentWaeponType > 0)
        {
            currentWaeponType--;
            LoadMaterial();
            ShowLockStatus();
            LoadIMWaepon(currentWaeponType);
            SpamCosTom();
        }
    }
    private void SpawnMaterial(string textIndex, RenderTexture renderTexture, int index, GameObject waepon)
    {
        UIMaterialWaepon prifab = Instantiate(uIMaterialWaepon, parentMateri);
        // Lấy trạng thái unlock
        bool isLocked = listGunData[(int)currentWaeponType].isLocked[index];
        // Đặt trạng thái khóa cho giao diện người dùng
        prifab.SetLocket(isLocked);
        prifab.Instan(waepon);

        prifab.OnInit(textIndex, renderTexture, HandelClick);
        temporaryList.Add(prifab);

    }

    private void HandelClick(int index, Image ImageKhung, RenderTexture texture)
    {
        ShowItem(index, ImageKhung, texture);
        SpamCosTom();
    }
    private void ShowItem(int index, Image ImageKhung, RenderTexture texture)
    {
        this.currenState = index;
        rawImShoWaepon.texture = texture;

        if (currentImagekhung != null)
        {
            currentImagekhung.gameObject.SetActive(false);
        }
        currentImagekhung = ImageKhung;
        currentImagekhung.gameObject.SetActive(true);
        SpamCosTom();
        UpdateBT();
    }

    private void LoadMaterial()
    {
        // Xóa tất cả trong danh sách tạm trước khi tạo mới
        foreach (var item in temporaryList)
        {
            Destroy(item.gameObject);
        }
        temporaryList.Clear();
        // Kiểm tra xem vũ khí hiện tại đã mua chưa
        if (listGunData[(int)currentWaeponType].isBoght)
        {
            List<taMaterialWeapons> currentWeaponMaterials = materialWaeponSO.listmateriaWaepon[(int)currentWaeponType].listmaterials;
            for (int i = 0; i < currentWeaponMaterials.Count; i++)
            {
                taMaterialWeapons material = currentWeaponMaterials[i];
                // Kiểm tra null trước khi gọi SpawnMaterial()
                if (material.textures != null)
                {
                    SpawnMaterial(i.ToString(), material.textures, i, material.waepon);
                }                
            }
        }
        SetMaterialCostom();
    }
    private void ShowLockStatus()
    {
        if (listGunData[(int)currentWaeponType].isBoght)
        {
            //show den item nao den item dang mac 
            for (int i = 0; i < listGunData[(int)currentWaeponType].isEnppui.Count; i++)
            {
                //show den item nao dang mac
                if (listGunData[(int)currentWaeponType].isEnppui[i])
                {
                    UIMaterialWaepon uiMate = temporaryList[i];
                    ShowItem(i, uiMate.ImageKhung, uiMate.renderTexture);
                    return;
                }
            }
            //show den item nao den item da mo khoa 
            for (int i = 0; i < listGunData[(int)currentWaeponType].isLocked.Count; i++)
            {
                //show den item nao dang mo khoa
                if (!listGunData[(int)currentWaeponType].isLocked[i])
                {
                    UIMaterialWaepon uiMate = temporaryList[i];
                    ShowItem(i, uiMate.ImageKhung, uiMate.renderTexture);
                }
            }
        }
    }
    private void LoadIMWaepon(WaeponType weaponType)
    {
        int index = (int)weaponType; // Chuyển đổi WaeponType thành int
        if (!listGunData[(int)currentWaeponType].isBoght)
        {
            rawImShoWaepon.texture = materialWaeponSO.listmateriaWaepon[index].sprite.texture;
        }
        else
        {
            rawImShoWaepon.texture = materialWaeponSO.listmateriaWaepon[index].listmaterials[currenState].textures;
        }
        UpdateBT();
    }
    //phan chon costom
    private void SpamCosTom()
    {
        if (currenState == 0 && listGunData[(int)currentWaeponType].isBoght)
        {
            uIMaterialSelections.gameObject.SetActive(true);
            uIMaterialSelections.LoadIMMaterial();
        }
        else
        {
            uIMaterialSelections.gameObject.SetActive(false);
        }
    }
    //mỗi khi load
    private void SetMaterialCostom()
    {
        if (listGunData[(int)currentWaeponType].isBoght)
        {
            UIMaterialWaepon uIMaterial = temporaryList[0];
            MeshRenderer meshRenderer = uIMaterial.obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                Material[] materials = meshRenderer.sharedMaterials;
                materials = DataManager.Instance.useData.colorList[(int)currentWaeponType].materials;
                meshRenderer.sharedMaterials = materials;
            }
        }
    }
    //mỗi khi click
    public void SetMaterial(int indexMate, Material material)
    {
        //đang mac && mo khoa
        if (currenState == 0 && listGunData[(int)currentWaeponType].isEnppui[currenState])
        {
            // Đoạn mã để ép kiểu mảng thành danh sách
            List<Material> materialList = DataManager.Instance.useData.colorList[(int)currentWaeponType].materials.ToList();
            Player.instance.ChangWaepon(currentWaeponType
         , materialList);
        }
        //lấy waepon đau tien cua moi weapon
        UIMaterialWaepon uIMaterial = temporaryList[0];
        MeshRenderer meshRenderer = uIMaterial.obj.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Material[] materials = meshRenderer.sharedMaterials;
            materials[indexMate] = material;
            meshRenderer.sharedMaterials = materials;
        }

    }
}
