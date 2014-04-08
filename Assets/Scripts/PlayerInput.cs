﻿using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	private Vector3 lastClientClick = Vector3.zero;
	private RaycastHit hit;
	
	void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo msg) 
	{
		Camera.main.SendMessage("SetPlayerTarget", transform);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
//			movementScript.combatScript.target = null;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if(Physics.Raycast (ray, out hit, 100.0f)) {
				if (lastClientClick != hit.point) {
					lastClientClick = hit.point;
					uLink.NetworkView.Get(this).RPC("SendMovementInput", uLink.RPCMode.Server, hit.point, hit.collider.name);
				}
			}
		}
	}
}
