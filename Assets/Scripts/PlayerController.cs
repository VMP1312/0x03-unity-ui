using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private int score = 0;
    public int health = 5;
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
            Debug.Log("Score: " + score.ToString());
            other.gameObject.SetActive(false);
        }

        ///Decrement Health when touch a trap.
        if(other.gameObject.CompareTag("Trap"))
        {
            health--;
            other.gameObject.SetActive(true);
            Debug.Log("Health: " + health.ToString());
        }

        ///Send Goal reach message.
         if(other.gameObject.CompareTag("Goal"))
        {
            other.gameObject.SetActive(true);
            Debug.Log("You win!");
        }
    }

    void Update()
    {
        if(health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(0);
        }
    }
}
