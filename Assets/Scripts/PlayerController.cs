using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
    [BoxGroup("Animator")] public Animator animator;
    [BoxGroup("Player Properties")] public float speed;

    [BoxGroup("Physics")] [SerializeField] private float flipImpulseForce = 5f;

    [BoxGroup("Animator Parameters")] public string runParameter;

    [BoxGroup("Field of View Properties")] public float startFOW = 60;
    [BoxGroup("Field of View Properties")] public float endFOW = 80;

    private Transform tr;
    private Rigidbody rb;
    private Coroutines coroutine;
    private bool isMouseDown = false;
    private float swipeSensitivity = 2f;
    private Vector2 lastTouchPos;
    private Vector2 swipeDelta;
    private Vector3 mousePos;

    private void Awake()
    {
        GlobalManager.Player = this;
        tr = this.transform;
        rb = GetComponent<Rigidbody>();
        coroutine = GetComponent<Coroutines>();
    }

    void Update()
    {
        if (GlobalManager.GameManager.clickable)
        {
            InputLogic();
        }
    }

    public void SpawnPlayer()
    {
        this.gameObject.SetActive(true);
        animator.Rebind();
        rb.velocity = Vector3.zero;

        int random = Random.Range(0, GlobalManager.LevelManager.platforms.Count - 1);
        tr.position = GlobalManager.LevelManager.platforms[random].transform.position;
    }

    public void KillPlayer()
    {
        this.gameObject.SetActive(false);
        rb.velocity = Vector3.zero;
    }

    #region Player Movement
    private void InputLogic()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            lastTouchPos = Input.mousePosition;
            StartCoroutine(coroutine.LerpFieldOfView(Camera.main.fieldOfView, endFOW, 0.5f));
        }
        else if (Input.GetMouseButtonUp(0) && isMouseDown)
        {
            TouchEnded();
        }

        if (isMouseDown)
        {
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
            swipeDelta = (Vector2)Input.mousePosition - lastTouchPos;
            if (swipeDelta.magnitude > swipeSensitivity)
            {
                tr.LookAt(tr.position + new Vector3(swipeDelta.x, tr.position.y, swipeDelta.y));
                rb.velocity = tr.forward * speed * Time.deltaTime / tr.localScale.x;              
            }          
        }
        return;
#endif
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!isMouseDown)
            {
                TouchStart(touch);
            }
            else
            {
                TouchMoved(touch);
            }
        }
        else if (isMouseDown)
        {
            TouchEnded();
        }
    }

    private void TouchStart(Touch touch)
    {
        isMouseDown = true;
        lastTouchPos = touch.position;
        StartCoroutine(coroutine.LerpFieldOfView(Camera.main.fieldOfView, endFOW, 0.5f));
    }

    private void TouchMoved(Touch touch)
    {
        swipeDelta = touch.position - lastTouchPos;
        if (swipeDelta.magnitude > swipeSensitivity)
        {            
            tr.LookAt(tr.position + new Vector3(swipeDelta.x, tr.position.y, swipeDelta.y));
            rb.velocity = tr.forward * speed * Time.deltaTime / tr.localScale.x;
        }      
    }

    private void TouchEnded()
    {
        isMouseDown = false;
        rb.velocity = Vector3.zero;
        StartCoroutine(coroutine.LerpFieldOfView(Camera.main.fieldOfView, startFOW, 0.5f));
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
        }
    }
}
