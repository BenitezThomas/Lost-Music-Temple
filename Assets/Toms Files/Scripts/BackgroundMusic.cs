using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event playGameSong;
    [SerializeField] AK.Wwise.Event stopGameSong;
    [SerializeField] AK.Wwise.Event playMenuSong;
    [SerializeField] AK.Wwise.Event stopMenuSong;

    // Start is called before the first frame update
    void Start()
    {
        StartMenuSong();
    }

    public void StartGameSong()
    {
        stopMenuSong.Post(gameObject);
        playGameSong.Post(gameObject);
    }

    public void StartMenuSong()
    {
        stopGameSong.Post(gameObject);
        playMenuSong.Post(gameObject);
    }
}
