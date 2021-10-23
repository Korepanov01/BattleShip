using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool paused = false;

    private AudioListener audioListener;
    private Canvas canvas;
    private PlayerControl playerControl;

    private void Start()
    {
        Cursor.visible = false;
        audioListener = GameObject.FindWithTag("MainCamera").GetComponent<AudioListener>();
        var player = GameObject.FindWithTag("Player");
        canvas = player.GetComponent<Transform>().Find("Canvas").GetComponent<Canvas>();
        playerControl = player.GetComponent<PlayerControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (paused)
            {
                Time.timeScale = 1.0f;
                canvas.enabled = true;
                audioListener.enabled = true;
                Cursor.visible = false;
                paused = false;
                playerControl.enabled = true;
            }
            else
            {
                Time.timeScale = 0.0f;
                canvas.enabled = false;
                audioListener.enabled = false;
                Cursor.visible = true;
                paused = true;
                playerControl.enabled = false;
            }
        }
    }
}
