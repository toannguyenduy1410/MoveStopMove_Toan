using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelection : MonoBehaviour
{
    [SerializeField] private SkinSO skinSO;
    [SerializeField] private SkinPageSkinSO skinPageSkinSO;
    [SerializeField] private Transform parentPageSkin;
    [SerializeField] private Transform parentSkin;
    [SerializeField] private UIPageSkin uIPageSkin;
    [SerializeField] private UISkin uISkin;
    [SerializeField] private Button btSelect;
    [SerializeField] private Button btCoint;
    [SerializeField] private Button btEnpui;
    [SerializeField] private Button btUnlock;
    [SerializeField] private TextMeshProUGUI txtCoint;
    private List<UIPageSkin> listUIPageSkin = new List<UIPageSkin>();
    private List<UISkin> listUISkin = new List<UISkin>();
    private List<Skin> listSkin = new List<Skin>();
    private SkinData skinData;
    private UISkin curentUISkin;
    private Image curentImGRPage;
    private PageSkinType curentPage;
    private int curentSkin;
    private void Awake()
    {
        SetDataSkin();
    }
    private void OnEnable()
    {
        OnInit();
    }
    private void OnDisable()
    {
        if (Player.instance != null)
        {
           
        }
    }
    void Start()
    {
        btSelect.onClick.AddListener(OnSelectButtonClick);
        btCoint.onClick.AddListener(OnCointtButtonClick);
        btUnlock.onClick.AddListener(OnUnLockButtonClick);
        btEnpui.onClick.AddListener(OnEnquiButtonClick);
    }
    private void OnInit()
    {
        //DataManager.Instance.LoadSkin();
        //listSkin = DataManager.Instance.skinData.listSkinData;

        curentPage = DataManager.Instance.skinData.DataCurrentPageSkin;
        curentSkin = DataManager.Instance.skinData.dataCurrentEnppui;
        LoadPageSkin();

        UIPageSkin uIPageSk = listUIPageSkin[(int)curentPage];
        ShowPage((int)curentPage, uIPageSk.imageGR);

        curentUISkin = listUISkin[curentSkin];
        ShowSkin(curentUISkin);
    }
    private void OnSelectButtonClick()
    {
        //tat het roi bat
        for (int i = 0; i < listUISkin.Count; i++)
        {
            listUISkin[i].SetIsEnppui(false);
        }
        curentUISkin.SetIsEnppui(true);
        
        SetEnppui();
        UPdateUIButton();
    }
    private void OnCointtButtonClick()
    {       
        int coint = GetCointCountByType(curentPage);
        if (Coint.Instance.Coints > coint)
        {
            //mo locked
            listSkin[(int)curentPage].isLocked[curentSkin] = false;
            //tat het xong r bat
            for (int i = 0; i < listUISkin.Count; i++)
            {
                listUISkin[i].SetIsEnppui(false);
            }
            curentUISkin.SetIsEnppui(true);
            curentUISkin.SetIsLocket(false);           
            SetEnppui();
            UPdateUIButton();
            Coint.Instance.DesCoint(coint);
            Coint.Instance.LoadCoint();
        }
    }
    private void OnUnLockButtonClick()
    {
        //mo locked
        listSkin[(int)curentPage].isLocked[curentSkin] = false;
        //tat het xong r bat
        for (int i = 0; i < listUISkin.Count; i++)
        {
            listUISkin[i].SetIsEnppui(false);
        }
        curentUISkin.SetIsEnppui(true);
        curentUISkin.SetIsLocket(false);       
        SetEnppui();
        UPdateUIButton();
    }
    //bt Enqqui
    private void OnEnquiButtonClick()
    {        
        //lấy ra item nao dang mac
        PageSkinType dataCurentPage = DataManager.Instance.skinData.DataCurrentPageSkin;
        int dataCurrenSkin = DataManager.Instance.skinData.dataCurrentEnppui;

        if ((int)dataCurentPage >= 0 && (int)dataCurentPage < listSkin.Count)
        {
            //set thang cũ dang mac thanh fase
            listSkin[(int)dataCurentPage].isEnppui[dataCurrenSkin] = false;                                
        }
        skinData.isEnppui = false;
        //lu data
        DataManager.Instance.skinData.listSkinData = listSkin;        
        DataManager.Instance.skinData = skinData;        
        DataManager.Instance.SaveSkin();

        //tat het 
        for (int i = 0; i < listUISkin.Count; i++)
        {
            listUISkin[i].SetIsEnppui(false);
        }
        UPdateUIButton();
    }
    private void UPdateUIButton()
    {
        btSelect.gameObject.SetActive(false);
        btCoint.gameObject.SetActive(false);
        btUnlock.gameObject.SetActive(false);
        btEnpui.gameObject.SetActive(false);
        //neu da chưa mo khoa
        if (listSkin[(int)curentPage].isLocked[curentSkin])
        {
            btCoint.gameObject.SetActive(true);
            btUnlock.gameObject.SetActive(true);
            //update coint
            int coint = GetCointCountByType(curentPage);
            txtCoint.text = coint.ToString();
        }
        else
        {    //dang mac
            if (listSkin[(int)curentPage].isEnppui[curentSkin])
            {
                btEnpui.gameObject.SetActive(true);
            }
            else
            {
                btSelect.gameObject.SetActive(true);
            }

        }
    }
    private void SetEnppui()
    {
        //lấy ra item nao dang mac
        PageSkinType dataCurentPage = DataManager.Instance.skinData.DataCurrentPageSkin;
        int dataCurrenSkin = DataManager.Instance.skinData.dataCurrentEnppui;

        if ((int)dataCurentPage >= 0 && (int)dataCurentPage < listSkin.Count)
        {
            //set thang cũ dang mac thanh fase
            listSkin[(int)dataCurentPage].isEnppui[dataCurrenSkin] = false;
            //set thang dang mac thanh true
            listSkin[(int)curentPage].isEnppui[curentSkin] = true;
        }
        skinData.isEnppui = true;      
        //lu data       
        DataManager.Instance.skinData = skinData;
        DataManager.Instance.skinData.listSkinData = listSkin;
        DataManager.Instance.skinData.DataCurrentPageSkin = curentPage;
        DataManager.Instance.skinData.dataCurrentEnppui = curentSkin;
        DataManager.Instance.SaveSkin();
    }
    
    private void SetDataSkin()
    {

        //Load dữ liệu skin từ DataManager
        DataManager.Instance.LoadSkin();
        skinData = DataManager.Instance.skinData;
        listSkin = DataManager.Instance.skinData.listSkinData;
       
        // Duyệt qua từng loại skin
        foreach (PageSkinType pageSkinType in Enum.GetValues(typeof(PageSkinType)))
        {
            int skinSOCount = GetSkinCountByType(pageSkinType); // Số lượng item cho từng loại skin            
            // Kiểm tra xem có skin mới không            

            if ((int)pageSkinType >= listSkin.Count)
            {
                Skin newSkinData = new Skin();
                // Thêm skin mới vào listSkin nếu có
                for (int i = 0; i < skinSOCount; i++)
                {
                    newSkinData.isLocked.Add(true);
                    newSkinData.isEnppui.Add(false);
                    // Thêm skin mới vào danh sách listSkin
                    listSkin.Add(newSkinData);
                }
            }
            else
            {
                //locket
                int slIsLocked = listSkin[(int)pageSkinType].isLocked.Count;
                int slIsLockedMissing = skinSOCount - slIsLocked;
                for (int i = 0; i < slIsLockedMissing; i++)
                {
                    listSkin[(int)pageSkinType].isLocked.Add(true);
                   
                }
                //Enppui
                int slIsEnppui = listSkin[(int)pageSkinType].isEnppui.Count;
                int slIsEnppuiMissing = skinSOCount - slIsEnppui;
                for (int i = 0; i < slIsEnppuiMissing; i++)
                {                  
                    listSkin[(int)pageSkinType].isEnppui.Add(false);
                }
            }
        }

        //// Lưu trạng thái skin vào DataManager

        DataManager.Instance.skinData.listSkinData = listSkin;
        DataManager.Instance.SaveSkin();
    }

    private void SpawnPageSkin(string index, Sprite sprite)
    {
        UIPageSkin PageskinPrp = Instantiate(uIPageSkin, parentPageSkin);
        PageskinPrp.OnInit(index, sprite, HandelClickPage);
        listUIPageSkin.Add(PageskinPrp);
    }
    private void HandelClickPage(int index, Image imageGR)
    {
        curentPage = (PageSkinType)index;
        LoadSkin((int)curentPage);
        ShowPage(index, imageGR);

        //neu dang la pageskin dang luu
        if (curentPage == DataManager.Instance.skinData.DataCurrentPageSkin)
        {
            curentSkin = DataManager.Instance.skinData.dataCurrentEnppui;
            UISkin skinprf = listUISkin[curentSkin];
            ShowSkin(skinprf);
        }
        else //set thằng đầu tiên và dang mac
        {
            UISkin skinprf = listUISkin[0];
            curentSkin = 0;
            ShowSkin(skinprf);
        }
    }
    private void ShowPage(int index, Image imageGR)
    {
        if (curentImGRPage != null)
        {
            curentImGRPage.gameObject.SetActive(true);
        }
        curentImGRPage = imageGR;
        curentImGRPage.gameObject.SetActive(false);
        LoadSkin((int)curentPage);
    }
    private void SpawnSkin(string index, Sprite sprite)
    {
        UISkin skinPrp = Instantiate(uISkin, parentSkin);
        skinPrp.OnInit(index, sprite, HandelClickskin);
        //lay trang thai locked
        bool islocket = listSkin[(int)curentPage].isLocked[int.Parse(index)];
        bool isEnqui = listSkin[(int)curentPage].isEnppui[int.Parse(index)];
        skinPrp.SetIsLocket(islocket);
        skinPrp.SetIsEnppui(isEnqui);
        listUISkin.Add(skinPrp);
    }
    private void HandelClickskin(int index)
    {
        curentSkin = index;
        ShowSkin(listUISkin[index]);
    }

    private void ShowSkin(UISkin uISkin)
    {
        // Ẩn skin hiện tại (nếu có)
        if (curentUISkin != null)
        {
            curentUISkin.SetIsBprders(false);
        }

        // Hiển thị skin mới
        if (uISkin != null)
        {
            curentUISkin = uISkin;
            curentUISkin.SetIsBprders(true);

            // Cập nhật dữ liệu skin và nút UI khác nếu cần
            SetSkin(curentSkin);
            UPdateUIButton();
        }
    }
    private void LoadPageSkin()
    {
        for (int i = 0; i < skinPageSkinSO.sprites.Count; i++)
        {
            SpawnPageSkin(i.ToString(), skinPageSkinSO.sprites[i]);
        }
    }
    private void LoadSkin(int currenPage)
    {
        foreach (var item in listUISkin)
        {
            Destroy(item.gameObject);
        }
        listUISkin.Clear();
        if (currenPage == (int)PageSkinType.SkinHair)
        {
            for (int i = 0; i < skinSO.listSkinHair.Count; i++)
            {
                SpawnSkin(i.ToString(), skinSO.listSkinHair[i].sprite);
            }
        }
        else if (currenPage == (int)PageSkinType.SkinPant)
        {
            for (int i = 0; i < skinSO.listSkinPant.Count; i++)
            {
                SpawnSkin(i.ToString(), skinSO.listSkinPant[i].sprite);
            }
        }
        else if (currenPage == (int)PageSkinType.SkinShield)
        {
            for (int i = 0; i < skinSO.listSkinShield.Count; i++)
            {
                SpawnSkin(i.ToString(), skinSO.listSkinShield[i].sprite);
            }
        }
        else if (currenPage == (int)PageSkinType.SkinSet)
        {
            for (int i = 0; i < skinSO.listSkinSet.Count; i++)
            {
                SpawnSkin(i.ToString(), skinSO.listSkinSet[i].sprite);
            }
        }
    }
    private void SetSkin(int index)
    {
        if (Player.instance != null)
        {
            if (curentPage == PageSkinType.SkinHair)
            {
                Player.instance.ChangHair(skinSO.listSkinHair[index].listObjSkin[0].objSkin
                    , skinSO.listSkinHair[index].listObjSkin[0].parent
                    , skinSO.listSkinHair[index].matPlayer);
            }
            else if (curentPage == PageSkinType.SkinPant)
            {
                Player.instance.Changpants(skinSO.listSkinPant[index].material
                    , skinSO.listSkinPant[index].matPlayer);
            }
            else if (curentPage == PageSkinType.SkinShield)
            {
                Player.instance.ChangShield(skinSO.listSkinShield[index].objSkin.objSkin
                    , skinSO.listSkinShield[index].objSkin.parent
                    , skinSO.listSkinShield[index].matPlayer);
            }
            else if (curentPage == PageSkinType.SkinSet)
            {
                Player.instance.ChangSetSkin(skinSO.listSkinSet[index].listObjSkin
                    , skinSO.listSkinSet[index].material);
            }
        }
    }
    // Phương thức này trả về số lượng Skin dựa trên loại Skin
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
    //lấy số lượng coint
    private int GetCointCountByType(PageSkinType skinType)
    {
        switch (skinType)
        {
            case PageSkinType.SkinHair:
                return skinSO.listSkinHair[curentSkin].coint;
            case PageSkinType.SkinPant:
                return skinSO.listSkinPant[curentSkin].coint;
            case PageSkinType.SkinShield:
                return skinSO.listSkinShield[curentSkin].coint;
            case PageSkinType.SkinSet:
                return skinSO.listSkinSet[curentSkin].coint;
            default:
                return 0;
        }
    }
}
