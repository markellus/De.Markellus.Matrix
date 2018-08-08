using System;
using System.Collections.Generic;
using System.Text;

namespace De.Markellus.Matrix
{
    public class Angle
    {
        private double _dAngle;

        public Angle(double dAngle = 0.0, EnumAngleType eAngleType = EnumAngleType.Radian)
        {
            switch (eAngleType)
            {
                case EnumAngleType.Radian:
                    _dAngle = dAngle;
                    break;
                case EnumAngleType.Degree:
                    _dAngle = AngleConversion.DegreeToRadian(dAngle);
                    break;
                case EnumAngleType.Gradian:
                    _dAngle = AngleConversion.GradianToRadian(dAngle);
                    break;
            }
        }

        public double GetDegree()
        {
            return AngleConversion.RadianToDegree(_dAngle);
        }

        public double GetRadian()
        {
            return _dAngle;
        }

        public double GetGradian()
        {
            return AngleConversion.RadianToGradian(_dAngle);
        }
    }
}
