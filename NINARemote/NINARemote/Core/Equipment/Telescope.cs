﻿using Newtonsoft.Json.Linq;
using NINARemote.Core.Equipment.Interfaces;
using NINARemote.Core.Interfaces;
using NINARemote.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NINARemote.Core.Equipment
{
    public class Telescope : BaseViewModel, IEquipment<TelescopeInfo>
    {
        public IFetchable<TelescopeInfo> DeviceFetcher { get; set; }

        private TelescopeInfo _currentInfo = new TelescopeInfo();
        public TelescopeInfo CurrentInfo
        {
            get => _currentInfo;
            set => SetProperty(ref _currentInfo, value);
        }

        public Telescope()
        {
            Title = "Telescope";

            DeviceFetcher = new TelescopeFetcher();
        }

        /// <summary>
        /// Call this method when CurrentInfo == null and it gets selected in the picker
        /// </summary>
        /// <returns></returns>
        public async Task UpdateDevice()
        {
            try
            {
                TelescopeInfo info = await DeviceFetcher.Fetch(); // In two lines, so info doesn't become null
                CurrentInfo = info;
            } catch (Exception e)
            {
                // TODO: Make alert if error
            }
        }
    }

    public class TelescopeFetcher : IFetchable<TelescopeInfo>
    {
        public int Interval { get; set; } = 0;
        public bool UseInterval { get; set; } = false;

        public async Task<TelescopeInfo> Fetch()
        {
            HttpClient client = new HttpClient() { Timeout = new System.TimeSpan(0, 0, 0, 0, 500) };
            string url = $"{Utility.GetAPIHost()}/api/equipment?property=telescope";
            string json = await client.GetStringAsync(url);
            client.Dispose();

            JObject obj = JObject.Parse(json);
            JObject res = JObject.Parse(obj.Value<string>("Response"));

            if (!obj.Value<bool>("Success"))
            {
                // TODO: Make an alert
                return new TelescopeInfo();
            }

            return new TelescopeInfo()
            {
                Altitude = res.Value<string>("AltitudeString"),
                AtHome = res.Value<bool>("AtHome"),
                Azimuth = res.Value<string>("AzimuthString"),
                Connected = res.Value<bool>("Connected"),
                Dec = res.Value<string>("DeclinationString"),
                Description = res.Value<string>("Description"),
                DeviceId = res.Value<string>("DeviceId"),
                DriverInfo = res.Value<string>("DriverInfo"),
                DriverVersion = res.Value<string>("DriverVersion"),
                IsParked = res.Value<bool>("AtParked"),
                IsTracking = res.Value<bool>("TrackingEnabled"),
                Latitude = res.Value<double>("SiteLatitude"),
                Longitude = res.Value<double>("SiteLongitude"),
                Name = res.Value<string>("Name"),
                RA = res.Value<string>("RightAscensionString"),
                Slewing = res.Value<bool>("Slewing"),
                TimeToMeridianFlip = res.Value<string>("TimeToMeridianFlipString")
            };
        }
    }

    public class TelescopeInfo : DeviceInfo
    {
        // RightAscensionString
        public string RA { get; set; }

        // DeclinationString
        public string Dec { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TimeToMeridianFlip { get; set; }
        public string Altitude { get; set; }
        public string Azimuth { get; set; }
        public bool IsParked { get; set; }
        public bool IsTracking { get; set; }
        public bool AtHome { get; set; }
        public bool Slewing { get; set; }
    }
}