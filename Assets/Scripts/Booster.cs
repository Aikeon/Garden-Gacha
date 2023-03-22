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
        Gold
    }

    [Header("Graines")]
    [SerializeField] private Graine[] grainesBronze;
    [SerializeField] private Graine[] grainesArgent;
    [SerializeField] private Graine[] grainesOr;
    private Graine _choosenGraine;
    private bool graineIsKnown = false;
    
    [Header("Rarerté")]
    [SerializeField] private Material sachetBronze;
    [SerializeField] private Material sachetArgent;
    [SerializeField] private Material sachetOr;
    public Rarity rarity;
    
    
    [SerializeField] private XRGrabInteractable left;
    private Rigidbody leftRb;
    private Vector3 leftVelocity;
    private Vector3 leftPrevPos;
    [SerializeField] private XRGrabInteractable right;
    private Rigidbody rightRb;
    private Vector3 rightVelocity;
    private Vector3 rightPrevPos;
    [SerializeField] private GameObject middle;
    private Vector3 middleBaseScale;
    [SerializeField] Transform topReference;
    [SerializeField] Transform botReference;
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
        middleBaseScale = middle.transform.localScale;
        leftRightDist = Vector3.Distance(right.transform.position,left.transform.position);
        leftPrevPos = left.transform.position;
        rightPrevPos = right.transform.position;
        leftRb = left.GetComponent<Rigidbody>();
        rightRb = right.GetComponent<Rigidbody>();
        
    }

    public void StartMaterial(Rarity _rarity)
    {
        rarity = _rarity;
        switch (rarity) 
        {
            case Rarity.Bronze: 
                price = 5;
                left.GetComponent<Renderer>().material = sachetBronze;
                right.GetComponent<Renderer>().material = sachetBronze;
                middle.GetComponent<Renderer>().material = sachetBronze;
                break;
            case Rarity.Silver: 
                price = 10; 
                left.GetComponent<Renderer>().material = sachetArgent;
                right.GetComponent<Renderer>().material = sachetArgent;
                middle.GetComponent<Renderer>().material = sachetArgent;
                break;
            case Rarity.Gold: 
                price = 25; 
                left.GetComponent<Renderer>().material = sachetOr;
                right.GetComponent<Renderer>().material = sachetOr;
                middle.GetComponent<Renderer>().material = sachetOr;
                break;
        }
        SetGraine();
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
        Debug.Log((topReference.localPosition.y - botReference.localPosition.y));
        middle.transform.localEulerAngles = Mathf.Atan((topReference.localPosition.x + botReference.localPosition.x) / (topReference.localPosition.y - botReference.localPosition.y)) * 180 / Mathf.PI * Vector3.forward;
        // middle.transform.position = (left.transform.position + right.transform.position)/2f;
        // middle.transform.localEulerAngles = (left.transform.localEulerAngles + right.transform.localEulerAngles) / 2f;
        // middle.transform.localEulerAngles = Vector3.Cross(left.transform.position - right.transform.position, left.transform.up);
        // middle.transform.localScale = new Vector3(Vector3.Distance(left.transform.position, right.transform.position) - 0.1f,middleBaseScale.y,middleBaseScale.z);
        if (left.transform.parent == right) left.transform.localRotation = Quaternion.identity;
        if (right.transform.parent == left) right.transform.localRotation = Quaternion.identity;
        if (attached)
        {
            if (leftGrabbed && rightGrabbed && Vector3.Dot(leftVelocity, rightVelocity) < -4f)
            {
                SpawnSeeds();
                Destroy(middle);
                left.transform.SetParent(null);
                right.transform.SetParent(null);
                attached = false;
            }
        }
        leftPrevPos = left.transform.position;
        rightPrevPos = right.transform.position;   
    }

    private void SetGraine()
    {
        switch (rarity)
        {
            case Rarity.Bronze:
                int r = Random.Range(0, 100);
                switch (r)
                {
                    case < 85:
                    {
                        int i = Random.Range(0, grainesBronze.Length);
                        _choosenGraine = grainesBronze[i];
                        break;
                    }
                    case < 98:
                    {
                        int j = Random.Range(0, grainesArgent.Length);
                        _choosenGraine = grainesArgent[j];
                        break;
                    }
                    default:
                    {
                        int k = Random.Range(0, grainesOr.Length);
                        _choosenGraine = grainesOr[k];
                        break;
                    }
                }
                break;
            case Rarity.Silver:
                int s = Random.Range(0, 100);
                switch (s)
                {
                    case < 15:
                    {
                        int i = Random.Range(0, grainesBronze.Length);
                        _choosenGraine = grainesBronze[i];
                        break;
                    }
                    case < 90:
                    {
                        int j = Random.Range(0, grainesArgent.Length);
                        _choosenGraine = grainesArgent[j];
                        break;
                    }
                    default:
                    {
                        int k = Random.Range(0, grainesOr.Length);
                        _choosenGraine = grainesOr[k];
                        break;
                    }
                }
                break;
            case Rarity.Gold:
                int t = Random.Range(0, 100);
                switch (t)
                {
                    case < 3:
                    {
                        int i = Random.Range(0, grainesBronze.Length);
                        _choosenGraine = grainesBronze[i];
                        break;
                    }
                    case < 40:
                    {
                        int j = Random.Range(0, grainesArgent.Length);
                        _choosenGraine = grainesArgent[j];
                        break;
                    }
                    default:
                    {
                        int k = Random.Range(0, grainesOr.Length);
                        _choosenGraine = grainesOr[k];
                        break;
                    }
                }
                break;
        }
    }

    public void SetMaterial()
    {
        if (graineIsKnown) return;
        left.GetComponent<Renderer>().material = _choosenGraine.vegData.boosterMaterial;
        right.GetComponent<Renderer>().material = _choosenGraine.vegData.boosterMaterial;
        middle.GetComponent<Renderer>().material = _choosenGraine.vegData.boosterMaterial;
        graineIsKnown = true;
    }

    public void Hover()
    {
        if (!purchased)
            switch (rarity)
            {
                case Rarity.Bronze: TheGuyBehaviour.Instance.state = 1; break;
                case Rarity.Silver: TheGuyBehaviour.Instance.state = 2; break;
                case Rarity.Gold: TheGuyBehaviour.Instance.state = 3; break;
            }
    }

    public void StopHover()
    {
        if (!purchased)
        {
            TheGuyBehaviour.Instance.state = 0;
        }
    }

    public void GrabLeft()
    {
        latestGrabbed = left;
        if (!purchased)
        {
            purchased = true;
            GameManager.Instance.money -= price;
           // TheGuyBehaviour.Instance.state = 0;
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
          //  TheGuyBehaviour.Instance.state = 0;
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
        }
        for (int k = 0; k < numberOfSeeds; k++)
        {
            var sachet = Instantiate(_choosenGraine, (left.transform.position + right.transform.position) / 2f, Quaternion.identity);
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
