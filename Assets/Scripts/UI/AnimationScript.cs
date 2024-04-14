using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    [SerializeField] private Sprite cat2;
    [SerializeField] private Sprite cat3;
    [SerializeField] private Sprite cat4;
    [SerializeField] private GameObject fire;
    [SerializeField] private Sprite DevilCat;
    [SerializeField] private GameObject textPart; 
    [SerializeField] private AudioClip[] clips;

    private AudioSource aS;

    private Image catImg;
    private Image fireImg;

    private int currPhase = 0;
    private float timer = 0;
    private float maxTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        catImg = cat.GetComponent<Image>();
        fireImg = fire.GetComponent<Image>();
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currPhase == 0){
            
            // move cat towards its position but the x position is 0
            // cat is an ui object with a rect trsnform
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, new Vector3(Screen.width/2, cat.transform.position.y, cat.transform.position.z), 0.15f);
            
            //play the first sound
            if(!aS.isPlaying){
                aS.clip = clips[0];
                aS.Play();
            }
            if(cat.transform.position.x == Screen.width/2) currPhase++;
        }else if(currPhase == 1){
            maxTime = 5.0f;
            timer += Time.deltaTime;
            if(catImg.sprite != cat2){
                catImg.sprite = cat2;
                //play the second sound
                if(clips[1] != null){
                    aS.clip = clips[1];
                    aS.Play();
                }
            }
            if(timer >= maxTime){
                currPhase++;
                timer = 0;
            }
        }else if(currPhase == 2){
            maxTime = 5.0f;
            timer += Time.deltaTime;
            if(catImg != cat3){
                catImg.sprite = cat3;
                fire.SetActive(true);
                if(clips[2] != null){
                    aS.clip = clips[2];
                    aS.Play();
                }
            }
            if(timer >= maxTime){
                currPhase++;
                timer = 0;
            }
        }else if(currPhase == 3){
            maxTime = 10.0f;
            timer += Time.deltaTime;
            if(fireImg != DevilCat){
                fireImg.sprite = DevilCat;
                catImg.sprite = cat4;
                if(clips[3] != null){
                    aS.clip = clips[3];
                    aS.Play();
                }
            }
            if(timer >= maxTime){
                currPhase++;
                timer = 0;
            }
        }
        else{
            if(textPart.activeInHierarchy == false){
                textPart.SetActive(true);
            }
        }
        
    }
}
