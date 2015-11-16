using UnityEngine;
using System.Collections;

public class PlayerPrefsUnlock : MonoBehaviour {
		
	/*void PlayerPrefKey(){
       PlayerPrefs.SetString("unlockLevel2", "unlock");
    }*/
	
	public int[] levels;
	public int completed;

	void Start()
	{
		completed = PlayerPrefs.GetInt("unlock");
	}
	void PlayerPrefsKey(string value){

		switch (completed) {
			case 1:
				value = "unlockLevel2";
				break;
			case 2:
				value = "unlockLevel3";
				break;
			case 3:
				value = "unlockLevel4";
				break;
			case 4:
				value = "unlockLevel5";
				break;
			default:
				value = "Level is locked";
				break;

		}    
	}

	void SetPPKey(string value){
			PlayerPrefs.SetString("unlock", value);
	}
}
 