using UnityEngine;
using System.Collections; 

public class BeeFly : MonoBehaviour
{
    public GameObject flower;         
    public GameObject hive;            
    public ParticleSystem dripNectar;  

    public float floatAmplitude = 0.5f; 
    public float floatSpeed = 2f;       
    public float moveSpeed = 2f;        
    public float waitTime = 2f;         

    private Vector3 startPos;
    private bool movingToFlower = true; 
    private Renderer beeRenderer;        

//help Chat Gpt
//https://chatgpt.com/share/67334013-5f60-8001-b240-d6ce7a47b4f1
    void Start()
    {
        flower = GameObject.FindGameObjectWithTag("Flower");
        hive = GameObject.Find("hive");
        dripNectar = GameObject.Find("dripNectar").GetComponent<ParticleSystem>();
        startPos = transform.position;
        beeRenderer = GetComponent<Renderer>();
        StartCoroutine(MoveBetweenFlowerAndHive());
    }

    void Update()
    {
        BeeFlyMovement();
    }

    void BeeFlyMovement()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        Vector3 targetPosition;

        if (movingToFlower)
        {
            if (flower != null)
            {   targetPosition = new Vector3(flower.transform.position.x, flower.transform.position.y, transform.position.z);
            }
            else
            {      flower = GameObject.FindGameObjectWithTag("Flower");        return;
            }
        }
        else if (hive != null)
        {
            targetPosition = new Vector3(hive.transform.position.x, newY, transform.position.z);
        } else
        {
            return; }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, transform.position.z), moveSpeed * Time.deltaTime);
        FlipBee();
    }

//face correct direction
    private void FlipBee()
    {
        if (movingToFlower)
        {   transform.localScale = new Vector3(-0.17f, 0.17f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.17f, 0.17f, 1); }
    }

//timers
//https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Transform.SetParent.html
    IEnumerator MoveBetweenFlowerAndHive()
    { while (true)
        {
            if (movingToFlower)
            {  yield return new WaitUntil(() => flower != null && Mathf.Abs(transform.position.x - flower.transform.position.x) < 0.1f);
                yield return new WaitForSeconds(waitTime);
                movingToFlower = false;
                if (dripNectar != null)
                {
                    dripNectar.Play();
                }
            }
            else
            {
                yield return new WaitUntil(() => hive != null && Mathf.Abs(transform.position.x - hive.transform.position.x) < 0.1f);
                SetVisibility(false);
                if (dripNectar != null)
                {   dripNectar.Clear();
                    dripNectar.Stop();
                }
                yield return new WaitForSeconds(waitTime);
                movingToFlower = true;
                SetVisibility(true);
            } }
    }


//https://discussions.unity.com/t/disable-renderer-of-a-child/86154
    private void SetVisibility(bool isVisible)
    {
        beeRenderer.enabled = isVisible;
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = isVisible;} }
}
