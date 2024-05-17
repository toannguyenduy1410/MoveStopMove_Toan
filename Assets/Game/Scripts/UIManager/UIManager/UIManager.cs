using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<System.Type, UICanvas> canvasActives = new Dictionary<System.Type, UICanvas>();
    Dictionary<System.Type, UICanvas> canvasPrifab = new Dictionary<System.Type, UICanvas>();
    [SerializeField] private Transform parent;
    private void Awake()
    {
        UICanvas[] prifabs = Resources.LoadAll<UICanvas>("UI/");
        for (int i = 0; i < prifabs.Length; i++)
        {
            canvasPrifab.Add(prifabs[i].GetType(), prifabs[i]);//vì khai báoSystem.Type nên phải getType
        }
    }
    
    //mở canvas
    public T OpenUI<T>() where T : UICanvas//where T : UICanvas(T phải là lớp con của UICanvas)
    {
        T canvas = GetUI<T>();//lấy canvass

        canvas.SetUp();//trước khi active
        canvas.Open();//sau khi đã active
        return canvas;
    }   
    //đóng canvas sau time(s)
    public void CloseUI<T>(float time) where T : UICanvas
    {
        if (IsOpened<T>())
        {
            //mk truyền vào khóa đó sẽ chưa giá trị tương ứng mà không cần duyệt tất cả
            canvasActives[typeof(T)].Close(time);
        }
    }
    //đóng canvass truc tiep
    public void CloseUIDirectly<T>() where T : UICanvas
    {
        if (IsOpened<T>())
        {
            canvasActives[typeof(T)].CloseDirectly();
        }
       
    }
    //kiem tra canvas dc tạo chua
    public bool IsLoaded<T>() where T : UICanvas
    {
        //kiem tra canvas a duoc tao chua && khi có rồi cx phải != null
        return canvasActives.ContainsKey(typeof(T)) && canvasActives[typeof(T)] != null;
    }
    //kiem tra canvas duoc active chưa 
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && canvasActives[typeof(T)].gameObject.activeSelf;
    }
    //lấy active canvas
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())//nếu chưa tìm thấy sẽ tạo mới
        {
            T prifab = GetUIPrifab<T>();//lấy dc đó là prifab nào
            T canvas = Instantiate(prifab,parent);
            canvasActives[typeof(T)] = canvas;//lưu vào key tương ứng giá trị
        }
        return canvasActives[typeof(T)] as T;
        //chuyển đổi từ kiểu System.Type thành kiểu T vì key là System.Type
        //vì nó đang mang giá trị là Gameobj( canvasActives[typeof(T)] = canvas )
    }
    //get prifab
    private T GetUIPrifab<T>() where T : UICanvas
    {

        return canvasPrifab[typeof(T)] as T;
    }


    //dong tat ca
    public void CloseUIAll()
    {
        foreach(var canvas in canvasActives)
        {
            if(canvas.Value != null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }
}

