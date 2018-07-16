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
       // Debug.Log(addEnemyMarkerCommunicators.Count);
	} 

    public static void AddBlackBoardSubscriber(HumanoidTargeter blackBoard){
        instance.targeters.Add(blackBoard);
    }

    public static void CommunicateAddEnemyMarker(HumanoidTargeter origin, EnemyMarker marker){
        bool communicatedByMouth = false;
        bool communicatedByRadio = false;
        //DeleteExistingDeleteEnemyMarker(marker);
        //Debug.Log("communicating add by " + origin.gameObject.name);
        List<IEnumerator> addMarkerCoroutines = new List<IEnumerator>();
        foreach(HumanoidTargeter targeter in instance.targeters){
            //Debug.Log(targeter.gameObject.name + " does not have origin: " + !targeter.AlreadyHasMarker(marker));
            if(targeter != origin && !targeter.AlreadyHasMarker(marker) /*&& !AlreadyAddCommunicating(marker,origin)*/){
                if (origin.CanCommunicate(targeter)){
                    marker.SwitchToNewFounder(origin);

                    IEnumerator newCoroutine = CreateAddEnemyMarkerCoroutine(
                        targeter,
                        marker,
                        origin.GetTimeToCommunicateByMouth()
                    );
                    addMarkerCoroutines.Add(newCoroutine);
                    instance.StartCoroutine(newCoroutine);
                    communicatedByMouth = true;
                }else if (targeter.HasRadio() && origin.HasRadio())
                {
                    marker.SwitchToNewFounder(origin);

                    IEnumerator newCoroutine = CreateAddEnemyMarkerCoroutine(
                        targeter,
                        marker,
                        origin.GetTimeToCommunicateByRadio()
                    );
                    addMarkerCoroutines.Add(newCoroutine);
                    instance.StartCoroutine(newCoroutine);
                    communicatedByRadio = true;
                }
            }
        }
        if(addMarkerCoroutines.Count != 0){
            Communicator communicator = new Communicator(
                marker,
                addMarkerCoroutines
            );
            instance.addEnemyMarkerCommunicators.Add(communicator);
            float timeBeforeRemoveCommunicator = 0f;;
            if(communicatedByMouth && communicatedByRadio){
                timeBeforeRemoveCommunicator = Mathf.Max(
                    origin.GetTimeToCommunicateByMouth()
                    , origin.GetTimeToCommunicateByRadio()
                );
            }else if(communicatedByMouth){
                timeBeforeRemoveCommunicator = origin.GetTimeToCommunicateByMouth();
            }else if(communicatedByRadio){
                timeBeforeRemoveCommunicator = origin.GetTimeToCommunicateByRadio();
            }
            timeBeforeRemoveCommunicator *= 0.9f;
            IEnumerator removeCoroutine = GetRemoveFromCommunicatorList(
                timeBeforeRemoveCommunicator,
                instance.addEnemyMarkerCommunicators,
                communicator
            );
            instance.StartCoroutine(removeCoroutine);
        }
    }

    public static void InterruptAddEnemyMarker(HumanoidTargeter whoToInterrupt){
        for (int i = 0; i < instance.addEnemyMarkerCommunicators.Count; i++)
        {
            var communicator = instance.addEnemyMarkerCommunicators[i];
            //Debug.Log(communicator.enemyMarker.GetFounder().Equals(whoToInterrupt));
            Debug.Log("Founder: " + communicator.enemyMarker.GetFounder().gameObject.name);
            if(communicator.enemyMarker.GetFounder().Equals( whoToInterrupt)){
                List<IEnumerator> coroutines = communicator.coroutines;
                coroutines.ForEach(c => instance.StopCoroutine(c));
                Debug.Log("stopping communications");
                instance.addEnemyMarkerCommunicators.Remove(communicator);
            }
        }
    }

    public static void CommunicateDeleteEnemyMarker(HumanoidTargeter origin, EnemyMarker marker){
        DeleteExistingAddEnemyMarker(marker);
        List<IEnumerator> deleteMarkerCoroutines = new List<IEnumerator>();
        bool communicatedByMouth = false;
        bool communicatedByRadio = false;
        foreach (HumanoidTargeter targeter in instance.targeters)
        {
            if (targeter != origin && targeter.AlreadyHasMarker(marker) /*&& !AlreadyDeleteCommunicating(marker)*/)
            {
                
                if (origin.CanCommunicate(targeter))
                {
                    marker.SwitchToNewFounder(origin);

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
                    communicatedByMouth = true;
                }else if(targeter.HasRadio() && origin.HasRadio())
                {
                    marker.SwitchToNewFounder(origin);

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
                    communicatedByRadio = true;
                }
            }
        }
        if (deleteMarkerCoroutines.Count != 0)
        {
            Communicator communicator = new Communicator(
                marker,
                deleteMarkerCoroutines
            );
            instance.deleteEnemyMarkerCommunicators.Add(communicator);
            float timeBeforeRemoveCommunicator = 0f; ;
            if (communicatedByMouth && communicatedByRadio)
            {
                timeBeforeRemoveCommunicator = Mathf.Max(
                    origin.GetTimeToCommunicateByMouth()
                    , origin.GetTimeToCommunicateByRadio()
                );
            }
            else if (communicatedByMouth)
            {
                timeBeforeRemoveCommunicator = origin.GetTimeToCommunicateByMouth();
            }
            else if (communicatedByRadio)
            {
                timeBeforeRemoveCommunicator = origin.GetTimeToCommunicateByRadio();
            }
            IEnumerator removeCoroutine = GetRemoveFromCommunicatorList(
                timeBeforeRemoveCommunicator,
                instance.deleteEnemyMarkerCommunicators,
                communicator
            );
            instance.StartCoroutine(removeCoroutine);
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

    private static bool AlreadyAddCommunicating(EnemyMarker marker,HumanoidTargeter origin){
        if(marker.GetFounder() == origin){
            //return false;
        }
        foreach(Communicator communicator in instance.addEnemyMarkerCommunicators){
            if(communicator.SameIssuer(marker)){
                return true;
            }
        }
        return false;
    }

    private static bool AlreadyDeleteCommunicating(EnemyMarker marker)
    {
        foreach (Communicator communicator in instance.deleteEnemyMarkerCommunicators)
        {
            if (communicator.enemyMarker == marker)
            {
                return true;
            }
        }
        return false;
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

    private static IEnumerator GetRemoveFromCommunicatorList(float seconds,
                                                      List<Communicator> list,
                                                      Communicator remove){
        yield return new WaitForSeconds(seconds);
        list.Remove(remove);
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
