using UnityEngine;
using System.Collections;

public class PlayMove : MonoBehaviour 
{
	UnityEngine.AI.NavMeshAgent navAgent; 
	public GameObject Dummy;
	Animator DummyAny;
	Vector3 targetPosition;

	void Start () 
	{
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		DummyAny = Dummy.GetComponent<Animator> ();
		targetPosition = Dummy.transform.position;
	}
	

	void Update () 
	{

		RaycastHit Hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonUp (0)) 
		{
			if (Physics.Raycast (ray, out Hit)) 
			{
				navAgent.SetDestination (Hit.point);
				targetPosition =Hit.point;
				
			
				//float target = navAgent.remainingDistance(Hit.point);

				Debug.Log ("rayHit");

			}


	  }
		StopMove ();





	
	}

	public void StopMove()
	{
		float targetDis = Vector3.Distance( targetPosition, this.transform.position);
		Debug.Log (targetDis);

		if (targetDis >= 0.5f) 
		{
			DummyAny.SetBool ("Run", true);


		} 
		else 
		{
			DummyAny.SetBool ("Run", false);
			Debug.Log ("Stop");
		}
	}
}
