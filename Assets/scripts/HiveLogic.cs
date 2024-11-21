using UnityEngine;

public class HiveLogic : MonoBehaviour
{
    public GameObject honeypotPrefab;
    private int beeVisitCount = 0;
    public int honeypotCount = 0; 
    public int maxHoneypots = 1;

    private void OnTriggerEnter2D(Collider2D other)
    { // Log taht trigger was activated
        Debug.Log("Hive trigger activated: " + other.gameObject.name);

       
        if (other.gameObject.CompareTag("Bee"))
        {
            beeVisitCount++; // visit count
            Debug.Log(" 'Bee' tag. Total visits: " + beeVisitCount);

            if (beeVisitCount >= 3)
            {  if (honeypotCount < maxHoneypots)
                {
                    ProduceHoneypot(); // Produce 
 }
                else
                {
                    Debug.Log(" Cannot produce more honey.");}
                beeVisitCount = 0; // Reset
            }
        }
        else
        {
  Debug.Log("Object entering the hive not tag 'Bee'."); }
    }

    public void ProduceHoneypot()
    {
      
        Vector3 honeypotPosition = new Vector3(17f, 13f, 0f); 
        Instantiate(honeypotPrefab, honeypotPosition, Quaternion.identity);
        honeypotCount++; 
        Debug.Log("Honeypot produced at position: " + honeypotPosition);
    }

}
