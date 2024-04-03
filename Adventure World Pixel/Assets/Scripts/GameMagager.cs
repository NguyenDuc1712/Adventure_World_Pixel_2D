using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMagager : MonoBehaviour
{
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Awake()
    {
        // khóa trỏ chuột 
       // Cursor.visible = false;
       // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

       // if (gameOverUI.activeInHierarchy)
    //    {
            // mở trỏ chuột khi người chơi bị tiêu /diệt 
         //   Cursor.visible = true;
          //  Cursor.lockState = CursorLockMode.None;
//}
     //   else
    //    {
            // trò chơi đang hoạt động khóa con trỏ chuột 
       //     Cursor.visible = true;
           // Cursor.lockState = CursorLockMode.Locked;
     //   }

    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        

    }
}
