using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public List<GameObject> bubles = new List<GameObject>();
    public GameObject head;
    public Camera camera;
    public float speed;
    public float horizontalSpeed;
    public float bubleDistance;
    public GameObject bubleSource;
    public GameObject text;
    public GameObject audioPlus;
    public GameObject audioCube;
    public GameObject particlesCubeSource;
    public GameObject particlesPulseSource;

    private Rigidbody _headRigidBody;
    private LinkedList<Vector3> _nodePoints = new LinkedList<Vector3>();
    private AudioSource _audioCube;
    private AudioSource _audioPlus;
    private List<GameObject> _particalList = new List<GameObject>();

    void Start()
    {
        _headRigidBody = head.GetComponent<Rigidbody>();
        _audioCube = audioCube.GetComponent<AudioSource>();
        _audioPlus = audioPlus.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()

    {
        _headRigidBody.velocity = new Vector3(0, 0, speed);
    
        text.GetComponent<Text>().text = (bubles.Count + 1).ToString();

        if ((head.transform.position.z - 20) > camera.transform.position.z)
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, head.transform.position.z - 20);
       
        if (Input.GetButton("Horizontal"))
        {
            //Debug.Log("Horizontal button pressed");

            int delta;
            if (Input.GetAxis("Horizontal") > 0) delta = 1; else delta = -1;

            head.transform.position = new Vector3(head.transform.position.x + horizontalSpeed * delta * Time.deltaTime , head.transform.position.y, head.transform.position.z);

            _nodePoints.AddFirst(head.transform.position);
            //Debug.Log(_nodePoints.Count);
      
        };


        if (Input.GetButtonDown("+"))
        {
            //Debug.Log("+ button pressed");

            bubles.Add(Instantiate(bubleSource));
        };

        if (Input.GetButtonDown("-"))
        {
            //Debug.Log("- button pressed");
            if (bubles.Count > 0)
            {
                Destroy(bubles[bubles.Count - 1]);
                bubles.RemoveAt(bubles.Count - 1);
            }
        };

        if (bubles.Count > 0)
        {
            for (int i = 0; i < bubles.Count; ++i)
            {
                bubles[i].transform.position = new Vector3(bubles[i].transform.position.x, bubles[i].transform.position.y, head.transform.position.z - (i + 1) * bubleDistance);

                foreach (Vector3 node in _nodePoints)
                {
                    if (bubles[i].transform.position.z > node.z)
                    {
                        break;
                    }

                    bubles[i].transform.position = new Vector3(node.x, bubles[i].transform.position.y, bubles[i].transform.position.z);

                }
            }
        };

    }

    public void PlusCollision (Collider collision)
    {
        for (int i = 0; i < collision.gameObject.GetComponent<PlusScript>().nominal; ++i)
            bubles.Add(Instantiate(bubleSource));
        Destroy(collision.gameObject);

        GameObject newParticals = Instantiate(particlesPulseSource);
        newParticals.transform.position = head.transform.position;
        newParticals.GetComponent<ParticleSystem>().Emit(50);
        Debug.Log("PlusCollisionEvent");
        Debug.Log(bubles.Count);

        _audioPlus.Play();
    }

    public void CubeCollision(Collider collision)
    {
        if (bubles.Count > 0)
        {
            
            if (--collision.gameObject.GetComponent<Cube>().nominal == 0)
                Destroy(collision.gameObject);

            GameObject newParticals = Instantiate(particlesCubeSource);
            newParticals.transform.position = head.transform.position;
            newParticals.GetComponent<ParticleSystem>().Emit(50);
            Debug.Log("ParticleCollisionEvent");

            head.transform.position = bubles[0].transform.position;

            Destroy(bubles[bubles.Count - 1]);
            _audioCube.Play();
            bubles.RemoveAt(bubles.Count - 1);
            Debug.Log("CubeCollisionEvent");
            Debug.Log(bubles.Count);

        }
    }

    /* public void CubeCollision(Collision collision)
    {
        if (bubles.Count > 0)
        {

            if (--collision.gameObject.GetComponent<Cube>().nominal == 0)
                Destroy(collision.gameObject);

            head.transform.position = bubles[0].transform.position;

            Destroy(bubles[bubles.Count - 1]);
            bubles.RemoveAt(bubles.Count - 1);

        }
    }*/
}
