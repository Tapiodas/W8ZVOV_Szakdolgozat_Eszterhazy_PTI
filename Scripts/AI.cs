using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Barracuda;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Integrations.Match3;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;

using UnityEngine;


public class AI : Agent
{

    //NeuralNetwork nn;
    //float[] inputsToNN = {};
    //float[] outputsFromNN;


    //public int numRaycasts;
    //public float angleBetweenRaycasts;
    //public float viewDistance;

    //float horizontal;
    //float vertical;

    //float rotateHorizontal;
    //float rotateVertical;
    //float rotateZAxis;

    float yaw;
    float pitch;
    float roll;

    public float moveSpeed;
    public float rotationSpeed;

    public Rigidbody rb;
    public LineDrawer ld;

  

    public string OwnWall;
    public string EnemyWall;


    public GameObject enemyAgent;

    //public GameObject startingpos;
    public RayPerceptionSensorComponent3D[] rayPerceptionSensors;
    public RayPerceptionSensorComponent3D rayPerceptionSensorkozepso;
    public RayPerceptionSensorComponent3D rayPerceptionSensoralso;
    public RayPerceptionSensorComponent3D rayPerceptionSensorfelso;

    public TextMeshPro NN;

    private string nev;

    UtkozesEnum utkozesEnum;
    public EpisodManager episodManager;

    public UtkozesEnum UtkozesEnum1 { get => utkozesEnum; set => utkozesEnum = value; }

    public enum UtkozesEnum
    {   start,
        PalyaFalnakUtkozott,
        EllensegFalnakUtkozott,
        SajatFalnakUtkozott
    }



    public override void OnEpisodeBegin()
    {
        nev = this.name;
        // NN.text = GetComponent<Unity.MLAgents.Policies.BehaviorParameters>().BehaviorName;

        episodManager.WinState1 = EpisodManager.WinState.None;
        episodManager.Utkozes = false;

        rb = GetComponent<Rigidbody>();
        ld = GetComponent<LineDrawer>();
        rb.velocity = Vector3.zero; 
        ld.besierPontok = new List<Vector3>();
        ld.Wallparent = new GameObject();
        ld.Wallparent.name = "Wallparent";
        ld.Wallparent.tag = "Wallparent";
        rayPerceptionSensors = gameObject.GetComponents<RayPerceptionSensorComponent3D>();
      
        // nn = GetComponent<NeuralNetwork>();
        float rx = UnityEngine.Random.Range(-5, 5);
        float rz = UnityEngine.Random.Range(-5, 5);
        float ry = UnityEngine.Random.Range(-5, 5);

       
       // transform.localPosition = startingpos.transform.localPosition;
        transform.localPosition = new Vector3(rx, ry, rz);
        ld.stopCoroutine = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.transform.localPosition);
        sensor.AddObservation(rayPerceptionSensors[0]);
        sensor.AddObservation(rayPerceptionSensors[1]);
        sensor.AddObservation(rayPerceptionSensors[2]);

    }


    private int movement(int input)
    {

        switch (input)
        {
            case 0:
                // code block
                return -1;
            case 1:
                // code block
                return 0;
            case 2:
                // code block
                return 1;
            default:
                // code block
                break;
        }

        return 0;
    }
    //az akciókat irányítja
    public override void OnActionReceived(ActionBuffers actions)
    {
        float a = actions.DiscreteActions[0];
        float rotateHorizontal = movement(actions.DiscreteActions[0]);
        float rotateVertical = movement(actions.DiscreteActions[1]);
        float rotateZAxis = movement(actions.DiscreteActions[2]);

       // Debug.Log("rotateHorizontal " + a+ " rotateVertical " + rotateVertical+ " rotateZAxis " + rotateZAxis);
      //  Debug.Log("rotateHorizontal " + a);




       // Debug.Log(rotateHorizontal+ "  "+ rotateVertical+" "+ rotateZAxis);
        Move(rotateHorizontal, rotateVertical, rotateZAxis) ;
     
    }

    //ha én akarom irányítani
    public override void Heuristic(in ActionBuffers actionsOut)
    {
       // ActionSegment<float> continousACtions = actionsOut.ContinuousActions;

    }





    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(nev + " name");

        if (other.gameObject.CompareTag("MapWall"))
        {
            utkozesEnum = UtkozesEnum.PalyaFalnakUtkozott;


            episodManager.WinState1 = (EpisodManager.WinState)Enum.Parse(typeof(EpisodManager.WinState), nev + "PalyaFalnakUtkozott");






        } else if (other.gameObject.CompareTag(EnemyWall))
        {
            utkozesEnum = UtkozesEnum.EllensegFalnakUtkozott;

            episodManager.WinState1 = (EpisodManager.WinState)Enum.Parse(typeof(EpisodManager.WinState), nev + "EllenfelFalnakUtkozott");



        }
        else if (other.gameObject.CompareTag(OwnWall))
        {
            utkozesEnum = UtkozesEnum.SajatFalnakUtkozott;

            episodManager.WinState1 = (EpisodManager.WinState)Enum.Parse(typeof(EpisodManager.WinState), nev + "SajatFalnakUtkozott");

        }
        else if (other.gameObject.CompareTag("Agent"))
        {
            utkozesEnum = UtkozesEnum.EllensegFalnakUtkozott;
            episodManager.WinState1 = (EpisodManager.WinState)Enum.Parse(typeof(EpisodManager.WinState), nev + "EllenfelFalnakUtkozott");


        }


        // Debug.Log(utkozesEnum.ToString());


    }





    public void End()
    {
        EndEpisode();
    }





        public void Move(float rotateHorizontal, float rotateVertical, float rotateZAxis)
    {
        
       
        rb.AddForce(transform.forward * moveSpeed * Time.deltaTime,ForceMode.Impulse);
       
        yaw = rotateHorizontal * rotationSpeed;
        pitch = -rotateVertical * rotationSpeed;
        roll = -rotateZAxis * rotationSpeed;

     
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(pitch, yaw, roll) * Time.deltaTime);
      
        rb.MoveRotation(rb.rotation * deltaRotation);
    }











}
