using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidTargeterCommunication : MonoBehaviour {


    private static HumanoidTargeterCommunication instance;

    private HashSet<HumanoidTargeter> targeters;
    private List<Communicator> communicators;

    // Use this for initialization
    private void Awake()
    {
        targeters = new HashSet<HumanoidTargeter>();
        communicators = new List<Communicator>();
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(communicators.Count);
    }

    public static void AddBlackBoardSubscriber(HumanoidTargeter blackBoard)
    {
        instance.targeters.Add(blackBoard);
    }

    public static void Communicate(CommunicationPackage<CommunicatableEnemyMarker> package)
    {
        bool communicatedByMouth = false;
        bool communicatedByRadio = false;
        HumanoidTargeter issuer = package.GetIssuer();
        List<IEnumerator> communicatorCoroutines = new List<IEnumerator>();
        foreach (HumanoidTargeter targeter in instance.targeters)
        {
            if (targeter != issuer && !package.AlreadyCommunicated(targeter)) 
            {
                if (issuer.CanCommunicate(targeter))
                {
                    package.AddToCommunicated(targeter);

                    IEnumerator newCoroutine = CreateCommunicatorCoroutine(
                        issuer.GetTimeToCommunicateByMouth(),
                        targeter,
                        package
                    );
                    communicatorCoroutines.Add(newCoroutine);
                    instance.StartCoroutine(newCoroutine);
                    communicatedByMouth = true;
                }
                else if (targeter.HasRadio() && issuer.HasRadio())
                {
                    package.AddToCommunicated(targeter);

                    IEnumerator newCoroutine = CreateCommunicatorCoroutine(
                        issuer.GetTimeToCommunicateByRadio(),
                        targeter,
                        package
                    );
                    communicatorCoroutines.Add(newCoroutine);
                    instance.StartCoroutine(newCoroutine);
                    communicatedByRadio = true;
                }
            }
        }
        if (communicatorCoroutines.Count != 0)
        {
            Communicator communicator = new Communicator(
                issuer,
                communicatorCoroutines
            );
            instance.communicators.Add(communicator);
            float timeBeforeRemoveCommunicator = 0f; ;
            if (communicatedByMouth && communicatedByRadio)
            {
                timeBeforeRemoveCommunicator = Mathf.Max(
                    issuer.GetTimeToCommunicateByMouth()
                    , issuer.GetTimeToCommunicateByRadio()
                );
            }
            else if (communicatedByMouth)
            {
                timeBeforeRemoveCommunicator = issuer.GetTimeToCommunicateByMouth();
            }
            else if (communicatedByRadio)
            {
                timeBeforeRemoveCommunicator = issuer.GetTimeToCommunicateByRadio();
            }
            IEnumerator removeCoroutine = GetRemoveFromCommunicatorList(
                timeBeforeRemoveCommunicator,
                communicator
            );
            instance.StartCoroutine(removeCoroutine);
        }
    }

    public static void InterruptUpdate(HumanoidTargeter whoToInterrupt)
    {
        instance.communicators.ForEach(c =>
        {
            if(c.issuer == whoToInterrupt){
                c.coroutines.ForEach(co => instance.StopCoroutine(co));
                instance.communicators.Remove(c);
            }
        });
    }

    private static IEnumerator CreateCommunicatorCoroutine(float timeToCommunicate,
                                                           HumanoidTargeter reciever,
                                                           CommunicationPackage<CommunicatableEnemyMarker> package){
        yield return new WaitForSeconds(timeToCommunicate);
        reciever.RecieveCommunication(package);
    }

    private static IEnumerator GetRemoveFromCommunicatorList(float seconds,
                                                      Communicator remove)
    {
        yield return new WaitForSeconds(seconds);
        instance.communicators.Remove(remove);
    }


    private class Communicator
    {
        public HumanoidTargeter issuer;
        public List<IEnumerator> coroutines;

        public Communicator(HumanoidTargeter issuer,
                            List<IEnumerator> coroutines)
        {
            this.issuer = issuer;
            this.coroutines = coroutines;
        }
    }
}
