﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelCheckIn_BackSystem.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:12648/DataService/WebService/Interface/HeartBeatUpload.asmx")]
        public string HotelCheckIn_BackSystem_PlatService_HeartBeatUpload {
            get {
                return ((string)(this["HotelCheckIn_BackSystem_PlatService_HeartBeatUpload"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.1.155:8015/DataService/WebService/InterFace.asmx")]
        public string HotelCheckIn_BackSystem_PlatServ_InterFace {
            get {
                return ((string)(this["HotelCheckIn_BackSystem_PlatServ_InterFace"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.1.155:8013/DataService/WebService/Interface/HeartBeatUpload.asmx")]
        public string HotelCheckIn_BackSystem_PlatServ_HeartBeatUpload {
            get {
                return ((string)(this["HotelCheckIn_BackSystem_PlatServ_HeartBeatUpload"]));
            }
        }
    }
}
