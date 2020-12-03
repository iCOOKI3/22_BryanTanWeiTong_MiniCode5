using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;

    public Text energy;

    private int numberOfSpawn = 4;

    public GameObject addEnergyPrefab;

    public GameObject minusEnergyPrefab;

    bool playerMove = true;

    float moveSpeed = 10.0f;

    float jumpSpeed = 5.0f;

    public float EnergyCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove == true)
        {
            //Forward Anim
            if (Input.GetKey(KeyCode.W))
            {
                StartRun();
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.SetBool("isRun", false);
            }
            //Backward Anim
            if (Input.GetKey(KeyCode.S))
            {
                StartRun();
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.SetBool("isRun", false);
            }
            //Left Anim
            if (Input.GetKey(KeyCode.A))
            {
                StartRun();
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                playerAnim.SetBool("isRun", false);
            }
            //Right Anim
            if (Input.GetKey(KeyCode.D))
            {
                StartRun();
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnim.SetBool("isRun", false);
            }
            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed);
                playerAnim.SetTrigger("trigJump");
            }
            //Energy Text
            energy.GetComponent<Text>().text = "Energy: " + EnergyCount;
        }
        if(EnergyCount >= 50)
        {
            SceneManager.LoadScene("WinScene");
        }
        if(GameManager.instance.levelTime == 0)
        {
            playerMove = false;
            playerAnim.SetBool("isRun",false);
            SceneManager.LoadScene("LoseScene");
        }
    }

    void StartRun()
    {
        playerAnim.SetBool("isRun",true);
        playerAnim.SetFloat("startRun",0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("AddEnergy"))
        {
            EnergyCount += 5;
            GameManager.instance.levelTime += 5;
            MorePrefab();
            Destroy(collision.gameObject);
            Debug.Log("+5 to energy count");
        }
        else if(collision.gameObject.CompareTag("MinusEnergy"))
        {
            EnergyCount -= 25;
            GameManager.instance.levelTime -= 5;
            Destroy(collision.gameObject);
            Debug.Log("-5 to energy count");
        }

    }

    private void MorePrefab()
    {
        for (int i = 0; i < numberOfSpawn; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));

            if (Random.Range(0, 2) < 1)
            {
                Instantiate(addEnergyPrefab, randomPos, Quaternion.identity);
            }
            else
            { 
                Instantiate(minusEnergyPrefab, randomPos, Quaternion.identity);
                
            }
            
        }
    }
}

