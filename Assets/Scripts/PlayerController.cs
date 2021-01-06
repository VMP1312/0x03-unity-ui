using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text scoreText;
    private int score = 0;
    public Text healthText;
    public int health = 5;
    public Text WinLoseText;
    public Image WinLoseBG;
    private Rigidbody rb;
 

    // Start is called before the first frame update.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // Move player on X and Y axis.
        float XInput = Input.GetAxis("Horizontal");
        float ZInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (XInput, 0.0f, ZInput);
        
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        ///Increment Score when pick a coin.
        if(other.gameObject.CompareTag("Pickup"))
        {
            score++;
            // Debug.Log("Score: " + score.ToString());
            other.gameObject.SetActive(false);
            SetScoreText();
        }

        ///Decrement Health when touch a trap.
        if(other.gameObject.CompareTag("Trap"))
        {
            health--;
            other.gameObject.SetActive(true);
            // Debug.Log("Health: " + health.ToString());
            SetHealthText();
        }

        ///Send Goal reach message.
         if(other.gameObject.CompareTag("Goal"))
        {
            other.gameObject.SetActive(true);
            // Debug.Log("You win!");
            WinLoseBG.gameObject.SetActive(true);
            WinLoseText.color = Color.black;
            WinLoseBG.color = Color.green;
            WinLoseText.text = "You Win!";
            StartCoroutine(LoadScene(3));
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
        if(health == 0)
        {
            // Debug.Log("Game Over!");
            WinLoseBG.gameObject.SetActive(true);
            WinLoseBG.color = Color.red;
            WinLoseText.color = Color.white;
            WinLoseText.text = "Game Over!";
            StartCoroutine(LoadScene(3));
        }
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(0);
    }
}
