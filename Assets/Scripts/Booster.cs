using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Booster : MonoBehaviour
{
    public enum Rarity
    {
        Bronze,
        Silver,
        Gold,
        SHINY,
    }
    private float[,] weights = new float[4,7] 
        {{0.5f,0.15f,0.15f,0.1f,0.1f,0,0},
        {0.5f,0.15f,0.15f,0.1f,0.1f,0,0},
        {0.5f,0.15f,0.15f,0.1f,0.1f,0,0},
        {0.5f,0.15f,0.15f,0.1f,0.1f,0,0}};
    [SerializeField] private GameObject[] sachets;
    [SerializeField] private Rarity rarity;
    [SerializeField] private XRGrabInteractable left;
    private Rigidbody leftRb;
    private Vector3 leftVelocity;
    private Vector3 leftPrevPos;
    [SerializeField] private XRGrabInteractable right;
    private Rigidbody rightRb;
    private Vector3 rightVelocity;
    private Vector3 rightPrevPos;
    private float leftRightDist;
    private bool leftGrabbed = false;
    private bool rightGrabbed = false;
    private bool attached = true;
    private bool purchased = false;
    public BoosterSpawner origin;
    private float price;
    private XRGrabInteractable latestGrabbed;
    // Start is called before the first frame update
    void Start()
    {
        leftRightDist = Vector3.Distance(right.transform.position,left.transform.position);
        leftPrevPos = left.transform.position;
        rightPrevPos = right.transform.position;
        leftRb = left.GetComponent<Rigidbody>();
        rightRb = right.GetComponent<Rigidbody>();
        switch (rarity) //TODO équilibrer
        {
            case Rarity.Bronze: price = 5; break;
            case Rarity.Silver: price = 15; break;
            case Rarity.Gold: price = 50; break;
            case Rarity.SHINY: price = 200; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        left.enabled = (GameManager.Instance.money >= price) || purchased;
        right.enabled = (GameManager.Instance.money >= price) || purchased;
        leftVelocity = (left.transform.position - leftPrevPos) / Time.deltaTime;
        rightVelocity = (right.transform.position - rightPrevPos) / Time.deltaTime;
        leftRb.isKinematic = (rightGrabbed || latestGrabbed == left) && attached;
        rightRb.isKinematic = (leftGrabbed || latestGrabbed == right) && attached;
        if (left.transform.parent == right) left.transform.localRotation = Quaternion.identity;
        if (right.transform.parent == left) right.transform.localRotation = Quaternion.identity;
        if (attached)
        {
            if (leftGrabbed && rightGrabbed && Vector3.Dot(leftVelocity, rightVelocity) < -4f)
            {
                SpawnSeeds();
                left.transform.SetParent(null);
                right.transform.SetParent(null);
                attached = false;
            }
        }
        leftPrevPos = left.transform.position;
        rightPrevPos = right.transform.position;   
    }

    public void GrabLeft()
    {
        latestGrabbed = left;
        if (!purchased)
        {
            purchased = true;
            GameManager.Instance.money -= price;
            origin.pickedBooster();
        }
        leftGrabbed = true;
        if (attached)
        {
            if (!rightGrabbed)
            {
                right.transform.SetParent(left.transform);
                rightRb.MovePosition(left.transform.position + left.transform.right * leftRightDist);
            }
            else 
            {
                right.transform.SetParent(null);
                left.transform.SetParent(null);
            }
        }
        
    }
    public void ReleaseLeft()
    {
        if (rightGrabbed) latestGrabbed = right;
        if (attached) 
        {
            right.transform.SetParent(null);
            left.transform.SetParent(right.transform);
            left.transform.localPosition = - leftRightDist * Vector3.right / right.transform.localScale.x;
            left.transform.localRotation = Quaternion.identity;
            left.transform.localScale = Vector3.one;
        }
        leftGrabbed = false;
        if (!attached)
        {
            left.transform.SetParent(null);
            right.transform.SetParent(null);
        }
    }

    public void GrabRight()
    {
        latestGrabbed = right;
        if (!purchased)
        {
            purchased = true;
            GameManager.Instance.money -= price;
            origin.pickedBooster();
        }
        rightGrabbed = true;
        if (attached)
        {
            if (!leftGrabbed)
            {
                left.transform.SetParent(right.transform);
                leftRb.MovePosition(right.transform.position - right.transform.right * leftRightDist);
            }
            else 
            {
                right.transform.SetParent(null);
                left.transform.SetParent(null);
            }
        }
    }
    public void ReleaseRight()
    {
        if (leftGrabbed) latestGrabbed = left;
        if (attached) 
        {
            left.transform.SetParent(null);
            right.transform.SetParent(left.transform);
            right.transform.localPosition = leftRightDist * Vector3.right / left.transform.localScale.x;
            right.transform.localRotation = Quaternion.identity;
            right.transform.localScale = Vector3.one;
        }
        rightGrabbed = false;
        if (!attached)
        {
            left.transform.SetParent(null);
            right.transform.SetParent(null);
        }
    }

    void SpawnSeeds()
    {
        var numberOfSeeds = 0;
        //TODO faire l'équilibrage
        switch (rarity)
        {
            case Rarity.Bronze: numberOfSeeds = Random.Range(3,5); break;
            case Rarity.Silver: numberOfSeeds = Random.Range(3,5); break;
            case Rarity.Gold: numberOfSeeds = Random.Range(3,5); break;
            case Rarity.SHINY: numberOfSeeds = Random.Range(3,5); break;
        }
        for (int k = 0; k < numberOfSeeds; k++)
        {
            var rd = Random.Range(0f,1f);
            var index = 0;
            while (rd - weights[(int)rarity,index] > 0)
            {
                rd -= weights[(int)rarity,index];
                index++;
            }
            var sachet = Instantiate(sachets[index], (left.transform.position + right.transform.position) / 2f, Quaternion.identity);
            sachet.GetComponent<Rigidbody>().AddForce(0.1f * Random.onUnitSphere, ForceMode.Impulse);
        }
        StartCoroutine(DespawnGarbage());
    }

    IEnumerator DespawnGarbage()
    {
        var leftRend = left.GetComponent<Renderer>();
        var rightRend = right.GetComponent<Renderer>();
        var timeEllapsed = 0f;
        while (timeEllapsed < 8f)
        {
            leftRend.material.color = new Color(leftRend.material.color.r, leftRend.material.color.g, leftRend.material.color.b, Mathf.Lerp(0,1,(8 - timeEllapsed)/3f));
            rightRend.material.color = new Color(rightRend.material.color.r, rightRend.material.color.g, rightRend.material.color.b, Mathf.Lerp(0,1,(8 - timeEllapsed)/3f));
            timeEllapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(left.gameObject);
        Destroy(right.gameObject);
        Destroy(gameObject);
    }
}
