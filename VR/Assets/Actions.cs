using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
	public GameObject grass_obj;
	public GameObject badGrass;
	public GameObject grass_obj_growed;
	public GameObject uncutTree;
	public GameObject cutTree;
	public GameObject plank;
	public GameObject fireStone;
	//private GameObject originalGameObject;// = GameObject.Find("Camera");
	private GameObject holdPositionObject;// = originalGameObject.transform.GetChild(0).gameObject;
    // Start is called before the first frame update
	private GameObject child;
	private bool collidedWithMud = false;
	private bool firstGrowthCompleted = false; //seeds
	private bool secondGrowthCompleted = false; //watering
	private bool wateringCanHasWater = false;
	private bool badGrassRemoved = false;
	private bool badGrassFinishedGrowth = false;
	private bool thirdGrowthCompleted = false;
	private int clickedTimes;
	private bool collidedWithTree = false;
	private bool treeIsCut = false;
	private bool boilerHasLog = false;
	private bool collidedWithBoiler = false;
	private bool boilerIsBurning = false;
	
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
        if (collidedWithMud && child != null&& child.name == "PotionRed" && !firstGrowthCompleted){
			if (Input.GetKeyDown(KeyCode.G) && grass_obj.transform.position.y <= 0)
			{
				Debug.Log("Collided, and in mud and pressed G");
				grass_obj.transform.position += new Vector3(0f, 0.3f, 0f);
				
			}
			else if(grass_obj.transform.position.y >= 0)
				firstGrowthCompleted = true;
		}
		// first watering
		if (collidedWithMud && child.name == "WateringCan_01" && firstGrowthCompleted && !secondGrowthCompleted && wateringCanHasWater){
			Debug.Log("Collided with mud with wateringCan and water");
			
			if (Input.GetKeyDown(KeyCode.G) && clickedTimes <= 4)
			{

				clickedTimes++;
				Debug.Log("Collided with watercan Growing");
				//grass_obj.transform.localScale  += new Vector3(0.1f, 0.05f, 0f);
				for(int i = 0; i < grass_obj.transform.childCount; i++)
				{
					GameObject Go = grass_obj.transform.GetChild(i).gameObject;
					Go.transform.localScale +=  new Vector3(0.15f, 0.15f, 0f);
				}
				
			}
			else if (clickedTimes > 4)
			{
				wateringCanHasWater = false;
				secondGrowthCompleted = true;
				clickedTimes = 0;
			}
		}
		
		// Bad grass growth
		if (!badGrassRemoved && !badGrassFinishedGrowth && secondGrowthCompleted && !thirdGrowthCompleted)
		{
			//badGrass.transform.position += new Vector3(0f, 0.05f, 0f);
			if (badGrass.transform.position.y <= 0)
			{
				Debug.Log("Collided, and in mud and pressed G");
				badGrass.transform.position += new Vector3(0f, 0.15f, 0f) * Time.deltaTime;
				
			}
			else
			{
				badGrassFinishedGrowth = true;
			}
		
		}
		if (!badGrassRemoved && badGrassFinishedGrowth && secondGrowthCompleted && !thirdGrowthCompleted && child != null&& child.name == "PotionBlue" &&  collidedWithMud && Input.GetKeyDown(KeyCode.G))
		{
			badGrass.transform.position -= new Vector3(0f, 0.5f, 0f);
			if (badGrass.transform.position.y <= -1.5) {
				badGrassRemoved = true;
				GameObject.Destroy(badGrass);
			}
		}
		
		// 2nd watering
		if (collidedWithMud && child.name == "WateringCan_01" && !thirdGrowthCompleted && wateringCanHasWater && badGrassRemoved){
			Debug.Log("Collided with mud with wateringCan and water 2d watering");
			
			if (Input.GetKeyDown(KeyCode.G) && clickedTimes <= 4)
			{

				clickedTimes++;
				Debug.Log("Collided with watercan Growing 2nd watering");
				//grass_obj.transform.localScale  += new Vector3(0.1f, 0.05f, 0f);
			
				
			}
			else if (clickedTimes > 4)
			{
				wateringCanHasWater = false;
				thirdGrowthCompleted = true;
				
				grass_obj_growed.transform.position += new Vector3(0f, 2f, 0f);
				grass_obj.transform.position -= new Vector3(0f, 2f, 0f);
				clickedTimes = 0;
			}
		}
		// chopping tree
		if (collidedWithTree && child != null && child.name == "Axe" && !treeIsCut)
		{
			Debug.Log("Collided with tree while with axe");
			
			if (Input.GetKeyDown(KeyCode.G) && clickedTimes <= 4)
			{
				clickedTimes++;
			}
			else if (clickedTimes > 4)
			{
				treeIsCut = true;
				uncutTree.transform.position -= new Vector3(0f, 10f, 0f);
				cutTree.transform.position += new Vector3(0f, 2f, 0f);
				plank.transform.position = new Vector3(0f, 0f, 0f);
				//plank.isKinematic = false;
				// plank
				
			}
		}
		// adding log to boiler
		if (collidedWithBoiler && !boilerHasLog && child != null && child.name == "Plank")
		{
			Debug.Log("Collided with boiled with log");
			if (Input.GetKeyDown(KeyCode.G))
			{
				boilerHasLog = true;
				GameObject.Destroy(child);
				
			}
		}
		if (collidedWithBoiler && child != null && child.name == "FireStone" && boilerHasLog && !boilerIsBurning) {//fireStone
		if (Input.GetKeyDown(KeyCode.G))
			{
				boilerIsBurning = true;
				Debug.Log("Boiler is burning");
				
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
		
		if (other.gameObject.name == "TreeUncut") {
			collidedWithTree = true;
			Debug.Log("Collided with uncut tree");
		}
		else
			collidedWithTree = false;
		
		//collidedWithBoiler
		if (other.gameObject.name == "Boiler1") {
			collidedWithBoiler = true;
			Debug.Log("Collided with boiler");
		}
		else
			collidedWithBoiler = false;

    }  
}
