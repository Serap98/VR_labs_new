using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
	public GameObject grass_obj;
	//private GameObject originalGameObject;// = GameObject.Find("Camera");
	private GameObject holdPositionObject;// = originalGameObject.transform.GetChild(0).gameObject;
    // Start is called before the first frame update
	private GameObject child;
	private bool collidedWithMud = false;
	private bool firstGrowthCompleted = false; //seeds
	private bool secondGrowthCompleted = false; //watering
	private bool wateringCanHasWater = false;
	private int clickedTimes;
    void Start()
    {
        //originalGameObject = GameObject.Find("Camera");
		holdPositionObject = GameObject.Find("holdPosition");
		clickedTimes = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (holdPositionObject.transform.childCount > 0)
		{
			child = holdPositionObject.transform.GetChild(0).gameObject;
			//Debug.Log(child.name);
		}
		else
		{
			child = null;
		}
		// First growth
        if (collidedWithMud && child.name == "PotionRed" && !firstGrowthCompleted){
			if (Input.GetKeyDown(KeyCode.G) && grass_obj.transform.position.y <= 0)
			{
				Debug.Log("Collided, and in mud and pressed G");
				grass_obj.transform.position += new Vector3(0f, 0.3f, 0f);
				
			}
			else if(grass_obj.transform.position.y >= 0)
				firstGrowthCompleted = true;
		}

		if (collidedWithMud && child.name == "WateringCan_01" && firstGrowthCompleted && !secondGrowthCompleted && wateringCanHasWater){
			Debug.Log("Collided with mud with wateringCan and water");
			
			if (Input.GetKeyDown(KeyCode.G) && clickedTimes <= 4)
			{

				clickedTimes++;
				Debug.Log("Collided with watercan Growing");
				grass_obj.transform.localScale  += new Vector3(0.1f, 0.05f, 0f);
				
			}
			else if (clickedTimes > 4)
			{
				wateringCanHasWater = false;
				secondGrowthCompleted = true;
			}
		}
    }
	
	void OnCollisionEnter(Collision  other) {  
	//Debug.Log("inside in trigger enter func");
        //print("Another object has entered the trigger");
		if (other.gameObject.tag == "Mud")
		
		{
			collidedWithMud = true;
			Debug.Log("collidedWithMud");
			
		}
		else
			collidedWithMud = false;
		
		if (other.gameObject.name == "Fountain" && child != null && child.name == "WateringCan_01") {
			wateringCanHasWater = true;
			Debug.Log("Watering can has water");
		}

    }  
}
