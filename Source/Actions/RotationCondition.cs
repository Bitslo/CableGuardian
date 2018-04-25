using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CableGuardian
{    
    public enum CompareOperator { Equal, EqualOrGreaterThan }

    class RotationCondition
    {
        public Trigger ParentTrigger { get; private set; }

        public uint TargetFullRotations { get; set; } = 0;
        public uint TargetFullRotationsMax { get; set; } = 99;
        public uint TargetPeakFullRotations { get; set; } = 0;        
       

        public CompareOperator CompOperator { get; set; } = CompareOperator.EqualOrGreaterThan;
        public Direction TargetRotationSide { get; set; } = Direction.Either;
        public AccumulationStatus TargetAccumulation { get; set; } = AccumulationStatus.Either;

        /// <summary>
        /// The new condition will be automatically linked to the provided trigger.
        /// </summary>
        /// <param name="parentTrigger"></param>
        public RotationCondition(Trigger parentTrigger)
        {
            ParentTrigger = parentTrigger;            
        }

        /// <summary>
        /// true when all the properties of the condition return true when compared to the rotation event
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool IsTrue(RotationEventArgs e)
        {                  
            if (CompOperator == CompareOperator.Equal)
            {
                if (e.FullRotations != TargetFullRotations)
                    return false;
            }
            else if (CompOperator == CompareOperator.EqualOrGreaterThan)
            {
                if (e.FullRotations < TargetFullRotations)
                    return false;
            }
            

            if (e.FullRotations > TargetFullRotationsMax)
            {
                return false;
            }


            if (TargetRotationSide != Direction.Either && e.RotationSide != TargetRotationSide)
            {
                return false;
            }

            if (TargetAccumulation != AccumulationStatus.Either && e.RotationAccumulationStatus != TargetAccumulation)
            {
                return false;
            }

                            
            if (e.PeakFullRotations < TargetPeakFullRotations)
                return false;                               
            
                        
            return true;
        }


        public void Delete()
        {
            ParentTrigger = null;
        }

        public void LoadFromXml(XElement xCondition)
        {
            if (xCondition != null)
            {
                if (Enum.TryParse(xCondition.GetElementValueTrimmed("TargetRotationSide"), out Direction d))
                    TargetRotationSide = d;
                else
                    TargetRotationSide = Direction.Either;

                if (Enum.TryParse(xCondition.GetElementValueTrimmed("CompOperator"), out CompareOperator op))
                    CompOperator = op;
                else
                    CompOperator = CompareOperator.EqualOrGreaterThan;

                TargetFullRotations = (uint)xCondition.GetElementValueInt("TargetFullRotations");
                TargetFullRotationsMax = (uint)xCondition.GetElementValueInt("TargetFullRotationsMax");

                if (Enum.TryParse(xCondition.GetElementValueTrimmed("TargetAccumulation"), out AccumulationStatus s))
                    TargetAccumulation = s;
                else
                    TargetAccumulation = AccumulationStatus.Either;

                TargetPeakFullRotations = (uint)xCondition.GetElementValueInt("TargetPeakFullRotations");                
                
            }
        }

        public XElement GetXml()
        {
            return new XElement("RotationCondition",
                                   new XElement("TargetRotationSide", TargetRotationSide),
                                   new XElement("CompOperator", CompOperator),
                                   new XElement("TargetFullRotations", TargetFullRotations),
                                   new XElement("TargetFullRotationsMax", TargetFullRotationsMax),
                                   new XElement("TargetAccumulation", TargetAccumulation),
                                   new XElement("TargetPeakFullRotations", TargetPeakFullRotations));
        }

        public override string ToString()
        {
            string output;
            if (ParentTrigger?.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
                output = $" AND peak rotations \u2265 {TargetPeakFullRotations}";
            else
            {
                string compOp = (CompOperator == CompareOperator.Equal) ? "=" : "\u2265";
                output = $" AND side is {TargetRotationSide} AND rotations {compOp} {TargetFullRotations}";
                if (CompOperator != CompareOperator.Equal)
                    output += (TargetFullRotationsMax < 99) ? $" AND rotations \u2264 {TargetFullRotationsMax}" : "";
                output += $" AND twisting is {TargetAccumulation}";
            }
            
            return output;
        }
    }
}
