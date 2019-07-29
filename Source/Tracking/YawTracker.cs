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

        public const string S_Yaw0 = "facing Front (0\u00B0)";        
        public const string S_Yaw180 = "facing Back (180\u00B0)";
        public const string S_Yaw0Yaw180 = "facing Front or Back";
        public const string S_ResetPosition = "total rotation = 0 (reset position)";

        /// <summary>
        /// Occurs when Yaw axis value 0 is crossed in the reset position (= zero rotation)
        /// </summary>
        public EventHandler<RotationEventArgs> ResetPosition;
        /// <summary>
        /// Occurs when Yaw axis value 0 is crossed (front facing).
        /// </summary>
        public EventHandler<RotationEventArgs> Yaw0;
        /// <summary>
        /// Occurs when Yaw axis value 180 degrees is crossed (half turn, facing away from 0).
        /// </summary>
        public EventHandler<RotationEventArgs> Yaw180;                     

        VRObserver HmdObserver { get; }
        public double YawValue { get { return (_YawValue == null) ? 0.0 : (double)_YawValue ; } } 
        double? _YawValue { get; set; } = null;
        int InitialHalfTurns = 0;
        

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
        int CurrentHalfTurn
        {
            get { return _CurrentHalfTurn; }
            set
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

        public YawTracker(VRObserver hmdObserver, int initialHalfTurns = 0)
        {            
            HmdObserver = hmdObserver;                     
            _YawValue = null;
            InitialHalfTurns = initialHalfTurns;    // in case we start counting from a non-zero value (autorestart app)      

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

        /// <summary>
        /// Checks the difference of the last two Yaw values and determines if either 0 or 180 degree position was crossed.
        /// Invokes rotation events and updates the lap counter (which in turn is used for counting the half-turns).
        /// NOTE, the angular difference between the last two Yaw values must always be below 180 degrees for this to work!
        /// </summary>
        /// <param name="newYawValue"></param>
        void UpdateRotation(double newYawValue)
        {
            // Oculus rotation axis range is in radians from -pi to +pi. 
            // Zero is the middle point (facing forward). Rotation is positive when turning left. 

            // only when starting from a non-zero number of completed turns
            if (InitialHalfTurns != 0)
            {
                _YawValue = newYawValue;                
                CurrentHalfTurn = (int)((InitialHalfTurns < 0) ? InitialHalfTurns - 1 : InitialHalfTurns + 1);
                InitialHalfTurns = 0;
            }

            if (_YawValue != null) // null = first time (or after reset)
            {                
                if (Math.Abs((double)(_YawValue - newYawValue)) < 3.14F) 
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
        }
        
        public void Reset()
        {   
            CurrentHalfTurn = (YawValue < 0) ? -1 : 1;            
            PeakHalfTurns = 0;
            _YawValue = null;
        }

        void InvokeResetPosition(RotationEventArgs e)
        {
            if (ResetPosition != null)
            {
                ResetPosition(this, e);
            }
        }

        void InvokeYaw0(RotationEventArgs e)
        {
            if (Yaw0 != null)
            {
                Yaw0(this, e);
            }
        }

        void InvokeYaw180(RotationEventArgs e)
        {
            if (Yaw180 != null)
            {
                Yaw180(this, e);
            }
        }              
    }
}
