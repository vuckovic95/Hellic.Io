using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
    #region Public Properties
    [BoxGroup("Animator")] public Animator animator;
    [BoxGroup("Player Properties")] public float speed;
    [BoxGroup("Physics")] [SerializeField] private float flipImpulseForce = 5f;
    [BoxGroup("Mesh")] public GameObject mesh;
    [BoxGroup("Field of View Properties")] public float startFOW = 60;
    [BoxGroup("Field of View Properties")] public float endFOW = 80;
    [BoxGroup("State")] public State state;
    [BoxGroup("Points")] public int points;
    [BoxGroup("Scale Factor")] public float scaleFactor;

    public enum State
    {
        Attack,
        Defence
    }
    #endregion

    #region Private Properties
    private Transform tr;
    private Rigidbody rb;
    private Coroutines coroutine;
    private bool isMouseDown = false;
    private float swipeSensitivity = 3f;
    private Vector2 lastTouchPos;
    private Vector2 swipeDelta;
    private Vector3 mousePos;
    private List<GameObject> food = new List<GameObject>();
    #endregion

    #region Awake / Start / Update
    private void Awake()
    {
        GlobalManager.Player = this;
        tr = this.transform;
        rb = GetComponent<Rigidbody>();
        coroutine = GetComponent<Coroutines>();
    }

    void FixedUpdate()
    {
        if (GlobalManager.GameManager.clickable)
        {
            InputLogic();
        }
    }

    private void Update()
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
#endif
        return;
    }
    #endregion

    #region Functions
    public void SpawnPlayer()
    {
        this.gameObject.SetActive(true);
        animator.Rebind();
        rb.velocity = Vector3.zero;
        points = 4;

        int random = Random.Range(0, GlobalManager.LevelManager.platforms.Count - 1);
        tr.position = GlobalManager.LevelManager.platforms[GlobalManager.LevelManager.platforms.Count / 2].transform.position;     
    }

    public void KillPlayer()
    {
        this.gameObject.SetActive(false);
        rb.velocity = Vector3.zero;

        if (food.Count != 0)
        {
            foreach (GameObject o in food)
            {
                o.SetActive(false);
            }
        }       
        food.Clear();
    }
    #endregion

    #region Player Movement
    private void InputLogic()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (isMouseDown)
        {
            state = State.Attack;
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
            swipeDelta = (Vector2)Input.mousePosition - lastTouchPos;
            if (swipeDelta.magnitude > swipeSensitivity)
            {
                tr.LookAt(tr.position + new Vector3(swipeDelta.x, tr.position.y, swipeDelta.y));

                if (tr.localScale.x <= 1.5)
                {
                    rb.velocity = tr.forward * speed * Time.deltaTime / (tr.localScale.x / 2);
                }
                else
                {
                    rb.velocity = tr.forward * speed * Time.deltaTime / (1.5f / 2);
                }
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
        state = State.Attack;
        isMouseDown = true;
        lastTouchPos = touch.position;
        StartCoroutine(coroutine.LerpFieldOfView(Camera.main.fieldOfView, endFOW, 0.5f));
    }

    private void TouchMoved(Touch touch)
    {
        swipeDelta = touch.position - lastTouchPos;
        state = State.Attack;
        if (swipeDelta.magnitude > swipeSensitivity)
        {            
            tr.LookAt(tr.position + new Vector3(swipeDelta.x, tr.position.y, swipeDelta.y));

            if (tr.localScale.x <= 1.5)
            {
                rb.velocity = tr.forward * speed * Time.deltaTime / (tr.localScale.x / 2);
            }
            else
            {
                rb.velocity = tr.forward * speed * Time.deltaTime / (1.5f / 2);
            }
        }      
    }

    private void TouchEnded()
    {
        isMouseDown = false;
        rb.velocity = Vector3.zero;
        StartCoroutine(coroutine.LerpFieldOfView(Camera.main.fieldOfView, startFOW, 0.5f));
        state = State.Defence;
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
        }          
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Bot")
        {
            StartCoroutine(coroutine.WaitForSeconds(0.2f, () =>
            {
                if (points > 0)
                {
                    points--;
                    if (tr.localScale.x > 1)
                    {
                        Vector3 scaleHelper = new Vector3(tr.localScale.x - scaleFactor, tr.localScale.y - scaleFactor, tr.localScale.z - scaleFactor);
                        tr.localScale = Vector3.Lerp(tr.localScale, scaleHelper, 0.1f);
                    }
                    if(food.Count > 0)
                    {                                                                     
                        int randomX = Random.Range(-3, 3);
                        int randomZ = Random.Range(-3, 3);
                        int randomPlace = Random.Range(0, 4);
                        int randomDirection = Random.Range(0, 4);

                        switch (randomPlace)
                        {
                            case 0:
                                food[0].transform.position = new Vector3(tr.position.x + 5, tr.position.y + 1.2f, tr.position.z - 5);
                                break;
                            case 1:
                                food[0].transform.position = new Vector3(tr.position.x - 5, tr.position.y + 1.2f, tr.position.z + 5);
                                break;
                            case 2:
                                food[0].transform.position = new Vector3(tr.position.x - 5, tr.position.y + 1.2f, tr.position.z - 5);
                                break;
                            case 3:
                                food[0].transform.position = new Vector3(tr.position.x + 5, tr.position.y + 1.2f, tr.position.z + 5);
                                break;
                        }

                        switch (randomDirection)
                        {
                            case 0:
                                //food[0].GetComponent<Rigidbody>().velocity = food[0].transform.forward * 0.002f;
                                food[0].GetComponent<Rigidbody>().AddForce(food[0].transform.forward * 2f, ForceMode.Impulse);
                                break;
                            case 1:
                                //food[0].GetComponent<Rigidbody>().velocity = food[0].transform.forward * -0.002f;
                                food[0].GetComponent<Rigidbody>().AddForce(food[0].transform.forward * -2f, ForceMode.Impulse);
                                break;
                            case 2:
                                //food[0].GetComponent<Rigidbody>().velocity = food[0].transform.right * 0.002f;
                                food[0].GetComponent<Rigidbody>().AddForce(food[0].transform.right * 2f, ForceMode.Impulse);
                                break;
                            case 3:
                                //food[0].GetComponent<Rigidbody>().velocity = food[0].transform.right * -0.002f;
                                food[0].GetComponent<Rigidbody>().AddForce(food[0].transform.right * -2f, ForceMode.Impulse);
                                break;
                        }

                        //StartCoroutine(coroutine.WaitForSeconds(0.1f, () =>
                        //{
                        //    food[0].GetComponent<Rigidbody>().AddForce(0, 0, 0);
                        //    food[0].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                        //    food[0].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        //    food[0].gameObject.transform.position = food[0].gameObject.transform.position;
                        //}));   
                        
                        GlobalManager.LevelManager.foodCurrent.Add(food[0]);
                        food[0].gameObject.SetActive(true);
                        food.RemoveAt(0);
                    }
                }
                else
                {
                    GlobalManager.GameManager.Lose();
                }
            }));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            points++;
            food.Add(other.gameObject);
            GlobalManager.LevelManager.foodCurrent.Remove(other.gameObject);
            other.gameObject.SetActive(false);
            if(tr.localScale.x <= 3)
            {
                Vector3 scaleHelper = new Vector3(tr.localScale.x + scaleFactor, tr.localScale.y + scaleFactor, tr.localScale.z + scaleFactor);               
                tr.localScale = Vector3.Lerp(tr.localScale, scaleHelper, 0.1f);
            }
        }
    }
    #endregion
}
