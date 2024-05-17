using System;
using System.Collections;
using System.Collections.Generic;

using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class UIMaterialSelections : MonoBehaviour
{
    [SerializeField] private UIButtonMaterial uIButtonMaterialPrifab;
    [SerializeField] private UIImageMaterial uIImageMaterialPrifab;
    [SerializeField] private Transform parentBT;
    [SerializeField] private Transform parentIM;
    [SerializeField] private ListMaterialSO listMaterialSO;
    [SerializeField] private MaterialWaeponSO materialWaeponSO;
    [SerializeField] private UIWeaponSelection uIWeaponSelection;
    private List<UIImageMaterial> tempIMaterial = new List<UIImageMaterial>();
    private List<ColorWaepon> ColorWp = new List<ColorWaepon>();
    private Image currentActiveImage;
    private int curentIndex;
    private WaeponType curenWP;
    private void Awake()
    {
        //DataManager.Instance.Load();
        //ColorWp = DataManager.Instance.useData.colorList;
        // SetData();
    }
    void Start()
    {
        LoadBTMaterial();
    }
    public void SetData()
    {
        DataManager.Instance.Load();
        ColorWp = DataManager.Instance.useData.colorList;
        
        foreach (WaeponType gunType in Enum.GetValues(typeof(WaeponType)))
        {            
            if ((int)gunType >= ColorWp.Count)
            {
                int colorCount = materialWaeponSO.listmateriaWaepon[(int)gunType].AmountMaterial;//so luong itemMate

                ColorWaepon weaponData = new ColorWaepon();
                weaponData.colors = new Color32[colorCount];
                weaponData.materials = new Material[colorCount];
                // Mặc định mọi màu súng đều bị khóa khi bắt đầu
                for (int i = 0; i < colorCount; i++)
                {
                    int randomColor = UnityEngine.Random.Range(0, listMaterialSO.listmaterials.Count);
                    Color color = listMaterialSO.listmaterials[randomColor].ColorMaterial;
                    Material material = listMaterialSO.listmaterials[randomColor].Material;
                    weaponData.colors[i] = color;
                    weaponData.materials[i] = material;
                }
                ColorWp.Add(weaponData);
            }
        }
        // Lưu dữ liệu vào DataManager
        DataManager.Instance.useData.colorList = ColorWp;
        DataManager.Instance.Save();
    }
    private void SpawnBTMaterial(Color32 ColorMaterial, Material material)
    {
        UIButtonMaterial uIButtonMate = Instantiate(uIButtonMaterialPrifab, parentBT);
        uIButtonMate.OnInit(ColorMaterial, material, HandelClickBT);
    }
    private void HandelClickBT(Material material, Color32 color32)
    {
        //truyen color        
        UIImageMaterial uIImageMaterial = tempIMaterial[curentIndex];
        uIImageMaterial.SetColor(color32);
        ColorWp[(int)curenWP].colors[curentIndex] = color32;//save color
        ColorWp[(int)curenWP].materials[curentIndex] = material;//save Material
        DataManager.Instance.useData.colorList = ColorWp;
        DataManager.Instance.Save();
        //truyen material
        uIWeaponSelection.SetMaterial(curentIndex, material);
    }
    private void SpawnIMMaterial(string index, Color32 ColorMaterial)
    {
        UIImageMaterial uIImageMate = Instantiate(uIImageMaterialPrifab, parentIM);
        uIImageMate.OnInit(index, ColorMaterial, HandelClickIM);
        tempIMaterial.Add(uIImageMate);
    }
    private void HandelClickIM(int index, Image imageKH)
    {
        curentIndex = index;
        //show CurrenIM
        ShowCurrenIM(imageKH);
    }
    private void ShowCurrenIM(Image imageKH)
    {
        if (imageKH != null)
        {
            if (currentActiveImage != null)
            {
                currentActiveImage.gameObject.SetActive(false);
            }
            currentActiveImage = imageKH;
            imageKH.gameObject.SetActive(true);
        }
    }
    public void LoadBTMaterial()
    {
        foreach (var material in listMaterialSO.listmaterials)
        {
            SpawnBTMaterial(material.ColorMaterial, material.Material);
        }
    }
    public void LoadIMMaterial()
    {
        curentIndex = 0;//về 0 mỗi khi load lên
        curenWP = uIWeaponSelection.currentWaeponType;
        // Xóa tất cả trong danh sách tạm trước khi tạo mới
        foreach (var item in tempIMaterial)
        {
            Destroy(item.gameObject);
        }
        tempIMaterial.Clear();
        for (int i = 0; i < materialWaeponSO.listmateriaWaepon[(int)curenWP].AmountMaterial; i++)
        {
            SpawnIMMaterial(i.ToString(), ColorWp[(int)curenWP].colors[i]);
        }
        //show mỗi khi load lên
        UIImageMaterial uIImageMaterial = tempIMaterial[curentIndex];
        ShowCurrenIM(uIImageMaterial.imMaterialKh);
    }
}
