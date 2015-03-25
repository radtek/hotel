using System;
using System.Collections.Generic;
using System.Xml;
using CommonLibrary;
using CommonLibrary.exception;
using HotelCheckIn_InterfaceSystem.model;

namespace HotelCheckIn_InterfaceSystem.PmsRequest
{
   public class AnalysisXml
   { /// <summary>
       /// 解析查询酒店房间信息 get_room_info_list返回的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RoomList get_room_info_list_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           RoomList roomList = new RoomList();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               roomList.request_Id = request_id_node.InnerText;
           }

           XmlNode rooms_node = responseNode.SelectSingleNode("rooms");
           if (null != rooms_node)
           {
               List<Room> room_list = new List<Room>();
               XmlNodeList roomNodes = rooms_node.SelectNodes("room");
               int roomCount = roomNodes.Count;
               for (int i = 0; i < roomCount; i++)
               {
                   Room room = new Room();
                   room.room_No = roomNodes[i].SelectSingleNode("room_no").InnerText;
                   room.room_Id = roomNodes[i].SelectSingleNode("room_id").InnerText;
                   room.room_Type_Id = roomNodes[i].SelectSingleNode("room_type_id").InnerText;
                   room.room_Building_Id = roomNodes[i].SelectSingleNode("room_building_id").InnerText;
                   room.room_Floor_Id = roomNodes[i].SelectSingleNode("room_floor_id").InnerText;
                   room.room_Direction_Id = roomNodes[i].SelectSingleNode("room_direction_id").InnerText;
                   room.door_Room_No = roomNodes[i].SelectSingleNode("door_room_no").InnerText;
                   room_list.Add(room);
               }
               roomList.room_List = room_list;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return roomList;
               }
               else if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           XmlNode room_types_node = responseNode.SelectSingleNode("room_types");
           if (null != room_types_node)
           {
               roomList.room_Types_List = this.getListDm(room_types_node, "room_type", "code");
           }
           XmlNode room_buildings_node = responseNode.SelectSingleNode("room_buildings");
           if (null != room_buildings_node)
           {
               roomList.room_Buildings_List = this.getListDm(room_buildings_node, "room_building", "building_code");
           }
           XmlNode room_floors_node = responseNode.SelectSingleNode("room_floors");
           if (null != room_floors_node)
           {
               roomList.room_Floors_List = this.getListDm(room_floors_node, "room_floor", "floor_code");
           }
           XmlNode room_directions_node = responseNode.SelectSingleNode("room_directions");
           if (null != room_directions_node)
           {
               roomList.room_Directions_List = this.getListDirections(room_directions_node);
           }

           roomList.return_Info = this.getReturnInfo(responseNode);

           return roomList;
       }

       /// <summary>
       /// 解析查询酒店当前可用房间列表 get_avail_room_list产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public AvailRooms get_avail_room_list_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           AvailRooms result = new AvailRooms();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode avail_rooms_node = responseNode.SelectSingleNode("avail_rooms");
           if (null != avail_rooms_node)
           {
               List<Room> avail_room_list = new List<Room>();
               XmlNodeList avail_rooms = avail_rooms_node.SelectNodes("avail_room");
               int count = avail_rooms.Count;
               for (int i = 0; i < count; i++)
               {
                   Room room = new Room();
                   room.room_Id = avail_rooms[i].SelectSingleNode("room_id").InnerText;
                   room.room_No = avail_rooms[i].SelectSingleNode("room_no").InnerText;
                   room.room_Type_Id = avail_rooms[i].SelectSingleNode("room_type_id").InnerText;
                   avail_room_list.Add(room);
               }
               result.avail_Room_List = avail_room_list;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return result;
               }
               else if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           XmlNode total_avail_count_node = responseNode.SelectSingleNode("avail_room_count");
           if (null != total_avail_count_node)
           {
               result.total_Avail_Count = total_avail_count_node.InnerText;
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析查询酒店当前可用钟点房列表 get_avail_clock_room_list产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public AvailRooms get_avail_clock_room_list_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           AvailRooms result = new AvailRooms();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode avail_rooms_node = responseNode.SelectSingleNode("avail_rooms");
           if (null != avail_rooms_node)
           {
               List<Room> avail_room_list = new List<Room>();
               XmlNodeList avail_rooms = avail_rooms_node.SelectNodes("avail_room");
               int count = avail_rooms.Count;
               for (int i = 0; i < count; i++)
               {
                   Room room = new Room();
                   room.room_Id = avail_rooms[i].SelectSingleNode("room_id").InnerText;
                   room.room_No = avail_rooms[i].SelectSingleNode("room_no").InnerText;
                   room.room_Type_Id = avail_rooms[i].SelectSingleNode("room_type_id").InnerText;
                   avail_room_list.Add(room);
               }
               result.avail_Room_List = avail_room_list;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return result;
               }
               else if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           XmlNode total_avail_count_node = responseNode.SelectSingleNode("avail_room_count");
           if (null != total_avail_count_node)
           {
               result.total_Avail_Count = total_avail_count_node.InnerText;
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析查询酒店每日房价列表 get_room_rate_list产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RoomRateList get_room_rate_list_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           RoomRateList result = new RoomRateList();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode room_tates_node = responseNode.SelectSingleNode("room_rates");
           if (null != room_tates_node)
           {
               List<RoomRate> rate_list = new List<RoomRate>();
               XmlNodeList room_rates = room_tates_node.SelectNodes("room_rate");
               int count = room_rates.Count;
               for (int i = 0; i < count; i++)
               {
                   RoomRate roomRate = new RoomRate();
                   roomRate.room_Type_Id = room_rates[i].SelectSingleNode("room_type_id").InnerText;
                   roomRate.rate_Date = room_rates[i].SelectSingleNode("rate_date").InnerText;
                   roomRate.rate_Code = room_rates[i].SelectSingleNode("rate_code").InnerText;
                   roomRate.Rate = room_rates[i].SelectSingleNode("rate").InnerText;
                   rate_list.Add(roomRate);
               }
               result.rate_List = rate_list;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return result;
               }
               else if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析查询酒店钟点房价列表 get_clock_room_rate_list产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public ClockRoomRateList get_clock_room_rate_list_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           ClockRoomRateList result = new ClockRoomRateList();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode clock_room_rates_node = responseNode.SelectSingleNode("clock_room_rates");
           if (null != clock_room_rates_node)
           {
               List<ClockRate> clock_room_rate_list = new List<ClockRate>();
               XmlNodeList clock_room_rates = clock_room_rates_node.SelectNodes("clock_room_rate");
               int count = clock_room_rates.Count;
               for (int i = 0; i < count; i++)
               {
                   ClockRate clockRate = new ClockRate();
                   clockRate.room_Type_Id = clock_room_rates[i].SelectSingleNode("room_type_id").InnerText;
                   clockRate.room_Code = clock_room_rates[i].SelectSingleNode("rate_code").InnerText;
                   clockRate.Hour = clock_room_rates[i].SelectSingleNode("hour").InnerText;
                   clockRate.Rate = clock_room_rates[i].SelectSingleNode("rate").InnerText;
                   clock_room_rate_list.Add(clockRate);
               }
               result.clock_Room_Rate_List = clock_room_rate_list;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return result;
               }
               else if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析会员卡信息
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns>CardInfo</returns>
       public CardInfo get_card_info_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           CardInfo card = new CardInfo();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               card.request_Id = request_id_node.InnerText;
           }

           XmlNode card_info_node = responseNode.SelectSingleNode("card_info");
           if (null != card_info_node)
           {
               card.card_Id = card_info_node.SelectSingleNode("card_id").InnerText;
               card.card_Type_Id = card_info_node.SelectSingleNode("card_type_id").InnerText;
               card.card_Type_Name = card_info_node.SelectSingleNode("card_type_name").InnerText;
               card.rate_Code = card_info_node.SelectSingleNode("rate_code").InnerText;
               card.guest_Market_Code = card_info_node.SelectSingleNode("guest_market_code").InnerText;
               card.card_No = card_info_node.SelectSingleNode("card_no").InnerText;
               card.user_Name = card_info_node.SelectSingleNode("user_name").InnerText;
               card.gender_Id = card_info_node.SelectSingleNode("gender_id").InnerText;
               card.id_Card_Type_Id = card_info_node.SelectSingleNode("id_card_type_id").InnerText;
               card.id_Card_No = card_info_node.SelectSingleNode("id_card_no").InnerText;
               card.Birthday = card_info_node.SelectSingleNode("birthday").InnerText;
               card.Deposit = card_info_node.SelectSingleNode("deposit").InnerText;
               card.Email = card_info_node.SelectSingleNode("email").InnerText;
               card.Mobile = card_info_node.SelectSingleNode("mobile").InnerText;
               card.Address = card_info_node.SelectSingleNode("address").InnerText;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           card.return_Info = this.getReturnInfo(responseNode);

           return card;

       }

       /// <summary>
       /// 解析查询酒店今日入住订单列表(通过证件号) get_arriving_list_by_guest产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public ArrivingList get_arriving_list_by_guest_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           ArrivingList result = new ArrivingList();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode allocations_node = responseNode.SelectSingleNode("allocations");
           if (null != allocations_node)
           {
               List<Allocation> allocation_list = new List<Allocation>();
               XmlNodeList allocations = allocations_node.SelectNodes("allocation");
               int count = allocations.Count;
               for (int i = 0; i < count; i++)
               {
                   Allocation entity = analysisAllocation(allocations[i]);
                   allocation_list.Add(entity);
               }
               result.allocation_List = allocation_list;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return result;
               }
               else if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析查询酒店今日入住订单列表(通过会员卡信息) get_arriving_list_by_card产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public ArrivingList get_arriving_list_by_card_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           ArrivingList result = new ArrivingList();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode allocations_node = responseNode.SelectSingleNode("allocations");
           if (null != allocations_node)
           {
               List<Allocation> allocation_list = new List<Allocation>();
               XmlNodeList allocations = allocations_node.SelectNodes("allocation");
               int count = allocations.Count;
               for (int i = 0; i < count; i++)
               {
                   Allocation entity = analysisAllocation(allocations[i]);
                   allocation_list.Add(entity);
               }
               result.allocation_List = allocation_list;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return result;
               }
               else if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析预订客人登记入住create_order_register产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RegistBack create_order_register_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           RegistBack result = new RegistBack();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode register_id_node = responseNode.SelectSingleNode("register_id");
           if (null != register_id_node)
           {
               result.register_Id = register_id_node.InnerText;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               string returnCode = info.return_Code;
               if (this.isExceptionStr(returnCode))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else if (returnCode.Equals(ErrorCode.REGISTER_ROOM_NOT_EXIST) || returnCode.Equals(ErrorCode.REGISTER_ROOM_IS_USING)
                   || returnCode.Equals(ErrorCode.REGISTER_ALLOCATION_NOT_EXIST) || returnCode.Equals(ErrorCode.REGISTER_ROOM_IS_NOT_FULLY_AVALIABLE)
                   || returnCode.Equals(ErrorCode.REGISTER_NO_GUESTS))
               {
                   result.return_Info = info;
                   return result;
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析步入客人登记入住create_walking_register产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RegistBack create_walking_register_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           RegistBack result = new RegistBack();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode register_id_node = responseNode.SelectSingleNode("register_id");
           if (null != register_id_node)
           {
               result.register_Id = register_id_node.InnerText;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               string returnCode = info.return_Code;
               if (this.isExceptionStr(returnCode))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               else if (returnCode.Equals(ErrorCode.REGISTER_ROOM_NOT_EXIST) || returnCode.Equals(ErrorCode.REGISTER_ROOM_IS_USING)
                   || returnCode.Equals(ErrorCode.REGISTER_RATE_CODE_NOT_EXIST) || returnCode.Equals(ErrorCode.REGISTER_ROOM_IS_NOT_FULLY_AVALIABLE)
                   || returnCode.Equals(ErrorCode.REGISTER_NO_ROOM_DAILY_RATE) || returnCode.Equals(ErrorCode.REGISTER_NO_GUESTS))
               {
                   result.return_Info = info;
                   return result;
               }
               else
               {
                   throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析钟点客人登记入住create_clock_register产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RegistBack create_clock_register_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           RegistBack result = new RegistBack();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode register_id_node = responseNode.SelectSingleNode("register_id");
           if (null != register_id_node)
           {
               result.register_Id = register_id_node.InnerText;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               string returnCode = info.return_Code;
               if (this.isExceptionStr(returnCode))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               if (returnCode.Equals(ErrorCode.REGISTER_ROOM_NOT_EXIST) || returnCode.Equals(ErrorCode.REGISTER_ROOM_IS_USING)
                   || returnCode.Equals(ErrorCode.REGISTER_RATE_CODE_NOT_EXIST) || returnCode.Equals(ErrorCode.REGISTER_NO_ROOM_DAILY_RATE)
                   || returnCode.Equals(ErrorCode.REGISTER_NO_GUESTS))
               {
                   result.return_Info = info;
                   return result;
               }
               throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
           }

           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析查询客人在住登记单列表get_checking_registers产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RegistList get_checking_registers_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           RegistList result = new RegistList();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode registers_node = responseNode.SelectSingleNode("registers");
           if (null != registers_node)
           {
               List<Register> regist_list = new List<Register>();
               XmlNodeList registers = registers_node.SelectNodes("register");
               int count = registers.Count;
               for (int i = 0; i < count; i++)
               {
                   Register entity = analysisRegister(registers[i]);
                   regist_list.Add(entity);
               }
               result.regist_List = regist_list;
           }
           else
           {
               ReturnInfo info = this.getReturnInfo(responseNode);
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return result;
               }
               else if (this.isExceptionStr(info.return_Code))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
           }
           result.return_Info = this.getReturnInfo(responseNode);
           return result;
       }

       /// <summary>
       /// 解析登记单结账退房check_out_register产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RegistBack check_out_register_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           RegistBack result = new RegistBack();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode register_id_node = responseNode.SelectSingleNode("register_id");
           if (null != register_id_node)
           {
               result.register_Id = register_id_node.InnerText;
           }
           else
           {
               ReturnInfo info = getReturnInfo(responseNode);
               string returnCode = info.return_Code;
               if (this.isExceptionStr(returnCode))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               if (returnCode.Equals(ErrorCode.REGISTER_NOT_EXIST) || returnCode.Equals(ErrorCode.REGISTER_HAS_CHECKOUT)
                   || returnCode.Equals(ErrorCode.REGISTER_WRONG_BALANCE_MONEY))
               {
                   result.return_Info = info;
                   return result;
               }
               throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
           }

           result.return_Info = getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析查询登记单账单get_register_bill产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RegistAccount get_register_bill_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           RegistAccount result = new RegistAccount();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode register_accounts_node = responseNode.SelectSingleNode("register_accounts");
           if (null != register_accounts_node)
           {
               List<Bill> register_account_list = new List<Bill>();
               XmlNodeList register_accounts = register_accounts_node.SelectNodes("register_account");
               int count = register_accounts.Count;
               for (int i = 0; i < count; i++)
               {
                   Bill entity = analysisBill(register_accounts[i]);
                   register_account_list.Add(entity);
               }
               result.register_Account_List = register_account_list;
           }
           else
           {
               ReturnInfo info = getReturnInfo(responseNode);
               string returnCode = info.return_Code;
               if (string.IsNullOrEmpty(info.return_Code) && string.IsNullOrEmpty(info.Description))
               {
                   return result;
               }
               else if (isExceptionStr(returnCode))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               if (returnCode.Equals(ErrorCode.REGISTER_NOT_EXIST))
               {
                   result.return_Info = info;
                   return result;
               }
               throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
           }

           result.return_Info = getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析会员登录 card_login产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public CardDetailList card_login_analysis(string xmlStr)
       {
           XmlNode responseNode = getResponseNode(xmlStr);

           CardDetailList result = new CardDetailList();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode card_list_node = responseNode.SelectSingleNode("card_list");
           if (null != card_list_node)
           {
               List<CardDetail> card_list = new List<CardDetail>();
               XmlNodeList card_details = card_list_node.SelectNodes("card_detail");
               int count = card_details.Count;
               for (int i = 0; i < count; i++)
               {
                   CardDetail cardDetail = analysisCardDetail(card_details[i]);
                   card_list.Add(cardDetail);
               }
               result.card_List = card_list;
           }
           else
           {
               ReturnInfo info = getReturnInfo(responseNode);
               string returnCode = info.return_Code;
               if (isExceptionStr(returnCode))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               if (returnCode.Equals(ErrorCode.CARD_WRONG_CARD_NO) || returnCode.Equals(ErrorCode.CARD_WRONG_PASSWORD) || returnCode.Equals(ErrorCode.CARD_NOT_ACTIVED))
               {
                   result.return_Info = info;
                   return result;
               }
               throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
           }

           result.return_Info = getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析根据门卡号查询登记单get_room_checking_guests产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public RegisterInfo get_room_checking_guests_analysis(string xmlStr)
       {
           XmlNode responseNode = getResponseNode(xmlStr);

           RegisterInfo result = new RegisterInfo();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           XmlNode register_info_node = responseNode.SelectSingleNode("register_info");
           if (null != register_info_node)
           {
               result.register_Id = register_info_node.SelectSingleNode("register_id").InnerText;
               result.register_Type = register_info_node.SelectSingleNode("register_type").InnerText;
               result.room_Type = register_info_node.SelectSingleNode("room_type").InnerText;
               result.room_No = register_info_node.SelectSingleNode("room_no").InnerText;
               result.guest_Names = register_info_node.SelectSingleNode("guest_names").InnerText;
               result.check_In_Date = register_info_node.SelectSingleNode("check_in_date").InnerText;
               result.check_Out_Date = register_info_node.SelectSingleNode("check_out_date").InnerText;
               result.Note = register_info_node.SelectSingleNode("note").InnerText;
           }
           else
           {
               ReturnInfo info = getReturnInfo(responseNode);
               string returnCode = info.return_Code;
               if (isExceptionStr(returnCode))
               {
                   throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
               }
               if (returnCode.Equals(ErrorCode.ROOM_CARD_NOT_EXIST))
               {
                   result.return_Info = info;
                   return result;
               }
               throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);
           }

           result.return_Info = getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析锁定或解除锁定房间lock_room产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public VoidReturn lock_room_analysis(string xmlStr)
       {
           XmlNode responseNode = getResponseNode(xmlStr);

           VoidReturn result = new VoidReturn();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }

           ReturnInfo info = getReturnInfo(responseNode);
           string returnCode = info.return_Code;
           if ("".Equals(returnCode))
           {
               result.return_Info = info;
               return result;
           }
           if (isExceptionStr(returnCode))
           {
               throw new Exception("PMS返回通用错误: return_code: " + info.return_Code + " \n description: " + info.Description);
           }
           if (returnCode.Equals(ErrorCode.ROOM_CARD_NOT_EXIST) || returnCode.Equals(ErrorCode.MISSING_DOOR_CARD_NO) || returnCode.Equals(ErrorCode.REGISTER_NOT_EXIST))
           {
               result.return_Info = info;
               return result;
           }
           throw new Exception("PMS返回未知错误: return_code: " + info.return_Code + " \n description: " + info.Description);

           return result;
       }

       /// <summary>
       /// 解析锁定或解除锁定房间lock_room产生的xml
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       public VoidReturn issue_door_card_analysis(string xmlStr)
       {
           XmlNode responseNode = this.getResponseNode(xmlStr);

           VoidReturn result = new VoidReturn();
           XmlNode request_id_node = responseNode.SelectSingleNode("request_id");
           if (null != request_id_node)
           {
               result.request_Id = request_id_node.InnerText;
           }
           result.return_Info = this.getReturnInfo(responseNode);

           return result;
       }

       /// <summary>
       /// 解析CardDetail
       /// </summary>
       /// <param name="cardDetailNode"></param>
       /// <returns></returns>
       private CardDetail analysisCardDetail(XmlNode cardDetailNode)
       {
           CardDetail entity = new CardDetail();
           entity.card_Type_Id = cardDetailNode.SelectSingleNode("card_type_id").InnerText;
           entity.card_No = cardDetailNode.SelectSingleNode("card_no").InnerText;
           entity.user_Name = cardDetailNode.SelectSingleNode("user_name").InnerText;
           entity.Gender = cardDetailNode.SelectSingleNode("gender").InnerText;
           entity.id_Card_Type_Id = cardDetailNode.SelectSingleNode("id_card_type_id").InnerText;
           entity.id_Card_No = cardDetailNode.SelectSingleNode("id_card_no").InnerText;
           return entity;
       }

       /// <summary>
       /// 解析账单
       /// </summary>
       /// <param name="billNode"></param>
       /// <returns></returns>
       private Bill analysisBill(XmlNode billNode)
       {
           Bill bill = new Bill();
           bill.account_Type_Name = billNode.SelectSingleNode("account_type_name").InnerText;
           bill.Payment = billNode.SelectSingleNode("payment").InnerText;
           bill.Credit = billNode.SelectSingleNode("credit").InnerText;
           bill.Debit = billNode.SelectSingleNode("debit").InnerText;
           bill.Money = billNode.SelectSingleNode("money").InnerText;
           bill.created_At = billNode.SelectSingleNode("created_at").InnerText;
           return bill;
       }

       /// <summary>
       /// 解析客人在住登记单
       /// </summary>
       /// <param name="registNode"></param>
       /// <returns></returns>
       private Register analysisRegister(XmlNode registNode)
       {
           Register register = new Register();
           register.register_Id = registNode.SelectSingleNode("register_id").InnerText;
           register.room_Id = registNode.SelectSingleNode("room_id").InnerText;
           register.check_In_Date = registNode.SelectSingleNode("check_in_date").InnerText;
           register.check_In_Time = registNode.SelectSingleNode("check_in_time").InnerText;
           register.check_Out_Date = registNode.SelectSingleNode("check_out_date").InnerText;
           register.check_Out_Time = registNode.SelectSingleNode("check_out_time").InnerText;
           register.room_Order_Id = registNode.SelectSingleNode("room_order_id").InnerText;
           register.company_Id = registNode.SelectSingleNode("company_id").InnerText;
           register.rate_Code = registNode.SelectSingleNode("rate_code").InnerText;
           register.room_Rate = registNode.SelectSingleNode("room_rate").InnerText;
           register.guest_Names = registNode.SelectSingleNode("guest_names").InnerText;
           register.member_Card_No = registNode.SelectSingleNode("member_card_no").InnerText;
           register.biz_Source_Id = registNode.SelectSingleNode("biz_source_id").InnerText;
           register.guest_Market_Id = registNode.SelectSingleNode("guest_market_id").InnerText;
           register.total_Fee = registNode.SelectSingleNode("total_fee").InnerText;
           register.total_Consume = registNode.SelectSingleNode("total_consume").InnerText;
           return register;
       }

       /// <summary>
       /// 解析订单
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       private Allocation analysisAllocation(XmlNode allocationNode)
       {

           Allocation entity = new Allocation();

           entity.allocation_Id = allocationNode.SelectSingleNode("allocation_id").InnerText;
           entity.room_Id = allocationNode.SelectSingleNode("room_id").InnerText;
           entity.room_Type_Id = allocationNode.SelectSingleNode("room_type_id").InnerText;
           entity.check_In_Date = allocationNode.SelectSingleNode("check_in_date").InnerText;
           entity.check_Out_Date = allocationNode.SelectSingleNode("check_out_date").InnerText;
           entity.order_Check_In_Date = allocationNode.SelectSingleNode("order_check_in_date").InnerText;
           entity.order_Check_Out_Date = allocationNode.SelectSingleNode("order_check_out_date").InnerText;
           entity.booker_Name = allocationNode.SelectSingleNode("booker_name").InnerText;
           entity.booker_Mobile = allocationNode.SelectSingleNode("booker_mobile").InnerText;
           entity.booker_Time = allocationNode.SelectSingleNode("booker_time").InnerText;
           entity.rate_Code = allocationNode.SelectSingleNode("rate_code").InnerText;
           entity.Deposit = allocationNode.SelectSingleNode("deposit").InnerText;

           return entity;
       }

       /// <summary>
       /// 获取请求返回节点
       /// </summary>
       /// <param name="xmlStr"></param>
       /// <returns></returns>
       private XmlNode getResponseNode(string xmlStr)
       {
           XmlDocument doc = new XmlDocument();
           try
           {
               doc.LoadXml(xmlStr);
           }
           catch (Exception e)
           {
               throw new AnalysisXmlException("构建xml文档失败，pms请求返回格式不对！" + xmlStr + " \n " + e.Message);
           }

           XmlNode responseNode = doc.SelectSingleNode("pms_response");   //没有pms_response节点，请求失败，返回的是错误包
           if (null == responseNode)
           {
               throw new AnalysisXmlException("无pms_response节点:" + xmlStr);
           }
           return responseNode;
       }

       /// <summary>
       /// 产生房间方向等...
       /// </summary>
       /// <param name="node"></param>
       /// <param name="nodes"></param>
       /// <param name="node"></param>
       /// <returns></returns>
       private List<DM> getListDm(XmlNode xmlNode, string node, string code_node)
       {
           List<DM> dm_list = new List<DM>();

           XmlNodeList dm_nodes = xmlNode.SelectNodes(node);
           int dmCount = dm_nodes.Count;
           for (int i = 0; i < dmCount; i++)
           {
               DM dm = new DM();
               dm.Id = dm_nodes[i].SelectSingleNode("id").InnerText;
               dm.Name = dm_nodes[i].SelectSingleNode("name").InnerText;
               dm.Code = dm_nodes[i].SelectSingleNode(code_node).InnerText;
               dm_list.Add(dm);
           }

           return dm_list;
       }
       /// <summary>
       /// 方向
       /// </summary>
       /// <param name="xmlNode"></param>
       /// <returns></returns>
       private List<DM> getListDirections(XmlNode xmlNode)
       {
           List<DM> dm_list = new List<DM>();

           XmlNodeList dm_nodes = xmlNode.SelectNodes("room_direction");
           int dmCount = dm_nodes.Count;
           for (int i = 0; i < dmCount; i++)
           {
               DM dm = new DM();
               dm.Id = dm_nodes[i].SelectSingleNode("id").InnerText;
               dm.Name = dm_nodes[i].SelectSingleNode("name").InnerText;
               dm.Code = "";
               dm_list.Add(dm);
           }

           return dm_list;
       }

       private ReturnInfo getReturnInfo(XmlNode node)
       {
           ReturnInfo info = new ReturnInfo();
           info.return_Code = node.SelectSingleNode("return_info").SelectSingleNode("return_code").InnerText;
           info.Description = node.SelectSingleNode("return_info").SelectSingleNode("description").InnerText;
           return info;
       }

       /// <summary>
       /// 返回错误代码是否是需要抛出异常的代码
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       private bool isExceptionStr(string str)
       {
           return (str.Equals(ErrorCode.EXCEPTION_STR1) || str.Equals(ErrorCode.EXCEPTION_STR2) || str.Equals(ErrorCode.EXCEPTION_STR3) || str.Equals(ErrorCode.EXCEPTION_STR4) || str.Equals(ErrorCode.EXCEPTION_STR5));
       }

    }
}
