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

        public uint TargetHalfTurns { get; set; } = 3;
        public uint TargetHalfTurnsMax { get; set; } = 99;
        public uint TargetPeakHalfTurns { get; set; } = 3;        
       

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
            if (ParentTrigger.TriggeringEvent != YawTrackerOrientationEvent.ResetPosition)
            {
                if (CompOperator == CompareOperator.Equal)
                {
                    if (e.HalfTurns != TargetHalfTurns)
                        return false;
                }
                else if (CompOperator == CompareOperator.EqualOrGreaterThan)
                {
                    if (e.HalfTurns < TargetHalfTurns)
                        return false;

                    if (e.HalfTurns > TargetHalfTurnsMax)                    
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
            }
            else
            {
                if (e.PeakHalfTurns < TargetPeakHalfTurns)
                    return false;
            }
                        
            return true;
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

                if (xCondition.GetElementValueOrNull("TargetHalfTurns") != null)
                    TargetHalfTurns = (uint)xCondition.GetElementValueInt("TargetHalfTurns", 3);
                else
                {
                    TargetHalfTurns = (uint)xCondition.GetElementValueInt("TargetFullRotations") * 2; // backwards compatibility
                    if (TargetHalfTurns > 99)
                    {
                        TargetHalfTurns = 99;
                    }
                }

                if (xCondition.GetElementValueOrNull("TargetHalfTurnsMax") != null)
                    TargetHalfTurnsMax = (uint)xCondition.GetElementValueInt("TargetHalfTurnsMax");
                else
                {
                    TargetHalfTurnsMax = (uint)xCondition.GetElementValueInt("TargetFullRotationsMax") * 2; // backwards compatibility                                
                    if (TargetHalfTurnsMax > 99)
                    {
                        TargetHalfTurnsMax = 99;
                    }
                }

                if (Enum.TryParse(xCondition.GetElementValueTrimmed("TargetAccumulation"), out AccumulationStatus s))
                    TargetAccumulation = s;
                else
                    TargetAccumulation = AccumulationStatus.Either;

                if (xCondition.GetElementValueOrNull("TargetPeakHalfTurns") != null)
                    TargetPeakHalfTurns = (uint)xCondition.GetElementValueInt("TargetPeakHalfTurns", 3);
                else
                {
                    TargetPeakHalfTurns = (uint)xCondition.GetElementValueInt("TargetPeakFullRotations") * 2; // backwards compatibility
                    if (TargetPeakHalfTurns > 99)
                    {
                        TargetPeakHalfTurns = 99;
                    }
                }
            }
        }

        public XElement GetXml()
        {
            return new XElement("RotationCondition",
                                   new XElement("TargetRotationSide", TargetRotationSide),
                                   new XElement("CompOperator", CompOperator),
                                   new XElement("TargetHalfTurns", TargetHalfTurns),
                                   new XElement("TargetHalfTurnsMax", TargetHalfTurnsMax),
                                   new XElement("TargetAccumulation", TargetAccumulation),
                                   new XElement("TargetPeakHalfTurns", TargetPeakHalfTurns));
        }

        public override string ToString()
        {
            string output;
            if (ParentTrigger?.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
                output = $" AND peak half-turns \u2265 {TargetPeakHalfTurns}";
            else
            {
                string compOp = (CompOperator == CompareOperator.Equal) ? "=" : "\u2265";
                output = $" AND side is {TargetRotationSide} AND half-turns {compOp} {TargetHalfTurns}";
                if (CompOperator != CompareOperator.Equal)
                    output += (TargetHalfTurnsMax < 99) ? $" AND half-turns \u2264 {TargetHalfTurnsMax}" : "";
                output += $" AND twisting is {TargetAccumulation}";
            }
            
            return output;
        }
    }
}
