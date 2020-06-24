using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CableGuardian
{   
    public enum Direction { Left, Right, Either }
    public enum AccumulationStatus { Increasing, Decreasing, Either }
    public enum YawTrackerOrientationEvent { ResetPosition, Yaw0, Yaw180, Yaw0Yaw180 }
    
    class RotationEventArgs : EventArgs
    {           
        /// <summary>
        /// Direction of the last movement.         
        /// </summary>
        public Direction MovementDirection { get; }
        /// <summary>
        /// Did the last movement cause the directional rotation to increase (go away from ResetPosition) or decrease (approach ResetPosition).
        /// </summary>
        public AccumulationStatus RotationAccumulationStatus { get; }        
        /// <summary>
        /// Current half rotation count (180 degrees) from ResetPosition.
        /// </summary>
        public uint HalfTurns { get; }        
        /// <summary>
        /// Highest amount of half rotations reached since last ResetPosition crossing.
        /// Will be reset to 0 AFTER the Yaw0 event has fired when half-turns == 0.
        /// </summary>
        public uint PeakHalfTurns { get; }
        /// <summary>
        /// Direction of cumulative rotation from ResetPosition.                 
        ///  = the direction where cable twisting increases.
        /// </summary>
        public Direction RotationSide { get; }       

        public double Yaw { get; }
        public RotationEventArgs(Direction movementDirection, AccumulationStatus rotationAccumulation, uint halfTurns, uint peakHalfTurns, Direction rotationSide, double yaw)
        {            
            MovementDirection = movementDirection;
            RotationAccumulationStatus = rotationAccumulation;            
            HalfTurns = halfTurns;            
            PeakHalfTurns = peakHalfTurns;
            RotationSide = rotationSide;            
            Yaw = yaw;
        }
    }

    /// <summary>
    /// Keeps track of half-turns (180 degree) around y-axis (Yaw) to either direction.
    /// NOTE: Input value (absolute axis position) must be updated frequently enough - 
    /// Angular difference between samples must always be below 180 degrees (half circle)!
    /// </summary>
    class YawTracker
    {   
        /// <summary>
        /// In case the coordinate systems are different between VR APIs, Left and Right can be flipped with this
        /// </summary>
        public bool InvertLeftRight { get { return (LeftRightMultiplier == -1); } set { LeftRightMultiplier = (value) ? -1 : 1; } } 
        int LeftRightMultiplier = 1;

        public const string S_Yaw0 = "facing Front  (0\u00B0)";        
        public const string S_Yaw180 = "facing Back  (180\u00B0)";
        public const string S_Yaw0Yaw180 = "facing Front or Back";
        public const string S_ResetPosition = "total rotation = 0  (NEUTRAL)";

        const double Threshold180Abs = 3.14F;

        /// <summary>
        /// Occurs when Yaw axis value 0 is crossed in neutral orientation (= zero rotation)
        /// </summary>
        public event EventHandler<RotationEventArgs> ResetPosition;
        /// <summary>
        /// Occurs when Yaw axis value 0 is crossed (front facing).
        /// </summary>
        public event EventHandler<RotationEventArgs> Yaw0;
        /// <summary>
        /// Occurs when Yaw axis value 180 degrees is crossed (half turn, facing away from 0).
        /// </summary>
        public event EventHandler<RotationEventArgs> Yaw180;                     

        VRObserver HmdObserver { get; }
        public double YawValue { get { return (_YawValue == null) ? 0.0 : (double)_YawValue ; } } 
        double? _YawValue { get; set; } = null;
        int InitialHalfTurn = 0;
        double ExpectedInitialYaw = 0;

        /// <summary>
        /// The highest amount of half rotations in one direction since leaving ResetPosition.
        /// Resets to 0 at ResetPosition.
        /// </summary>
        uint PeakHalfTurns { get; set; } = 0;
               
        int _CurrentHalfTurn = 1;
        /// <summary>
        /// Current (incomplete) half-turn (180 degrees). Sign (+- denotes direction).
        /// Updated from UpdateRotation().
        /// NOTE: never zero (0): ...-3,-2,-1,1,2,3...
        /// </summary>
        public int CurrentHalfTurn
        {
            get { return _CurrentHalfTurn; }
            private set
            {                
                _CurrentHalfTurn = value;                
                CompletedHalfTurns = (uint)Math.Abs((_CurrentHalfTurn < 0) ? _CurrentHalfTurn + 1 : _CurrentHalfTurn - 1);
                RotationSide = (_CurrentHalfTurn < 0) ? Direction.Right : Direction.Left;

                if (CompletedHalfTurns > PeakHalfTurns)                
                    PeakHalfTurns = CompletedHalfTurns;
            }
        }

        /// <summary>
        /// Number of fully completed half-turns (180 degrees)
        /// </summary>
        public uint CompletedHalfTurns { get; private set; }
        /// <summary>
        /// Number of fully completed half-turns (180 degrees) with sign for direction info
        /// </summary>
        public int CompletedHalfTurns_Signed { get { return (int)CompletedHalfTurns * ((RotationSide == Direction.Left) ? 1 : -1 ); } }
        /// <summary>
        /// Direction of cumulative rotation from ResetPosition.                 
        ///  = the direction where cable twisting increases.
        /// </summary>
        public Direction RotationSide { get; private set; }

        public YawTracker(VRObserver hmdObserver, int initialHalfTurn = 0, double expectedInitialYaw = 0)
        {            
            HmdObserver = hmdObserver;                     
            _YawValue = null;
            InitialHalfTurn = initialHalfTurn;    // in case we start from a recorded rotation value (auto-restart or "Remember turn count" -feature)      
            ExpectedInitialYaw = expectedInitialYaw; // same as above. This will be the last recorded yaw value to use as a reference point.

            HmdObserver.StateRefreshed += OnHmdStateRefreshed;            
        }

        void OnHmdStateRefreshed(object sender, VRObserverEventArgs e)
        {
            UpdateRotation(e.HmdYaw * LeftRightMultiplier);
        }
               
        public static double RadToDeg(double rad)
        {
            return rad * (180 / Math.PI);
        }

        bool RotationUpdateInProgress = false;
        /// <summary>
        /// Checks the difference of the last two Yaw values and determines if either 0 or 180 degree position was crossed.
        /// Invokes rotation events and updates the lap counter (which in turn is used for counting the half-turns).
        /// NOTE, the angular difference between the last two Yaw values must always be below 180 degrees for this to work!
        /// </summary>
        /// <param name="newYawValue"></param>
        void UpdateRotation(double newYawValue)
        {
            if (RotationUpdateInProgress)
                return;

            RotationUpdateInProgress = true;

            // Oculus rotation axis range is in radians from -pi to +pi. 
            // Zero is the middle point (facing forward). Rotation is positive when turning left. 


            // *****************
            // only once at startup IF starting from a saved point of rotation (turn count memory)
            if (InitialHalfTurn != 0)
            {
                _YawValue = newYawValue;                
                CurrentHalfTurn = GetCorrectedInitialHalfTurn(newYawValue);
                InitialHalfTurn = 0; 
            }
            // *****************


            if (_YawValue != null) // null = first time (or after reset)
            {                
                if (Math.Abs((double)(_YawValue - newYawValue)) < Threshold180Abs) 
                {
                    bool resetPos = false;
                    Direction moveDir = Direction.Either;
                    if (_YawValue < 0 && newYawValue >= 0) // Zero crossed from negative to positive (right side to left side, moving left)
                    {  
                        CurrentHalfTurn += (CurrentHalfTurn == -1) ? 2 : 1;
                        resetPos = (CurrentHalfTurn == 1);
                        moveDir = Direction.Left;

                        AccumulationStatus acc = (resetPos) ? AccumulationStatus.Either : ((RotationSide == Direction.Left) ? AccumulationStatus.Increasing : AccumulationStatus.Decreasing);
                        InvokeYaw0(new RotationEventArgs(moveDir, acc, CompletedHalfTurns, PeakHalfTurns, RotationSide, newYawValue));                   
                    }
                    else if (_YawValue >= 0 && newYawValue < 0) // Zero crossed from positive to negative (left side to right side, moving right)
                    {   
                        CurrentHalfTurn -= (CurrentHalfTurn == 1) ? 2 : 1;
                        resetPos = (CurrentHalfTurn == -1);
                        moveDir = Direction.Right;

                        AccumulationStatus acc = (resetPos) ? AccumulationStatus.Either : ((RotationSide == Direction.Right) ? AccumulationStatus.Increasing : AccumulationStatus.Decreasing);
                        InvokeYaw0(new RotationEventArgs(moveDir, acc, CompletedHalfTurns, PeakHalfTurns, RotationSide, newYawValue));                       
                    }

                    if (resetPos)
                    {
                        InvokeResetPosition(new RotationEventArgs(moveDir, AccumulationStatus.Either, CompletedHalfTurns, PeakHalfTurns, RotationSide, newYawValue));
                        PeakHalfTurns = 0; // this must NOT be reset before invoking the Yaw0 & ResetPosition events!
                    }
                }
                else // 180 degree position was crossed (or sample rate is too low --> incorrect result)
                {
                    if (_YawValue < newYawValue) // 180 crossed from negative to positive (right side to left side, moving right)
                    {
                        CurrentHalfTurn -= 1;
                        AccumulationStatus acc = (RotationSide == Direction.Right) ? AccumulationStatus.Increasing : AccumulationStatus.Decreasing;
                        InvokeYaw180(new RotationEventArgs(Direction.Right, acc, CompletedHalfTurns, PeakHalfTurns, RotationSide, newYawValue));
                    }
                    else // 180 crossed from positive to negative (left side to right side, moving left)
                    {
                        CurrentHalfTurn += 1;
                        AccumulationStatus acc = (RotationSide == Direction.Left) ? AccumulationStatus.Increasing : AccumulationStatus.Decreasing;
                        InvokeYaw180(new RotationEventArgs(Direction.Left, acc, CompletedHalfTurns, PeakHalfTurns, RotationSide, newYawValue));
                    }
                }
            }
            else // first time (or after reset)
            {
                // to detect initial orientation                
                CurrentHalfTurn = (newYawValue < 0) ? -1 : 1;
            }
            _YawValue = newYawValue;
            RotationUpdateInProgress = false;
        }

        int GetCorrectedInitialHalfTurn(double actualYawValue)
        {
            if (InitialHalfTurn == 0)
                throw new Exception("InitialHalfTurn cannot be zero when calling GetCorrectedInitialHalfTurn().");

            int output = InitialHalfTurn;
            // Check the current orientation of the HMD to get the correct initial turn count.
            // (even though the user's intention is to not rotate the headset when the app is not running (e.g. with SteamVR autostart/stop feature))...
            // ...the exact orientation is not the same at startup as it was at exit. It might have crossed either the zero or 180 during offline...
            // ... requiring to correct the initial half turn. Obviously this will only work when offline rotation < 180.

            // flatten edge cases
            if (ExpectedInitialYaw > Threshold180Abs)
                ExpectedInitialYaw = Threshold180Abs;

            // Correction needed only if the current orientation is not on the expected side (left vs right, + vs -):
            int expectedSide = ((InitialHalfTurn % 2 == 0) ? -1 * InitialHalfTurn : InitialHalfTurn) / Math.Abs(InitialHalfTurn);  // 1 or -1          
            if (expectedSide * actualYawValue < 0) // on different sides
            {
                // In case ExpectedInitialYaw == 0, we just take a point from the middle of the half circle abs scale (Threshold180Abs / 2).
                // This will result in blindly going via the pole closest to the actual yaw value. 
                //  --> User has a smaller window in which to rotate the hmd during offline.
                // (Missing yaw value is most likely the result of a failure to save the last yaw value at exit, highly unlikely)                
                double expectedInitialYawAbs = (ExpectedInitialYaw == 0) ? Threshold180Abs / 2 : Math.Abs(ExpectedInitialYaw) ;
                
                // find the shortest route (via north pole or south pole) from the expected yaw point to the actual yaw point...
                // ...and decrease or increase the initial half turn accordingly.
                if (Math.Abs(actualYawValue) < Threshold180Abs - expectedInitialYawAbs)                
                    output = InitialHalfTurn - (expectedSide); // via north pole (zero)                
                else                
                    output = InitialHalfTurn + (expectedSide); // via south pole (180)                                    
                
            }
            return (output == 0) ? expectedSide * -1 : output; // current half-turn is never zero. (-3, -2, -1, 1, 2, 3)
        }

        public void Reset()
        {   
            CurrentHalfTurn = (YawValue < 0) ? -1 : 1;            
            PeakHalfTurns = 0;
            _YawValue = null;
        }

        void InvokeResetPosition(RotationEventArgs e)
        {
            ResetPosition?.Invoke(this, e);
        }

        void InvokeYaw0(RotationEventArgs e)
        {
            Yaw0?.Invoke(this, e);
        }

        void InvokeYaw180(RotationEventArgs e)
        {
            Yaw180?.Invoke(this, e);
        }              
    }
}
