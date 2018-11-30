using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Action: takes time to finish   
 * Effect: instantaneous effect
 */
public class HumanoidModel : MonoBehaviour{
    [SerializeField]
    private HumanoidActionCoordinator actionCoordinator;
    [SerializeField]
    private MovementController movement;
    [SerializeField]
    private HumanoidDirection direction;

    [SerializeField]
    private Transform releasePoint;
    [SerializeField]
    private Transform centerBottom;

    [SerializeField]
    private float standingHeight;
    [SerializeField]
    private float kneelingHeight;
    [SerializeField]
    private float layingHeight;
    [SerializeField]
    private int sightDistance;
    [SerializeField]
    private int visionWideness;
    [SerializeField]
    private int visionSharpness;

    private bool isFlinch;
    private Weapon currentWeapon;
    private StressManager baseStressManager;
    private StandingState standingState;
    private float vantageHeight;
    private HumanoidVantage vantageData;


	public virtual void Awake(){
        standingState = StandingState.STAND;
        vantageHeight = standingHeight;
        direction.Face(centerBottom.position + new Vector3(0, 0, -1));
        vantageData = new HumanoidVantage(
            standingHeight,
            kneelingHeight,
            layingHeight
        );
    }
	// Use this for initialization
	public virtual void Start () {
	}
	
	// Update is called once per frame
	public virtual void Update () {
	}

    public bool InfoCanSee(HumanoidModel other){
        Vector3 thisVantagePoint = GetVantagePoint();
        Vector3 otherVantagePoint = other.GetVantagePoint();
        float distance = Vector3.Distance(thisVantagePoint, otherVantagePoint);
        if(distance > sightDistance){
            return false;
        }
        return direction.WithinRangeOfVision(otherVantagePoint, visionWideness)
            && EnvironmentPhysics.LineOfSightToVantagePointExists(
            visionSharpness,
            thisVantagePoint,
            otherVantagePoint
        ) ;
    }

    protected bool InfoCanSee(Vector3 location)
    {
        Vector3 thisVantagePoint = GetVantagePoint();
        Vector3 otherVantagePoint = location + new Vector3(0,0.1f,0);
        float distance = Vector3.Distance(thisVantagePoint, otherVantagePoint);
        if (distance > sightDistance)
        {
            return false;
        }
        return direction.WithinRangeOfVision(otherVantagePoint, visionWideness)
            && EnvironmentPhysics.LineOfSightToVantagePointExists(
            visionSharpness,
            thisVantagePoint,
            otherVantagePoint
        );
    }

    public virtual void EffectOnSeeEnemy(HumanoidModel enemy){
        Vector3 thisVantagePoint = GetVantagePoint();
        Projectile thisWeapon = currentWeapon.GetProjectile();

        Vector3 enemyVantagePoint = enemy.GetVantagePoint();
        Projectile enemyWeapon = enemy.currentWeapon.GetProjectile();

        TerrainDisparity terrainDisparity = EnvironmentPhysics.CalculateTerrainDisparityBetween(
            thisWeapon, enemyWeapon, thisVantagePoint,enemyVantagePoint
        );

        float coverDisparity = terrainDisparity.visibleToTarget - terrainDisparity.visibleToObserver;
        // the following line prevents advantageous cover from decreasing stress
        coverDisparity = coverDisparity < 0 ? 0 : coverDisparity;
        baseStressManager.PendSightStress(coverDisparity, enemy);

    }

    public virtual void EffectDoesNotSeeEnemy(HumanoidModel enemy)
    {
        baseStressManager.StopSightStress(enemy);
    }

    public void EffectBenefitFromCloseness(HumanoidModel friend)
    {
        float distance = (centerBottom.position.x - friend.centerBottom.position.x)
            + (centerBottom.position.y - friend.centerBottom.position.y);
    }

    public void ActionStartAttack()
    {
        actionCoordinator.SetAttack(currentWeapon, true);
    }

    public void ActionEndAttack(){
        actionCoordinator.SetAttack(currentWeapon, false);
    }

    public void ActionMoveTo(Vector2 location){
        // Have a coroutine, make it generic
        // coordinator.setMoving(true)

        // logic

        // coordinator.setMoving(false
    }

    public void ActionFlinch(){
        actionCoordinator.Flinch(.5f);
    }
    public void ActionReload(){
        actionCoordinator.Reload(currentWeapon);
    }

    public void EffectHitByProjectile(Projectile projectile){
        
    }

    public void EffectStand(){
        actionCoordinator.SetStand();
        standingState = StandingState.STAND;
        vantageHeight = standingHeight;
    }

    public void EffectKneel(){
        actionCoordinator.SetKneel();
        standingState = StandingState.KNEEL;
        vantageHeight = kneelingHeight;
    }

    public void EffectLay(){
        actionCoordinator.SetLay();
        standingState = StandingState.LAY;
        vantageHeight = layingHeight;
    }
    public void EffectSetTarget(Vector3 target){
        currentWeapon.SetTarget(target);
    }

    public virtual void EffectCancelAction(){
        actionCoordinator.Interrupt();
    }
    public void EffectExperienceImpact(Vector3 location, float radius, float power){
        float distance = Vector3.Distance(location, GetVantagePoint());
        if(distance < radius){
            baseStressManager.SuppressiveFireStress(power, distance);
        }
    }

    public void AEFlinchStart(){
        isFlinch = true;
    }

    public void AEFlinchEnd(){
        isFlinch = false;
    }
    public void AEAttack(){
        currentWeapon.Attack();
    }


    public bool InfoIsExecutingAction(){
        return actionCoordinator.ActionInProgress();
    }

    public bool InfoIsFlinch(){
        return isFlinch;
    }
    public Vector3 InfoGetCenterBottom()
    {
        return centerBottom.position;
    }
    public HumanoidVantage InfoGetVantageData(){
        /*
         *  Use object pooling and return new vantage data
         */
        vantageData.SetWeapon(currentWeapon);
        vantageData.SetLocation(centerBottom.position);
        return vantageData;
    }
    public Weapon InfoGetWeapon(){
        return currentWeapon;
    }

    protected void SwitchToWeapon(WeaponType weapon)
    {
        //Debug.Log(newWeapon.GetTakeoutBehaviour().name + " " + currentWeapon.GetStashBehaviour().name);

        var newWeapon = WeaponList.GetWeapon(weapon).Instantiate(releasePoint);
        actionCoordinator.SwitchWeapon(currentWeapon,newWeapon);
        currentWeapon = newWeapon;
    }

    protected void InitializeWeapon(WeaponType weapon){
        var newWeapon = WeaponList.GetWeapon(weapon).Instantiate(releasePoint);
        currentWeapon = newWeapon;
        actionCoordinator.InitializeWeapon(currentWeapon);
    }

    protected void InitializeStressManager(StressManager stressManager){
        this.baseStressManager = stressManager;
    }

    private enum StandingState
    {
        STAND,
        KNEEL,
        LAY
    }
    private Vector3 GetVantagePoint(){
        return centerBottom.position + new Vector3(0, vantageHeight, 0);
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GetVantagePoint(), new Vector3(1, 1, 1));
	}
}
