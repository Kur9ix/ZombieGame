using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{
    [Range(0, 24)]
    public float currentTime; 
    public Time time1;
    public Text clock;

    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 15;
        
        addTime(16);
    }

    // Update is called once per frame
    void Update()
    {
        clock.text = "Time: " + currentTime;

        switch(currentTime) {
	    case 1: canvasGroup.alpha = 1 ; break;
	    case 2: canvasGroup.alpha = 1 ; break;
	    case 3: canvasGroup.alpha = 1 ; break;
        case 4: canvasGroup.alpha = 1 ; break;
	    case 5: canvasGroup.alpha = 1 ; break;
	    case 6: canvasGroup.alpha = 1 ; break;
        case 7: canvasGroup.alpha = 1 ; break;
	    case 8: canvasGroup.alpha = 1 ; break;
	    case 9: canvasGroup.alpha = 1 ; break;
        case 10: canvasGroup.alpha = 1 ; break;
	    case 11: canvasGroup.alpha = 1 ; break;
	    case 12: canvasGroup.alpha = 0 ; break;
        case 13: canvasGroup.alpha = 1 ; break;
	    case 14: canvasGroup.alpha = 1 ; break;
	    case 15: canvasGroup.alpha = 1 ; break;
        case 16: canvasGroup.alpha = 1 ; break;
	    case 17: canvasGroup.alpha = 1 ; break;
	    case 18: canvasGroup.alpha = 1 ; break;
        case 19: canvasGroup.alpha = 1 ; break;
	    case 20: canvasGroup.alpha = 1 ; break;
	    case 21: canvasGroup.alpha = 1 ; break;
        case 22: canvasGroup.alpha = 1 ; break;
        case 23: canvasGroup.alpha = 1 ; break;
        case 24: canvasGroup.alpha = 1 ; break;

	    
}
    }

    public void setTime(float time){
        currentTime = time;
    }

    public void addTime(float time){
        if((currentTime+ time) >= 24){
            setTime(currentTime + time - 24);
        }else{
            currentTime+=time;
        }
    }

    public float getTime(){
        return currentTime;
    }
}
