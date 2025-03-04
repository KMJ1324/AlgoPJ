using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public float horizontal { get; private set; }
    public float vertical { get; private set; }

    public bool W { get; private set; }
    public bool A { get; private set; }
    public bool S { get; private set; }
    public bool D { get; private set; }

    public bool EventKeyDown { get; private set; }
    public bool isMoving { get; private set; }
    public bool ESC { get; private set; }

    public bool dialogSkipKey { get; set; }
    public bool bindkey { get; set; }
    public bool minigameBindkey { get; set; }


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("InputManager가 여러개 생성되었습니다");
        }
        instance = this;
    }

    private void Update()
    {
        if (GameManager.instance.inGame)
        {
            ESC = Input.GetKeyDown(KeyCode.Escape); // esc는 언제 어디서나 작동 되야하기에 bindKey에 영향 받지 않는다

            if(!minigameBindkey)
            {
                W = Input.GetKeyDown(KeyCode.W); // WASD는 미니게임시 사용함
                A = Input.GetKeyDown(KeyCode.A);
                S = Input.GetKeyDown(KeyCode.S);
                D = Input.GetKeyDown(KeyCode.D);
                EventKeyDown = Input.GetKeyDown(KeyCode.Space); 
            }
            else
            {
                W = false; // WASD는 미니게임시 사용함
                A = false;
                S = false;
                D = false;
                EventKeyDown = false;
            }
        }

        if (bindkey)
        {
            isMoving = false;
            horizontal = 0;
            vertical = 0;
            dialogSkipKey = dialogSkipKey == true ? true : Input.anyKeyDown;
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        isMoving = horizontal != 0 || vertical != 0;
        dialogSkipKey = false;
    }

    public void BindingKey()
    {
        bindkey = true;
    }

    public void DeBindingKey()
    {
        bindkey = false;
    }

    public void BindingMinigameKey()
    {
        minigameBindkey = true;
    }
    public void DeBindingMinigameKey()
    {
        minigameBindkey = false;
    }
}
