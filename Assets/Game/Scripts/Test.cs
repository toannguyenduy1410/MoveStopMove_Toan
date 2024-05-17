using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        //Vector2 point = new Vector2(0, 0);
        //Vector2 a = new Vector2(0, 2);
        //Vector2 b = new Vector2(2, 0);
        //bool checkboll = CheckPoint(point, a, b);
        //Debug.Log(checkboll);
        //Test1();
        //Test2();
        Test5(10);
    }
   
    public void Test1()
    {
        int[] arr = new int[10] { 1, 5, 3, 7, 6, 8, 4, 8, 1, 9 };
        int max = int.MinValue;
        int min = int.MaxValue;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] > max && arr[i] % 2 == 0)
            {
                max = arr[i]; // Cập nhật số lớn nhất
            }
            if (arr[i] < min && arr[i] % 2 != 0)
            {
                min = arr[i]; // Cập nhật số lớn nhất
            }
        }
        Debug.Log(min);
        Debug.Log(max);
    }
    public void Test2()
    {
        string s = "";
        List<int> list = new List<int>() { 1, 2, 3, 4, 5, 4, 4, 6, 8, 9, 10 };
        list.RemoveAll(x => x == 4);
        for (int i = 0; i < list.Count; i++)
        {
            s += list[i] + " ";
        }
        Debug.Log(s);
    }
    public bool Test4(Vector2 point, Vector2 a, Vector2 b)
    {
        Vector2 vectorAP = point - a;
        Vector2 vectorAB = b - a;

        float crossProduct = vectorAP.x * vectorAB.y - vectorAP.y * vectorAB.x;
        return Mathf.Approximately(crossProduct, 0f);
    }
    public void Test5(int count)
    {             
        List<int> list = new List<int>();
        if (count % 2 == 0)
        {

            int count2 = count / 2;
            Debug.Log(count2);
            for (int i = 1; i <= count2; i++)
            {
                list.Add(i);
                list.Add(i);
            }

            // Trộn ngẫu nhiên danh sách
            System.Random rand = new System.Random();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                int randIndex = rand.Next(0, i + 1);
                int temp = list[i];
                list[i] = list[randIndex];
                list[randIndex] = temp;
            }

            // Tạo chuỗi chứa các số trong danh sách đã trộn
            string s = string.Join(" ", list);
            Debug.Log(s);
        }
    }
}
