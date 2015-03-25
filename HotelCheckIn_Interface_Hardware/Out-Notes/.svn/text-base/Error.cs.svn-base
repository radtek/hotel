
namespace HotelCheckIn_Interface_Hardware.Out_Notes
{
    public class Error
    {
        private int _errorCode;
        private string _errorMsg;

        public int Code
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
                switch (_errorCode)
                {
                    case 0x30:
                        _errorMsg = "Good";
                        break;
                    case 0x31:
                        _errorMsg = "Normal stop";
                        break;
                    case 0x32:
                        _errorMsg = "Pickup error";
                        break;
                    case 0x33:
                        _errorMsg = "JAM at CHK 1,2 Sensor";
                        break;
                    case 0x34:
                        _errorMsg = "Overflow bill";
                        break;
                    case 0x35:
                        _errorMsg = "JAM at EXIT Sensor or EJT Sensor";
                        break;
                    case 0x36:
                        _errorMsg = "JAM at DIV Sensor";
                        break;
                    case 0x37:
                        _errorMsg = "Undefined command";
                        break;
                    case 0x38:
                        _errorMsg = "Upper Bill-End";
                        break;
                    case 0x3B:
                        _errorMsg = "Note request error";
                        break;
                    case 0x3C:
                        _errorMsg = "Counting Error(between DIV Sensor and EJT Sensor)";
                        break;
                    case 0x3D:
                        _errorMsg = "Counting Error(between EJT Sensor and EXIT Sensor)";
                        break;
                    case 0x3F:
                        _errorMsg = "Reject Tray is not recognized";
                        break;
                    case 0x40:
                        _errorMsg = "Lower Bill-End";
                        break;
                    case 0x41:
                        _errorMsg = "Motor Stop";
                        break;
                    case 0x42:
                        _errorMsg = "JAM at Div Sensor";
                        break;
                    case 0x43:
                        _errorMsg = "Timeout (From DIV Sensor to EJT Sensor)";
                        break;
                    case 0x44:
                        _errorMsg = "Over Reject";
                        break;
                    case 0x45:
                        _errorMsg = "Cassette is not recognized";
                        break;
                    case 0x47:
                        _errorMsg = "Dispensing timeout";
                        break;
                    case 0x49:
                        _errorMsg = "Diverter solenoid or SOL Sensor error";
                        break;
                    case 0x4A:
                        _errorMsg = "SOL Sensor error";
                        break;
                    case 0x4E:
                        _errorMsg = "Purge error (Jam at Div Sensor)";
                        break;
                    default:
                        _errorMsg = "Undefined Error";
                        break;
                }
            }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
    }
}
