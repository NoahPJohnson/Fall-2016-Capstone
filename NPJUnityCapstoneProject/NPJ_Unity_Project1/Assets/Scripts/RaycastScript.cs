using UnityEngine;
using System.Collections;

public class RaycastScript : MonoBehaviour
{
    [SerializeField] Transform playerViewObject;
    [SerializeField] Transform playerBody;
    [SerializeField] Transform reticle;
    [SerializeField] Transform gazeRay;
    ReticleScript reticleScript;
    [SerializeField] Transform target;
    [SerializeField] bool lookingAtTarget = false;
    [SerializeField] GameObject projectileOriginal;
    [SerializeField] GameObject firedProjectile;

    //[SerializeField] Texture2D attackLineTexture;
    //Color attackLineColor;
    Ray aimRay;
    [SerializeField] LayerMask mask;
    Ray inputRay;

    [SerializeField] int bonus = 0;
    [SerializeField] int maxBonus = 5;
    [SerializeField] float bonusInterval = 1f;
    float timer;
    
    float range = 50f;
    int enemyLayer = 1 << 8;

    // Use this for initialization
    void Start ()
    {
        reticleScript = reticle.GetComponent<ReticleScript>();
        //attackLineColor = Color.red;
	}
	
	// Update is called once per frame
	void Update ()
    {
        aimRay = Camera.main.ScreenPointToRay(reticle.position);
        inputRay = Camera.main.ScreenPointToRay(reticleScript.GetMousePositionVector());
        //aimRay.origin = playerViewObject.position;
        //aimRay.direction = Vector3.Magnitude(playerViewObject.position + reticle.position);
        gazeRay.position = aimRay.origin;
        gazeRay.forward = aimRay.direction;
        Debug.DrawRay(aimRay.origin, aimRay.direction);
        //RaycastHit hit;
        
        /*if (Physics.Raycast(aimRay.origin, aimRay.direction, out hit, 500f, mask))
        {
            if (hit.collider.tag == "Enemy")
            {
                target = hit.collider.transform;
                timer += Time.deltaTime;
                if (timer > bonusInterval && bonus < maxBonus)
                {
                    timer = 0;
                    bonus++;
                }
            }
        }*/
        if (lookingAtTarget == true)
        {
            timer += Time.deltaTime;
            if (timer > bonusInterval && bonus < maxBonus)
            {
                timer = 0;
                bonus++;
            }
        }
        else
        {
            timer = 0;
            if (target != null)
            {
                Debug.Log("Fire at:" + target.name);
                //Handles.DrawBezier(playerBody.position, target.position, Vector3.left, Vector3.up, attackLineColor, attackLineTexture, 5);
                firedProjectile = Instantiate<GameObject>(projectileOriginal);
                firedProjectile.transform.position = transform.position;
                firedProjectile.GetComponent<ProjectileScript>().SetProjectileTarget(target);
                if (bonus > 0)
                {
                    firedProjectile.GetComponent<ProjectileScript>().SetProjectileBoost(bonus);
                }
                target = null;
            }
            bonus = 0;
        }
        //Debug.Log("Target = " + target.name);
    }

    public void SetTarget(Transform targetToSet)
    {
        target = targetToSet;
        lookingAtTarget = true;
    }

    public void LookAway()
    {
        lookingAtTarget = false;
    }

    public Ray GetInputRay()
    {
        return inputRay;
    }
}
