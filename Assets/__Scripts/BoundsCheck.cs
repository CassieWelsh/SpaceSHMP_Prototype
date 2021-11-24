using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Предотвращает выход игрового объекта за границы экрана.
/// Важно: работает ТОЛЬКО с ортографической камерой Main Camera в [0, 0, 0].
/// </summary>

public class BoundsCheck : MonoBehaviour 
{
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true; 

    [Header("Set Dynamically")]
    public bool isOnScreen = true; 
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown; //a

    void Awake()
    {
        camHeight = Camera.main.orthographicSize; 
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate() 
    {
        Vector3 pos = transform.position; 
        isOnScreen = true; 
        offRight = offLeft = offUp = offDown = false; //b

        if (pos.x > camWidth - radius) 
        {
            pos.x = camWidth - radius;
            offRight = true; //c
        }

        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }

        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
        }

        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown); //d
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;    
            isOnScreen = true;    
            offRight = offLeft = offUp = offDown = false; //e
        }
    }

    // Рисует границы в панели Scene (Сцена) с помощью OnDrawGizmos()
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, .1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
