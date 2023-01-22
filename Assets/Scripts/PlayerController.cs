using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0f;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    private Rigidbody2D rigidbody2d;
    public TextMeshProUGUI countText;
    private float timeRemaining = 12;
    private int seconds;
    public TextMeshProUGUI Timer;
    public GameObject player;
    public GameObject startText;
    private int count;
    float horizontal;
    float vertical;
    public GameObject coin;
    public GameObject winParticle;
    public AudioClip collectedClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    AudioSource audioSource;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        startText.SetActive(true);
        StartCoroutine(StartScreen());
        count = 0;
        SetCountText();
        winParticle.SetActive(false);
    }

    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();
        if(count >= 5)
        {
            WinTextObject.SetActive(true);
            GameObject winParticleObject= Instantiate(winParticle, transform.position, Quaternion.identity);
            Destroy(player);
            winParticle.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            seconds = (int)(timeRemaining % 60);
            Timer.text = "Time Left " + seconds.ToString();
        }
        if (timeRemaining < 0)
        {
            LoseTextObject.SetActive(true);
        }
        if(count >= 5)
        {
            Time.timeScale = 0;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            
        }
    }    

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rigidbody2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    IEnumerator StartScreen()
    {
        yield return new WaitForSeconds (2);
        startText.SetActive(false);
    }
}
