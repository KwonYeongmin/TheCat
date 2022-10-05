using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catPattern : MonoBehaviour
{

    Intro IntroChapter;

    public GameObject DND_Icon;

    private Dictionary <int,starID> cat_points;
    private List<starID> lines;
    private bool unlocking;
    new bool enabled = true;

    public GameObject linePrefab;
    public Canvas canvas;

    //가장마지막, 에디트가 되고 있는 아이
    private GameObject lineOnEdit;
    private RectTransform lineOnEditRCTs;
    private starID starOnEdit;

    //circle=catStar, circles=cat_points circleidentifier=starID
   
   private List <int> correctPattern;

    //
    public bool isLineOn = false;

   

    void Start()
    {
        IntroChapter = GameObject.FindWithTag("Chapter").GetComponent<Intro>();


        cat_points = new Dictionary<int, starID>();
        lines = new List<starID>();

        for(int i =0; i< transform.childCount; i++) 
        {   
            var catStar = transform.GetChild(i);
            var identifier = catStar.GetComponent<starID>();
            identifier.id = i;
            cat_points.Add(i,identifier);

         //  Debug.Log(cat_points[i].name);
        }
        
        
    }

    // Update is called once per frame
    void Update()
        
    {
        
        if (enabled == false)
        {
            return;
        }

        if (unlocking)
        {
            //라인이랑 좌표계를 일치시켜줘야한다.
            Vector3 mousePos = canvas.transform.InverseTransformPoint(Input.mousePosition);

            lineOnEditRCTs.sizeDelta = 
                new Vector2(lineOnEditRCTs.sizeDelta.x, Vector3.Distance(mousePos, starOnEdit.transform.localPosition));

            lineOnEditRCTs.rotation = Quaternion.FromToRotation(Vector3 .up,(mousePos - starOnEdit.transform.localPosition).normalized);
         }


    }

   
    void trySetLineEdit(starID star) 
    {
        //
        if(isLineOn != true)
        {
            lines.Clear();
        }

        //클릭된 스타에서 생성된 라인이 있는지 없는지
        foreach (var line in lines)
        {
            if (line.id == star.id)
            {
                return;
            }
        }
        //
        if (isLineOn == false)
        {
            Destroy(lineOnEdit);            
        }
        lineOnEdit = createLine(star.transform.localPosition, star.id);

        lineOnEditRCTs = lineOnEdit.GetComponent<RectTransform>();
        starOnEdit = star;
    }


    //애니메이션

        void enableColorFade(Animator anim) 
    {
        anim.enabled = true;
        anim.Rebind();
    }
    

    GameObject createLine(Vector3 pos,int id) 
    {
        var line = GameObject.Instantiate(linePrefab, canvas.transform);
        line.name = "line" + id;

        line.transform.localPosition = pos;

        //라인이 어디서 시작
        var lineIdf = line.AddComponent<starID>();
        lineIdf.id = id;
        lines.Add(lineIdf);
        //Debug.Log(lines.Count);
        
        for(int i=2; i < 17; i++) 
        {
            if (lines.Count == i)
            {
                //levelManager.catstar_sound(); 
            }
        }
        
        return line;
        
    }

    //라인제거
    IEnumerator release()
    {
        enabled = false;
        isLineOn = false;

        //3초 후에 밑에서 부터 읽어라.
        //yield return new WaitForSeconds(3f);
       
        
        foreach(var star in cat_points)
        {
            star.Value.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }

        foreach (var line in lines)
        {
            Destroy(line.gameObject);
            
        }

        lines.Clear();

        lineOnEdit = null;
        lineOnEditRCTs = null;
        starOnEdit = null;

        enabled = true;

        yield return null;
    }

    public void OnMouseEnterStar(starID idf)
    {

        if (enabled == false)
        {
            return;
        }

        //Debug.Log(idf.id);
        if (unlocking)
        {
            lineOnEditRCTs.sizeDelta
                = new Vector2(lineOnEditRCTs.sizeDelta.x, Vector3.Distance(starOnEdit.transform.localPosition, idf.transform.localPosition));
            lineOnEditRCTs.rotation = Quaternion.FromToRotation(
                Vector3.up, (idf.transform.localPosition - starOnEdit.transform.localPosition).normalized);
        }

        trySetLineEdit(idf);
        Start_cat_D();
    }

    public void OnMouseExitStar(starID idf)
    {
        if(enabled == false)
        {
            return;
        }
       // Debug.Log(idf.id);
    }

    public void OnMouseDownStar(starID idf)
    {
        if(enabled == false)
        {
            return;
        }
      //  Debug.Log(idf.id);
        unlocking =true;

        //
        isLineOn = true;
        trySetLineEdit(idf);
        // 이거 숨기기
        createLine(idf.transform.localPosition,idf.id);
        
    }

    public void OnMouseUpStar(starID idf)
    {
        if (unlocking)
        {
            foreach(var line in lines) 
            {
                enableColorFade(cat_points[line.id].gameObject.GetComponent<Animator>());
            }

            Destroy(lines[lines.Count-1].gameObject);
            lines.RemoveAt(lines.Count-1);

            foreach(var line in lines)
            {
                enableColorFade(line.GetComponent<Animator>());
            }

            StartCoroutine(release());
        }

        //Debug.Log(idf.id);
        unlocking = false;
       
    }

    //라인 다시 터치가능
    public void Start_cat_D()
    {
    // 터치아이콘//
        if (lines.Count == 2) { DND_Icon.SetActive(false); }
        if (lines.Count >= 16) 
        {
           
            IntroChapter.End_StarInteraction();
        }
      //  for (int i = 1;  i < 16; i++) {  if (lines.Count == i) { levelManager.catstar_sound(); }}

    }

    
}
//unlocking되면 라인 사라진다.