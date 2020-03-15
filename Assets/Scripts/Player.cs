using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    const string interactionKey = "Interaction";
    
    void Update()
    {

	    if (Input.GetButtonDown(interactionKey))
	    {
		    InterAct();
	    }

	    if (Input.GetButtonUp(interactionKey))
	    {
		    
	    }
	    
    }

    void InterAct()
    {
	    
    }
}

[Serializable]
public class BehaviourBase
{
	
}