using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidTargeterCommunicator : MonoBehaviour {


    private static HumanoidTargeterCommunicator instance;

    private List<HumanoidTargeter> targeters;

    private List<Communicator> addEnemyMarkerCommunicators;
    private List<Communicator> deleteEnemyMarkerCommunicators;

    // Use this for initialization
    private void Awake()
    {
        targeters = new List<HumanoidTargeter>();
        addEnemyMarkerCommunicators = new List<Communicator>();
        deleteEnemyMarkerCommunicators = new List<Communicator>();
        instance = this;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	} 

    public static void AddBlackBoardSubscriber(HumanoidTargeter blackBoard){
        instance.targeters.Add(blackBoard);
    }

    public static void CommunicateAddEnemyMarker(HumanoidTargeter origin, EnemyMarker marker){
        //DeleteExistingDeleteEnemyMarker(marker);
        List<IEnumerator> addMarkerCoroutines = new List<IEnumerator>();
        foreach(HumanoidTargeter targeter in instance.targeters){
            if(targeter != origin && !targeter.AlreadyHasMarker(marker)){
                if(targeter.HasRadio() && origin.HasRadio()){
                    IEnumerator newCoroutine = CreateAddEnemyMarkerCoroutine(
                        targeter,
                        marker,
                        origin.GetTimeToCommunicateByRadio()
                    );
                    addMarkerCoroutines.Add(newCoroutine);
                    instance.StartCoroutine(newCoroutine);
                }else if (origin.CanCommunicate(targeter)){
                    IEnumerator newCoroutine = CreateAddEnemyMarkerCoroutine(
                        targeter,
                        marker,
                        origin.GetTimeToCommunicateByMouth()
                    );
                    addMarkerCoroutines.Add(newCoroutine);
                    instance.StartCoroutine(newCoroutine);
                }
            }
        }
        instance.addEnemyMarkerCommunicators.Add(new Communicator(
            marker, 
            addMarkerCoroutines
        ));
    }

    public static void InterruptAddEnemyMarker(HumanoidTargeter whoToInterrupt){
        for (int i = 0; i < instance.addEnemyMarkerCommunicators.Count; i++)
        {
            var communicator = instance.addEnemyMarkerCommunicators[i];
            if(communicator.enemyMarker.GetFounder() == whoToInterrupt){
                List<IEnumerator> coroutines = communicator.coroutines;
                coroutines.ForEach(c => instance.StopCoroutine(c));
                instance.addEnemyMarkerCommunicators.Remove(communicator);
            }
        }
    }

    public static void CommunicateDeleteEnemyMarker(HumanoidTargeter origin, EnemyMarker marker){
        DeleteExistingAddEnemyMarker(marker);
        List<IEnumerator> deleteMarkerCoroutines = new List<IEnumerator>();
        foreach (HumanoidTargeter targeter in instance.targeters)
        {
            if (targeter != origin && targeter.AlreadyHasMarker(marker))
            {
                if (targeter.HasRadio() && origin.HasRadio())
                {
                    IEnumerator newCoroutine = CreateDeleteEnemyMarkerCoroutine(
                        targeter,
                        marker,
                        origin.GetTimeToCommunicateByRadio()
                    );
                    deleteMarkerCoroutines.Add(newCoroutine);
                    instance.StartCoroutine(newCoroutine);
                    instance.deleteEnemyMarkerCommunicators.Add(new Communicator(
                        marker, deleteMarkerCoroutines
                    ));
                }
                else if (origin.CanCommunicate(targeter))
                {
                    IEnumerator newCoroutine = CreateDeleteEnemyMarkerCoroutine(
                        targeter,
                        marker,
                        origin.GetTimeToCommunicateByMouth()
                    );
                    deleteMarkerCoroutines.Add(newCoroutine);
                    instance.StartCoroutine(newCoroutine);
                    instance.deleteEnemyMarkerCommunicators.Add(new Communicator(
                        marker, deleteMarkerCoroutines
                    ));
                }
            }
        }
    }

    /*
     * If an add marker communication starts and then a delete marker
     * communication starts before it finishes, delete the add marker 
     * communication
     */
    private static void DeleteExistingAddEnemyMarker(EnemyMarker deleting){
        for (int i = 0; i < instance.addEnemyMarkerCommunicators.Count; i ++){
            var communicator = instance.addEnemyMarkerCommunicators[i];
            if (communicator.SameMarker(deleting))
            {
                communicator.coroutines.ForEach(c => instance.StopCoroutine(c));
                instance.addEnemyMarkerCommunicators.Remove(communicator);
            }
        }
    }

    private static void DeleteExistingDeleteEnemyMarker(EnemyMarker deleting)
    {
        for (int i = 0; i < instance.deleteEnemyMarkerCommunicators.Count; i++)
        {
            var communicator = instance.deleteEnemyMarkerCommunicators[i];
            if (communicator.SameMarker(deleting))
            {
                communicator.coroutines.ForEach(c => instance.StopCoroutine(c));
                instance.deleteEnemyMarkerCommunicators.Remove(communicator);
            }
        }
    }

    public static void InterruptDeleteEnemyMarker(HumanoidTargeter whoToInterrupt)
    {
        for (int i = 0; i < instance.deleteEnemyMarkerCommunicators.Count; i ++){
            var communicator = instance.deleteEnemyMarkerCommunicators[i];
           
            if (communicator.enemyMarker.GetFounder() == whoToInterrupt)
            {
                List<IEnumerator> coroutines = communicator.coroutines;
                coroutines.ForEach(c => instance.StopCoroutine(c));
                instance.deleteEnemyMarkerCommunicators.Remove(communicator);
            }
        }
    }

    private static IEnumerator CreateAddEnemyMarkerCoroutine(HumanoidTargeter targeter,
                                                      EnemyMarker marker,
                                                      float time){
        yield return new WaitForSeconds(time);
        targeter.ReceiveEnemyMarkerFromFriend(marker);
    }

    private static IEnumerator CreateDeleteEnemyMarkerCoroutine(HumanoidTargeter targeter,
                                                      EnemyMarker marker,
                                                      float time)
    {
        yield return new WaitForSeconds(time);
        targeter.DeleteEnemyMarkerFromFriend(marker);
    }

    private static Communicator MatchIssuer(EnemyMarker marker){
        return 
            instance.addEnemyMarkerCommunicators.Find(c => c.SameIssuer(marker));
    }

    private static Communicator MatchMarker(EnemyMarker marker)
    {
        return
            instance.addEnemyMarkerCommunicators.Find(c => c.SameMarker(marker));
    }

    private class Communicator{
        public EnemyMarker enemyMarker;
        public List<IEnumerator> coroutines;

        public Communicator(EnemyMarker enemyMarker,
                            List<IEnumerator> coroutines){
            this.enemyMarker = enemyMarker;
            this.coroutines = coroutines;
        }

        public bool SameIssuer(EnemyMarker other){
            return this.enemyMarker.GetEnemy().Equals(other.GetEnemy()) &&
                       this.enemyMarker.GetLocation().Equals(other.GetLocation()) &&
                       this.enemyMarker.GetFounder().Equals(other.GetFounder());
        }

        public bool SameMarker(EnemyMarker other)
        {
            return this.enemyMarker.GetEnemy().Equals(other.GetEnemy()) &&
                       this.enemyMarker.GetLocation().Equals(other.GetLocation());
        }
    }
}
