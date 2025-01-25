using BooTao2;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace BooTao2.Systems
{
    // This file contains 2 real ModConfigs (and also a bunch of fake ModConfigs showcasing various ideas). One is set to ConfigScope.ServerSide and the other ConfigScope.ClientSide
    // ModConfigs contain Public Fields and Properties that represent the choices available to the user. 
    // Those Fields or Properties will be presented to users in the Config menu.
    // DONT use static members anywhere in this class (except for an automatically assigned field named Instance with the same Type as the ModConfig class, if you'd rather write "MyConfigClass.Instance" instead of "ModContent.GetInstance<MyConfigClass>()"), tModLoader maintains several instances of ModConfig classes which will not work well with static properties or fields.

    /// <summary>
    /// ExampleConfigServer has Server-wide effects. Things that happen on the server, on the world, or influence autoload go here
    /// ConfigScope.ServerSide ModConfigs are SHARED from the server to all clients connecting in MP.
    /// </summary>

    public class BooTaoServerConfig : ModConfig
    {

        //public new string Name => "Stars Above Config (Server)";
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Pickaxes")]
		//
		[DefaultValue(true)]
		public bool Magnet;
		
		[DefaultValue(true)]
		public bool Homa1;
		
		[DefaultValue(true)]
		public bool Homa2;
		
		[DefaultValue(true)]
		public bool Homa3;
		
		[DefaultValue(true)]
		public bool Homa4;
		
		[DefaultValue(true)]
		public bool Homa5;
		
		[DefaultValue(true)]
		public bool Homa6;

        public override void OnChanged()
        {
            BooTaoPlayer.HomaConfig = [Magnet, Homa1, Homa2, Homa3, Homa4, Homa5, Homa6];
        }
    }
}