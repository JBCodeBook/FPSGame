using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    MouseHandler mouseHandler;

    public void Awake()
    {
        mouseHandler = FindObjectOfType<MouseHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseHandler.lockCursor();
    }


}
