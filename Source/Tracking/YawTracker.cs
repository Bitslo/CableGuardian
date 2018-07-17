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
        /// Full rotation count (360 degrees) from ResetPosition after the last movement.
        /// </summary>
        public uint FullRotations { get; }
        /// <summary>
        /// Highest amount of full rotations reached since last ResetPosition crossing.
        /// Will be reset to 0 AFTER the Yaw0 event has fired when fullrotations == 0.
        /// </summary>
        public uint PeakFullRotations { get; }
        /// <summary>
        /// Direction of cumulative rotation (e.g. full rotations) from ResetPosition. 
        /// </summary>
        public Direction RotationSide { get; }
       /*
        /// <summary>
        /// true when Yaw0 -event occurs at ResetPosition. 
        /// </summary>
        public bool AtResetPosition { get; } 
        */

        public double Yaw { get; }
        public RotationEventArgs(Direction movementDirection, AccumulationStatus rotationAccumulation, uint fullRotations, uint peakFullRotations, Direction rotationSide, /*bool atResetPosition,*/double yaw)
        {            
            MovementDirection = movementDirection;
            RotationAccumulationStatus = rotationAccumulation;
            FullRotations = fullRotations;
            PeakFullRotations = peakFullRotations;
            RotationSide = rotationSide;
            //AtResetPosition = atResetPosition;
            Yaw = yaw;
        }
    }

    /// <summary>
    /// Keeps track of full (360 degree) rotations around y-axis (Yaw) to either direction.
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

        public const string S_Yaw0 = "facing front (0)";        
        public const string S_Yaw180 = "facing away (180)";
        public const string S_Yaw0Yaw180 = "facing front OR away";
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
        
        public EventHandler<RotationEventArgs> FullRotationsChanged;

        VRObserver HmdObserver { get; }
        public double YawValue { get { return (_YawValue == null) ? 0.0 : (double)_YawValue ; } } 
        double? _YawValue { get; set; } = null;
               
                            
        /// <summary>
        /// The highest amount of full rotations in one direction since leaving ResetPosition.
        /// Resets to 0 at ResetPosition.
        /// </summary>
        uint PeakFullRotations { get; set; } = 0;        
    
        int _CurrentLap = 1;
        /// <summary>
        /// Current lap (full rotation in progress). Sign (+- denotes direction).
        /// Updated from UpdateRotation().
        /// NOTE: never zero (0): ...-3,-2,-1,1,2,3...
        /// </summary>
        int CurrentLap 
        {
            get { return _CurrentLap; }
            set
            {                
                uint prevFullRotations = GetFullRotations(); // full rotations are calculated based on CurrentLap
                _CurrentLap = value;
                uint currentFullRotations = GetFullRotations();                
                if (currentFullRotations != prevFullRotations)
                {
                    AccumulationStatus acc;
                    Direction moveDir;
                    if (currentFullRotations < prevFullRotations)
                    {
                        acc = AccumulationStatus.Decreasing;
                        if (GetRotationSide() == Direction.Left)                        
                            moveDir = Direction.Right;
                        else
                            moveDir = Direction.Left;
                    }
                    else
                    {
                        acc = AccumulationStatus.Increasing;
                        moveDir = GetRotationSide();
                    }

                    InvokeFullRotationsChanged(new RotationEventArgs(moveDir, acc, GetFullRotations(), PeakFullRotations, GetRotationSide(), YawValue));                                        
                } 
            }            
        }

        public YawTracker(VRObserver hmdObserver)
        {
            HmdObserver = hmdObserver;                     
            _YawValue = null;            

            HmdObserver.StateRefreshed += OnHmdStateRefreshed;
                        
            FullRotationsChanged += OnFullRotationsChanged;
        }

        /// <summary>
        /// Returns the current number of full rotations away from ResetPosition.         
        /// Use GetRotationSide() to check direction.
        /// </summary>
        public uint GetFullRotations()
        {
            return (uint)Math.Abs((CurrentLap < 0) ? CurrentLap + 1 : CurrentLap - 1);
        }

        /// <summary>
        /// Rotation offset direction from ResetPosition ( = Yaw0 @ zero full rotations).
        ///  = the direction where rotation accumulates.
        /// </summary>
        public Direction GetRotationSide()
        {            
            if (CurrentLap < 0)
                return Direction.Right;
            else
                return Direction.Left;
        }


        void OnFullRotationsChanged(object sender, RotationEventArgs e)
        {            
            uint fullRot = GetFullRotations();
            if (fullRot > PeakFullRotations)
            {
                PeakFullRotations = fullRot;
            }                        
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
        /// Invokes rotation events and updates the lap counter (which in turn is used for getting full rotations).
        /// NOTE, the angular difference between the last two Yaw values must always be below 180 degrees for this to work!
        /// </summary>
        /// <param name="newYawValue"></param>
        void UpdateRotation(double newYawValue)
        {
            // Oculus rotation axis range is in radians from -pi to +pi. 
            // Zero is the middle point (facing forward). Rotation is positive when turning left. 

            if (_YawValue != null) // null = first time (after reset)
            {                
                if (Math.Abs((double)(_YawValue - newYawValue)) < 3.14F) 
                {
                    bool resetPos = false;
                    Direction moveDir = Direction.Either;
                    if (_YawValue < 0 && newYawValue >= 0) // Zero crossed from negative to positive (right side to left side, moving left)
                    {
                        CurrentLap += (CurrentLap == -1) ? 2 : 1;
                        resetPos = (CurrentLap == 1);
                        moveDir = Direction.Left;

                        AccumulationStatus acc = (resetPos) ? AccumulationStatus.Either : ((GetRotationSide() == Direction.Left) ? AccumulationStatus.Increasing : AccumulationStatus.Decreasing);
                        InvokeYaw0(new RotationEventArgs(moveDir, acc, GetFullRotations(), PeakFullRotations, GetRotationSide(), newYawValue));                   
                    }
                    else if (_YawValue >= 0 && newYawValue < 0) // Zero crossed from positive to negative (left side to right side, moving right)
                    {
                        CurrentLap -= (CurrentLap == 1) ? 2 : 1;
                        resetPos = (CurrentLap == -1);
                        moveDir = Direction.Right;

                        AccumulationStatus acc = (resetPos) ? AccumulationStatus.Either : ((GetRotationSide() == Direction.Right) ? AccumulationStatus.Increasing : AccumulationStatus.Decreasing);
                        InvokeYaw0(new RotationEventArgs(moveDir, acc, GetFullRotations(), PeakFullRotations, GetRotationSide(), newYawValue));                       
                    }

                    if (resetPos)
                    {
                        InvokeResetPosition(new RotationEventArgs(moveDir, AccumulationStatus.Either, GetFullRotations(), PeakFullRotations, GetRotationSide(), newYawValue));
                        PeakFullRotations = 0; // this must NOT be reset before invoking the Yaw0 & ResetPosition events!
                    }
                }
                else // 180 degree position was crossed (or sample rate is too low --> incorrect result)
                {
                    if (_YawValue < newYawValue) // 180 crossed from negative to positive (right side to left side, moving right)
                    {
                        AccumulationStatus acc = (GetRotationSide() == Direction.Right) ? AccumulationStatus.Increasing : AccumulationStatus.Decreasing;
                        InvokeYaw180(new RotationEventArgs(Direction.Right, acc, GetFullRotations(), PeakFullRotations, GetRotationSide(), newYawValue));
                    }
                    else // 180 crossed from positive to negative (left side to right side, moving left)
                    {
                        AccumulationStatus acc = (GetRotationSide() == Direction.Left) ? AccumulationStatus.Increasing : AccumulationStatus.Decreasing;
                        InvokeYaw180(new RotationEventArgs(Direction.Left, acc, GetFullRotations(), PeakFullRotations, GetRotationSide(), newYawValue));
                    }
                }
            }
            _YawValue = newYawValue;
        }
        
        public void Reset()
        {            
            _CurrentLap = (YawValue < 0) ? -1 : 1;
            PeakFullRotations = 0;
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
      
        void InvokeFullRotationsChanged(RotationEventArgs e)
        {
            if (FullRotationsChanged != null)
            {
                FullRotationsChanged(this, e);
            }
        }
    }
}
