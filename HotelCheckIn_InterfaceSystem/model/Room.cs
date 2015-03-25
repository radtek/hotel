﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_InterfaceSystem.model
{
    public class Room
    {
        private string room_no;                     //房间号
        private string room_id;                     //房间ID
        private string room_type_id;                //房型ID
        private string room_type_name;              //房型名称
        private string room_building_id;            //楼栋ID
        private string room_floor_id;               //楼层ID
        private string room_direction_id;           //朝向ID
        private string door_room_no;                //门卡房号

        /// <summary>
        /// 房间号
        /// </summary>
        public string room_No
        {
            get { return this.room_no; }
            set { this.room_no = value; }
        }
        /// <summary>
        /// 房间ID
        /// </summary>
        public string room_Id
        {
            get { return this.room_id; }
            set { this.room_id = value; }
        }
        /// <summary>
        /// 房型ID
        /// </summary>
        public string room_Type_Id
        {
            get { return this.room_type_id; }
            set { this.room_type_id = value; }
        }
        /// <summary>
        /// 房型ID
        /// </summary>
        public string room_Type_Name
        {
            get { return this.room_type_name; }
            set { this.room_type_name = value; }
        }
        /// <summary>
        /// 楼栋ID
        /// </summary>
        public string room_Building_Id
        {
            get { return this.room_building_id; }
            set { this.room_building_id = value; }
        }
        /// <summary>
        /// 楼层ID
        /// </summary>
        public string room_Floor_Id
        {
            get { return this.room_floor_id; }
            set { this.room_floor_id = value; }
        }
        /// <summary>
        /// 朝向ID
        /// </summary>
        public string room_Direction_Id
        {
            get { return this.room_direction_id; }
            set { this.room_direction_id = value; }
        }
        /// <summary>
        /// 门卡房号
        /// </summary>
        public string door_Room_No
        {
            get { return this.door_room_no; }
            set { this.door_room_no = value; }
        }
    }
}
