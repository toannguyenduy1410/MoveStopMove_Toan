using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private bool isdestroyOnClose = false;

    //xử lí tai thỏ
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float radio = (float)Screen.width / (float)Screen.height;
        if(radio > 2.1f)
        {
            Vector2 leftBottom = rectTransform.offsetMin;
            Vector2 righBottom = rectTransform.offsetMax;

            leftBottom.y = 0;
            righBottom.y = -100;

            rectTransform.offsetMin = leftBottom;
            rectTransform.offsetMax = righBottom;
        }
    }
    //gọi trước khi được active
    public virtual void SetUp()
    {

    }
    //gọi sau khi được active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    //tắt canvas sau time(s)
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }
    //tắt trực tiếp
    public virtual void CloseDirectly()
    {
        if (isdestroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }        
    }
}
