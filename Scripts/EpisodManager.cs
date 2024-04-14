
using TMPro;
using Unity.MLAgents;

using UnityEngine;
using UnityEngine.Serialization;
using static AI;

public class EpisodManager : MonoBehaviour
{

    [FormerlySerializedAs("Player1")][FormerlySerializedAs("PlayerPiros")][Header("AI Settings")] public AI AgentPiros;
    [FormerlySerializedAs("Player2")][FormerlySerializedAs("PlayerSarga")] public AI AgentSarga;


    //public GameObject sarga;
    // public GameObject piros;

    // public GameObject sargahalal;
    //public GameObject piroshalal;


    AI AgentPirosAI;
    AI AgentSargaAI;
    LineDrawer AgentPirosAIld;
    LineDrawer AgentSargaAIld;

    private WinState winState1;
    Endstate endstate;

    public GameObject PirosScorereward;
    public GameObject SargaScorereward;
    public TextMeshPro MaxIdo;
    public TextMeshPro Utolsoido;
    public float ido;
    public float maxido;
    public GameObject sargaleesopoz;
    public GameObject pirosleesopoz;
    // [Header("Human Settings")] public bool humanIsPlaying;

    public GameObject sargasajatfalhalal;
    public GameObject sargapalyafalhalal;
    public GameObject saragamasikfalhalal;

    public GameObject pirossajatfalhalal;
    public GameObject pirospalyafalhalal;
    public GameObject pirosmasikfalhalal;

    public GameObject sarga;
    public GameObject piros;

    public TextMeshPro PirosScore;
    public TextMeshPro SargaScore;
    public float Pirossc;
    public float Sargasc;

    private float timerward = 0.01f;
    private float Sajatfalnakutkozott = -0.45f;
    private float MasikSajatfalnakutkozott = 1f;
    private float PalyaFalnakUtkozott = -0.25f;
    private float MasikPalyaFalnakUtkozott = 1f;
    private float Ellenfelfalanakutkozott = -0.75f;
    private float MasikEllenfelfalanakutkozott = 100f;


    private bool utkozes;

    public WinState WinState1
    {
        get => winState1;
        set
        {
            if (!Utkozes) // Ha az utkozes értéke false
            {
               // Debug.Log("utkozestortent");
                Utkozes = true;
                winState1 = value; // Beállítjuk a WinState1 értékét
                
            }
        }
    }

    public bool Utkozes { get => utkozes; set => utkozes = value; }

    public enum WinState
    {
        SargaPalyaFalnakUtkozott,
        SargaSajatFalnakUtkozott,
        SargaEllenfelFalnakUtkozott,
        PirosPalyaFalnakUtkozott,
        PirosSajatFalnakUtkozott,
        PirosEllenfelFalnakUtkozott,
        None

    }

    

    public enum Endstate
    {
        Torles,
        Vege,
        None

    }

    // Start is called before the first frame update
    void Awake()
    {
        maxido = 0;
        ido = 0;
        winState1 = WinState.None;
        endstate = Endstate.None;
        AgentPirosAI = AgentPiros.GetComponent<AI>();
        AgentSargaAI = AgentSarga.GetComponent<AI>();
        AgentPirosAIld = AgentPiros.GetComponent<LineDrawer>();
        AgentSargaAIld = AgentSarga.GetComponent<LineDrawer>();

    }
    //public GameObject MaxIdo;
    //public float ido;
    //public float maxido;
    // Update is called once per frame
    void FixedUpdate()
    {
       
        try
        {
            ido+=Time.fixedDeltaTime;
           // Debug.Log((Mathf.Round(ido * 1000f) / 1000f).ToString());

            switch (endstate)
            {
                case Endstate.Torles:
                    
                    Torles();
                    break;
                case Endstate.Vege:
                   
                    
                    Vege();


                    break;
                case Endstate.None:
                    jutalmazas(winState1);
                    break;
                default:
                    break;
            }

        }
        catch (System.Exception)
        {

            throw;
        }



    }



    //public void gyozelemvalto(WinState winState)
    //{
    //    if (AgentSargaAI.UtkozesEnum1 == UtkozesEnum.PalyaFalnakUtkozott)
    //    {
    //        winState = WinState.SargaPalyaFalnakUtkozott;
    //        jutalmazas(winState);
    //    }
    //    else if (AgentSargaAI.UtkozesEnum1 == UtkozesEnum.SajatFalnakUtkozott)
    //    {
    //        winState = WinState.SargaSajatFalnakUtkozott;
    //        jutalmazas(winState);
    //    }
    //    else if (AgentSargaAI.UtkozesEnum1 == UtkozesEnum.EllensegFalnakUtkozott)
    //    {
    //        winState = WinState.SargaEllenfelFalnakUtkozott;
    //        jutalmazas(winState);
    //    }
    //    else if (AgentPirosAI.UtkozesEnum1 == UtkozesEnum.PalyaFalnakUtkozott)
    //    {
    //        winState = WinState.PirosPalyaFalnakUtkozott;
    //        jutalmazas(winState);
    //    }
    //    else if (AgentPirosAI.UtkozesEnum1 == UtkozesEnum.SajatFalnakUtkozott)
    //    {
    //        winState = WinState.PirosSajatFalnakUtkozott;
    //        jutalmazas(winState);
    //    }
    //    else if (AgentPirosAI.UtkozesEnum1 == UtkozesEnum.PalyaFalnakUtkozott)
    //    {
    //        winState = WinState.PirosEllenfelFalnakUtkozott;
    //        jutalmazas(winState);
    //    }

    //}


    //public enum Player
    //{
    //    Piros,
    //    Sarga
    //}


    //public Player currentPlayer;

    //public Player humanPlayer;

    //private AI GetCurrentAgent()
    //{
    //    if (currentPlayer == AgenPiros.type) return AgenPiros;
    //    else return AgentSarga;
    //}





    private static void TronWinTrack(WinState winState)
    {
        switch (winState)
        {//academy kell a adatgyűjtéshez
            case WinState.SargaPalyaFalnakUtkozott:
                Academy.Instance.StatsRecorder.Add("Prios/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/PalyaFalnakUtkozott", 1, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);


                break;
            case WinState.SargaSajatFalnakUtkozott:
                Academy.Instance.StatsRecorder.Add("Prios/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/sajatFalnakUtkozott", 1, StatAggregationMethod.Average);
                break;
            case WinState.SargaEllenfelFalnakUtkozott:
                Academy.Instance.StatsRecorder.Add("Prios/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/EllenfelFalnakUtkozott", 1, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);
                break;
            case WinState.PirosPalyaFalnakUtkozott:
                Academy.Instance.StatsRecorder.Add("Prios/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/PalyaFalnakUtkozott", 1, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);
                break;
            case WinState.PirosSajatFalnakUtkozott:
                Academy.Instance.StatsRecorder.Add("Prios/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/sajatFalnakUtkozott", 1, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);

                break;
            case WinState.PirosEllenfelFalnakUtkozott:
                Academy.Instance.StatsRecorder.Add("Prios/EllenfelFalnakUtkozott", 1, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/EllenfelFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/PalyaFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Prios/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/sajatFalnakUtkozott", 0, StatAggregationMethod.Average);
                break;


        }
    }


    //public void getReward(float increment)
    //{
    //    //  AddReward(increment);
    //    score++;
    //    scoreText.text = score + "";

    //}



    public void jutalmazas(WinState winState)
    {
       // Debug.Log("jutalmazasfazis");

        //Instantiate(SargaScorereward, new Vector3(-9, 18, 0), Quaternion.identity);
        //Instantiate(PirosScorereward, new Vector3(-9, 18, 0), Quaternion.identity);


        //GameObject szulo = new GameObject();
        AgentPiros.SetReward(timerward); 
        AgentSarga.SetReward(timerward);

        
       // Debug.Log(winState.ToString() + " jutalmazasban");
        if (winState == WinState.SargaSajatFalnakUtkozott)
        {
           


           // Instantiate(sargasajatfalhalal, sarga.transform.position, sargasajatfalhalal.transform.rotation);
            AgentSarga.SetReward(Sajatfalnakutkozott);
            Sargasc += Sajatfalnakutkozott;
            AgentPiros.SetReward(MasikSajatfalnakutkozott);
            Pirossc += MasikSajatfalnakutkozott;
            TronWinTrack(winState);


            SargaScorereward.GetComponent<TextMeshPro>().text = Sajatfalnakutkozott.ToString();
            Instantiate(SargaScorereward, sargaleesopoz.transform.position, Quaternion.identity);
            PirosScorereward.GetComponent<TextMeshPro>().text = MasikSajatfalnakutkozott.ToString();
            Instantiate(PirosScorereward, pirosleesopoz.transform.position, Quaternion.identity);


        }
        else if (winState == WinState.SargaPalyaFalnakUtkozott)
        {
            //Instantiate(sargasajatfalhalal, sarga.transform.position, sargasajatfalhalal.transform.rotation);
            AgentSarga.SetReward(PalyaFalnakUtkozott);
            Sargasc += PalyaFalnakUtkozott;
            AgentPiros.SetReward(MasikPalyaFalnakUtkozott);
            Pirossc += MasikPalyaFalnakUtkozott;
            TronWinTrack(winState);

            SargaScorereward.GetComponent<TextMeshPro>().text = PalyaFalnakUtkozott.ToString();
            Instantiate(SargaScorereward, sargaleesopoz.transform.position, Quaternion.identity);
            PirosScorereward.GetComponent<TextMeshPro>().text = MasikPalyaFalnakUtkozott.ToString();
            Instantiate(PirosScorereward, pirosleesopoz.transform.position, Quaternion.identity);

        }
        else if (winState == WinState.SargaEllenfelFalnakUtkozott)
        {
           // Debug.Log("sarga az ellenfel falanak ment");
            // Instantiate(saragamasikfalhalal, sarga.transform.position, saragamasikfalhalal.transform.rotation);

            // EditorApplication.isPaused = true;
            AgentSarga.SetReward(Ellenfelfalanakutkozott);
            Sargasc += Ellenfelfalanakutkozott;
            AgentPiros.SetReward(MasikEllenfelfalanakutkozott);
            Pirossc += MasikEllenfelfalanakutkozott;
            TronWinTrack(winState);

            SargaScorereward.GetComponent<TextMeshPro>().text = Ellenfelfalanakutkozott.ToString();
            Instantiate(SargaScorereward, sargaleesopoz.transform.position, Quaternion.identity);
            PirosScorereward.GetComponent<TextMeshPro>().text = MasikEllenfelfalanakutkozott.ToString();
            Instantiate(PirosScorereward, pirosleesopoz.transform.position, Quaternion.identity);

           

        }
        else if (winState == WinState.PirosSajatFalnakUtkozott)
        {
          
         //   Instantiate(pirossajatfalhalal, piros.transform.position, pirossajatfalhalal.transform.rotation);
            AgentPiros.SetReward(Sajatfalnakutkozott);
            Pirossc += Sajatfalnakutkozott;
            AgentSarga.SetReward(MasikSajatfalnakutkozott);
            Sargasc += MasikSajatfalnakutkozott;
            TronWinTrack(winState);

            SargaScorereward.GetComponent<TextMeshPro>().text = MasikSajatfalnakutkozott.ToString();
            Instantiate(SargaScorereward, sargaleesopoz.transform.position, Quaternion.identity);
            PirosScorereward.GetComponent<TextMeshPro>().text = Sajatfalnakutkozott.ToString();
            Instantiate(PirosScorereward, pirosleesopoz.transform.position, Quaternion.identity);

        }
        else if (winState == WinState.PirosPalyaFalnakUtkozott)
        {
         //   Instantiate(pirospalyafalhalal, piros.transform.position, pirospalyafalhalal.transform.rotation);
            AgentPiros.SetReward(PalyaFalnakUtkozott);
            Pirossc += PalyaFalnakUtkozott;
            AgentSarga.SetReward(MasikPalyaFalnakUtkozott);
            Sargasc += MasikPalyaFalnakUtkozott;
            TronWinTrack(winState);

            SargaScorereward.GetComponent<TextMeshPro>().text = MasikPalyaFalnakUtkozott.ToString();
            Instantiate(SargaScorereward, sargaleesopoz.transform.position, Quaternion.identity);
            PirosScorereward.GetComponent<TextMeshPro>().text = PalyaFalnakUtkozott.ToString();
            Instantiate(PirosScorereward, pirosleesopoz.transform.position, Quaternion.identity);


        }
        else if (winState == WinState.PirosEllenfelFalnakUtkozott)
        {
           // Debug.Log("piros az ellenfel falanak ment");

            //  Instantiate(pirosmasikfalhalal, piros.transform.position, pirosmasikfalhalal.transform.rotation);
            // EditorApplication.isPaused = true;
            AgentPiros.SetReward(Ellenfelfalanakutkozott);
            Pirossc += Ellenfelfalanakutkozott;
            AgentSarga.SetReward(MasikEllenfelfalanakutkozott);
            Sargasc += MasikEllenfelfalanakutkozott;
            TronWinTrack(winState);

            SargaScorereward.GetComponent<TextMeshPro>().text = Ellenfelfalanakutkozott.ToString();
            Instantiate(SargaScorereward, sargaleesopoz.transform.position, Quaternion.identity);
            PirosScorereward.GetComponent<TextMeshPro>().text = MasikEllenfelfalanakutkozott.ToString();
            Instantiate(PirosScorereward, pirosleesopoz.transform.position, Quaternion.identity);


        }

        if (winState != WinState.None)
        {
            endstate = Endstate.Torles;
            winState1 = WinState.None;

        }
       
       // Debug.Log("piros az ellenfel falanak ment");

        //TronWinTrack(winState);
        //Vege();

        Pirossc += timerward;
        Sargasc += timerward;


        PirosScore.text = (Mathf.Round(Pirossc * 100f) / 100f).ToString() ;
        SargaScore.text = (Mathf.Round(Sargasc * 100f) / 100f).ToString();

    }

    public void Torles()
    {
        //Debug.Log("vege");
        if (AgentPirosAIld.Wallparent.gameObject == null && AgentSargaAIld.Wallparent.gameObject == null)
        {
            
            endstate = Endstate.Vege;
        }
        else
        { 

           // Debug.Log(AgentPirosAIld.Wallparent.gameObject + " torles");

            AgentPirosAIld.Wallparent.gameObject.name = "WallparentNemtorlodottprios";
            AgentSargaAIld.Wallparent.gameObject.name = "WallparentNemtorlodottsarga";

            if (AgentPirosAIld.Wallparent.gameObject != null)
            {

                Destroy(AgentPirosAIld.Wallparent.gameObject);


            }


            if (AgentSargaAIld.Wallparent.gameObject != null)
            {

                Destroy(AgentSargaAIld.Wallparent.gameObject);
            }




            //Debug.Log(AgentPirosAIld.Wallparent.gameObject);

           

            AgentPirosAIld.linePointsGameobject.Clear();// törölni kell a fal tömböt
            AgentPirosAI.UtkozesEnum1 = UtkozesEnum.start;

            AgentSargaAIld.linePointsGameobject.Clear();// törölni kell a fal tömböt
            AgentSargaAI.UtkozesEnum1 = UtkozesEnum.start;

        }










    }
    public void Vege()
    {
        //Debug.Log("vege");



        if (AgentPirosAIld.Wallparent.gameObject == null && AgentSargaAIld.Wallparent.gameObject == null)
        {
            Utolsoido.text = "Utolsó Episód idő: " + (Mathf.Round(ido * 10f) / 10f).ToString()+" sec";
            if (maxido < ido) { maxido = ido; MaxIdo.text = "Leghoszabb epizód: " + (Mathf.Round(ido * 10f) / 10f).ToString() + " sec"; }
            ido = 0;


            AgentPiros.End();
            AgentSarga.End();
        }


        endstate = Endstate.None;
    }



   
}

    //public IEnumerator End()
    //{


    //    yield return new WaitForEndOfFrame();

    //    foreach (GameObject item in AgentPirosAIld.linePointsGameobject)
    //    {
    //        Destroy(item);
    //    }
    //    foreach (GameObject item in AgentSargaAIld.linePointsGameobject)
    //    {
    //        Destroy(item);
    //    }
    //    AgentPirosAIld.linePointsGameobject.Clear();// törölni kell a fal tömböt
    //    AgentPirosAI.UtkozesEnum1 = UtkozesEnum.start;

    //    AgentSargaAIld.linePointsGameobject.Clear();// törölni kell a fal tömböt
    //    AgentSargaAI.UtkozesEnum1 = UtkozesEnum.start;
    //}

