using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuteManager : MonoBehaviour
{
    public static MuteManager instance;

    private bool isMuted;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
        // Start is called before the first frame update
        void Start()
    {
        isMuted = false;
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MutePressed();
        }
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}
