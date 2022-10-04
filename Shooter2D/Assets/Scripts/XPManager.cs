using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    private Rigidbody2D xpRigidBody;
    [SerializeField] private float xpSpeed;
    [SerializeField] private int xpAmount;
    

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        xpRigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        StartCoroutine(IncreaseXPSpeedOverTime());
        TargetAndMove();
    }
    public void TargetAndMove()
    {
        if (playerObject != null)
        {
            var playerTransform = playerObject.transform;
            var xpTransform = transform;

            var direction = (playerTransform.position - xpTransform.position).normalized;
            xpTransform.right = Vector3.Slerp(xpTransform.right, direction, 1f);
            
            xpRigidBody.velocity = xpTransform.right * xpSpeed;
        }
    }

    IEnumerator IncreaseXPSpeedOverTime()
    {
        
        yield return new WaitForSeconds(0.5f);
        xpSpeed++;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "XPController")
        {
            GameSystemManager.Instance.IncreaseXP(1);
            Destroy(this.gameObject);
        }
    }

}
