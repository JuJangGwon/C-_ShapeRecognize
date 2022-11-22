using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Result
{
    leftup = 0,
    up,
    rightup,
    right,
    rightdown,
    down,
    leftdown,
    left,
    triangle1,
    triangle2,
    Circle,
    Square,
    Z,
    none
}

public class TouchSignRecog : MonoBehaviour
{

    enum Direction
    {
        leftup = 1,
        up,
        rightup,
        right,
        rightdown,
        down,
        leftdown,
        left
    }

    Result Gesture_Result = Result.none;

    float[] degree = new float[50];          // 각도 저장하는 배열

    int[] Vals = new int[250];               // 방향 저장 배열
    int index = 0;                          // 변곡점 갯수

    Direction Dir_88 = 0;                         // 8 방향
    Vector2 StartPoint;
    Vector2 LastPoint;
    Vector2 Point;

    public Camera MainCamera;

    void Awake()
    {
        clear();
    }
    void clear()
    {
        Gesture_Result = Result.none;
        Dir_88 = 0;
        index = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            StartPoint = Point;
        }
        if (Input.GetMouseButton(0))
        {
            if (!(Input.mousePosition.x - Point.x > -25 && Input.mousePosition.x - Point.x < 25) || !(Input.mousePosition.y - Point.y > -25 && Input.mousePosition.y - Point.y < 25)) // 사용자의 미세함 떨림으로 인한 오차 범위 ㅇㅇ
            {
                DegreeCheck();
                Point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            LastPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            GestureRecognize();
            clear();
        }
    }
    void DegreeCheck()                  // 8 방향 체크
    {
        Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - Point;
        float Atan2s = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 180;

        if (Atan2s <= 315 - 22.5f && Atan2s > 270 - 22.5f) Dir_88 = Direction.leftup;
        else if (Atan2s <= 270 - 22.5f && Atan2s > 225 - 22.5f) Dir_88 = Direction.up;
        else if (Atan2s <= 225 - 22.5f && Atan2s > 180 - 22.5f) Dir_88 = Direction.rightup;
        else if (Atan2s < 180 - 22.5f && Atan2s > 135 - 22.5f) Dir_88 = Direction.right;
        else if (Atan2s <= 135 - 22.5f && Atan2s > 90 - 22.5f) Dir_88 = Direction.rightdown;
        else if (Atan2s <= 90 - 22.5f && Atan2s > 45 - 22.5f) Dir_88 = Direction.down;
        else if ((Atan2s >= 0 && Atan2s <= 22.5f) || (Atan2s > 337.5f && Atan2s < 360)) Dir_88 = Direction.leftdown;
        else if (Atan2s <= 337.5f && Atan2s > 315 - 22.5f) Dir_88 = Direction.left;
        if (Vals[index] != (int)Dir_88)
        {
            if (Dir_88 != 0)
            {
                index++;
                Vals[index] = (int)Dir_88;
                degree[index] = Atan2s;
            }
        }

    }
    void StraightLine_Check()
    {
        if (index == 2)
        {
            Debug.Log(Vals[1] - Vals[2]);
            if (Vals[0] - Vals[1] == 1 || Vals[0] - Vals[1] == -1)
            {
                if (degree[0] - degree[1] > -30) index = 1;    
                else if (degree[0] - degree[1] < 30) index = 1;  
            }
        }

        if (index == 1)
        {

            switch (Vals[1])
            {
                case 1:
                    Debug.Log("위로 긋는선");
                    Gesture_Result = Result.up;
                    break;
                case 2:
                    Debug.Log("오른쪽 위로 긋는 대각선");
                    Gesture_Result = Result.rightup;
                    break;
                case 3:
                    Debug.Log("오른쪽으로 긋는선");
                    Gesture_Result = Result.right;
                    break;
                case 4:
                    Debug.Log("오른쪽 아래로 긋는 대각선");
                    Gesture_Result = Result.rightdown;
                    break;
                case 5:
                    Debug.Log("아래로 긋는선");
                    Gesture_Result = Result.down;
                    break;
                case 6:
                    Debug.Log("왼쪽 아래로 긋는 대각선");
                    Gesture_Result = Result.leftdown;
                    break;
                case 7:
                    Debug.Log("왼쪽으로 긋는선");
                    Gesture_Result = Result.left;
                    break;
                case 8:
                    Debug.Log("왼쪽 위로 긋는 대각선");
                    Gesture_Result = Result.leftup;
                    break;
            }
        }

    }
    void Circle_Check()
    {
        if (index > 6 && index < 10)
        {
            if (Vals[1] == 8) if (Vals[2] == 1) if (Vals[3] == 2) if (Vals[4] == 3) if (Vals[5] == 4) if (Vals[6] == 5) if (Vals[7] == 6) { Debug.Log("Circle"); Gesture_Result = Result.Circle; }
            if (Vals[1] == 7) if (Vals[2] == 8) if (Vals[3] == 1) if (Vals[4] == 2) if (Vals[5] == 3) if (Vals[6] == 4) if (Vals[7] == 5) if (Vals[8] == 6) { Debug.Log("Circle"); Gesture_Result = Result.Circle; }
        }
    }
    void Z_Check()
    {
        if (index < 5)
        {
            if (Vals[1] == 3)
                if (Vals[2] == 5 || Vals[2] == 6)
                {
                    if (Vals[3] == 6 || Vals[3] == 5) if (Vals[4] == 3) Debug.Log("Z"); Gesture_Result = Result.Z;
                    if (Vals[3] == 3) Debug.Log("Z"); Gesture_Result = Result.Z;
                }
        }
    }
    void Triangle_Check()
    {
        if (index <= 5)
        {
            if (Vals[1] == 6)  // 삼각형 인식 
            {
                for (int i = 2; i <= index - 1; i++)
                    if (Vals[i] == 3)
                    {
                        if (Vals[index] == 8)
                        {
                            Debug.Log("삼각형");
                            Gesture_Result = Result.triangle1;
                        }
                    }
            }
           else if (Vals[1] == 3)
            {
                for (int i = 2; i <= 4; i++)
                    if (Vals[i] == 6)
                    {
                        if (Vals[index] == 8)
                        {
                            Debug.Log("역 삼각형 1");
                            Gesture_Result = Result.triangle2;
                        }
                        else if (Vals[index] == 1)
                        {
                            Debug.Log("역 삼각형 1");
                            Gesture_Result = Result.triangle2;
                        }
                    }
            }
            else if (Vals[1] == 7)
            {
                for (int i = 2; i <= 4; i++)
                {
                    if (Vals[i] == 4)
                    {
                        if (Vals[index] == 2)
                        {
                            Debug.Log("역 삼각형 2");
                            Gesture_Result = Result.triangle2;
                        }
                        else if (Vals[index] == 1)
                        {
                            Debug.Log("역 삼각형 2");
                            Gesture_Result = Result.triangle2;
                        }
                    }
                }
            }
        }
    }

    void GestureRecognize()
    {
        Triangle_Check();
        StraightLine_Check();
        Circle_Check();
        Z_Check();

    }
}
