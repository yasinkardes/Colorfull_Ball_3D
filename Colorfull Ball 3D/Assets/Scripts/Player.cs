using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //Dýþarýdan eriþtiklerimiz
    public CameraShake shakeCamera;
    public UIManager uiManagement;
    public Sound_Manager soundManager;

    //Player ile hareket edecekler
    public GameObject mainCam;
    public GameObject vectorForward;
    public GameObject vectorBack;

    //Player hýz ayarlarý
    [Range(20, 40)]
    public int speedModifier;
    public int forwardSpeed;

    private Rigidbody rb;
    private Touch touch;

    private bool speedBallForward = false;
    private bool firstTouchControl = false;

    private int soundLimitControl;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (Variables.firstTouch == 1 && speedBallForward == false)
        {
            transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorForward.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorBack.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        }
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    if (firstTouchControl == false)
                    {
                        Variables.firstTouch = 1;
                        uiManagement.FirstTouch();
                        firstTouchControl = true;
                    }
                }
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    rb.velocity = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime,
                                 transform.position.y,
                                 touch.deltaPosition.y * speedModifier * Time.deltaTime);

                    if (firstTouchControl == false)
                    {
                        Variables.firstTouch = 1;
                        uiManagement.FirstTouch();
                        firstTouchControl = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //rb.velocity = new Vector3(0, 0, 0);
                rb.velocity = Vector3.zero;
            }
        }
    }

    public GameObject[] FractureItems;
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            shakeCamera.CameraShakeOn();
            uiManagement.StartCoroutine("WhiteEffect");
            soundManager.BlowUpSound();

            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                Vibration.Vibrate(50);
            }
            else if (PlayerPrefs.GetInt("Vibration") == 2)
            {
                Debug.Log("No Vibration");
            }

            gameObject.transform.GetChild(0).gameObject.SetActive(false);

            foreach (GameObject item in FractureItems)
            {
                //Handheld.Vibrate();
                item.GetComponent<SphereCollider>().enabled = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
            StartCoroutine(TimeScaleControl());
            //StartCoroutine("TimeScaleControl");
        }

        if (col.gameObject.CompareTag("Untagged"))
        {
            soundLimitControl++;
        }

        if (col.gameObject.CompareTag("Untagged"))
        {
            soundManager.ObjectHitSound();
        }
    }

    public IEnumerator TimeScaleControl()
    {
        speedBallForward = true;
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(0.6f);
        uiManagement.Restart();
        rb.velocity = Vector3.zero;
    }
}
