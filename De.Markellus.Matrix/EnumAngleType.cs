using System;
using System.Collections.Generic;
using System.Text;

namespace De.Markellus.Matrix
{
    public enum EnumAngleType
    {
        Degree,
        Radian,
        Gradian
    }

    public class AngleConversion
    {
        public static double RadianToDegree(double dAngle)
        {
            return dAngle * 180 / Math.PI;
        }

        public static double DegreeToRadian(double dAngle)
        {
            return dAngle * Math.PI / 180;
        }

        public static double RadianToGradian(double dAngle)
        {
            return dAngle * 63.662;
        }

        public static double GradianToRadian(double dAngle)
        {
            return dAngle / 63.662;
        }
    }
}
