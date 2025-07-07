// Speaking Valley - Stardew Valley Voice Mod (for SMAPI 1.6)
// Created by Francis

using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using GenericModConfigMenu; // ✅ use the real GMCM API
using System;
using System.Collections.Generic;

namespace SpeakingValleyMod
{
    public class ModConfig
    {
        public string PreferredTTSService { get; set; } = "Azure";
        public string AzureApiKey { get; set; } = "";
        public string AzureRegion { get; set; } = "";
        public string ElevenLabsApiKey { get; set; } = "";
        public Dictionary<string, string> NpcVoices { get; set; } = new Dictionary<string, string>();
        public float DefaultPitch { get; set; } = 1.0f;
        public float DefaultRate { get; set; } = 1.0f;
    }

    public class ModEntry : Mod
    {
        private ModConfig Config;
        private Dictionary<string, string> VoiceCache = new Dictionary<string, string>();

        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.Display.MenuChanged += this.OnMenuChanged;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;

            Monitor.Log("Speaking Valley initialized.", LogLevel.Info);
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var gmcm = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (gmcm != null)
            {
                gmcm.Register(
                    this.ModManifest,
                    () => this.Config = new ModConfig(),
                    () => this.Helper.WriteConfig(this.Config)
                );

                gmcm.AddTextOption(
                    this.ModManifest,
                    () => this.Config.PreferredTTSService,
                    value => this.Config.PreferredTTSService = value,
                    () => "Preferred TTS Service",
                    () => "Choose between Azure or ElevenLabs"
                );

                gmcm.AddTextOption(
                    this.ModManifest,
                    () => this.Config.AzureApiKey,
                    value => this.Config.AzureApiKey = value,
                    () => "Azure API Key",
                    () => "Your Azure TTS API key"
                );

                gmcm.AddTextOption(
                    this.ModManifest,
                    () => this.Config.AzureRegion,
                    value => this.Config.AzureRegion = value,
                    () => "Azure Region",
                    () => "Your Azure region (e.g., eastus)"
                );

                gmcm.AddTextOption(
                    this.ModManifest,
                    () => this.Config.ElevenLabsApiKey,
                    value => this.Config.ElevenLabsApiKey = value,
                    () => "ElevenLabs API Key",
                    () => "Your ElevenLabs API key"
                );

                gmcm.AddNumberOption(
                    this.ModManifest,
                    () => this.Config.DefaultPitch,
                    value => this.Config.DefaultPitch = value,
                    () => "Pitch",
                    () => "Adjust voice pitch",
                    0.5f, 2.0f, 0.1f
                );

                gmcm.AddNumberOption(
                    this.ModManifest,
                    () => this.Config.DefaultRate,
                    value => this.Config.DefaultRate = value,
                    () => "Rate",
                    () => "Adjust voice rate",
                    0.5f, 2.0f, 0.1f
                );
            }
        }

        private void OnMenuChanged(object sender, MenuChangedEventArgs e)
        {
            // Placeholder for future UI handling
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            Monitor.Log("Ready to speak to NPCs today!", LogLevel.Debug);
        }

        private void PlayVoice(string npcName, string dialogue)
        {
            string service = this.Config.PreferredTTSService;
            string voice = this.Config.NpcVoices.ContainsKey(npcName)
                ? this.Config.NpcVoices[npcName]
                : "default";

            Monitor.Log(string.Format("Playing voice for {0} using {1} ({2})", npcName, service, voice), LogLevel.Debug);
            // Replace with TTS integration
        }
    }
}
