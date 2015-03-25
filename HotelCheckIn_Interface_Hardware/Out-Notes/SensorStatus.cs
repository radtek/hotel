using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.Out_Notes
{
    public class SensorStatus
    {
        
    }

    /// <summary>
    /// 100元传感器
    /// </summary>
    public class Sensor0Status
    {
        public int ChkSensor1 { get; set; }

        public int ChkSensor2 { get; set; }

        public int DivSensor1 { get; set; }

        public int DivSensor2 { get; set; }

        public int EjtSensor { get; set; }

        public int ExitSensor { get; set; }

        public int NearendSensor { get; set; }

        public int Always1 { get; set; }
    }

    /// <summary>
    /// 10元传感器
    /// </summary>
    public class Sensor1Status
    {
        public int SqlSensor { get; set; }

        public int Cassette0Sensor { get; set; }

        public int Cassette1Sensor { get; set; }

        public int ChkSensor3 { get; set; }

        public int ChkSensor4 { get; set; }

        public int NearendSensor { get; set; }

        public int RejectTraySw { get; set; }

        public int NotUsed { get; set; }
    }
}
