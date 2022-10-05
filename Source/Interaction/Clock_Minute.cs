using System.Collections;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Clock_Minute : MonoBehaviour , 
    IPointerDownHandler,IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RainInteraction InteractionController;

    [Header("Clock")]
    GameObject Clock;

    [Header("Minute")]
    Image Minite_Stroke;
    Color[] StrokeColor = new Color[2];

    [Header("Hour")]
    public GameObject Clock_Hour;

    [Header("UI")]
    public Collider2D clock_col;

    float minute_clock_rotz ;
    float hour_clock_rotz;
    bool bIsMinRotate = false;

    private int minCount = 0;

    private Vector2 direction;

    private Vector3 vec; 

    private void Start()
    {
        // Rotation Init : 0,0
        //this.gameObject.transform.rotation = new Vector3

        // Feedback Init
        StrokeColor[0] = new Color(1f, 1f, 1f, 1f); 
        StrokeColor[1] = new Color(1f, 1f, 1f, 0f);
        Minite_Stroke = this.transform.GetChild(0).gameObject.GetComponent<Image>();
        Minite_Stroke.color = StrokeColor[1];
    }

    /*
     0 : 위
     1 : 아래
     0 : 좌 
     1 : 우
         
         */
    private void Update() 
    {
        minute_clock_rotz = this.gameObject.transform.rotation.z;
        hour_clock_rotz = Clock_Hour.gameObject.transform.rotation.z;

    }

    


    public void OnBeginDrag(PointerEventData eventData)
    {
        // touchUI.onNoff(3, false); 

       //Debug.Log("BeginDrag");
        
        // feedback
        // SoundManager.Instance.PlayMergedBGM(SoundList.Chapter2_Clock);
        Minite_Stroke.color = StrokeColor[0];



        vec = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = new Vector2(eventData.position.x - transform.position.x, eventData.position.y - transform.position.y);
      if(bIsup)  transform.up = direction;
        // minute_clock_rotz = this.gameObject.GetComponent<RectTransform>().eulerAngles.z;

       // Debug.Log("OnDrag");

       // vec = eventData.position;
      //  Debug.Log("Pointer Dir : " + CheckDirection(vec, eventData.position).ToString());


        // 특정 조건에서
        InteractionController.EndInteraction();
        SoundManager.Instance.StopMergedBGM();

        /*
        if (minCount == 4)
        {
           
           //  AE.SPOutAnim();
           // this.clock.SetActive(false);
           // clock_audio.Stop();
        }
        */

        // feedback
        //SoundManager.Instance.ResumeMergedBGM();
        Minite_Stroke.color = StrokeColor[0];


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision==clock_col)
        {
            bIsMinRotate = true;
            minCount++;
            Move_HourHand();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      //  Debug.Log("OnEndDrag");
       
        
        
        // feedback
        //SoundManager.Instance.StopMergedBGM();
        Minite_Stroke.color = StrokeColor[1];
      
    }

    private bool bIsup = true;
    private int Direction_index_ud = 0;
    private int Direction_index_rl = 0;

    public Vector3 CheckDirection(Vector3 _start, Vector3 _end)
    {
        Vector3 result = _start - _end;

        result = result.normalized;

        //x축 y축 방향구분
        if (result.x * result.x < result.y * result.y)
        {
            //위
            if (result.y < 0)
            {
              //  Debug.Log("위");
                Direction_index_ud = 0;
                return Vector3.up;
            }


            //아래
            else
            {
               // Debug.Log("아래");


                Direction_index_ud = 1;
                return Vector3.down;
            }
                
        }

        //좌우
        else
        {
            //좌
            if (result.x > 0)
            {
              //  Debug.Log("좌");
                Direction_index_rl = 0;

                return Vector3.left;

            }

            //우
            else
            {
              //  Debug.Log("우");
                Direction_index_rl = 1;

                return Vector3.right;
            }
            
        }//end else
    }//end CheckDirection()


    public void OnPointerDown(PointerEventData eventData)
    {
       // Debug.Log("OnPointerDown");

        // feedback
        Minite_Stroke.color = StrokeColor[0];
        //SoundManager.Instance.PlayUIAudio(SoundList.Sound_Arrowbutton);

    }

    private void Move_HourHand() 
    {
        if (bIsMinRotate)
        {
            hour_clock_rotz += -30;
            Clock_Hour.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, hour_clock_rotz));
        }
    }


    

}
