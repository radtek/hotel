using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.ReadIdCard
{
    public interface IdCardInterFace
    {
        bool OpenPort();//打开端口
        bool ClosePort();//关闭端口
        bool IfHaveCard();//找卡，选卡
        ResultIdModel ReadCard();//读卡
        string GetSex(string sex);//解析性别
        string GetNation(string nation);//解析民族
    }
}
