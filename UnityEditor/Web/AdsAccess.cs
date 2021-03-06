﻿namespace UnityEditor.Web
{
    using System;
    using UnityEditor;
    using UnityEditor.Advertisements;
    using UnityEditor.Connect;
    using UnityEngine;

    [InitializeOnLoad]
    internal class AdsAccess : CloudServiceAccess
    {
        private const string kServiceDisplayName = "Ads";
        private const string kServiceName = "Unity Ads";
        private const string kServiceUrl = "https://public-cdn.cloud.unity3d.com/editor/production/cloud/ads";

        static AdsAccess()
        {
            UnityConnectServiceData cloudService = new UnityConnectServiceData("Unity Ads", "https://public-cdn.cloud.unity3d.com/editor/production/cloud/ads", new AdsAccess(), "unity/project/cloud/ads");
            UnityConnectServiceCollection.instance.AddService(cloudService);
        }

        public override void EnableService(bool enabled)
        {
            AdvertisementSettings.enabled = enabled;
        }

        public string GetAndroidGameId() => 
            AdvertisementSettings.GetGameId(RuntimePlatform.Android);

        public string GetIOSGameId() => 
            AdvertisementSettings.GetGameId(RuntimePlatform.IPhonePlayer);

        public override string GetServiceDisplayName() => 
            "Ads";

        public override string GetServiceName() => 
            "Unity Ads";

        public bool IsAndroidEnabled() => 
            AdvertisementSettings.IsPlatformEnabled(RuntimePlatform.Android);

        public bool IsInitializedOnStartup() => 
            AdvertisementSettings.initializeOnStartup;

        public bool IsIOSEnabled() => 
            AdvertisementSettings.IsPlatformEnabled(RuntimePlatform.IPhonePlayer);

        public override bool IsServiceEnabled() => 
            AdvertisementSettings.enabled;

        public bool IsTestModeEnabled() => 
            AdvertisementSettings.testMode;

        public override void OnProjectUnbound()
        {
            AdvertisementSettings.enabled = false;
            AdvertisementSettings.initializeOnStartup = false;
            AdvertisementSettings.SetPlatformEnabled(RuntimePlatform.IPhonePlayer, false);
            AdvertisementSettings.SetPlatformEnabled(RuntimePlatform.Android, false);
            AdvertisementSettings.SetGameId(RuntimePlatform.IPhonePlayer, "");
            AdvertisementSettings.SetGameId(RuntimePlatform.Android, "");
            AdvertisementSettings.testMode = false;
        }

        public void SetAndroidEnabled(bool enabled)
        {
            AdvertisementSettings.SetPlatformEnabled(RuntimePlatform.Android, enabled);
        }

        public void SetAndroidGameId(string value)
        {
            AdvertisementSettings.SetGameId(RuntimePlatform.Android, value);
        }

        public void SetInitializedOnStartup(bool enabled)
        {
            AdvertisementSettings.initializeOnStartup = enabled;
        }

        public void SetIOSEnabled(bool enabled)
        {
            AdvertisementSettings.SetPlatformEnabled(RuntimePlatform.IPhonePlayer, enabled);
        }

        public void SetIOSGameId(string value)
        {
            AdvertisementSettings.SetGameId(RuntimePlatform.IPhonePlayer, value);
        }

        public void SetTestModeEnabled(bool enabled)
        {
            AdvertisementSettings.testMode = enabled;
        }
    }
}

