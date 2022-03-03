using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public List<GameObject> playersInEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player1" && collision.GetType() == typeof(BoxCollider2D))
        {
            playersInEnd.Add(collision.gameObject);
        }

        if (collision.tag == "player2" && collision.GetType() == typeof(BoxCollider2D))
        {
            playersInEnd.Add(collision.gameObject);
        }

        checkEnd();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "player1" && collision.GetType() == typeof(BoxCollider2D))
        {
            playersInEnd.Remove(collision.gameObject);
        }

        if (collision.tag == "player2" && collision.GetType() == typeof(BoxCollider2D))
        {
            playersInEnd.Remove(collision.gameObject);
        }

        checkEnd();
    }

    void checkEnd()
    {
        if (playersInEnd.Count == 2)
        {
            //end level scene???
            //award star
            //load next level
            if (SceneManager.GetActiveScene().buildIndex < (SceneManager.sceneCountInBuildSettings - 1))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(2);
            }
            
        }
    }
}
