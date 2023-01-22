using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    //Stats
    [SerializeField] public float health = 10;
    [SerializeField] public float baseDamage = 1;
    [SerializeField] public float damageMulti = 1;
    [SerializeField] public float damageReduct = 1;
    [SerializeField] public float cooldown = 0.5f;

    [SerializeField] public float flashCount = 5;
    Color transparentMode = new Color(1f, 1f, 1f, .2f);
    Color nonTransparenMode = new Color(1f, 1f, 1f, 1f);

	private float pauseStartTime = 0;

    private bool paused = false;

    private float flashesLeft = 0;

	public void Update(){

        if(paused){
            float currentTime = Time.time;

            SpriteRenderer[] renderer = GetComponentsInChildren<SpriteRenderer>();

			if (currentTime >= pauseStartTime + cooldown){
                // deactivate pause after cooldown has passed
                pauseStartTime = 0;
                paused = false;

                if (renderer != null) {
					for (int i = 0; i < renderer.Length; i++)
					{
						renderer[i].color = nonTransparenMode;
					}
				}
                
            }else{
                // generate flashes
                float timeLeft = (pauseStartTime + cooldown) - currentTime;
                if(flashesLeft > 0 && timeLeft <= (cooldown / flashCount) * flashesLeft){
                    flashesLeft -= 1;
                    if (renderer != null) {
						for (int i = 0; i < renderer.Length; i++)
						{
							renderer[i].color = renderer[i].color == nonTransparenMode ? transparentMode : nonTransparenMode;
						}
					}
                    
                }

            }
        }
    }

    virtual public void TakeDamage(float damage)
    {
        if(!paused){
            this.health -= damage * damageReduct;
            if(health <= 0)
            {
                HandleDeath();
		    }

            pauseStartTime = Time.time;
            paused = true;
            flashesLeft = flashCount;
        }

        
    }

    public float DealDamage()
    {
        return baseDamage * damageMulti;
    }

    public delegate void functionType();
    private functionType _DeathFunction = null;

    /**Sets the function triggered upon death.*/
    public void changeDeathFuntion(functionType deathFunction){
        _DeathFunction = deathFunction;
    }

    /**Executes the set death function. Defaultbehavior: destroy this object*/
    public void HandleDeath(){
        if(_DeathFunction == null) Object.Destroy(gameObject);
        else _DeathFunction();
    }

	public float GetHealth()
	{
        return health;
	}
}
