using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event playGameSong;
    [SerializeField] AK.Wwise.Event stopGameSong;
    [SerializeField] AK.Wwise.Event playMenuSong;
    [SerializeField] AK.Wwise.Event stopMenuSong;
    [SerializeField] AK.Wwise.Event playLevel_1Music;
    [SerializeField] AK.Wwise.Event stopLevel_1Music;
    // Start is called before the first frame update
    void Start()
    {
        StartMenuSong();
    }

    public void StartGameSong()
    {
        stopMenuSong.Post(gameObject);
        playLevel_1Music.Post(gameObject);
    }

    public void StartMenuSong()
    {
        stopLevel_1Music.Post(gameObject);
        playMenuSong.Post(gameObject);
    }
}
