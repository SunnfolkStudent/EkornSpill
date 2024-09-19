using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioMannagerController : MonoBehaviour
{
    [Header("Audio Source")] [SerializeField]
    AudioSource musicSource;

    [Header("Audio Clips")] public AudioClip intro;
    public AudioClip introLoop;
    public AudioClip entrens;
    public AudioClip enemyLoop;
    public AudioClip end;
    public AudioClip death;

    //Time of a tab
    public float musicTimer = 1.90476f;

    //Waiting time
    public float relativTimer;

    //Active Timer
    public float timer = 0.0f;

    //Stage in music
    public int stage;

    public static AudioMannagerController instance;

    //Never Die
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
}

//Check if its main stage to start from beggining
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        //relativTimer = 0.5f;
        
        if (sceneName == "MainMenu")
        {
            //If scene is menu, start song from beginning
            stage = 1;
            print("this is the Menu");
        }
        else if (sceneName == "Level_1")
        {
            //If scene is SampelScene, start song from 3
            stage = 3;
            print("this is the Level_1");
            
        }
        
    }

    //constant check and changing of music
    private void Update()
    {
        //timmer
        timer += Time.deltaTime;

        
        if (stage <= 9)
        {
            if (timer >= relativTimer)
            {
                if (stage == 1)
                {
                    musicSource.clip = intro;
                    musicSource.Play();
                    timer = 0f;
                    print("intro");
                    relativTimer = musicTimer * 5.65f;
                    stage = 2;
                }
                else if (stage == 2)
                {
                    musicSource.clip = introLoop;
                    musicSource.Play();
                    timer = 0;
                    print("intro loop");
                    relativTimer = musicTimer * 3.85f;
                    
                    //Wait to SampleScene before going further
                    Scene currentBetterScene = SceneManager.GetActiveScene();
                    string sceneBetterName = currentBetterScene.name;
                    
                    if (sceneBetterName == "Level_1")
                    {
                        musicSource.clip = entrens;
                        musicSource.Play();
                        timer = 0f;
                        print("entrens");
                        relativTimer = musicTimer * 2;
                        stage = 3;
                    }
                }
                else if (stage == 3)
                {
                    musicSource.clip = enemyLoop;
                    musicSource.Play();
                    timer = 0;
                    print("enemy loop");
                    relativTimer = musicTimer * 17.6f;
                }
                  
            }
            
        }
        
        
    }
}


