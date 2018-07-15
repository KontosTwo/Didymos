using UnityEngine;

public partial class GameplayCamera
{
    private class FocusStrategy : IMovementStrategy
    {

        private Transform focus;
        private Camera cam;
        private Transform camIndependentLocation;
        private int lowerAngleRotateLimit;
        private int upperAngleRotateLimit;
        private float minimumHeightFromTerrain;
        private float cameraRotationSpeed;
        private float minimumDistanceFromFocus;
        private float maximumDistanceFromFocus;

        private float distanceFromFocus;
        private Vector3 mousePosition;
        private float verticalAngle;
        private float horizontalAngle;

        public FocusStrategy(Camera cam, 
                             Transform camIndependentLocation,
                             Transform focus,
                             int lowerAngleRotateLimit,
                             int upperAngleRotateLimit,
                             float minimumHeightFromTerrain,
                             float cameraRotationSpeed,
                             float minimumDistanceFromFocus,
                             float maximumDistanceFromFocus,
                             int initialDistance,
                             float initialVerticalAngle,
                             float initialHorizontalAngle
                             )
        {
            this.focus = focus;
            this.cam = cam;
            this.camIndependentLocation = camIndependentLocation;
            this.lowerAngleRotateLimit = lowerAngleRotateLimit;
            this.minimumHeightFromTerrain = minimumHeightFromTerrain;
            this.upperAngleRotateLimit = upperAngleRotateLimit;
            this.cameraRotationSpeed = cameraRotationSpeed;
            this.minimumDistanceFromFocus = minimumDistanceFromFocus;
            this.maximumDistanceFromFocus = maximumDistanceFromFocus;
            this.distanceFromFocus = initialDistance;
            this.verticalAngle = initialVerticalAngle;
            this.horizontalAngle = initialHorizontalAngle;
            mousePosition = new Vector3();
        }

        public void Cleanup()
        {

        }

        public void Initialize()
        {
            camIndependentLocation.position = new Vector3(
                focus.position.x,
                focus.position.y,
                focus.position.z - distanceFromFocus
            );
            camIndependentLocation.LookAt(focus.transform.position);
            mousePosition = new Vector3();
        }

        public void Update()
        {
            SetToInitialOrientation();
            ApplyRotation();
            ApplyMouseOffset();
            ApplyTerrainMinimum();
            cam.transform.position = camIndependentLocation.position;
            cam.transform.eulerAngles = camIndependentLocation.eulerAngles;
        }

        public void UpdateMousePosition(Vector3 offset)
        {
            this.mousePosition = offset;
        }

        public void RotateUp(){
            if ((verticalAngle >= 88 && verticalAngle<= 90))
            {
                return;
            }else{
                verticalAngle += cameraRotationSpeed;
            }

        }

        public void RotateDown()
        {
            if ((verticalAngle >= 270 && verticalAngle <= 360)
                || (verticalAngle>= 0 && verticalAngle <= lowerAngleRotateLimit))
            {
                return;
            }else{
                verticalAngle -= cameraRotationSpeed;
            }

        }

        public void RotateLeft()
        {
            horizontalAngle += cameraRotationSpeed;
        }

        public void RotateRight()
        {
            horizontalAngle -= cameraRotationSpeed;
        }

        public void ZoomIn(float amount){
            if(distanceFromFocus > minimumDistanceFromFocus){
                distanceFromFocus -= amount;
            }
        }

        public void ZoomOut(float amount){
            if(distanceFromFocus < maximumDistanceFromFocus){
                distanceFromFocus += amount;
            }
        }

        private void SetToInitialOrientation(){
            camIndependentLocation.position = new Vector3(
                focus.position.x,
                focus.position.y,
                focus.position.z - distanceFromFocus
            );
            camIndependentLocation.LookAt(focus.transform.position);
        }


        private void ApplyRotation(){
            camIndependentLocation.RotateAround(
                focus.position,
                new Vector3(camIndependentLocation.right.x, 
                            0, 
                            camIndependentLocation.right.z
                           ),
                verticalAngle
            );  
            camIndependentLocation.RotateAround(
                focus.position,
                Vector3.up,
                horizontalAngle
            );  

        }

        private void ApplyMouseOffset(){
            Vector3 focusScreenPos = cam.WorldToScreenPoint(focus.position);
            Vector2 vectorToMouse = mousePosition - focusScreenPos;
            float vectorToMouseLength = vectorToMouse.magnitude;
            float vectorToMouseAngle = Mathf.Atan2(
                vectorToMouse.y,
                vectorToMouse.x
            );

            Vector3 camRight = camIndependentLocation.transform.right;
            Vector3 camForward = camIndependentLocation.transform.forward;
            Vector3 screenVectorToMouse = Quaternion.AngleAxis(Mathf.Rad2Deg * vectorToMouseAngle, camForward) * camRight;
            Vector3 nScreenVectorToMouse = screenVectorToMouse.normalized;

            float screenMouseOffset = vectorToMouseLength * distanceFromFocus / 1000;
            nScreenVectorToMouse.Scale(FastMath.CreateVectorCube(screenMouseOffset));

            camIndependentLocation.position += nScreenVectorToMouse;
        }

        private void ApplyTerrainMinimum(){
            float terrainHeight = EnvironmentPhysics.FindHeightAt(
                camIndependentLocation.position.x,
                camIndependentLocation.position.z
            );
            float heightAboveTerrain = camIndependentLocation.position.y
                                                            - terrainHeight;
            float tempVerticalAngle = verticalAngle;
            while(heightAboveTerrain < minimumHeightFromTerrain
                  && tempVerticalAngle < 89){
                camIndependentLocation.RotateAround(
                focus.position,
                new Vector3(camIndependentLocation.right.x, 
                            0, 
                            camIndependentLocation.right.z
                           ),
                            1
                );
                tempVerticalAngle++;
                heightAboveTerrain = camIndependentLocation.position.y
                                                            - terrainHeight;
            }
        }

        public Vector3 GetLocation()
        {
            return camIndependentLocation.position;
        }

        public Vector3 GetDirection()
        {
            return camIndependentLocation.forward;
        }
    }
}
